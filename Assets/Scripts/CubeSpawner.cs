using System.Collections;
using UnityEngine;
using Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    private Pool<Cube> _cubePool;
    private Coroutine _currentCoroutine;
    private Transform _transform;
    private float _lastSpawnTime;

    public float CubeSpeed
    {
        get => Cube.Speed;
        set => Cube.Speed = value;
    }
    
    public float MaxPathLength
    {
        get => Cube.MaxPathLength;
        set
        {
            if (value < 0) return;
            Cube.MaxPathLength = value;
        }
    }
    
    private float _spawnDelay = 1.0f; 
    public float SpawnDelay
    {
        get => _spawnDelay;
        set
        {
            if (value == _spawnDelay || value < 0) return;
            StopCoroutine(_currentCoroutine);
            float timeBeforeNextSpawn = value - (Time.unscaledTime - _lastSpawnTime);
            _spawnDelay = value;
            _currentCoroutine = StartCoroutine(Spawn(timeBeforeNextSpawn));
        }
    }

    private void Awake()
    {
        _cubePool = new Pool<Cube>();
        _transform = transform;
    }

    private void Start()
    {
        _currentCoroutine = StartCoroutine(Spawn(0));
    }

    IEnumerator Spawn(float startDelay)
    {
        if (startDelay > 0)
        {
            yield return new WaitForSeconds(startDelay);
        }

        while (true)
        {
            if (_cubePool.IsEmpty || _cubePool.GetNext().gameObject.activeSelf)
            {
                _cubePool.Add(Instantiate(cubePrefab, _transform.position, _transform.rotation, _transform));
            }
            else
            {
                _cubePool.Next();
                _cubePool.GetValue().Reuse(_transform.position);
            }
            _lastSpawnTime = Time.unscaledTime;
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
