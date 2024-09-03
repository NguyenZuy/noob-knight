using Unity.Entities;
using Unity.Mathematics;

namespace Zuy.NoobKnight.LocalPlayer
{
    public struct LocalPlayer : IComponentData
    {
        public float moveSpeed;
        public bool lerpStopping;
        public float3 cachedInput;
    }

    public struct PlayerInput : IComponentData
    {
        public float2 direction;
        public bool isMoving;
    }
}
