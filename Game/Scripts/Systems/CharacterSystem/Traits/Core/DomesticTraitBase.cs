using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;

namespace Character {
    public abstract class DomesticTraitBase : TraitBase {
        // Traits for Domestic Advisor
        public abstract bool isActivated(Player player);
        public DomesticTraitBase(string description, int value) : base(description, value){}
        public abstract float GetTraitValue(Player player);

        public static DomesticTraitBase GetRandomDomesticTrait(Player player){

            List<DomesticTraitBase> trait_list = new List<DomesticTraitBase>(){
                new PeaceKeeper(),
                new Financier(),
                new ProductionExpert(),
                new NutritionExpert(),
                new StabilityExpert(),
                new ScienceExpert(),
            };

            int random_index = Random.Range(0, trait_list.Count);
            return trait_list[random_index];
        }
    }
}