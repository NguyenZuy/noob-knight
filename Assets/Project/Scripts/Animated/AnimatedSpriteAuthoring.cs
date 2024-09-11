using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Zuy.NoobKnight.Animated
{
    [RequireComponent(typeof(MeshRenderer))]
    public class AnimatedSpriteAuthoring : MonoBehaviour
    {
        //public byte defaulTypeCharacter;
        //public byte defaultTypeAnimation;
        //public float defaultSpeed;

        //public float gridPixelSize;
        //public Texture2D textureSheet;
        //public Sprite[] spriteFrames;

        public AnimatedCollectionSO animatedCollectionSO;

        class Baker : Baker<AnimatedSpriteAuthoring>
        {
            public override void Bake(AnimatedSpriteAuthoring authoring)
            {
                var firstAnimation = authoring.animatedCollectionSO.animatedTextureSheets[0];

                var texelSize = firstAnimation.textureSheet.texelSize;

                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent(entity, new MaterialOverrideOffset
                {
                    Offset = firstAnimation.spriteFrames.Length > 0 // offset
                        ? firstAnimation.spriteFrames[0].rect.position * texelSize
                        : float2.zero,
                    Scale = new float2(texelSize * firstAnimation.gridPixelSize) // scale
                });

                var frameElements = AddBuffer<SpriteFrameElement>(entity);

                for(int i = 0; i < firstAnimation.spriteFrames.Length; i++)
                {
                    bool isLast = i == firstAnimation.spriteFrames.Length - 1;
                    frameElements.Add(new SpriteFrameElement
                    {
                        offset = firstAnimation.spriteFrames[i].rect.position * texelSize,
                        isLast = isLast
                    });
                }

                foreach(var spriteFrame in firstAnimation.spriteFrames)
                {
                    
                }

                AddComponent(entity, new CurrentAnimatedCollection
                {
                    typeCharacter = authoring.animatedCollectionSO.typeCharacter,
                    typeAnimation = firstAnimation.typeAnimation,
                    speed = firstAnimation.speed,
                });
            }
        }
    }

    public struct CurrentAnimatedCollection : IComponentData
    {
        public int typeCharacter;
        public int direction; // 0: left, 1: right, 2: top, 3: bottom
        public int typeAnimation;
        public float speed;
    }

    [MaterialProperty("_OffsetXYScaleZW")]
    public struct MaterialOverrideOffset : IComponentData
    {
        public float2 Offset;
        public float2 Scale;
    }

    public struct SpriteFrameElement : IBufferElementData
    {
        public float2 offset;
        public bool isLast;
    }
}
