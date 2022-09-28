using Creatures.Hero;
using UnityEngine;

namespace Components.Outfit
{
    public class SwapOutfit : MonoBehaviour
    {
        [SerializeField] private Outfit _myOutfit;

        public void Swap()
        {
            var heroOutfit = FindObjectOfType<Hero>().GetComponentInChildren<Outfit>();
            (heroOutfit.OutfitType, _myOutfit.OutfitType) = (_myOutfit.OutfitType, heroOutfit.OutfitType);
        }
        
    }
}