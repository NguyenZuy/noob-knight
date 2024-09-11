using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Zuy.NoobKnight.LocalPlayer;
using UnityEngine;
using log4net.Util;

namespace Zuy.NoobKnight.Camera
{
    public partial class CameraSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<LocalPlayer.LocalPlayerComponent>();
        }

        protected override void OnUpdate()
        {
            var localPlayerEntity = EntityManager.CreateEntityQuery(typeof(LocalPlayer.LocalPlayerComponent)).GetSingletonEntity();
            var localPlayerLocalTransform = EntityManager.GetComponentData<LocalTransform>(localPlayerEntity);

            // Target position for the camera (ignore Z-axis)
            Vector3 targetPosition = new Vector3(localPlayerLocalTransform.Position.x, localPlayerLocalTransform.Position.y, UnityEngine.Camera.main.transform.position.z);

            Vector3 velocity = Vector3.zero;
            // Smoothly move the camera towards the target position
            UnityEngine.Camera.main.transform.position = Vector3.SmoothDamp(UnityEngine.Camera.main.transform.position, targetPosition, ref velocity, 0);
        }
    }
}
