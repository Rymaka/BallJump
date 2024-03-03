using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    //if distance less - spawning next platform
    [SerializeField] private float _minTargetDistance = 50f;

    [SerializeField] private float _gap = 8f;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _platformPrefab;
    [SerializeField] private int _difficultyIncreaseInterval = 5;
    [SerializeField] private DistanceController _distanceController;
    private float[] _distanceList;

    private Vector3 _lastSpawnPosition;
    private int _platformCount = 0;

    private void Awake()
    {
        _distanceList = _distanceController.distanceList;
    }

    private void Start()
    {
        _gap = _distanceList[_platformCount];
        _lastSpawnPosition = Vector3.forward * _gap;
    }

    private void Update()
    {
        if (_lastSpawnPosition.z - _target.position.z < _minTargetDistance)
        {
            if(_platformCount > 0)
            {
                _gap = _distanceList[_platformCount];
                _lastSpawnPosition += Vector3.forward * _gap;
            }
            SpawnPlatform(_platformPrefab, _lastSpawnPosition);
        }
    }


    private void SpawnPlatform(Transform prefab, Vector3 position)
    {

        Transform newPlatform = Instantiate(prefab, transform);
        newPlatform.position = position;

        Debug.Log(_gap);
        Debug.Log(_platformCount);
        _platformCount++;
    }

}
