using System.Collections.Generic;
using System.Numerics;

namespace Systems.Procedural_Generation {
    public interface IProceduralGenerator {
        public List<Vector3> GenerateProceduralPoints();
    }
}