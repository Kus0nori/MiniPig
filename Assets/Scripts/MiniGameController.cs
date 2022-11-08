using System.Collections;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public static bool MiniGameIsActive;
    private float _miniGameTime = 5f;
    private int _miniGameStep = 25;
    private int _nextMiniGameScore;
    private GameObject[] _circleInstances;
    private int _circlesCount = 5;
    public GameObject miniGameCircle;
    private int _bonusPoints = 50;
    public int clickedCirclesCounter = 0;
    private void Start()
    {
        _circleInstances = new GameObject[_circlesCount];
        _nextMiniGameScore = ((int)Fingering.ScoreNumber / _miniGameStep) * _miniGameStep + _miniGameStep;
    }

    private void Update()
    {
        if (Fingering.ScoreNumber >= _nextMiniGameScore)
        {
            StartCoroutine(MiniGameOnEnable());
            _nextMiniGameScore += _miniGameStep;
        }
    }

    private IEnumerator MiniGameOnEnable()
    {
        MiniGameIsActive = true;
        for (var i = 0; i < _circlesCount; i++)
        {
            var randPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-2.5f, 2.5f), -1);
            _circleInstances[i] = Instantiate(miniGameCircle, randPosition, Quaternion.identity);
        }
        yield return new WaitForSeconds(_miniGameTime);
        if (clickedCirclesCounter == _circlesCount)
        {
            Fingering.ScoreNumber += _bonusPoints;
            _nextMiniGameScore += _bonusPoints;
            clickedCirclesCounter = 0;
        }
        else
        {
            Fingering.ScoreNumber -= _bonusPoints;
            _nextMiniGameScore -= _bonusPoints;
        }
        for (var i = 0; i < _circleInstances.Length; i++)
        {
            Destroy(_circleInstances[i]);
            _circleInstances[i] = null;
        }
        MiniGameIsActive = false;
    }
}
