using Constants;
using Ecs.Services;
using Ecs.Systems;
using GameData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.UnityEditor;
using SceneData;
using UnityEngine;

namespace Ecs
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
#endif
    public sealed class EcsGameStartup : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private PlayerSceneData _playerSceneData = null;
        #endregion

        #region Fields
        private EcsWorld _world;
        private IEcsSystems _systemsInUpdate, _systemsInFixedUpdate;
#if UNITY_EDITOR
        private IEcsSystems _editorSystems;
#endif
        #endregion

        #region MonoBehaviour API       
        private void Awake()
        {
            TimeService timeService = new TimeService();

            _world = new EcsWorld();
            _systemsInUpdate = new EcsSystems(_world);
            _systemsInUpdate
                .Add(new TimeSystem())
                .Add(new PlayerInitSystem())
                .Add(new UserKeyboardInputSystem())
                .Inject(_playerData, _playerSceneData, timeService)
                .Init();

            PhysicalTimeService physicalTimeService = new PhysicalTimeService();

            _systemsInFixedUpdate = new EcsSystems(_world);
            _systemsInFixedUpdate
                .Add(new PhysicalTimeSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
                .Inject(_playerData, physicalTimeService)
                .Init();
#if UNITY_EDITOR
            _editorSystems = new EcsSystems(_world);
            _editorSystems
                .Add(new EcsWorldDebugSystem())
                .AddWorld(new EcsWorld(), Worlds.EVENTS)
                .Add(new EcsWorldDebugSystem(Worlds.EVENTS))
                .Init();
#endif
        }

        private void Update()
        {
            _systemsInUpdate.Run();
#if UNITY_EDITOR
            _editorSystems.Run();
#endif
        }

        private void FixedUpdate()
        {
            _systemsInFixedUpdate.Run();
        }

        private void OnDestroy()
        {
            if (_systemsInUpdate != null)
            {
                _systemsInUpdate.Destroy();
                _systemsInUpdate = null;
            }

            if (_systemsInFixedUpdate != null)
            {
                _systemsInFixedUpdate.Destroy();
                _systemsInFixedUpdate = null;
            }

#if UNITY_EDITOR
            if (_editorSystems != null)
            {
                _editorSystems.Destroy();
                _editorSystems = null;
            }
#endif

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
        #endregion
    }
}