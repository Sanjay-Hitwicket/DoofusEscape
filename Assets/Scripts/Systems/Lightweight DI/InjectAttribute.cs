using System;

namespace Systems.Lightweight_DI {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute {
        // Optional: You can add parameters here if needed in the future
        // For example: public InjectAttribute(string id = null) { }
    }
} 