using Ecs.Components;
using Ecs.Services;
using GameData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class PlayerRotationSystem : IEcsRunSystem
    {
        #region Fields
        private readonly EcsCustomInject<PlayerData> _playerData = default;
        private readonly EcsCustomInject<PhysicalTimeService> _physicalTimeService = default;
        private readonly EcsFilterInject<Inc<Player, Unit, TurnCommand>> _playerPool = default;
        #endregion

        #region Ecs Methods
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerPool.Value)
            {
                ref TurnCommand turnCommand = ref _playerPool.Pools.Inc3.Get(entity);

                ref Unit unit = ref _playerPool.Pools.Inc2.Get(entity);
                float currentVelocity = 0;
                float targetRotationSpeed = turnCommand.Horizontal * _playerData.Value.HullRotationSpeed;
                unit.CurrentRotationSpeed = Mathf.SmoothDamp(unit.CurrentRotationSpeed, targetRotationSpeed, ref currentVelocity, _playerData.Value.HullRotationSmoothTime);

                ref Player player = ref _playerPool.Pools.Inc1.Get(entity);
                player.Rigidbody.rotation *= Quaternion.Euler(0.0f, unit.CurrentRotationSpeed * _physicalTimeService.Value.FixedDeltaTime, 0.0f);
            }
        }
        #endregion  
    }
}