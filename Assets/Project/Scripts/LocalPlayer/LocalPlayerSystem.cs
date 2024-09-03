using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Zuy.NoobKnight.LocalPlayer
{
    partial class LocalPlayerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var input = LocalPlayerInput.Instance.cachedInput;
            var inputDelta = new float3(input.x, input.z, 0f) * deltaTime;

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

            public void Execute(ref LocalTransform localTransform, in LocalPlayer localPlayer)
            {
                localTransform.Position += InputDelta * localPlayer.moveSpeed;
            }
        }
    }
}
