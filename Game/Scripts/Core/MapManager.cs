using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using Cities;




namespace Terrain {

    public static class  MapManager
    {
        
        public static Vector2 map_size = new Vector2(25, 25);
        private static ElevationStrat elevation_strategy = ElevationStrat.Groupings;                  
        private static LandStrat land_strategy = LandStrat.Perlin;
        private static RegionStrat region_strategy = RegionStrat.MapFactor;
        private static FeaturesStrat features_strategy = FeaturesStrat.RegionSpecific;
        private static ResourceStrat resource_strategy = ResourceStrat.RegionRandom;
        private static CapitalStrat capital_strategy =  CapitalStrat.Random;  //TO DO: Convert to enum

        // Handlers are used to handle different algorithms for each map generation
        public static TerrainMapHandler terrain_map_handler;
        public static CityMapHandler city_map_handler;
        public static TerritoryMapHandler territory_map_handler;
        public static void GenerateTerrainMaps(){
            terrain_map_handler = new TerrainMapHandler();
            terrain_map_handler.GenerateTerrainMap(elevation_strategy, land_strategy, region_strategy, features_strategy, resource_strategy, map_size);
        }

        public static void GenerateCitiesMaps(){
            List<Player> player_list = PlayerManager.player_list;
            city_map_handler = new CityMapHandler();
            city_map_handler.GenerateCitiesMap(terrain_map_handler, player_list, map_size, capital_strategy);
        }

        public static void GenerateTerritoryMaps(){
            List<Player> player_list = PlayerManager.player_list;
            territory_map_handler = new TerritoryMapHandler();
            territory_map_handler.GenerateCapitalTerritory(city_map_handler, player_list, map_size);
        }

        public static void GenerateShores(){
            terrain_map_handler.GenerateShores();
        }

        public static Vector2 GetMapSize(){
            return map_size;
        }

        public enum ElevationStrat{
            Groupings,
        }

        public enum LandStrat{
            Perlin,
            
        }

        public enum RegionStrat{
            MapFactor,
        }

        public enum FeaturesStrat{
            RegionSpecific,
        }

        public enum CapitalStrat{
            Random,
        }

        public enum ResourceStrat{
            RegionRandom,
        }

    }
}



  