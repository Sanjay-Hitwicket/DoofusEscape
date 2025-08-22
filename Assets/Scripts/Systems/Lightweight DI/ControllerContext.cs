using System;
using System.Collections.Generic;

namespace Systems.Lightweight_DI {
    public class ControllerContext<T> where T: BaseController {
        public readonly Dictionary<Type, T> controllers = new();

        public void Register<TController>(TController controller) where TController : T {
            controllers[typeof(TController)] = controller;
            controller.Initialize();
        }

        public TController Get<TController>() where TController : T
        => (TController)controllers[typeof(TController)];
    }
}