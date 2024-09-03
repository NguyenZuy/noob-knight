using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Zuy.NoobKnight.LocalPlayer
{
    public class LocalPlayerAuthoring : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public bool lerpStopping = false;
        
        class LocalPlayerBaker : Baker<LocalPlayerAuthoring>
        {
            public override void Bake(LocalPlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new LocalPlayer
                {
                    moveSpeed = authoring.moveSpeed,
                    lerpStopping = authoring.lerpStopping,
                    cachedInput = float3.zero
                });
            }
        }
    }
}
