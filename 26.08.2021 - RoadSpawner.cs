using System.Collections.Generic;
using UnityEngine;

namespace ProcedureGeneration_1
{
    public class RoadSpawner : MonoBehaviour
    {
        [Header("Main Components Road")]

        [SerializeField] private GameObject[] _roadPrefabs;
        [SerializeField] private GameObject _firstRoadObject;

        [Header("Observer Element")]

        [SerializeField] private Transform _gameTarget;

        [Header("List RoadBlocks")]

        readonly List<GameObject> CurrentBlocks = new List<GameObject>();

        [Header("Area Spawn Blocks")]

        [Range(0, 322)]
        private static float _spawnPosition = 322;
        [Range(0, 261)]
        private static float _roadLength = 261;

        [Header("Components Observer")]

        [Range(0, 160)]
        private static int _saveZoneForPlayer = 160;
        [Range(0, 6)]
        private static int _roadCount = 6;

        private void Start()
        {
            _roadLength = _firstRoadObject.GetComponent<BoxCollider>().bounds.size.x;
            CurrentBlocks.Add(_firstRoadObject);

            for (int i = 0; i < _roadCount; i++)
            {
                GenerationRoads();
            }
        }

        private void Update()
        {
            CheckForSpawn();
        }

        private void CheckForSpawn()
        {
            if (_gameTarget.position.x - _saveZoneForPlayer > (_spawnPosition - _roadCount * _roadLength)) //проверка дистанции для удаления начальноного объекта листа и генерации нового
            {
                GenerationRoads();
                DeleteRoads();
            }
        }

        private void GenerationRoads()
        {
            GameObject block = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)], transform); //параметр генерации объектов в рамках листа
            block.transform.position = new Vector3(_spawnPosition, 0.2f, 4); //параметры спавна
            CurrentBlocks.Add(block);
            _spawnPosition += _roadLength;
        }

        private void DeleteRoads()
        {
            Destroy(CurrentBlocks[0]);
            CurrentBlocks.RemoveAt(0);
        }
    }
}
