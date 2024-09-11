using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Zuy.NoobKnight.Animated;

namespace Zuy.NoobKnight.LocalPlayer
{
    class LocalPlayerAuthoring : MonoBehaviour
    {
        public float moveSpeed = 5f;
    }

    class Baker : Baker<LocalPlayerAuthoring>
    {
        public override void Bake(LocalPlayerAuthoring authoring)
        { 
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new LocalPlayerComponent
            {
                moveSpeed = authoring.moveSpeed,
                
            });

        }
    }
}
