using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Cities;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using static Terrain.ForeignEnums;


namespace Diplomacy
{
    public static class DiplomacyManager
    {
        private const float LEADER_MULTIPLIER = 1.5f;
        private const float DISIMILAIR_TRAIT_MULTIPLIER = .1f;

        public static List<string> DEBUG_MESSAGE = new List<string>();

        //This method calculates the starting relationship between two players
        //It takes into account the traits of the leader and the government characters
        public static void GenerateStartingRelationships(){
            foreach(Player player in PlayerManager.player_list){
                foreach(Player known_player in player.GetKnownPlayers()){
                    float relationship = 0;
                    relationship += GenerateBaseRelationship(player, known_player);
                    relationship += CalculateTraitRelationshipImpact(player, known_player); 
                    relationship += CalculateSimiliarTraitsImpact(player, known_player);
                    player.government.cabinet.foreign_advisor.relations.Add(known_player, relationship);
                }
            }

            foreach(Player player in PlayerManager.player_list){
                foreach(Player known_player in player.GetKnownPlayers()){
                    float relationship = player.government.cabinet.foreign_advisor.relations[known_player];
                    float relationshipImpact = CalculateRelationshipDependantRelationshipImpact(player, known_player);
                    player.government.cabinet.foreign_advisor.relations[known_player] = relationship + relationshipImpact;
                }
            }
        }

        public static float GenerateBaseRelationship(Player player, Player known_player){
            float relationship = 0;
            if(player.government_type == known_player.government_type) relationship += 10;
            if(player.home_region == known_player.home_region) relationship += 10;
            
            return relationship;
        }

        //This method calculates the relationship between two players
        //It takes into account the traits of the leader and the government characters
        public static float CalculateTraitRelationshipImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(TraitBase leader_trait in player.government.leader.traits)
                if(leader_trait is ForeignTraitBase)
                    relationship += ((ForeignTraitBase) leader_trait).GetTraitValue(player, known_player);
                
            foreach(ForeignTraitBase foreign_trait in player.government.cabinet.foreign_advisor.traits){
                relationship += foreign_trait.GetTraitValue(player, known_player);
            }
            
            return relationship;
        }

        //This method calculates the relationship between two players
        //It takes into account the similiar traits of the characters
        public static float CalculateSimiliarTraitsImpact(Player player, Player known_player){
            float relationship = 0;
            foreach(ForeignTraitBase foreign_trait in player.government.cabinet.foreign_advisor.traits){
                if(known_player.government.cabinet.foreign_advisor.traits.Contains(foreign_trait)){
                    relationship += 5;
                }
            }
            foreach(TraitBase foreign_trait in player.government.leader.traits){
                if(known_player.government.cabinet.foreign_advisor.traits.Contains(foreign_trait)){
                    relationship += 5;
                }
            }

            return relationship;
        }

        //Calculates the impact of one players relationship due to the relationship of another player towards itself
        //Requires relationships to already be calculated
        public static float CalculateRelationshipDependantRelationshipImpact(Player player, Player known_player){
            var other_relationship = known_player.government.cabinet.foreign_advisor.GetRelationshipLevel(player);
            
            if(other_relationship == RelationshipLevel.Welcoming) return 15;
            
            if(other_relationship == RelationshipLevel.Friendly) return 10;
            
            if(other_relationship == RelationshipLevel.Unfriendly) return -10;
            
            if(other_relationship == RelationshipLevel.Hostile) return -15;
            
            else return 0;
        }
    }
}
