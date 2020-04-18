using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenFader : MonoBehaviour
{
    [SerializeField]
    private Image _fader;
    [SerializeField]
    private GameObject[] _objectsToShow = null;
    [SerializeField]
    private float _timeBeforeTransition = 0.0f;
    [SerializeField]
    private float _transitionDuration = 0.0f;
    private int _currentObjectIndex = 0;

    private float _updateTimer = 0.0f;


    void Start()
    {
        Color faderColor = _fader.color;
        faderColor.a = 0.0f;
        _fader.color = faderColor;
        int arraySize = _objectsToShow.Length;

        if (arraySize > 0)
        {
            _objectsToShow[0].SetActive(true);
            _currentObjectIndex = 0;
        }

        for (int i = 1; i < arraySize; ++i)
        {
            _objectsToShow[i].SetActive(false);
        }
    }

    void Update()
    {
        _updateTimer += Time.deltaTime;
        if (_updateTimer >= _timeBeforeTransition)
        {
            _updateTimer = 0.0f;
            if (_currentObjectIndex < _objectsToShow.Length - 1)
            {
                _objectsToShow[_currentObjectIndex].SetActive(false);
                _objectsToShow[_currentObjectIndex + 1].SetActive(true);
                ++_currentObjectIndex;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
