# Lightweight DI Framework with [Inject] Attribute

This framework provides a simple dependency injection system with automatic injection using the `[Inject]` attribute, similar to Zenject.

## Features

- **Automatic Injection**: Use `[Inject]` attribute to automatically inject dependencies
- **Multiple Injection Methods**: Support for MonoBehaviour and regular classes
- **Type Safety**: Compile-time type checking for injected dependencies
- **Performance Optimized**: Uses reflection only during injection, not during runtime
- **Unity Integration**: Seamless integration with Unity's component system

## Quick Start

### 1. Basic Usage with MonoBehaviour

```csharp
using Systems.Lightweight_DI;
using DoofusEscape;

public class MyComponent : InjectableMonoBehaviour {
    [Inject] private TileSpawner _tileSpawner;
    
    private void Start() {
        // _tileSpawner is automatically injected and ready to use
        Debug.Log($"TileSpawner: {_tileSpawner != null}");
    }
}
```

### 2. Basic Usage with Regular Class

```csharp
using Systems.Lightweight_DI;
using DoofusEscape;

public class MyService : InjectableObject {
    [Inject] private TileSpawner _tileSpawner;
    
    public void DoWork() {
        // _tileSpawner is automatically injected and ready to use
        // Your logic here
    }
}
```

### 3. Manual Injection

```csharp
using Systems.Lightweight_DI;
using DoofusEscape;

public class MyComponent : MonoBehaviour {
    [Inject] private TileSpawner _tileSpawner;
    
    private void Start() {
        // Manually trigger injection
        InjectionHelper.InjectInto(this);
        
        // Now _tileSpawner is injected and ready to use
    }
}
```

## Available Classes

### InjectableMonoBehaviour
Base class for Unity components that need automatic dependency injection. Injection happens automatically in `Awake()`.

### InjectableObject
Base class for regular C# classes that need automatic dependency injection. Injection happens automatically in the constructor.

### InjectAttribute
Attribute to mark fields for automatic injection. Only works with types that inherit from `BaseController`.

### InjectionHelper
Utility class providing helper methods for manual injection and system status checking.

## Registration

Dependencies must be registered in your `FeatureBootstrapper`:

```csharp
public class MyFeatureBootstrapper : FeatureBootstrapper {
    public override void Register(ControllerContext<BaseController> context) {
        context.Register(new TileSpawner());
        context.Register(new PlayerController());
        // Register other controllers...
    }
}
```

## Best Practices

1. **Use InjectableMonoBehaviour**: For Unity components, inherit from `InjectableMonoBehaviour` for automatic injection
2. **Use InjectableObject**: For regular classes, inherit from `InjectableObject` for automatic injection
3. **Manual Injection**: Use `InjectionHelper.InjectInto()` when you need more control over when injection happens
4. **Check Initialization**: Use `InjectionHelper.IsInitialized()` to check if the DI system is ready
5. **Naming Convention**: Use underscore prefix for injected fields (e.g., `_tileSpawner`)

## Error Handling

The framework provides detailed logging for injection issues:
- Missing dependencies
- Type mismatches
- System not initialized
- Injection failures

## Performance Considerations

- Reflection is only used during injection, not during runtime
- Injection happens once per object (in Awake or constructor)
- No performance impact during normal gameplay

## Migration from Manual Injection

Before:
```csharp
public class OldComponent : MonoBehaviour {
    private TileSpawner tileSpawner;
    
    private void Start() {
        tileSpawner = ControllerProvider.Get<TileSpawner>();
    }
}
```

After:
```csharp
public class NewComponent : InjectableMonoBehaviour {
    [Inject] private TileSpawner _tileSpawner;
    
    private void Start() {
        // _tileSpawner is already injected and ready to use
    }
}
```

## Limitations

- Only supports injection of types that inherit from `BaseController`
- Injection happens at object creation time (Awake/constructor)
- No support for circular dependencies
- No support for conditional injection based on field values 