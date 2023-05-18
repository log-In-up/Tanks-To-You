using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Player data", menuName = "Game data/Player data", order = 0)]
    public sealed class PlayerData : ScriptableObject
    {
        [field: SerializeField, Min(0.0f)] public float HullRotationSmoothTime { get; private set; }
        [field: SerializeField, Min(0.0f)] public float HullRotationSpeed { get; private set; }
        [field: SerializeField, Min(0.0f)] public float MovementSmoothTime { get; private set; }
        [field: SerializeField, Min(0.0f)] public float MovementSpeed { get; private set; }
        [field: SerializeField, Min(0.0f)] public float TurretRotationSmoothTime { get; private set; }
        [field: SerializeField, Min(0.0f)] public float TurretRotationSpeed { get; private set; }
    }
}