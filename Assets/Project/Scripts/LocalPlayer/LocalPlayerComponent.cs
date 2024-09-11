using Unity.Entities;
using Unity.Mathematics;

namespace Zuy.NoobKnight.LocalPlayer
{
    public struct LocalPlayerComponent : IComponentData
    {
        public float moveSpeed;
        public int direction; // 0: left, 1: right, 2: top, 3: bottom
    }
}
