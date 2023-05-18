using UnityEngine;

namespace Ecs.Components
{
    struct Unit
    {
        public Transform Transform;
        public Vector3 Position;
        public Quaternion Rotation;
        public float MovementSpeed, MovementSmoothTime;
        public float RotationSpeed, RotationSmoothTime;
        public float TurretRotationSpeed, TurretRotationSmoothTime;
        public float CurrentMovementSpeed, CurrentRotationSpeed, CurrentTurretRotationSpeed;
    }
}