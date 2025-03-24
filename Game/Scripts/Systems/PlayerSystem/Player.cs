using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Character;
using static Terrain.GovernmentEnums;
using Cities;
using System.Linq;
using static Terrain.RegionsEnums;
using static Terrain.PriorityEnums;
using AI;
using static Terrain.ForeignEnums;


namespace Players {
    public class Player {

        public string state_prefix { get; set; } = "";
        public string state_suffix { get; set; } = "";
        public string name  { get; set; }
        public int id  { get; set; }
        public float wealth { get; set; }
        public int knowledge_level { get; set; }
        public int heritage_level { get; set; }
        public int belief_level { get; set; }
        public Color team_color  { get; set; }
        public GovernmentType government_type { get; set; }
        public Government government  { get; set; }
        public HexRegion home_region { get; set; }
        public List<CityPriority> main_priorities = new List<CityPriority>();
        private List<City> cities = new List<City>();
        private List<List<float>> fog_of_war_map;




        public Player(string name, int id){
            DebugHandler.player_messages_pipeline.Add($"Player created: {id} - {name}");
            this.name = name;
            this.team_color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
            this.id = id;
            PlayerManager.player_id_to_player.Add(id, this);

            this.wealth = UnityEngine.Random.Range(0, 2000);
            this.knowledge_level = UnityEngine.Random.Range(0, 5);
            this.heritage_level = UnityEngine.Random.Range(0, 5);
            this.belief_level = UnityEngine.Random.Range(0, 5);


            this.main_priorities.Add(new SciencePriority());
            this.main_priorities.Add(new ReligionPriority());
            this.main_priorities.Add(new EconomyPriority());
            this.main_priorities.Add(new StabilityPriority());
            this.main_priorities.Add(new ProductionPriority());
            this.main_priorities.Add(new NourishmentPriority());
            this.main_priorities.Add(new DefensePriority());
        }

        public void SimulateGovernment(){
            government.cabinet.StartDomesticTurn(this, fog_of_war_map);
            government.cabinet.StartForeignTurn();
        }

        public int GetStability(){
            int stability = 0;
            foreach(City city in cities){
                stability += (int)city.stability;
            }
            return stability/cities.Count;
        }

        public int GetNutrition(){
            int nutrition = 0;
            foreach(City city in cities){
                nutrition += (int)city.nourishment;
            }
            return nutrition/cities.Count;
        }

        public int GetProduction(){
            int production = 0;
            foreach(City city in cities){
                production += (int)city.construction;
            }
            return production/cities.Count;
        }

        public int GetPopulation(){
            int population = 0;
            foreach(City city in cities){
                population += (int) city.inhabitants;
            }
            return population;
        }
        internal void CalculatePriorities(bool isDebug = false)
        {
            foreach(CityPriority priority in main_priorities){
                priority.CalculatePriority(this, isDebug);
            }
        }

        public CityPriority GetHighestPriority()
        {
            CityPriority highest_priority = null;

            foreach(CityPriority priority in main_priorities)
                if(highest_priority == null || priority.priority > highest_priority.priority)
                    highest_priority = priority;

            return highest_priority;
        }
    
        public RelationshipLevel GetRelationshipLevel(Player player){
            return government.cabinet.foreign_advisor.GetRelationshipLevel(player);
        }
        public List<TraitBase> GetAllTraits(){
            List<TraitBase> traits = new List<TraitBase>();
            foreach(TraitBase trait in government.leader.traits)
                traits.Add(trait);

            foreach(TraitBase trait in government.cabinet.foreign_advisor.traits)
                traits.Add(trait);

            foreach(TraitBase trait in government.cabinet.domestic_advisor.traits)
                traits.Add(trait);
                
            return traits;
        }
       
        public List<string> GetAllTraitsStr(){
            List<TraitBase> traits = new List<TraitBase>();
            foreach(TraitBase trait in government.leader.traits)
                traits.Add(trait);

            foreach(TraitBase trait in government.cabinet.foreign_advisor.traits)
                traits.Add(trait);

            foreach(TraitBase trait in government.cabinet.domestic_advisor.traits)
                traits.Add(trait);

            List<string> traits_str = new List<string>();
            foreach(TraitBase trait in traits)
                traits_str.Add(trait.Name);

            return traits_str;
        }

        public bool HasTrait(string trait_name){
            foreach(TraitBase trait in GetAllTraits())
                if(trait.Name == trait_name)
                    return true;
            return false;
        }
    
        public CityPriority GetPriority(string priority){
            foreach(CityPriority p in main_priorities)
                if(p.name == priority)
                    return p;
            return null;
        }
       
        public void AddCity(City city) => cities.Add(city);
        public City GetCapital() => cities[0];
        public string GetOfficialName() => $"{state_prefix}{name}{state_suffix}";
        public City GetCityByIndex(int index) => cities[index];
        public Vector2 GetCapitalCoordinate()=> cities[0].col_row;
        public List<City> GetCities() => cities;
        public List<List<float>> GetFogOfWarMap() => fog_of_war_map;
        public void SetFogOfWarMap(List<List<float>> fog_of_war_map) => this.fog_of_war_map = fog_of_war_map;
        internal Player GetRandomKnownPlayerNullable() => government.cabinet.foreign_advisor.GetRandomKnownPlayerNullable();
        public bool isIsolated() => government.cabinet.foreign_advisor.known_players.Count == 0;
        public List<Player> GetKnownPlayers() => government.cabinet.foreign_advisor.known_players;

    }
}