using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Zuy.NoobKnight.LocalPlayer
{
    class LocalPlayerAuthoring : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public bool lerpStopping = false;
    }

    class LocalPlayerBaker : Baker<LocalPlayerAuthoring>
    {
        public override void Bake(LocalPlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new LocalPlayerComponent
            {
                moveSpeed = authoring.moveSpeed,
                lerpStopping = authoring.lerpStopping,
                cachedInput = float3.zero
            });
        }
    }
}
