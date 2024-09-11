using System;
using System.Linq;
using UnityEngine;
using Zuy.Workspace.Base;

namespace Zuy.NoobKnight.Animated
{
    public class TextureSheetManager : BaseSingleton<TextureSheetManager>
    {
        public AnimatedCollectionSO[] animatedCollectionSOs;

        private const string MAIN_TEXTURE_PROPERTY_NAME = "_MainTex";
        
        public void ChangeDirection(int typeCharacter, int typeAnimation, int)

        public float TrasitionsTo(int typeCharacter, int typeAnimation)
        {
            var animatedCollectionSO = animatedCollectionSOs.FirstOrDefault(collection => collection.typeCharacter == typeCharacter);
            var animation = animatedCollectionSO.animatedTextureSheets.FirstOrDefault(animation => animation.typeAnimation == typeAnimation);
            animatedCollectionSO.material.SetTexture(MAIN_TEXTURE_PROPERTY_NAME, animation.textureSheet);
            return animation.speed;
        }
    }

    [Serializable]
    public class AnimatedTextureSheet
    {
        public int typeAnimation;
        public float speed;
        public int gridPixelSize;
        public Texture2D textureSheet;
        public Sprite[] spriteFrames;
    }
}