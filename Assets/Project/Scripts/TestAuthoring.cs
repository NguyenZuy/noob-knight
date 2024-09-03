using Unity.Entities;
using UnityEngine;

public class TestAuthoring : MonoBehaviour
{
    class TestBaker : Baker<TestAuthoring>
    {
        public override void Bake(TestAuthoring authoring)
        {
            throw new System.NotImplementedException();
        }
    }
}
