using Constants;
using Ecs.Components;
using GameData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SceneData;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        #region Fields
        private readonly EcsCustomInject<PlayerData> _playerData = default;
        private readonly EcsCustomInject<PlayerSceneData> _playerSceneData = default;
        private readonly EcsPoolInject<MoveCommand> _moveCommandPool = default;
        private readonly EcsPoolInject<Player> _playerPool = default;
        private readonly EcsPoolInject<TurnCommand> _turnCommandPool = default;
        private readonly EcsPoolInject<Unit> _unitPool = default;
        #endregion

        #region Ecs Methods
        public void Init(IEcsSystems systems)
        {
            Object playerPrefab = Resources.Load(ResourcePaths.PLAYER);
            PlayerSpawnPoint point = _playerSceneData.Value.GetRandomSpawnPoint();
            GameObject playerGameObject = (GameObject)Object.Instantiate(playerPrefab, point.transform.position, point.transform.rotation, _playerSceneData.Value.PlayerSpawnParent);

            int playerEntity = _unitPool.Value.GetWorld().NewEntity();

            _moveCommandPool.Value.Add(playerEntity);
            ref Player player = ref _playerPool.Value.Add(playerEntity);
            if (playerGameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                player.Rigidbody = rigidbody;
            }
            else Debug.LogError("There is no Rigidbody component on the player.");

            _turnCommandPool.Value.Add(playerEntity);
            ref Unit unit = ref _unitPool.Value.Add(playerEntity);

            unit.Transform = playerGameObject.transform;
            unit.Position = playerGameObject.transform.position;
            unit.Rotation = playerGameObject.transform.rotation;
            unit.MovementSpeed = _playerData.Value.MovementSpeed;
            unit.RotationSpeed = _playerData.Value.HullRotationSpeed;
            unit.TurretRotationSpeed = _playerData.Value.TurretRotationSpeed;
        }
        #endregion
    }
}