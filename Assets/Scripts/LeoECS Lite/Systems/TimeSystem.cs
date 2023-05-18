using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.Systems
{
    public sealed class TimeSystem : IEcsRunSystem
    {
        #region Fields
        private readonly EcsCustomInject<TimeService> _timeService = default;
        #endregion

        #region Ecs Methods
        public void Run(IEcsSystems systems)
        {
            _timeService.Value.Time = Time.time;
            _timeService.Value.UnscaledTime = Time.unscaledTime;
            _timeService.Value.DeltaTime = Time.deltaTime;
            _timeService.Value.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
        #endregion
    }
}