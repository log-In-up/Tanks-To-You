using UnityEngine;
using Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Constants;

namespace Ecs.Systems
{
    public sealed class UserKeyboardInputSystem : IEcsRunSystem
    {
        #region Fields
        private readonly EcsFilterInject<Inc<Player, MoveCommand, TurnCommand>> _units = default;
        #endregion

        #region Ecs Methods
        public void Run(IEcsSystems systems)
        {
            foreach (int intity in _units.Value)
            {
                float verticalInput = Input.GetAxisRaw(KeyboardInput.VerticalAxis);
                float horizontalInput = Input.GetAxisRaw(KeyboardInput.HorizontalAxis);

                Vector2 movementInput = new Vector2(horizontalInput, verticalInput).normalized;

                ref TurnCommand turnCommand = ref _units.Pools.Inc3.Get(intity);
                turnCommand.Horizontal = movementInput.x;

                ref MoveCommand moveCommand = ref _units.Pools.Inc2.Get(intity);
                moveCommand.Vertical = movementInput.y;
            }
        }
        #endregion
    }
}