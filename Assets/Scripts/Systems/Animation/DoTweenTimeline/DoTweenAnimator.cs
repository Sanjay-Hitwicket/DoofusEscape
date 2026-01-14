using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

// Data structure for animation blocks
[System.Serializable]
public class AnimationBlock {
    public string id = System.Guid.NewGuid().ToString();
    public string name = "New Animation";
    public float startTime = 0f;
    public float duration = 1f;
    public GameObject target;
    public AnimationType type = AnimationType.Move;
    public Vector3 targetValue;
    public Ease easeType = Ease.Linear;
    public Color color = new Color(0.3f, 0.6f, 1f);
}

public enum AnimationType {
    Move,
    Rotate,
    Scale,
    Fade
}

// MonoBehaviour component to attach to GameObjects
public class DoTweenAnimator : MonoBehaviour {
    public List<AnimationBlock> animationBlocks = new List<AnimationBlock>();
    public float timelineLength = 10f;
    
    public void PlayTimeline() {
        // Kill any existing tweens on this object
        DOTween.Kill(this);
        
        // Sort blocks by start time
        var sortedBlocks = animationBlocks.OrderBy(b => b.startTime).ToList();
        
        foreach (var block in sortedBlocks) {
            if (block.target == null) continue;
            
            Tween tween = null;
            
            switch (block.type) {
                case AnimationType.Move:
                    tween = block.target.transform.DOMove(block.targetValue, block.duration);
                    break;
                case AnimationType.Rotate:
                    tween = block.target.transform.DORotate(block.targetValue, block.duration);
                    break;
                case AnimationType.Scale:
                    tween = block.target.transform.DOScale(block.targetValue, block.duration);
                    break;
                case AnimationType.Fade:
                    var renderer = block.target.GetComponent<Renderer>();
                    if (renderer != null) {
                        tween = renderer.material.DOFade(block.targetValue.x, block.duration);
                    }
                    break;
            }
            
            if (tween != null) {
                tween.SetEase(block.easeType);
                tween.SetDelay(block.startTime);
                tween.SetId(this); // Set ID for proper cleanup
            }
        }
    }
    
    public void StopTimeline() {
        DOTween.Kill(this);
    }
}

// Custom Inspector for DoTweenAnimator
[CustomEditor(typeof(DoTweenAnimator))]
public class DoTweenAnimatorEditor : Editor {
    public override void OnInspectorGUI() {
        DoTweenAnimator animator = (DoTweenAnimator)target;
        
        EditorGUILayout.Space();
        
        // Open Timeline button
        if (GUILayout.Button("Open Timeline Editor", GUILayout.Height(30))) {
            DOTweenTimelineEditorWindow.OpenWindow(animator);
        }
        
        EditorGUILayout.Space();
        
        // Timeline length
        animator.timelineLength = EditorGUILayout.FloatField("Timeline Length", animator.timelineLength);
        animator.timelineLength = Mathf.Max(1f, animator.timelineLength);
        
        EditorGUILayout.Space();
        
        // Playback buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Play Timeline")) {
            animator.PlayTimeline();
        }
        if (GUILayout.Button("Stop Timeline")) {
            animator.StopTimeline();
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Blocks", EditorStyles.boldLabel);
        
        // Display all animation blocks
        if (animator.animationBlocks.Count == 0) {
            EditorGUILayout.HelpBox("No animation blocks. Open Timeline Editor to add blocks.", MessageType.Info);
        }
        
        for (int i = 0; i < animator.animationBlocks.Count; i++) {
            var block = animator.animationBlocks[i];
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.BeginHorizontal();
            block.name = EditorGUILayout.TextField(block.name);
            
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(25))) {
                animator.animationBlocks.RemoveAt(i);
                EditorUtility.SetDirty(animator);
                return;
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
            
            EditorGUI.indentLevel++;
            block.target = (GameObject)EditorGUILayout.ObjectField("Target", block.target, typeof(GameObject), true);
            block.type = (AnimationType)EditorGUILayout.EnumPopup("Type", block.type);
            block.startTime = EditorGUILayout.FloatField("Start Time", block.startTime);
            block.duration = EditorGUILayout.FloatField("Duration", block.duration);
            block.targetValue = EditorGUILayout.Vector3Field("Target Value", block.targetValue);
            block.easeType = (Ease)EditorGUILayout.EnumPopup("Ease", block.easeType);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }
        
        if (GUI.changed) {
            EditorUtility.SetDirty(animator);
        }
    }
}