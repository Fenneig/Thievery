using System;
using UnityEngine;

namespace Components.Outfit
{
    [Serializable]
    public struct OutfitSprites
    {
        public Sprite[] Sprites;
    }

    public class Outfit : MonoBehaviour
    {
        [SerializeField] private OutfitSprites[] _sprites;
        [SerializeField] private SpriteRenderer _outfit;
        [SerializeField] private int _outfitType;

        public OutfitType OutfitType
        {
            get => (OutfitType) _outfitType;
            set => _outfitType = (int) value;
        }

        private void LateUpdate()
        {
            ChangeSkin();
        }

        public void ChangeSkin(OutfitType type)
        {
            _outfitType = (int) type;
        }

        private void ChangeSkin()
        {
            var spriteName = _outfit.sprite.name.Split('_');
            var spriteNum = int.Parse(spriteName[spriteName.Length-1]);
            _outfit.sprite = _sprites[_outfitType].Sprites[spriteNum];
        }
    }
}