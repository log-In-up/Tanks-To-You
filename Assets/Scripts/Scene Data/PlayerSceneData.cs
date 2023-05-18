using UnityEngine;

namespace SceneData
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
#endif
    public sealed class PlayerSceneData : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private FindObjectsSortMode _sortMode;
#endif
        #region Editor fields
        [SerializeField] private PlayerSpawnPoint[] _playerSpawnPoints = null;
        [SerializeField] private Transform _unitsParent = null;
        #endregion

        #region Properties
        public Transform PlayerSpawnParent => _unitsParent;
        #endregion

        #region Public methods
        public PlayerSpawnPoint GetRandomSpawnPoint()
        {
            int index = Random.Range(0, _playerSpawnPoints.Length);
            return _playerSpawnPoints[index];
        }
        #endregion

#if UNITY_EDITOR
        [ContextMenu("Find player spawn points")]
        private void FindPlayerSpawnPoints()
        {
            _playerSpawnPoints = FindObjectsByType<PlayerSpawnPoint>(_sortMode);

            string log = _playerSpawnPoints.Length > 0 ? $"Successfully found {_playerSpawnPoints.Length} player spawn points." : "Player spawn points not found.";
            Debug.Log(log);
        }
#endif
    }
}