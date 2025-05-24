using System;
using System.Collections.Generic;

namespace Doofus.Systems {
    public class ControllerContext<T> where T: BaseController
    {
        public readonly Dictionary<Type, T> controllers = new();
        
        public void Register<TController>(TController controller) where TController : T
        => controllers[typeof(TController)] = controller;
        
        public TController Get<TController>() where TController : T
        => (TController)controllers[typeof(TController)];
    }
}