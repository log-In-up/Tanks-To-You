using Ecs.Components;
using Ecs.Services;
using GameData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class PlayerMovementSystem : IEcsRunSystem
    {
        #region Fields
        private readonly EcsCustomInject<PlayerData> _playerData = default;
        private readonly EcsCustomInject<PhysicalTimeService> _physicalTimeService = default;
        private readonly EcsFilterInject<Inc<Player, Unit, MoveCommand>> _playerPool = default;
        #endregion

        #region Ecs Methods
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerPool.Value)
            {
                ref Unit unit = ref _playerPool.Pools.Inc2.Get(entity);
                ref MoveCommand moveCommand = ref _playerPool.Pools.Inc3.Get(entity);

                float currentVelocity = 0;
                unit.CurrentMovementSpeed = Mathf.SmoothDamp(unit.CurrentMovementSpeed, moveCommand.Vertical * _playerData.Value.MovementSpeed, ref currentVelocity, _playerData.Value.MovementSmoothTime);

                ref Player player = ref _playerPool.Pools.Inc1.Get(entity);
                Rigidbody rigidbody = player.Rigidbody;
                rigidbody.MovePosition(rigidbody.position + (unit.Transform.forward * unit.CurrentMovementSpeed) * _physicalTimeService.Value.FixedDeltaTime);
            }
        }
        #endregion
    }
}