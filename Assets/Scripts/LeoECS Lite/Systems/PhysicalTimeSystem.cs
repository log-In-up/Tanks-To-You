using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class PhysicalTimeSystem : IEcsRunSystem
    {
        #region Fields
        private readonly EcsCustomInject<PhysicalTimeService> _physicalTimeService = default;
        #endregion

        #region Ecs Methods
        public void Run(IEcsSystems systems)
        {
            _physicalTimeService.Value.Time = Time.time;
            _physicalTimeService.Value.UnscaledTime = Time.unscaledTime;
            _physicalTimeService.Value.FixedDeltaTime = Time.fixedDeltaTime;
            _physicalTimeService.Value.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
        #endregion
    }
}