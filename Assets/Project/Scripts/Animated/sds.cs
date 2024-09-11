//using System;
//using Unity.Burst;
//using Unity.Entities;
//using Unity.Mathematics;
//using Unity.Rendering;
//using UnityEngine;

//namespace Zuy.NoobKnight.Animated
//{
//    [RequireComponent(typeof(MeshRenderer))]
//    public class AnimatedSpriteAuthoring : MonoBehaviour
//    {
//        [SerializeField] Texture2D spriteTextureSheet;

//        [Serializable]
//        struct AnimationCollection
//        {
//            public Sprite[] spriteFrames; 
//        }

//        [SerializeField] int gridPixelSize = 400;
//        [SerializeField] AnimationCollection[] animations;

//        class Baker : Baker<AnimatedSpriteAuthoring>
//        {
//            public override void Bake(AnimatedSpriteAuthoring authoring)
//            {
//                var texelSize = (authoring.spriteTextureSheet).texelSize;

//                if (GetComponent<MeshRenderer>().sharedMaterial.mainTexture != authoring.spriteTextureSheet)
//                    Debug.LogWarning("Sprite texture sheet does not match the material's main texture");

//                var entity = GetEntity(TransformUsageFlags.Renderable);
//                AddComponent(entity, new MaterialOverrideOffsetScale
//                {
//                    Offset = authoring.animations.Length > 0 && authoring.animations[0].spriteFrames.Length > 0 // offset
//                        ? authoring.animations[0].spriteFrames[0].rect.position * texelSize
//                        : float2.zero,
//                    Scale = new float2(texelSize * authoring.gridPixelSize) // scale
//                });

//                var frameElements = AddBuffer<SpriteFrameElement>(entity);
//                var animationClips = AddBuffer<SpriteAnimationClip>(entity);
//                foreach (var animation in authoring.animations)
//                {
//                    animationClips.Add(new SpriteAnimationClip
//                    {
//                        startIndex = frameElements.Length,
//                        count = animation.spriteFrames.Length
//                    });

//                    foreach (var spriteFrame in animation.spriteFrames)
//                    {
//                        frameElements.Add(new SpriteFrameElement
//                        {
//                            offset = spriteFrame.rect.position * texelSize
//                        });
//                    }
//                }
//                AddComponent(entity, new SpriteCurrentAnimationSelected
//                {
//                    AnimationIndex = 0,
//                    AnimationSpeed = 4.0f // Thiết lập giá trị mặc định cho tốc độ animation
//                });
//            }
//        }

//    }


//    [MaterialProperty("_OffsetXYScaleZW")]
//    struct MaterialOverrideOffsetScale : IComponentData
//    {
//        public float2 Offset;
//        public float2 Scale;
//    }
//    public struct SpriteCurrentAnimationSelected : IComponentData
//    {
//        public int AnimationIndex;
//        public float AnimationSpeed; // Thêm biến tốc độ
//    }


//    struct SpriteAnimationClip : IBufferElementData
//    {
//        public int startIndex;
//        public int count;
//    }

//    struct SpriteFrameElement : IBufferElementData
//    {
//        public float2 offset;
//    }


//    partial struct SpriteSheetSystem : ISystem
//    {
//        [BurstCompile]
//        public void OnUpdate(ref SystemState state)
//        {
//            foreach (var (materialOverride, animationSelected, spriteFrames, animationClips) in
//                SystemAPI.Query<RefRW<MaterialOverrideOffsetScale>, SpriteCurrentAnimationSelected, DynamicBuffer<SpriteFrameElement>, DynamicBuffer<SpriteAnimationClip>>())
//            {
//                var animationClip = animationClips[animationSelected.AnimationIndex];
//                var frameIndex = (int)(SystemAPI.Time.ElapsedTime * animationSelected.AnimationSpeed) % animationClip.count;
//                var frame = spriteFrames[animationClip.startIndex + frameIndex];
//                materialOverride.ValueRW = new MaterialOverrideOffsetScale
//                {
//                    Offset = frame.offset,
//                    Scale = materialOverride.ValueRO.Scale
//                };
//            }
//        }
//    }

//}