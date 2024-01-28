using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cabinet;
using PlayerGovernment;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PlayerManager
{
    /*
        PlayerManager is used to manipulate the players in the game
    */
    public delegate void PlayersCreatedEventHandler(object sender, EventArgs e);
    public event PlayersCreatedEventHandler PlayersCreated;
    public static Dictionary<float, Player> player_id_to_player = new Dictionary<float, Player>();
    public List<Player> player_list = new List<Player>();
    public PlayerManager(){
        
    }

    public void GeneratePlayers(int num_players){
        
        for(int i = 0; i < num_players; i++){
            player_list.Add(new Player("player", "Player " + i, i));
        }

        SetGovernmentTypes();

    }

    private void SetGovernmentTypes(){
        List<EnumHandler.GovernmentType> government_types = Enum.GetValues(typeof(EnumHandler.GovernmentType)).Cast<EnumHandler.GovernmentType>().ToList();
        foreach(Player player in player_list){
            int random_index = UnityEngine.Random.Range(1, government_types.Count);
            player.SetGovernmentType(government_types[random_index]);
        }

        SetStatePrefix();
    }

    private void SetStatePrefix()
    {
        foreach(Player player in player_list){
            List<string> state_prefixes = IOHandler.ReadPrefixNamesRegionSpecified("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\GovernmentPrefixes.xml", player.government_type.ToString());
            int random_index = UnityEngine.Random.Range(0, state_prefixes.Count);
            string state_prefix = state_prefixes[random_index];
            player.SetStatePrefix(state_prefix);
        }
    }

    public void SetStateName(List<HexTile> hex_list)
    {
        foreach(Player player in player_list){
            Vector2 capital_coordinates = player.GetCityByIndex(0).GetColRow();
            HexTile capital_hex = hex_list.FirstOrDefault(x => x.GetColRow() == capital_coordinates);
            EnumHandler.HexRegion hex_region = capital_hex.region_type;

            List<string> state_names = IOHandler.ReadStateNamesRegionSpecified("C:\\Users\\Nico\\Desktop\\Projects\\Strategy\\Assets\\Game\\Resources\\Data\\StateNames.xml", hex_region.ToString());
            int random_index = UnityEngine.Random.Range(0, state_names.Count);
            string state_name = state_names[random_index];
            player.SetStateName(state_name);

        }
    }


    public void GenerateGovernments(MapGeneration map_generation){      // TO DO: REFACTOR - GET CHARACTER GENERATION OUT OF GOV.T GENERATION
        foreach(Player player in player_list){
            Government government = new Government(player.government_type);
            player.SetGovernment(government);
        }
    }


    



    

}
