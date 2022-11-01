using UnityEngine;
using UnityEngine.Android;

public class Cube : MonoBehaviour
{
    [HideInInspector] public static float Speed = 2.0f;
    [HideInInspector] public static float MaxPathLength = 10.0f;
    
    private static Vector3 _moveDirection;
    private Vector3 _lastPosition;
    private float _pathLength;
    private Transform _currentTransform;

    private float _lastSpeed;

    private void Awake()
    {
        _moveDirection = Vector3.forward * Speed;
        _lastPosition = gameObject.transform.position;
        _pathLength = 0.0f;
        _currentTransform = transform;
        _lastSpeed = Speed;
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition;
        if (Speed != _lastSpeed)
        {
            currentPosition = _currentTransform.position;
            _pathLength += Vector3.Distance(_lastPosition, currentPosition);
            _lastPosition = currentPosition;
            _lastSpeed = Speed;
            _moveDirection = Vector3.forward * Speed;
        }
        _currentTransform.Translate(_moveDirection * Time.fixedDeltaTime);
        currentPosition = _currentTransform.position;
        if (_pathLength + Vector3.Distance(_lastPosition, currentPosition) > MaxPathLength)
        {
            gameObject.SetActive(false);
        }
    }

    public void Reuse(Vector3 startPosition)
    {
        _currentTransform.position = startPosition;
        _moveDirection = Vector3.forward * Speed;
        _lastPosition = startPosition;
        _pathLength = 0.0f;
        _lastSpeed = Speed;
        gameObject.SetActive(true);
    }
}
