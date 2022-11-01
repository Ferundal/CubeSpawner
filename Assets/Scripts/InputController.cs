using TMPro;
using UnityEngine;


public class InputController : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner cubeSpawner;
    [SerializeField]
    private TMP_InputField tmpSpeedInputField;
    [SerializeField]
    private TMP_InputField tmpDistanceInputField;
    [SerializeField]
    private TMP_InputField tmpSpawnDelayInputField;

    private void Awake()
    {
        SetSpeed(tmpSpeedInputField.text);
        SetMaxPathLength(tmpDistanceInputField.text);
        SetSpawnDelay(tmpSpawnDelayInputField.text);
    }

    public void SetSpeed(string text)
    {
        float number;
        float.TryParse(text, out number);
        cubeSpawner.CubeSpeed = number;
    }
    
    public void SetMaxPathLength(string text)
    {
        float number;
        float.TryParse(text, out number);
        cubeSpawner.MaxPathLength = number;
    }

    public void SetSpawnDelay(string text)
    {
        float number;
        float.TryParse(text, out number);
        cubeSpawner.SpawnDelay = number;
    }
}
