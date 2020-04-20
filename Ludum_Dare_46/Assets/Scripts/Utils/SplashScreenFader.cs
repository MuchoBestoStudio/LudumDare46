using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenFader : MonoBehaviour
{
    private enum FadeState
    {
        None,
        FadeIn,
        FadeOut
    }

    [SerializeField]
    private Image _fader;
    [SerializeField]
    private GameObject[] _objectsToShow = null;
    [SerializeField]
    private float _timeBeforeTransition = 0.0f;
    [SerializeField]
    private float _transitionDuration = 0.0f;
    private int _currentObjectIndex = 0;
    [SerializeField]
    private FadeState _fadeState = FadeState.None;

    private float _updateTimer = 0.0f;

    private bool _isLoadingScene = false;

    void Start()
    {
        PlayerPrefs.SetInt("PreviousScene", 0);

        transform.SetAsLastSibling();
        Color faderColor = _fader.color;
        faderColor.a = 0.0f;
        _fader.color = faderColor;
        _fadeState = FadeState.None;
        _isLoadingScene = false;

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
        if (_isLoadingScene)
        {
            return;
        }

        _updateTimer += Time.deltaTime;

        if (_fadeState == FadeState.FadeIn)
        {
            Color faderColor = _fader.color;
            faderColor.a = _updateTimer / _transitionDuration;
            _fader.color = faderColor;
        }
        else if (_fadeState == FadeState.FadeOut)
        {
            Color faderColor = _fader.color;
            faderColor.a = 1.0f - _updateTimer / _transitionDuration;
            _fader.color = faderColor;
        }

        if (_updateTimer >= _timeBeforeTransition)
        {
            _updateTimer = 0.0f;
            if (_fadeState == FadeState.None)
            {
                _fadeState = FadeState.FadeIn;
            }
            else if (_fadeState == FadeState.FadeIn && _currentObjectIndex < _objectsToShow.Length - 1)
            {
                _objectsToShow[_currentObjectIndex].SetActive(false);
                _objectsToShow[_currentObjectIndex + 1].SetActive(true);
                ++_currentObjectIndex;
                _fadeState = FadeState.FadeOut;
            }
            else if (_fadeState == FadeState.FadeOut)
            {
                _fadeState = FadeState.None;
            }
            else
            {
                _isLoadingScene = true;
                SceneManager.LoadSceneAsync(1);
            }
        }    
    }
}
