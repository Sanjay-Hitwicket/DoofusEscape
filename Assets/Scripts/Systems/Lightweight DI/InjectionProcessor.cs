using System;
using System.Reflection;
using UnityEngine;

namespace Systems.Lightweight_DI {
    public static class InjectionProcessor {
        public static void InjectDependencies(object target) {
            if (target == null) {
                Debug.LogWarning("Attempted to inject dependencies into null target");
                return;
            }

            Type targetType = target.GetType();
            FieldInfo[] fields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields) {
                if (field.IsDefined(typeof(InjectAttribute), true)) {
                    InjectField(target, field);
                }
            }
        }

        private static void InjectField(object target, FieldInfo field) {
            Type fieldType = field.FieldType;
            
            // Check if the field type is a BaseController
            if (typeof(BaseController).IsAssignableFrom(fieldType)) {
                try {
                    // Use reflection to call ControllerProvider.Get<T>() with the correct type
                    MethodInfo getMethod = typeof(ControllerProvider).GetMethod("Get").MakeGenericMethod(fieldType);
                    object injectedValue = getMethod.Invoke(null, null);
                    
                    if (injectedValue != null) {
                        field.SetValue(target, injectedValue);
                        Debug.Log($"Successfully injected {fieldType.Name} into {target.GetType().Name}.{field.Name}");
                    } else {
                        Debug.LogWarning($"Failed to inject {fieldType.Name} into {target.GetType().Name}.{field.Name} - controller not found");
                    }
                } catch (Exception ex) {
                    Debug.LogError($"Error injecting {fieldType.Name} into {target.GetType().Name}.{field.Name}: {ex.Message}");
                }
            } else {
                Debug.LogWarning($"Field {field.Name} in {target.GetType().Name} is marked with [Inject] but is not a BaseController type");
            }
        }
    }
} 