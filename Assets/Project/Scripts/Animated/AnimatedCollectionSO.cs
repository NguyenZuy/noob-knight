using System.Collections.Generic;
using UnityEngine;
using Zuy.NoobKnight.Animated;

namespace Zuy.NoobKnight.Animated
{
    [CreateAssetMenu(fileName = "AnimatedCollection", menuName = "Scriptable Objects/AnimatedCollection")]
    public class AnimatedCollectionSO : ScriptableObject
    {
        public int typeCharacter;
        public Material material;
        [SerializeField] public List<AnimatedTextureSheet> animatedTextureSheets;
    }
}
