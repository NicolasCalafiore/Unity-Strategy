using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;
using static Terrain.RegionsEnums;


namespace Character {

    // Likes Similiar Government Type
    public class IdeologicalTrait : ForeignTraitBase {
        public IdeologicalTrait(): base(TraitManager.IDEOLOGICAL, "Likes Similiar Governments", 5) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameGovernmentType(player, other_player) ? value : value * -1;
        }
        public override bool isActivated(Player player, Player other_player) => isSameGovernmentType(player, other_player);
        
        
    }

    // Likes Similiar Region Type
    public class RegionalConnection : ForeignTraitBase {
        public RegionalConnection() : base(TraitManager.REGIONAL_CONNECTION, "Likes Similiar Cultures", 5) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ? value : 0;
        }
        public override bool isActivated(Player player, Player other_player) => isSameRegionType(player, other_player);
        

    }

    // Dislikes Similiar Region Type
    public class HomelandTrait : ForeignTraitBase {
        public HomelandTrait() : base(TraitManager.HOMELAND, "Dislikes Similiar Cultures", 8) {}
        public override float GetTraitValue(Player player, Player other_player){
            return isSameRegionType(player, other_player) ?  value * -1 :  value/2;
        }
        public override bool isActivated(Player player, Player other_player) => true;
        
    }

    // Universal
    public class PeacePromoter : ForeignTraitBase {
        public PeacePromoter() : base(TraitManager.PEACE_PROMOTER, "Promotes Peace", 5) {}
        public override float GetTraitValue(Player player, Player other_player) => value;
        public override bool isActivated(Player player, Player other_player) => true;
    }

    // Universal
    public class WarMonger : ForeignTraitBase {
        public WarMonger() : base(TraitManager.WAR_MONGER, "War Hungry", 5){}
        public override float GetTraitValue(Player player, Player other_player) => value * -1;
        public override bool isActivated(Player player, Player other_player) => true;
        

    }

    // Universal
    public class Diplomat : ForeignTraitBase {
        public Diplomat() : base(TraitManager.DIPLOMAT, "Very Persuasive", 3){}
        public override float GetTraitValue(Player player, Player other_player) => value;
        public override bool isActivated(Player player, Player other_player) => true;
    }

    // Single Region Focused
    public class RacistRegion : ForeignTraitBase {
        public RacistRegion() : base(TraitManager.RACIST_REGION, "Discriminator Against a Region", 5){
            region_target = RegionsEnums.GetRandomRegionLandType();
        }
        public override float GetTraitValue(Player player, Player other_player){
            return other_player.GetCityByIndex(0).region_type == region_target ? value * -1 : 0;
        }
        public override bool isActivated(Player player, Player other_player){
            return other_player.GetCityByIndex(0).region_type == region_target;
        }
    }

    // Single Player Focused
    public class RacistPlayer : ForeignTraitBase {
        public RacistPlayer(Player player) : base(TraitManager.RACIST, "Discriminator Towards a Player", 5){
            this.player_target = player.government.GetForeignByIndex(0).GetRandomKnownPlayerNullable();
        }
        public override float GetTraitValue(Player other_player, Player player){
            return other_player == player_target ? value * -1 : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return other_player == player_target;
        }
    }

    // Likes Nearby Players
    public class Neighborly : ForeignTraitBase {
        public Neighborly() : base(TraitManager.NEIGHBORLY, "Likes Nearby Players", 5){
            primary_int = 5;
        }
        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < primary_int ? value : 0;
        }
        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < primary_int;
        }
    }

    // Dislikes Nearby Players
    public class HomeFront : ForeignTraitBase {
        public HomeFront() : base(TraitManager.HOME_FRONT, "Dislikes Nearby Players", 5){
            primary_int = 5;
        }

        public override float GetTraitValue(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < primary_int ? value : 0;
        }

        public override bool isActivated(Player other_player, Player player){
            return PathFinding.GetManhattanDistance(player.GetCapitalCoordinate(), other_player.GetCapitalCoordinate()) < primary_int;
        }
    }
}
