using Unity.Entities;
using Unity.Mathematics;

namespace Zuy.NoobKnight.LocalPlayer
{
    public struct LocalPlayerComponent : IComponentData
    {
        public float moveSpeed;
        public bool lerpStopping;
        public float3 cachedInput;
    }
}
