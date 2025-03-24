using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Cities;
using Terrain;
using TMPro;
using UnityEngine;
using Diplomacy;
using UnityEditor;



public static class DebugHandler
{
    public static List<string> player_messages_pipeline = new List<string>() {};

    public static void PrintDebugPipelines()
    {
        foreach (string message in player_messages_pipeline)
            Debug.Log(message);
        
    }

    public static Dictionary<ForeignEnums.RelationshipLevel, Color> relationship_color = new Dictionary<ForeignEnums.RelationshipLevel, Color>(){
        {ForeignEnums.RelationshipLevel.Hostile, Color.red},
        {ForeignEnums.RelationshipLevel.Unfriendly, Color.yellow},
        {ForeignEnums.RelationshipLevel.Neutral, Color.gray},
        {ForeignEnums.RelationshipLevel.Friendly, Color.green},
        {ForeignEnums.RelationshipLevel.Welcoming, Color.blue}
    };

    public static void DisplayMessage(List<string> message){
        string MESSAGE = "";
        foreach(string line in message){
            MESSAGE += line + "\n";
        }
        Debug.Log(MESSAGE);
    }

        public static void PrintRelationships(Player player){
            List<string> message = new List<string>();
            foreach(Player known_player in player.GetKnownPlayers()){
                message.Add($"{player.GetOfficialName()} <--> {known_player.GetOfficialName()}");
                message.Add($"Final Relationship: {player.government.cabinet.foreign_advisor.GetRelationshipLevel(known_player)} ({player.government.cabinet.foreign_advisor.relations[known_player]})");

                foreach(TraitBase trait in player.government.leader.traits){
                    if(trait is ForeignTraitBase) 
                        message.Add($"{trait.Name}: {((ForeignTraitBase) trait).GetTraitValue(player, known_player)  * Leader.TRAIT_MULTIPLIER}");
                }
                foreach(ForeignTraitBase trait in player.government.cabinet.foreign_advisor.traits){
                    message.Add($"{trait.Name}: {trait.GetTraitValue(player, known_player)}");
                }

                message.Add($"Trait Comparisons: {DiplomacyManager.CalculateTraitComparisonsImpact(player, known_player)}");

                DisplayMessage(message);
                message.Clear();
            }
        }

    public static void Print2DMap(List<List<float>> map){
        string MESSAGE = "";
        for(int i = 0; i < map.Count; i++){
            for(int j = 0; j < map[i].Count; j++){
                MESSAGE += map[i][j] + " ";
            }
            MESSAGE += "\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void ClearLogConsole() {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    internal static void PrintPlayerState(Player player_view)
    {
        List<string> message = new List<string>
        {
            $"Player: {player_view.GetOfficialName()}",
            $"Priority: {player_view.GetHighestPriority().name}",
            $"Government: {player_view.government_type}",
            $"Stability: {player_view.GetStability()}",
            $"Nutrition: {player_view.GetNutrition()}",
            $"Production: {player_view.GetProduction()}",
            $"Wealth: {player_view.wealth}",
            $"Knowledge: {player_view.knowledge_level}",
            $"Belief: {player_view.belief_level}",
            $"Heritage: {player_view.heritage_level}",
            $"Capital: {player_view.GetCapitalCoordinate()}",
            $"Known Players: {player_view.GetKnownPlayers().Count}",
            $"Cities: {player_view.GetCities().Count}",
            $"Fog of War: {player_view.GetFogOfWarMap().Count}",
            $"Prioties",
            $"Science: {player_view.main_priorities[0].priority}",
            $"Religion: {player_view.main_priorities[1].priority}",
            $"Economy: {player_view.main_priorities[2].priority}",
            $"Stability: {player_view.main_priorities[3].priority}",
            $"Production: {player_view.main_priorities[4].priority}",
            $"Nourishment: {player_view.main_priorities[5].priority}",
            $"Defense: {player_view.main_priorities[6].priority}",

        };

        DisplayMessage(message);
    }
}
