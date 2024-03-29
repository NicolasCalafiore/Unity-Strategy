using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Water
{
    public class PerlinWaterStrategy : WaterStrategy
    {
        /*
            PerlinWaterStrategy is used to generate water on the map using slices of perlin noise
        */
        private float river_scale = 4f; // Higher --> More Linear & Skinnier
        private float ocean_scale = 5f; // Higher --> More Linear & Skinnier
        private Vector2 river_max_min = new Vector2( .5f, .46f);    // Greater Range --> Thicker/Wider river
        private Vector2 ocean_max_min = new Vector2( .7f, .45f);    // Greater Range --> Thicker/Wider ocean

        public override List<List<float>> GenerateWaterMap(Vector2 map_size, RegionsEnums.HexRegion region_type)   //Called from MapGeneration.GenerateWater
        {
            List<List<float>> map = MapUtils.GenerateMap();
        
            if(region_type == RegionsEnums.HexRegion.River){
                MapUtils.GeneratePerlinNoiseMap(map, map_size, river_scale);
                FilterPerlinMap(map, map_size, river_max_min, region_type); //Filter map to only include values within max_min range
            }
            else if(region_type == RegionsEnums.HexRegion.Ocean){
                MapUtils.GeneratePerlinNoiseMap(map, map_size, ocean_scale);
                FilterPerlinMap(map, map_size, ocean_max_min, region_type); //Filter map to only include values within max_min range
            }

            SetLand(map, region_type);
            return map;
        }
        public static void SetLand(List<List<float>> map, RegionsEnums.HexRegion region_type)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if(map[i][j] != (int) region_type){
                        map[i][j] = (int) LandEnums.LandType.Land;}
                    else{
                        map[i][j] = (int) LandEnums.LandType.Water;
                    }
                }
            }
        }
        
        public static void FilterPerlinMap(List<List<float>> map, Vector2 map_size, Vector2 max_min, RegionsEnums.HexRegion region_type){

            for (int i = 0; i < map_size.x; i++)
            {
                for (int j = 0; j < map_size.y; j++)
                {
                    if(map[i][j] <= max_min.x && map[i][j] >= max_min.y){
                        map[i][j] = (int) region_type;         //Ocean 0, River 1
                    }
                }
            }   
        }

    }
}
