using UnityEngine;
using Zuy.Workspace.Base;
using Zuy.Workspace.MobileController;

namespace Zuy.NoobKnight.LocalPlayer
{
    public class LocalPlayerInput : BaseSingleton<LocalPlayerInput>
    {
        public UniversalButton inputMove;
        public Vector3 cachedInput;
        public bool lerpStopping = false;
        public float moveSpeed;

        private void Update()
        {
            if (inputMove.isFingerDown)
            {
                cachedInput = inputMove.directionXZ;
                transform.forward = cachedInput;
            }
            else
            {
                if (lerpStopping)
                {
                    cachedInput = Vector3.Lerp(cachedInput, Vector3.zero, moveSpeed * Time.deltaTime);
                }
                else
                {
                    cachedInput = Vector3.zero;
                }
            }
        }
    }
}
