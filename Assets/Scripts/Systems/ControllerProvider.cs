using System;
using UnityEngine;

namespace Doofus.Systems {
    public static class ControllerProvider {
        private static ControllerContext<BaseController> _context;
        private static bool _initialized = false;

        public static void Initialize(ControllerContext<BaseController> context) {
            if (_initialized) {
                Debug.Log("ControllerProvider already initialized");
                return;
            }

            _context = context;
            _initialized = true;
            Debug.Log("ControllerProvider initialized");
        }

        public static T Get<T>() where T : BaseController {
            if (!_initialized) {
                Debug.Log("ControllerProvider not initialized");
                return default(T);
            }

            return _context.Get<T>();
        }
    }
}