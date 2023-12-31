using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Terrain
{
    public static class EnumHandler
    {

        /*
            Used to store all Enums
            Contains functions to convert float values (from List<List<float> map) to enums
        */

        public enum HexElevation{   //Make sure to update getter function if you add more elevation types
            Canyon = -50,
            Valley = -25,
            Flatland = 0,
            Small_Hill = 25,
            Large_Hill = 50,
            Mountain = 150,
        }

        public static HexElevation GetElevationType(float elevationValue)
        {
            Dictionary<float, HexElevation> elevationDict = new Dictionary<float, HexElevation>(){
                { (int) HexElevation.Canyon, HexElevation.Canyon},
                { (int) HexElevation.Valley, HexElevation.Valley},
                { (int) HexElevation.Flatland, HexElevation.Flatland},
                { (int) HexElevation.Small_Hill, HexElevation.Small_Hill},
                { (int) HexElevation.Large_Hill, HexElevation.Large_Hill},
                { (int) HexElevation.Mountain, HexElevation.Mountain},
            };

            return elevationDict[elevationValue];
            
        }






        public enum HexRegion{   //Make sure to update getter function if you add more region types
            Ocean ,
            River,
            Desert,
            Plains,
            Grassland,
            Tundra,
            Highlands,
            Jungle,
            Swamp,
            Shore,

        }

        public static HexRegion GetRegionType(float regionValue)
        {
            regionValue = Mathf.Round(regionValue);

            Dictionary<float, HexRegion> regionDict = new Dictionary<float, HexRegion>(){
                { (int) HexRegion.Ocean, HexRegion.Ocean},
                { (int) HexRegion.River, HexRegion.River},
                { (int) HexRegion.Desert, HexRegion.Desert},
                { (int) HexRegion.Plains, HexRegion.Plains},
                { (int) HexRegion.Grassland, HexRegion.Grassland},
                { (int) HexRegion.Tundra, HexRegion.Tundra},
                { (int) HexRegion.Highlands, HexRegion.Highlands},
                { (int) HexRegion.Jungle, HexRegion.Jungle},
                { (int) HexRegion.Swamp, HexRegion.Swamp},
                { (int) HexRegion.Shore, HexRegion.Shore},

            };


            return regionDict[regionValue];
            
        }






        public enum HexNaturalFeature{   //Make sure to update getter function if you add more feature types
            None,
            Forest,
            Oasis,
            Heavy_Vegetation,
            Rocks,
            Jungle,
            Swamp,
        }

        public static HexNaturalFeature GetNaturalFeatureType(float featureValue)
        {
            featureValue = Mathf.Round(featureValue);

            Dictionary<float, HexNaturalFeature> featureDict = new Dictionary<float, HexNaturalFeature>(){
                { (int) HexNaturalFeature.None, HexNaturalFeature.None},
                { (int) HexNaturalFeature.Forest, HexNaturalFeature.Forest},
                { (int) HexNaturalFeature.Oasis, HexNaturalFeature.Oasis},
                { (int) HexNaturalFeature.Heavy_Vegetation, HexNaturalFeature.Heavy_Vegetation},
                { (int) HexNaturalFeature.Rocks, HexNaturalFeature.Rocks},
                { (int) HexNaturalFeature.Jungle, HexNaturalFeature.Jungle},
                { (int) HexNaturalFeature.Swamp, HexNaturalFeature.Swamp},
            };

            return featureDict[featureValue];
            
        }





        public enum LandType{   //Make sure to update getter function if you add more land types
            Water,
            Land,
        }

        public static LandType GetLandType(float landValue){
            Dictionary<float, LandType> landDict = new Dictionary<float, LandType>(){
                { (int) LandType.Water, LandType.Water},
                { (int) LandType.Land, LandType.Land},
            };

            return landDict[landValue];
    
        }





        public enum HexResource{
            None,
            Iron,
            Cattle,
            Gems,
            Stone,
            Bananas,
            Incense,
            Wheat,


        }

        public static HexResource GetResourceType(float resourceValue){
            Dictionary<float, HexResource> resourceDict = new Dictionary<float, HexResource>(){
                { (int) HexResource.None, HexResource.None},
                { (int) HexResource.Iron, HexResource.Iron},
                { (int) HexResource.Cattle, HexResource.Cattle},
                { (int) HexResource.Gems, HexResource.Gems},
                { (int) HexResource.Stone, HexResource.Stone},
                { (int) HexResource.Bananas, HexResource.Bananas},
                { (int) HexResource.Incense, HexResource.Incense},
                { (int) HexResource.Wheat, HexResource.Wheat},


            };

            return resourceDict[resourceValue];
    
        }




        
        public enum StructureType{
            None,
            Capital,
        }

        public static StructureType GetStructureType(float structureValue){
            Dictionary<float, StructureType> structureDict = new Dictionary<float, StructureType>(){
                { (int) StructureType.None, StructureType.None},
                { (int) StructureType.Capital, StructureType.Capital},
            };

            return structureDict[structureValue];
    
        }





        public enum GovernmentType{
            None,
            Democracy,
            Monarchy,
            Dictatorship,
            Theocracy,
            Tribalism,

        }

        public static GovernmentType GetGovernmentType(float governmentValue){
            Dictionary<float, GovernmentType> governmentDict = new Dictionary<float, GovernmentType>(){
                { (int) GovernmentType.None, GovernmentType.None},
                { (int) GovernmentType.Democracy, GovernmentType.Democracy},
                { (int) GovernmentType.Monarchy, GovernmentType.Monarchy},
                { (int) GovernmentType.Dictatorship, GovernmentType.Dictatorship},
                { (int) GovernmentType.Theocracy, GovernmentType.Theocracy},
                { (int) GovernmentType.Tribalism, GovernmentType.Tribalism},

            };

            return governmentDict[governmentValue];
    
        }
    }
}