using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;

namespace Players {
    public class Player {
        /*
            Player class is used to store information about the player
        */

        private string state_prefix;
        private string name;
        private Color color;
        private int id;
        List<City> cities = new List<City>();
        EnumHandler.GovernmentType government_type;

        public Player(string state_prefix, string name, Color color, int id){
            this.state_prefix = state_prefix;
            this.name = name;
            this.color = color;
            this.id = id;

            GameManager.player_id_to_player.Add(id, this);
        }


        public void AddCity(City city){
            cities.Add(city);
        }

        public string GetName(){
            return name;
        }

        public City GetCity(int index){
            return cities[index];
        }

        public int GetId(){
            return id;
        }

        public void SetGovernmentType(EnumHandler.GovernmentType government_type){
            this.government_type = government_type;
        }

        public void SetStatePrefix(string state_prefix){
            this.state_prefix = state_prefix;
        }

        public EnumHandler.GovernmentType GetGovernmentType(){
            return government_type;
        }

        public string GetOfficialName(){
            return state_prefix + name;
        }

        public EnumHandler.GovernmentType GetGovernmentType(string government_type){
            return (EnumHandler.GovernmentType) Enum.Parse(typeof(EnumHandler.GovernmentType), government_type);
        }

        public void SetStateName(string name){
            this.name = name;
        }
    }
}