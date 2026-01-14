using System.Collections.Generic;
using System.Numerics;

namespace Systems.Procedural_Generation {
    public class ProceduralMapGenerator : IProceduralGenerator {
        private List<Vector3> mapPoints = new();
        
        public List<Vector3> GenerateProceduralPoints() {
            mapPoints.Clear();

            Vector3 TODO = Vector3.One;
            
            mapPoints.Add(TODO);
            return mapPoints;
        }
    }
}