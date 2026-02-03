using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DOTweenTimelineEditorWindow : EditorWindow {
    private DoTweenAnimator targetAnimator;
    private float currentTime = 0f;
    private bool isPlaying = false;
    private double lastEditorTime;
    private Vector2 scrollPos;
    
    // UI settings
    private const float TIMELINE_HEIGHT = 60f;
    private const float TRACK_HEIGHT = 40f;
    private const float HEADER_WIDTH = 150f;
    private const float TIME_RULER_HEIGHT = 30f;
    
    private AnimationBlock selectedBlock;
    private AnimationBlock draggingBlock;
    private AnimationBlock resizingBlock;
    private bool isResizingLeft;
    private bool isResizingRight;
    private bool isDraggingPlayhead;
    private float dragOffset;
    private float resizeStartTime;
    private float resizeStartDuration;
    
    public static void OpenWindow(DoTweenAnimator animator) {
        DOTweenTimelineEditorWindow window = GetWindow<DOTweenTimelineEditorWindow>("DOTween Timeline");
        window.targetAnimator = animator;
        window.Show();
    }

    private void OnEnable() {
        lastEditorTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable() {
        EditorApplication.update -= OnEditorUpdate;
        StopPlayback();
    }

    private void OnEditorUpdate() {
        if (isPlaying && targetAnimator != null) {
            float deltaTime = (float)(EditorApplication.timeSinceStartup - lastEditorTime);
            currentTime += deltaTime;
            
            if (currentTime >= targetAnimator.timelineLength) {
                currentTime = targetAnimator.timelineLength;
                StopPlayback();
            }
            
            Repaint();
        }
        lastEditorTime = EditorApplication.timeSinceStartup;
    }

    private void OnGUI() {
        if (targetAnimator == null) {
            EditorGUILayout.HelpBox("No DoTweenAnimator selected. Please select a GameObject with DoTweenAnimator component.", MessageType.Warning);
            return;
        }
        
        DrawToolbar();
        DrawTimeRuler();
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        DrawTimeline();
        EditorGUILayout.EndScrollView();
        
        DrawInspector();
        
        HandleInput();
    }

    private void DrawToolbar() {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        
        // Display current animator
        EditorGUILayout.LabelField("Animator:", GUILayout.Width(60));
        EditorGUILayout.ObjectField(targetAnimator, typeof(DoTweenAnimator), true, GUILayout.Width(200));
        
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("▶", EditorStyles.toolbarButton, GUILayout.Width(30))) {
            if (!isPlaying) PlayTimeline();
        }
        
        if (GUILayout.Button("⏸", EditorStyles.toolbarButton, GUILayout.Width(30))) {
            PausePlayback();
        }
        
        if (GUILayout.Button("⏹", EditorStyles.toolbarButton, GUILayout.Width(30))) {
            StopPlayback();
        }
        
        GUILayout.Space(20);
        
        if (GUILayout.Button("Add Block", EditorStyles.toolbarButton, GUILayout.Width(80))) {
            AddNewBlock();
        }
        
        if (GUILayout.Button("Clear All", EditorStyles.toolbarButton, GUILayout.Width(80))) {
            if (EditorUtility.DisplayDialog("Clear All", "Remove all animation blocks?", "Yes", "No")) {
                targetAnimator.animationBlocks.Clear();
                selectedBlock = null;
                EditorUtility.SetDirty(targetAnimator);
            }
        }
        
        GUILayout.Space(10);
        
        GUILayout.Label("Length:", GUILayout.Width(50));
        targetAnimator.timelineLength = EditorGUILayout.FloatField(targetAnimator.timelineLength, GUILayout.Width(60));
        targetAnimator.timelineLength = Mathf.Max(1f, targetAnimator.timelineLength);
        
        EditorGUILayout.EndHorizontal();
    }

    private void DrawTimeRuler() {
        Rect rulerRect = GUILayoutUtility.GetRect(position.width, TIME_RULER_HEIGHT);
        
        // Background
        EditorGUI.DrawRect(new Rect(rulerRect.x, rulerRect.y, HEADER_WIDTH, rulerRect.height), new Color(0.2f, 0.2f, 0.2f));
        EditorGUI.DrawRect(new Rect(rulerRect.x + HEADER_WIDTH, rulerRect.y, rulerRect.width - HEADER_WIDTH, rulerRect.height), new Color(0.25f, 0.25f, 0.25f));
        
        float timelineWidth = rulerRect.width - HEADER_WIDTH;
        
        // Draw time markers
        int markerCount = Mathf.FloorToInt(targetAnimator.timelineLength) + 1;
        for (int i = 0; i < markerCount; i++) {
            float x = HEADER_WIDTH + (i / targetAnimator.timelineLength) * timelineWidth;
            Handles.color = Color.gray;
            Handles.DrawLine(new Vector3(x, rulerRect.y + 20), new Vector3(x, rulerRect.y + rulerRect.height));
            
            GUI.Label(new Rect(x - 15, rulerRect.y, 30, 20), i.ToString() + "s", EditorStyles.miniLabel);
        }
        
        // Draw playhead
        float playheadX = HEADER_WIDTH + (currentTime / targetAnimator.timelineLength) * timelineWidth;
        Handles.color = Color.red;
        Handles.DrawLine(new Vector3(playheadX, rulerRect.y, 0), new Vector3(playheadX, position.height, 0));
        
        // Playhead handle
        Rect playheadHandle = new Rect(playheadX - 5, rulerRect.y, 10, rulerRect.height);
        EditorGUI.DrawRect(playheadHandle, Color.red);
        EditorGUIUtility.AddCursorRect(playheadHandle, MouseCursor.SlideArrow);
        
        // Handle playhead dragging
        Event e = Event.current;
        if (e.type == EventType.MouseDown && playheadHandle.Contains(e.mousePosition)) {
            isDraggingPlayhead = true;
            e.Use();
        }
        
        if (isDraggingPlayhead) {
            if (e.type == EventType.MouseDrag) {
                float normalizedPos = Mathf.Clamp01((e.mousePosition.x - HEADER_WIDTH) / timelineWidth);
                currentTime = normalizedPos * targetAnimator.timelineLength;
                Repaint();
                e.Use();
            }
            else if (e.type == EventType.MouseUp) {
                isDraggingPlayhead = false;
                e.Use();
            }
        }
    }

    private void DrawTimeline() {
        float timelineWidth = position.width - HEADER_WIDTH - 20;
        
        if (targetAnimator.animationBlocks.Count == 0) {
            EditorGUILayout.HelpBox("No animation blocks. Click 'Add Block' to create one.", MessageType.Info);
            return;
        }
        
        foreach (var block in targetAnimator.animationBlocks) {
            EditorGUILayout.BeginHorizontal();
            
            // Track header
            Rect headerRect = GUILayoutUtility.GetRect(HEADER_WIDTH, TRACK_HEIGHT);
            EditorGUI.DrawRect(headerRect, new Color(0.2f, 0.2f, 0.2f));
            GUI.Label(headerRect, block.name, EditorStyles.boldLabel);
            
            // Timeline area
            Rect trackRect = GUILayoutUtility.GetRect(timelineWidth, TRACK_HEIGHT);
            EditorGUI.DrawRect(trackRect, new Color(0.15f, 0.15f, 0.15f));
            
            // Draw grid lines
            int gridCount = Mathf.FloorToInt(targetAnimator.timelineLength) + 1;
            for (int i = 0; i < gridCount; i++) {
                float x = trackRect.x + (i / targetAnimator.timelineLength) * trackRect.width;
                Handles.color = new Color(0.3f, 0.3f, 0.3f);
                Handles.DrawLine(new Vector3(x, trackRect.y), new Vector3(x, trackRect.y + trackRect.height));
            }
            
            // Draw animation block
            float blockX = trackRect.x + (block.startTime / targetAnimator.timelineLength) * trackRect.width;
            float blockWidth = (block.duration / targetAnimator.timelineLength) * trackRect.width;
            Rect blockRect = new Rect(blockX, trackRect.y + 5, blockWidth, trackRect.height - 10);
            
            Color blockColor = block == selectedBlock ? Color.grey : block.color;
            EditorGUI.DrawRect(blockRect, blockColor);
            
            // Draw resize handles
            float handleWidth = 8f;
            Rect leftHandle = new Rect(blockRect.x, blockRect.y, handleWidth, blockRect.height);
            Rect rightHandle = new Rect(blockRect.x + blockRect.width - handleWidth, blockRect.y, handleWidth, blockRect.height);
            
            // Highlight handles on hover
            if (leftHandle.Contains(Event.current.mousePosition)) {
                EditorGUI.DrawRect(leftHandle, new Color(1f, 1f, 1f, 0.5f));
                EditorGUIUtility.AddCursorRect(leftHandle, MouseCursor.ResizeHorizontal);
            }
            
            if (rightHandle.Contains(Event.current.mousePosition)) {
                EditorGUI.DrawRect(rightHandle, new Color(1f, 1f, 1f, 0.5f));
                EditorGUIUtility.AddCursorRect(rightHandle, MouseCursor.ResizeHorizontal);
            }
            
            // Draw block label
            GUIStyle labelStyle = new GUIStyle(EditorStyles.miniLabel);
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Label(blockRect, $"{block.type}\n{block.duration:F2}s", labelStyle);
            
            // Handle block interaction
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0) {
                if (leftHandle.Contains(e.mousePosition)){
                    // Start resizing from left
                    resizingBlock = block;
                    isResizingLeft = true;
                    isResizingRight = false;
                    selectedBlock = block;
                    resizeStartTime = block.startTime;
                    resizeStartDuration = block.duration;
                    e.Use();
                }
                else if (rightHandle.Contains(e.mousePosition)) {
                    // Start resizing from right
                    resizingBlock = block;
                    isResizingRight = true;
                    isResizingLeft = false;
                    selectedBlock = block;
                    resizeStartTime = block.startTime;
                    resizeStartDuration = block.duration;
                    e.Use();
                }
                else if (blockRect.Contains(e.mousePosition)) {
                    // Start dragging the whole block
                    selectedBlock = block;
                    draggingBlock = block;
                    dragOffset = e.mousePosition.x - blockX;
                    e.Use();
                    Repaint();
                }
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        // Handle dragging and resizing
        Event evt = Event.current;
        
        // Handle resizing
        if (resizingBlock != null) {
            if (evt.type == EventType.MouseDrag) {
                float mouseX = evt.mousePosition.x - HEADER_WIDTH;
                float mouseTime = (mouseX / timelineWidth) * targetAnimator.timelineLength;
                
                if (isResizingLeft) {
                    // Resize from left: adjust start time and duration
                    float endTime = resizeStartTime + resizeStartDuration;
                    float newStartTime = Mathf.Clamp(mouseTime, 0, endTime - 0.1f);
                    resizingBlock.startTime = newStartTime;
                    resizingBlock.duration = endTime - newStartTime;
                }
                else if (isResizingRight) {
                    // Resize from right: adjust duration only
                    float newDuration = Mathf.Max(0.1f, mouseTime - resizingBlock.startTime);
                    newDuration = Mathf.Min(newDuration, targetAnimator.timelineLength - resizingBlock.startTime);
                    resizingBlock.duration = newDuration;
                }
                
                EditorUtility.SetDirty(targetAnimator);
                Repaint();
                evt.Use();
            }
            else if (evt.type == EventType.MouseUp) {
                resizingBlock = null;
                isResizingLeft = false;
                isResizingRight = false;
                evt.Use();
            }
        }
        // Handle dragging (only if not resizing)
        else if (draggingBlock != null) {
            if (evt.type == EventType.MouseDrag) {
                float mouseX = evt.mousePosition.x - HEADER_WIDTH - dragOffset;
                float newStartTime = (mouseX / timelineWidth) * targetAnimator.timelineLength;
                draggingBlock.startTime = Mathf.Clamp(newStartTime, 0, targetAnimator.timelineLength - draggingBlock.duration);
                EditorUtility.SetDirty(targetAnimator);
                Repaint();
                evt.Use();
            }
            else if (evt.type == EventType.MouseUp) {
                draggingBlock = null;
                evt.Use();
            }
        }
    }

    private void DrawInspector() {
        if (selectedBlock == null) return;
        
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Block Inspector", EditorStyles.boldLabel);
        
        EditorGUI.BeginChangeCheck();
        
        selectedBlock.name = EditorGUILayout.TextField("Name", selectedBlock.name);
        selectedBlock.startTime = EditorGUILayout.FloatField("Start Time", selectedBlock.startTime);
        selectedBlock.duration = EditorGUILayout.FloatField("Duration", selectedBlock.duration);
        selectedBlock.target = (GameObject)EditorGUILayout.ObjectField("Target", selectedBlock.target, typeof(GameObject), true);
        selectedBlock.type = (AnimationType)EditorGUILayout.EnumPopup("Animation Type", selectedBlock.type);
        selectedBlock.targetValue = EditorGUILayout.Vector3Field("Target Value", selectedBlock.targetValue);
        selectedBlock.easeType = (Ease)EditorGUILayout.EnumPopup("Ease", selectedBlock.easeType);
        selectedBlock.color = EditorGUILayout.ColorField("Block Color", selectedBlock.color);
        
        if (EditorGUI.EndChangeCheck()) {
            EditorUtility.SetDirty(targetAnimator);
            Repaint();
        }
        
        if (GUILayout.Button("Delete Block")) {
            targetAnimator.animationBlocks.Remove(selectedBlock);
            selectedBlock = null;
            EditorUtility.SetDirty(targetAnimator);
        }
    }

    private void HandleInput() {
        Event e = Event.current;
        
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Delete && selectedBlock != null) {
            targetAnimator.animationBlocks.Remove(selectedBlock);
            selectedBlock = null;
            EditorUtility.SetDirty(targetAnimator);
            e.Use();
            Repaint();
        }
    }

    private void AddNewBlock() {
        var block = new AnimationBlock {
            name = "Animation " + (targetAnimator.animationBlocks.Count + 1),
            startTime = currentTime,
            duration = 1f,
            color = Random.ColorHSV(0, 1, 0.5f, 0.8f, 0.8f, 1f)
        };
        targetAnimator.animationBlocks.Add(block);
        selectedBlock = block;
        EditorUtility.SetDirty(targetAnimator);
    }

    private void PlayTimeline() {
        isPlaying = true;
        targetAnimator.PlayTimeline();
    }

    private void PausePlayback() {
        isPlaying = false;
    }

    private void StopPlayback() {
        isPlaying = false;
        currentTime = 0f;
        if (targetAnimator != null) {
            targetAnimator.StopTimeline();
        }
    }
}