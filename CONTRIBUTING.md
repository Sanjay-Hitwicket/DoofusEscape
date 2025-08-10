# Contributing to Lightweight DI Framework

Thank you for your interest in contributing to the Lightweight DI Framework! This document provides guidelines and information for contributors.

## 🤝 **How to Contribute**

### **Reporting Issues**

Before creating bug reports, please check the existing issues to avoid duplicates. When creating a bug report, please include:

- **Unity Version**: What version of Unity are you using?
- **Framework Version**: Which version of the framework are you using?
- **Description**: A clear and concise description of the bug
- **Steps to Reproduce**: Step-by-step instructions to reproduce the issue
- **Expected Behavior**: What you expected to happen
- **Actual Behavior**: What actually happened
- **Screenshots**: If applicable, add screenshots to help explain the problem
- **Code Example**: A minimal code example that reproduces the issue

### **Feature Requests**

We welcome feature requests! When suggesting a new feature, please include:

- **Description**: A clear description of the feature
- **Use Case**: Why this feature would be useful
- **Implementation Ideas**: Any thoughts on how it could be implemented
- **Examples**: Code examples of how you'd like to use the feature

### **Code Contributions**

#### **Before You Start**

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Read** the existing code to understand the architecture
4. **Follow** the coding standards outlined below

#### **Coding Standards**

- **C# Conventions**: Follow Microsoft C# coding conventions
- **Naming**: Use descriptive names for variables, methods, and classes
- **Comments**: Add XML documentation for public APIs
- **Error Handling**: Include proper error handling and logging
- **Performance**: Consider performance implications of your changes
- **Testing**: Test your changes thoroughly

#### **Code Style**

```csharp
// ✅ Good: Clear naming, proper documentation
/// <summary>
/// Injects dependencies into the specified target object.
/// </summary>
/// <param name="target">The object to inject dependencies into.</param>
public static void InjectDependencies(object target) {
    if (target == null) {
        Debug.LogWarning("Attempted to inject dependencies into null target");
        return;
    }
    
    // Implementation...
}

// ❌ Bad: Unclear naming, no documentation
public static void Inject(object obj) {
    // Implementation...
}
```

#### **Performance Guidelines**

- **Avoid Reflection**: Use delegate-based approaches when possible
- **Minimize Allocations**: Reduce garbage collection pressure
- **Profile Changes**: Test performance impact of your changes
- **Document Performance**: Note any performance implications in your PR

#### **Submitting Changes**

1. **Commit** your changes with clear, descriptive commit messages
2. **Push** to your feature branch
3. **Create** a Pull Request with:
   - Clear description of changes
   - Link to related issues
   - Performance impact assessment
   - Test results

### **Commit Message Format**

Use clear, descriptive commit messages:

```
feat: Add support for circular dependency detection
fix: Resolve memory leak in UltraFastInjectionProcessor
docs: Update README with new installation instructions
test: Add unit tests for InjectionHelper
refactor: Simplify ControllerProvider initialization
```

## 🧪 **Testing**

### **Before Submitting**

- **Unit Tests**: Add unit tests for new functionality
- **Integration Tests**: Test integration with Unity
- **Performance Tests**: Verify no performance regressions
- **Manual Testing**: Test in a real Unity project

### **Test Structure**

```csharp
[TestFixture]
public class InjectionProcessorTests {
    [Test]
    public void InjectDependencies_WithValidTarget_ShouldInjectSuccessfully() {
        // Arrange
        var target = new TestInjectable();
        
        // Act
        InjectionProcessor.InjectDependencies(target);
        
        // Assert
        Assert.IsNotNull(target.InjectedDependency);
    }
}
```

## 📚 **Documentation**

### **Code Documentation**

- **Public APIs**: Add XML documentation for all public methods and classes
- **Examples**: Include usage examples in comments
- **Performance Notes**: Document any performance considerations

### **User Documentation**

- **README**: Update README.md for new features
- **Manual**: Update the complete manual for significant changes
- **Examples**: Add examples for new functionality

## 🔍 **Review Process**

### **Pull Request Review**

1. **Automated Checks**: Ensure all automated checks pass
2. **Code Review**: At least one maintainer must approve
3. **Testing**: Verify tests pass and functionality works
4. **Documentation**: Ensure documentation is updated
5. **Performance**: Verify no performance regressions

### **Review Criteria**

- **Functionality**: Does the code work as intended?
- **Performance**: Are there any performance implications?
- **Maintainability**: Is the code easy to understand and maintain?
- **Documentation**: Is the code properly documented?
- **Testing**: Are there adequate tests?

## 🏷️ **Labels and Milestones**

### **Issue Labels**

- `bug`: Something isn't working
- `enhancement`: New feature or request
- `documentation`: Improvements or additions to documentation
- `good first issue`: Good for newcomers
- `help wanted`: Extra attention is needed
- `performance`: Performance-related issues
- `question`: Further information is requested

### **Pull Request Labels**

- `breaking-change`: Breaking changes
- `bug-fix`: Bug fixes
- `feature`: New features
- `documentation`: Documentation changes
- `performance`: Performance improvements

## 📞 **Getting Help**

### **Questions and Discussions**

- **GitHub Discussions**: Use GitHub Discussions for questions
- **Issues**: Create an issue for bugs or feature requests
- **Documentation**: Check the complete manual first

### **Community Guidelines**

- **Be Respectful**: Treat others with respect and kindness
- **Be Helpful**: Help others when you can
- **Be Patient**: Maintainers are volunteers
- **Be Constructive**: Provide constructive feedback

## 🎯 **Areas for Contribution**

### **High Priority**

- **Performance Optimizations**: Improve injection performance
- **Error Handling**: Better error messages and handling
- **Documentation**: Improve and expand documentation
- **Testing**: Add more comprehensive tests

### **Medium Priority**

- **New Features**: Additional injection approaches
- **Unity Integration**: Better Unity editor integration
- **Examples**: More example projects and use cases
- **Tools**: Development and debugging tools

### **Low Priority**

- **Code Style**: Code style improvements
- **Refactoring**: Code organization improvements
- **Minor Features**: Small convenience features

## 🙏 **Thank You**

Thank you for contributing to the Lightweight DI Framework! Your contributions help make this project better for the entire Unity community.

---

**Happy coding! 🚀** 