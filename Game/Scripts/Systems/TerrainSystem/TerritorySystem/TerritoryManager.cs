using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Players;
using System.Linq;
using Cities;




namespace Terrain {

    public class TerritoryManager
    {
        private static bool DefenseMapIsActive = false;

        public TerritoryManager(){}

        public float CalculateCityNourishment(City city){
            float nourishment = 0;

            foreach(HexTile hex in city.GetHexTerritoryList()){
                nourishment += hex.nourishment;
            }
            return nourishment;
        }

        public float CalculateCityConstruction(City city){
            float construction = 0;

            foreach(HexTile hex in city.GetHexTerritoryList()){
                construction += hex.construction;
            }
            return construction;
        }

    

    }
}