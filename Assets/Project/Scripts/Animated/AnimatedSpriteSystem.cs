using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Zuy.NoobKnight.Animated
{
    public partial struct AnimatedSpriteSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            double elapsedTime = state.WorldUnmanaged.Time.ElapsedTime;

            var job = new SpriteAnimatedJob
            {
                elapsedTime = elapsedTime
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }

    [BurstCompile]
    public partial struct SpriteAnimatedJob : IJobEntity
    {
        public double elapsedTime;

        public void Execute(ref MaterialOverrideOffset materialOverrideOffset, ref DynamicBuffer<SpriteFrameElement> spriteFrames, in CurrentAnimatedCollection currentAnimatedCollection)
        {
            int frameIndex = (int)(elapsedTime * currentAnimatedCollection.speed) % spriteFrames.Length;
            int index = frameIndex;
            var frame = spriteFrames[index];
            materialOverrideOffset = new MaterialOverrideOffset
            {
                Offset = frame.offset,
                Scale = materialOverrideOffset.Scale
            };

        }
    }
}
