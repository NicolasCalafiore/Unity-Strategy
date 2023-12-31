using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Terrain
{
    public static class HexTileUtils
    {

        /*
            Contains all functions used for general hex manipulation
            Used to set all HexTile properties
            Used to create HexTile objects
        */
        public static List<HexTile> CreateHexObjects(Vector2 map_size){
            List<HexTile> hex_list = new List<HexTile>();

            // Create Hex objects for each column and row representative of the map
            for(int column = 0; column < map_size.x; column++)
            {
                for(int row = 0; row < map_size.y; row++)
                {
                    HexTile hex = new HexTile(column, row);
                    hex_list.Add(hex);
                }
            }

            return hex_list;
        }
        public static void SetHexFeatures(List<List<float>> features_map, List<HexTile> hex_list)
        {
            for(int i = 0; i < hex_list.Count; i++){
                Vector2 coordinates = hex_list[i].GetColRow();
                hex_list[i].SetFeatureType(EnumHandler.GetNaturalFeatureType(features_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetHexLand(List<List<float>> land_map, List<HexTile> hex_list)
        {
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetLandType(EnumHandler.GetLandType(land_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetHexElevation(List<List<float>> elevation_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetElevation(EnumHandler.GetElevationType(elevation_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }
        public static void SetHexRegion(List<List<float>> regions_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetRegionType(EnumHandler.GetRegionType(regions_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetHexResource(List<List<float>> resource_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                hex.SetResourceType(EnumHandler.GetResourceType(resource_map[ (int) coordinates.x][ (int) coordinates.y]));
            }
        }

        public static void SetStructureType(List<List<float>> structures_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                 Vector2 coordinates = hex.GetColRow();
                hex.SetStructureType(EnumHandler.GetStructureType(structures_map[(int) coordinates.x][(int) coordinates.y]));
            }
        }

        public static void SetTerritoryType(List<List<float>> territory_map, List<HexTile> hex_list){
            foreach(HexTile hex in hex_list){
                Vector2 coordinates = hex.GetColRow();
                if((int) territory_map[(int) coordinates.x][(int) coordinates.y] == -1){
                    hex.SetOwnerPlayer(null);
                }
                else{
                    hex.SetOwnerPlayer(GameManager.player_id_to_player[(int) territory_map[(int) coordinates.x][(int) coordinates.y]]);
                }
            }
        }



        

    }
}