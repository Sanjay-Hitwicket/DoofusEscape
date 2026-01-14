# Lightweight DI System - Simplified Guide

## Overview

The lightweight DI system provides automatic injection completion handling with two main components:

1. **SceneInitializer** - Primary initialization (Zenject-like)
2. **InjectableMonoBehaviour** - Automatic lifecycle management

## Setup

### Primary Setup (Recommended)

1. **Add SceneInitializer to your scene:**
   - Create a GameObject in your scene
   - Add the `SceneInitializer` component
   - Assign your `BootstrapInstaller` in the inspector

2. **Use InjectableMonoBehaviour for your views:**
   ```csharp
   public class PlayerView : InjectableMonoBehaviour {
       [Inject] private PlayerMovementController _playerMovementController;
       
       protected override void OnInjectionComplete() {
           // Safe to use injected dependencies here
           _playerMovementController.SayHi();
       }
   }
   ```

### Fallback Setup (Legacy)

If you prefer the old way, you can still use `GameBootstrapper`:
- Add `GameBootstrapper` to your scene
- Assign your `BootstrapInstaller` in the inspector

## How It Works

### Scene-Based Initialization

`SceneInitializer` uses Unity's `RuntimeInitializeOnLoadMethod` to initialize the DI system before any `Awake()` calls:

```csharp
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
private static void OnAfterSceneLoad() {
    // Initialize DI system before any objects are created
}
```

### Automatic Lifecycle Management

`InjectableMonoBehaviour` automatically handles injection timing:

```csharp
public abstract class InjectableMonoBehaviour : MonoBehaviour, IInjectable {
    // Automatically waits for injection completion
    private async void Start() {
        if (!_injectionComplete) {
            await UniTask.WaitUntil(() => _injectionComplete);
        }
        OnInjectionComplete();
    }
    
    // Override this method instead of Start()
    protected virtual void OnInjectionComplete() {
        // Safe to use injected dependencies here
    }
}
```

## Usage Examples

### Simple View with Injection

```csharp
public class PlayerView : InjectableMonoBehaviour {
    [Inject] private PlayerMovementController _playerMovementController;
    
    protected override void OnInjectionComplete() {
        // Safe to use _playerMovementController here
        _playerMovementController.SayHi();
    }
}
```

### View with Unity Components

```csharp
public class TileView : InjectableMonoBehaviour {
    [SerializeField] private Transform tile;
    [Inject] private TileSpawner _tileSpawner;
    
    protected override void Awake() {
        base.Awake();
        // Setup Unity components here
    }
    
    protected override void OnInjectionComplete() {
        // Safe to use injected dependencies here
        _tileSpawner.SayHi();
    }
}
```

## Benefits

1. **No Manual Waiting**: No need to write `await WaitForInjectionComplete()` everywhere
2. **Automatic Timing**: Injection completion is handled automatically
3. **Clean Code**: Views focus on logic, not injection timing
4. **Robust Initialization**: Scene-based initialization ensures proper order
5. **Performance**: Minimal overhead, efficient async handling

## Migration Guide

### From Manual Waiting

**Before:**
```csharp
public class MyView : InjectableMonoBehaviour {
    [Inject] private MyController _controller;
    
    private async void Start() {
        await WaitForInjectionComplete();
        _controller.DoSomething();
    }
}
```

**After:**
```csharp
public class MyView : InjectableMonoBehaviour {
    [Inject] private MyController _controller;
    
    protected override void OnInjectionComplete() {
        _controller.DoSomething();
    }
}
```

### From Regular MonoBehaviour

**Before:**
```csharp
public class MyView : MonoBehaviour {
    private MyController _controller;
    
    private void Start() {
        _controller = ControllerProvider.Get<MyController>();
        _controller.DoSomething();
    }
}
```

**After:**
```csharp
public class MyView : InjectableMonoBehaviour {
    [Inject] private MyController _controller;
    
    protected override void OnInjectionComplete() {
        _controller.DoSomething();
    }
}
```

## Best Practices

1. **Always use `OnInjectionComplete()`** instead of `Start()` when you need injected dependencies
2. **Keep `Awake()` for Unity-specific setup** (component references, etc.)
3. **Use `OnInjectionComplete()` for business logic** that depends on injected services
4. **Prefer SceneInitializer** over GameBootstrapper for robust initialization

## Troubleshooting

### "Injection not complete" errors
- Ensure you're using `OnInjectionComplete()` instead of `Start()`
- Check that `SceneInitializer` is properly set up in your scene
- Verify `BootstrapInstaller` is assigned in the inspector

### Performance issues
- The automatic waiting is very lightweight
- Consider using `UltraFastInjectionProcessor` for critical performance paths

### Initialization order issues
- Use `SceneInitializer` for guaranteed initialization order
- Check that `BootstrapInstaller` is properly configured
