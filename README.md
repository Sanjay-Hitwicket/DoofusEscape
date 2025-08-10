# Lightweight DI Framework for Unity

[![Unity Version](https://img.shields.io/badge/Unity-2021.3+-blue.svg)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Unity-blue.svg)](https://unity.com/)

A high-performance, lightweight dependency injection framework designed specifically for Unity games. Provides multiple injection approaches to balance convenience with performance.

## 🎯 **Why Lightweight DI Framework?**

- **🚀 High Performance**: Multiple injection approaches optimized for Unity
- **🎮 Unity-First**: Designed specifically for Unity's component system
- **⚡ Zero Reflection**: Ultra-fast injection with minimal runtime overhead
- **🔧 Flexible**: Choose the injection approach that fits your needs
- **📚 Well-Documented**: Comprehensive manual and examples included

## 📊 **Performance Comparison**

| Approach | Injection Time | Memory | GC Pressure | Use Case |
|----------|----------------|---------|-------------|----------|
| **Ultra-Fast** | 0.01-0.05ms | 0-10 bytes | Minimal | ✅ Most projects |
| **Properties** | 0.001-0.01ms | 0 bytes | None | ✅ High-performance |
| **Manual** | 0.001ms | 0 bytes | None | ✅ Simple cases |

## 🚀 **Quick Start**

### Installation

1. **Clone or Download** this repository
2. **Copy** the `Assets/Scripts/Systems/Lightweight DI/` folder to your Unity project
3. **Import** the scripts into your project

### Basic Usage

```csharp
// 1. Create a controller
public class TileSpawner : BaseController {
    public override void Initialize() {
        // Setup logic
    }
    
    public void SpawnTile() {
        Debug.Log("Tile spawned!");
    }
}

// 2. Register it in your bootstrapper
public class MyFeatureBootstrapper : FeatureBootstrapper {
    public override void Register(ControllerContext<BaseController> context) {
        context.Register(new TileSpawner());
    }
}

// 3. Use automatic injection
public class MyComponent : InjectableMonoBehaviour {
    [Inject] private TileSpawner _tileSpawner;
    
    private void Start() {
        // _tileSpawner is automatically injected and ready to use!
        _tileSpawner.SpawnTile();
    }
}
```

## 🔄 **Injection Approaches**

### 1. Ultra-Fast Injection (Recommended)
```csharp
public class MyComponent : InjectableMonoBehaviour {
    [Inject] private TileSpawner _tileSpawner;
    // Automatic injection with minimal performance impact
}
```

### 2. Public Properties (Maximum Performance)
```csharp
public class MyComponent : InjectableMonoBehaviourWithProperties {
    public TileSpawner TileSpawner { get; set; }
    
    protected override void OnInjectDependencies() {
        TileSpawner = ControllerProvider.Get<TileSpawner>();
    }
}
```

### 3. Manual Injection (Original Way)
```csharp
public class MyComponent : MonoBehaviour {
    private TileSpawner _tileSpawner;
    
    private void Start() {
        _tileSpawner = ControllerProvider.Get<TileSpawner>();
    }
}
```

## 📁 **Project Structure**

```
Assets/Scripts/Systems/Lightweight DI/
├── Core/
│   ├── BaseController.cs              # Base class for all controllers
│   ├── ControllerContext.cs           # Container for registered controllers
│   ├── ControllerProvider.cs          # Static access to controllers
│   └── BootstrapInstaller.cs          # Handles registration of features
├── Injection/
│   ├── InjectAttribute.cs             # [Inject] attribute marker
│   ├── IInjectable.cs                 # Interface for injectable objects
│   ├── InjectableMonoBehaviour.cs     # Base class for Unity components
│   ├── InjectableObject.cs            # Base class for regular classes
│   └── InjectableMonoBehaviourWithProperties.cs # High-performance alternative
├── Processors/
│   ├── UltraFastInjectionProcessor.cs # Delegate-based (fast)
│   └── FastInjectionProcessor.cs      # Alternative fast approach
├── Utilities/
│   ├── InjectionHelper.cs             # Manual injection utilities
│   └── InjectionHandlerRegistry.cs    # Registry for injection handlers
└── Examples/
    ├── InjectionExamples.cs           # Usage examples
    └── TileViewHighPerformance.cs     # High-performance example
```

## 🎮 **Unity Integration**

### Setup in Your Scene

1. **Create a GameBootstrapper GameObject** in your scene
2. **Add the GameBootstrapper component** to it
3. **Create and assign a BootstrapInstaller** with your FeatureBootstrappers
4. **The system automatically initializes** when the scene starts

```csharp
// GameBootstrapper automatically handles initialization
public class GameBootstrapper : MonoBehaviour {
    [SerializeField] private BootstrapInstaller _bootstrapInstaller;
    
    private void Awake() {
        Context = new ControllerContext<BaseController>();
        ControllerProvider.Initialize(Context);
        UltraFastInjectionProcessor.Initialize();
        _bootstrapInstaller.Install(Context);
    }
}
```

## 📚 **Documentation**

- **[Complete Manual](Assets/Scripts/Systems/Lightweight%20DI/LightWeightDI_Complete_Manual.md)** - Comprehensive guide
- **[Performance Comparison](Assets/Scripts/Systems/Lightweight%20DI/PerformanceComparison.md)** - Detailed performance analysis
- **[Examples](Assets/Scripts/Systems/Lightweight%20DI/InjectionExamples.cs)** - Code examples for all approaches

## 🔧 **Features**

- ✅ **Multiple Injection Methods**: Choose the approach that fits your performance needs
- ✅ **Unity Integration**: Seamless integration with Unity's component system
- ✅ **Type Safety**: Compile-time checking for injected dependencies
- ✅ **Performance Optimized**: Minimal runtime overhead
- ✅ **Easy to Use**: Simple `[Inject]` attribute syntax
- ✅ **Error Handling**: Comprehensive error handling and logging
- ✅ **Scalable**: Works efficiently with hundreds of injected objects

## 🆚 **Comparison with Other DI Frameworks**

| Feature | Lightweight DI | Zenject | VContainer | Manual |
|---------|----------------|---------|------------|---------|
| **Performance** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Ease of Use** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐ |
| **Unity Integration** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Learning Curve** | ⭐⭐⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Memory Usage** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |

## 🤝 **Contributing**

We welcome contributions! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### How to Contribute

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

## 📄 **License**

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 **Acknowledgments**

- Inspired by the need for high-performance DI in Unity games
- Built with performance and ease of use in mind
- Designed to solve real problems faced by Unity developers

## 📞 **Support**

- **Issues**: [GitHub Issues](https://github.com/yourusername/lightweight-di-framework/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/lightweight-di-framework/discussions)
- **Documentation**: [Complete Manual](Assets/Scripts/Systems/Lightweight%20DI/LightWeightDI_Complete_Manual.md)

---

**Made with ❤️ for the Unity community** 