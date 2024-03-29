using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class ElevationEnums
    {
        public enum HexElevation
        {
            Mountain = 60,
            Large_Hill = 20,
            Small_Hill = 10,
            Flatland = 0,
            Valley = -Small_Hill,
            Canyon = -Large_Hill,
        }

        public static List<HexElevation> GetElevationTypes()
        {
            return Enum.GetValues(typeof(HexElevation)).Cast<HexElevation>().ToList();
        }

        public static HexElevation GetElevationType(float elevationValue)
        {
            return elevationDict.TryGetValue(elevationValue, out var elevation) ? elevation : default;
        }

        private static readonly Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>
        {
            { (int) HexElevation.Canyon, HexElevation.Canyon },
            { (int) HexElevation.Valley, HexElevation.Valley },
            { (int) HexElevation.Flatland, HexElevation.Flatland },
            { (int) HexElevation.Small_Hill, HexElevation.Small_Hill },
            { (int) HexElevation.Large_Hill, HexElevation.Large_Hill },
            { (int) HexElevation.Mountain, HexElevation.Mountain },
        };

        
        
    }
}