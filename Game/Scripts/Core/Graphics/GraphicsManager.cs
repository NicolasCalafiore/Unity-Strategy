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
using Character;
using TMPro;
using Terrain;
using Assets.Game.Scripts.Core.Graphics;

namespace Graphics {

    public static class  GraphicsManager
    {        
        private const int OVERLAY_GRADIENT_SCALE = 15;
        private static List<GameObject> alerts_go_list = new List<GameObject>();
        private static List<GameObject> line_renderer_list = new List<GameObject>(); 
        private static Dictionary<int, Color> continentColors = new Dictionary<int, Color>();
        private static bool map_overlay_is_active = false;
        public static Dictionary<GameObject, City> city_go_to_city = new Dictionary<GameObject, City>();

        public static void Initialize() => HexGraphicManager.generic_hex = IOHandler.LoadPrefab("Core/Hex"); 
        public static void SpawnTerrain(){
            
            Debug.Log("HexManager.hex_list.Count: " + HexManager.hex_list.Count);
            foreach(HexTile hex in HexManager.hex_list)
                HexGraphicManager.Spawn(hex);
            
            InitializeHexVisuals();
        }
        
        private static void InitializeHexVisuals(){

            List<List<float>> overlay_gradient = GenerateOverlayGradient();                
            foreach(HexTile hex in HexManager.hex_list){
                ApplyRegionEffects(hex);                                         
                ApplyOceanEffects(hex);                                          
                ApplyFeature(hex);                                         
                ApplyResource(hex);                                        
                ApplyTerritoryFlag(hex);                                     
                ApplyGradient(hex, overlay_gradient);                              
            }
        }
        
        private static List<List<float>> GenerateOverlayGradient()
        {
            List<List<float>> map = MapUtils.GenerateMap();
            MapUtils.GeneratePerlinNoiseMap(map, MapManager.GetMapSize(), OVERLAY_GRADIENT_SCALE);
            MapUtils.NormalizePerlinMap(map);
            MapUtils.RatioPerlinMap(.4f, map);

            return map;
        }

        private static void ApplyFeature(HexTile hex){

            if(!hex.HasFeature()) 
                return;

            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
            GameObject feature = GameObject.Instantiate(IOHandler.LoadFeature(hex.feature_type.ToString()));
            feature.transform.SetParent(hex_object.transform);
            feature.transform.localPosition = new Vector3(0, 0, 0);
        }

        private static void ApplyResource(HexTile hex){

            if(!hex.HasResource()) 
                return;

            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
            GameObject resource = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Resources/" + hex.resource_type.ToString()));
            resource.transform.SetParent(hex_object.transform);
            resource.transform.localPosition = new Vector3(0, 0, 0);
        }

        public static void ApplyRegionEffects(HexTile hex){

            if(!hex.IsLand()) 
                return;
            
            HexGraphicManager.GetHexGoByHex(hex).transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
        }

        public static void ApplyOceanEffects(HexTile hex){
            if(hex.IsLand()) 
                return;

            HexGraphicManager.GetHexGoByHex(hex).transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + hex.region_type.ToString());
        }

        private static void ApplyGradient(HexTile hex, List<List<float>> gradient_map){

            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
            Vector2 hex_col_row = hex.GetColRow();
            Color hex_color = hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;

            float color_change = gradient_map[ (int) hex_col_row.x][ (int) hex_col_row.y];
            hex_color -= new Color(color_change, color_change, color_change, 0f);
            hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex_color;

        }

        public static void SpawnStructures()  
        {
            int counter = 0;
            List<HexTile> hex_list = HexManager.hex_list;

            foreach(HexTile hex in hex_list)
                if(hex.structure_type == StructureEnums.StructureType.Capital) counter = SpawnCapital(counter, hex);
            
        }

        private static int SpawnCapital(int counter, HexTile hex){
            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
            GameObject structure_go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/City"));
            
            city_go_to_city.Add(structure_go, CityManager.GetCapitalsList()[counter]);
            CityManager.city_to_city_go.Add(CityManager.GetCapitalsList()[counter], structure_go);
            counter++;
            
            city_go_to_city[structure_go].name = CityManager.GenerateCityName(hex); 
            Player player =  city_go_to_city[structure_go].owner_player;

            structure_go.transform.SetParent(hex_object.transform);
            structure_go.transform.localPosition = new Vector3(0, 0, 0);
            structure_go.transform.Find("CityName").GetComponent<TextMeshPro>().text = city_go_to_city[structure_go].name;
            structure_go.transform.Find("State Name").GetComponent<TextMeshPro>().text = player.name;
            structure_go.transform.Find("State Name").GetComponent<TextMeshPro>().color = player.team_color;
        

            var CapitalFlags = structure_go.transform.Find("CapitalFlags");

            foreach (Transform child in CapitalFlags)
            {
                child.GetChild(0).GetComponent<MeshRenderer>().material.color = player.team_color;
            }

            return counter;;
        }

        public static void ApplyTerritoryFlag(HexTile hex){

            if(hex.owner_player == null) return;

            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
            GameObject territory_flag = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Territory_Flag"));
            
            territory_flag.transform.SetParent(hex_object.transform);
            territory_flag.transform.localPosition = new Vector3(0, 0, 0);
            territory_flag.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = hex.owner_player.team_color;
        }

        public static void SpawnConstructionAlert(HexTile hex){
            
            GameObject alert = GameObject.Instantiate(IOHandler.LoadPrefab("Players/Construction_Alert"));
            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);
        }

        public static void SpawnExpansionAlert(HexTile hextile){
            
            GameObject alert = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Players/Expansion_Alert"));
            GameObject hex_object = HexGraphicManager.GetHexGoByHex(hextile);

            alert.transform.SetParent(hex_object.transform);
            alert.transform.localPosition = new Vector3(0, 0, 0);

            alerts_go_list.Add(alert);
        }

        public static void UpdateAllPlayerUIPriorities(){
        foreach(Player player in PlayerManager.player_list){
                GameObject structure_go = CityManager.city_to_city_go[player.GetCapital()];
                structure_go.transform.Find("Priority").GetComponent<SpriteRenderer>().sprite = IOHandler.LoadSprite("City/" + player.GetHighestPriority().ToString());
            }
        }
        public static void RemoveAlerts(){

            if(alerts_go_list.Count == 0) return;

            for (int i = alerts_go_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
            {
                GameObject.Destroy(alerts_go_list[i]);
                alerts_go_list.RemoveAt(i);
            }
        }

        public static void DrawForeignLine(GameObject capital_city, GameObject foreign_city, float relationship_float = 0){

            Color color = DebugHandler.relationship_color[ForeignEnums.GetRelationshipLevel(relationship_float)];
            
            GameObject line_render_object = new GameObject();
            line_render_object.transform.SetParent(capital_city.transform);
            line_render_object.transform.localPosition = new Vector3(0, 0, 0);
            line_render_object.AddComponent<LineRenderer>();
            LineRenderer lineRenderer = line_render_object.GetComponent<LineRenderer>();
                
            Vector3 capital_point = capital_city.transform.position;
            capital_point.y += .05f;

            Vector3 foreign_point = foreign_city.transform.position;
            foreign_point.y += .05f;

            lineRenderer.SetPosition(0, capital_point);
            lineRenderer.SetPosition(1, foreign_point);
            lineRenderer.startWidth = .1f;
            lineRenderer.endWidth = .001f;
            lineRenderer.material.color = color;
            line_renderer_list.Add(line_render_object);
        }

        public static void RemoveForeignLines(){
            if(line_renderer_list.Count == 0) return;

            for (int i = line_renderer_list.Count - 1; i >= 0; i--) //Cannot use foreach because we are removing items from the list being iterated over
            {
                GameObject.Destroy(line_renderer_list[i]);
                line_renderer_list.RemoveAt(i);
            }
        }

        public static void SpawnAIFlags(Player player = null){
            RemoveAlerts();
            RemoveForeignLines();

            if(player == null) player = PlayerManager.player_view;
        
            List<HexTile> construction_tiles = player.government.cabinet.domestic_advisor.GetPotentialConstructionTiles();
            List<HexTile> expansion_tiles = player.government.cabinet.domestic_advisor.GetPotentialExpansionTiles();
            List<Player> known_players = player.government.cabinet.foreign_advisor.GetKnownPlayers();

            foreach(HexTile hex_tile in construction_tiles){
                SpawnConstructionAlert(hex_tile);   // TO DO: FIX CONSTRUCTION BUG (NOT SHOWING UP)
            }

            foreach(HexTile hex_tile in expansion_tiles){
                SpawnExpansionAlert(hex_tile);
            }

            foreach(Player i in known_players){
                City foreign_capital_city = i.GetCapital();
                GameObject foreign_capital_city_go = CityManager.city_to_city_go[foreign_capital_city];
                
                City capital_city = player.GetCapital();
                GameObject capital_city_go = CityManager.city_to_city_go[capital_city];

                float relationship_float = player.government.cabinet.foreign_advisor.GetRelationshipFloat(i);
                DrawForeignLine(capital_city_go, foreign_capital_city_go, relationship_float);
            }
        }

        public static void ShowRelationships(){
            
            List<Player> visible_players = FogManager.GetVisiblePlayers();
            
            foreach(Player i in visible_players){
                List<Player> known_players_2 = i.government.cabinet.foreign_advisor.GetKnownPlayers();
                foreach(Player j in known_players_2){
                    
                City foreign_capital_city = j.GetCapital();
                GameObject foreign_capital_city_go = CityManager.city_to_city_go[foreign_capital_city];
                
                City capital_city = i.GetCapital();
                GameObject capital_city_go = CityManager.city_to_city_go[capital_city];

                float relationship = i.government.cabinet.foreign_advisor.GetRelationshipFloat(j);
                DrawForeignLine(capital_city_go, foreign_capital_city_go, relationship);
                }
            }
        
        }

        internal static void ShowDefenseMap()
        {
            if(!map_overlay_is_active){

                foreach(HexTile hex in HexManager.hex_list){
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.AddHexColor(hex_object);

                    if(hex.defense > 7) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                    else if(hex.defense > 5) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                    else if(hex.defense > 3) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
                    else if(hex.defense > 1) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                    else hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                map_overlay_is_active = true;
            }
            else{
                foreach(KeyValuePair<GameObject, Color> entry in HexGraphicManager.GetHexColor()){
                    entry.Key.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
                }

                map_overlay_is_active = false;
            }
        }

        internal static void ShowNourishmentMap(){
            if(!map_overlay_is_active){
                HexGraphicManager.GetHexColor().Clear();

                foreach(HexTile hex in HexManager.hex_list){
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.GetHexColor().Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);

                    if(hex.nourishment > 7) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                    else if(hex.nourishment > 5) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                    else if(hex.nourishment > 3) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
                    else if(hex.nourishment > 1) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                    else hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                map_overlay_is_active = true;
            }
            else{
                foreach(KeyValuePair<GameObject, Color> entry in HexGraphicManager.GetHexColor()){
                    entry.Key.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
                }

                map_overlay_is_active = false;
            }
        }

        internal static void ShowConstructionMap(){
            if(!map_overlay_is_active){
                HexGraphicManager.GetHexColor().Clear();

                foreach(HexTile hex in HexManager.hex_list){
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.GetHexColor().Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);

                    if(hex.construction > 7) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                    else if(hex.construction > 5) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                    else if(hex.construction > 3) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
                    else if(hex.construction > 1) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                    else hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                map_overlay_is_active = true;
            }
            else{
                foreach(KeyValuePair<GameObject, Color> entry in HexGraphicManager.GetHexColor()){
                    entry.Key.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
                }

                map_overlay_is_active = false;
            }
        }

        internal static void ShowAppealMap(){
            if(!map_overlay_is_active){
                HexGraphicManager.GetHexColor().Clear();

                foreach(HexTile hex in HexManager.hex_list){
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.GetHexColor().Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);

                    if(hex.appeal > 10) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                    else if(hex.appeal > 7) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.cyan;
                    else if(hex.appeal > 5) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                    else if(hex.appeal > 3) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
                    else if(hex.appeal > 1) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                    else hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                map_overlay_is_active = true;
            }
            else{
                foreach(KeyValuePair<GameObject, Color> entry in HexGraphicManager.GetHexColor()){
                    entry.Key.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
                }

                map_overlay_is_active = false;
            }
        }

        internal static void ShowContinents(){
            if(!map_overlay_is_active){
                HexGraphicManager.GetHexColor().Clear();


                foreach(HexTile hex in HexManager.hex_list){
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.GetHexColor().Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);
                    List<Color> colors = GenerateDistinctColors(30);
                    if(hex.continent_id == 0) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                    for(int i = 1; i < colors.Count; i++){
                        if(hex.continent_id == i) hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colors[i];
                    }
                }

                map_overlay_is_active = true;
            }
            else{
                foreach(KeyValuePair<GameObject, Color> entry in HexGraphicManager.GetHexColor()){
                    entry.Key.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.Value;
                }

                map_overlay_is_active = false;
            }
        }
        
        public static void ShowRegions() {
            if (!map_overlay_is_active) {
                HexGraphicManager.GetHexColor().Clear();
                Dictionary<int, Color> regionColors = new Dictionary<int, Color>();

                foreach (HexTile hex in HexManager.hex_list) {
                    GameObject hex_object = HexGraphicManager.GetHexGoByHex(hex);
                    HexGraphicManager.GetHexColor().Add(hex_object, hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color);

                    // Check if the region ID already has a color assigned
                    if (!regionColors.ContainsKey( (int) hex.culture_id)) {
                        // If not, generate a new unique color and add it to the dictionary
                        regionColors[ (int) hex.culture_id] = GenerateUniqueColor(hex.culture_id);
                    }

                    // Set the hex object's color based on its region ID
                    hex_object.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = regionColors[(int) hex.culture_id];
                }
            }
        }

        private static Color GenerateUniqueColor(float continent_id) {
            // Check if the color for this continent_id already exists
            if (continentColors.TryGetValue( (int) continent_id, out Color existingColor)) {
                return existingColor; // Return the existing color
            } else {
                // Generate a new unique color
                bool isSimilar;
                Color newColor;
                do {
                    isSimilar = false;
                    newColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 0f); // Blue component is 0 to exclude blue
                    foreach (Color color in continentColors.Values) {
                        if (Mathf.Abs(newColor.r - color.r) < 0.1f && Mathf.Abs(newColor.g - color.g) < 0.1f) {
                            // If the new color is too similar to any existing color, flag it and break the loop
                            isSimilar = true;
                            break;
                        }
                    }
                } while (isSimilar); // Keep generating a new color until it's not similar to any existing color

                // Save the new, unique color for this continent_id
                continentColors[ (int) continent_id] = newColor;
                return newColor;
            }
        }

        public static List<Color> GenerateDistinctColors(int count) {
            List<Color> colors = new List<Color>();
            float hueStep = 1f / count;
            for (int i = 0; i < count; i++) {
                float hue = i * hueStep;
                float saturation = 0.75f;
                float lightness = 0.5f;
                Color color = Color.HSVToRGB(hue, saturation, lightness);
                colors.Add(color);
            }
            return colors;
        }

        public static void UpdateGraphics(GraphicCommand command)
        {
            //TO DO: IMPLEMENT GRAPHICS COMMANDS
        }
    }
}
