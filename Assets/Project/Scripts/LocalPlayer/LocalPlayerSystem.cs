using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.UIElements;
using Zuy.NoobKnight.Animated;

namespace Zuy.NoobKnight.LocalPlayer
{
    partial class LocalPlayerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<LocalPlayerComponent>();
        }

        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var input = LocalPlayerInput.Instance.cachedInput;
            var inputDelta = new float3(input.x, input.z, 0f) * deltaTime;
            if (SystemAPI.TryGetSingletonEntity<LocalPlayerComponent>(out Entity localPlayerEntity))
            {
                var animatedCollection = SystemAPI.GetComponentRW<CurrentAnimatedCollection>(localPlayerEntity);
                int newDirection = -1;

                if (inputDelta.x < 0f && inputDelta.z <= 0.5f)
                {
                    newDirection = 0; // Left
                }
                else if (inputDelta.x > 0f && inputDelta.z <= 0.5f)
                {
                    newDirection = 1; // Right
                }
                else if (inputDelta.z > 0f && math.abs(inputDelta.x) <= 0.5f)
                {
                    newDirection = 2; // Top
                }
                else if (inputDelta.z < 0f && math.abs(inputDelta.x) <= 0.5f)
                {
                    newDirection = 3; // Bottom
                }

                if (newDirection != -1 && newDirection != localPlayerComponent.direction)
                {
                    TextureSheetManager.Instance.ChangeDirection(localPlayerComponent., newDirection);
                }

            }

            // Create a job to process the entities in parallel
            var job = new LocalPlayerJob
            {
                InputDelta = inputDelta
            };

            // Schedule the job
            Dependency = job.ScheduleParallel(Dependency);
        }

        [BurstCompile]
        partial struct LocalPlayerJob : IJobEntity
        {
            public float3 InputDelta;

            public void Execute(ref LocalTransform localTransform, in LocalPlayerComponent localPlayer)
            {
                // Update position
                localTransform.Position += InputDelta * localPlayer.moveSpeed;
            }
        }
    }
}
