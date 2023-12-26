using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public abstract class SubregionStrategy
    {
        public abstract List<List<float>> GenerateSubregionsMap(Vector2 map_size, List<List<float>> regions_map, List<List<float>> ocean_map);
    }
}