using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using MuchoBestoStudio.LudumDare.Gameplay.Fire;

namespace MuchoBestoStudio.LudumDare.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("Your scene needs to have a Game Manager");
                    }
                }

                return _instance;
            }
        }

        [SerializeField]
        private InputActionMap _currentActionMap = null;
        private InputActionMap CurrentActionMap => _currentActionMap;

        private Controls _controls = null;
        private InputActionMap PlayerActionMap = null;
        private InputActionMap GameOverActionMap = null;

        [SerializeField]
        FireSource _fireSource = null;
        public Action onTimeUpdated = null;
        public Action onGameOver = null;
        public Action onRestartGame = null;
        public Action<bool> onPauseChanged = null;

        [SerializeField]
        private float _gameTime = 0.0f;
        public float GameTime => _gameTime;

        private bool _isGamePaused = false;
        public bool IsGamePaused => _isGamePaused;

        void InvokeGameOver()
        {
            CurrencySystem currencySystem = FindObjectOfType<CurrencySystem>();
            if (currencySystem)
            {
                currencySystem.Earn(_gameTime);
            }

            if (_currentActionMap != null)
            {
                _currentActionMap.Disable();
            }

            if (GameOverActionMap != null)
            {
                GameOverActionMap.Enable();
                _currentActionMap = GameOverActionMap;
            }
            onGameOver?.Invoke();
        }

        void InvokeRestartGame(InputAction.CallbackContext _)
        {
            onRestartGame?.Invoke();
            if (_currentActionMap != null)
            {
                _currentActionMap.Disable();
            }

            if (PlayerActionMap != null)
            {
                PlayerActionMap.Enable();
                _currentActionMap = PlayerActionMap;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(currentScene.buildIndex);
        }

        void TogglePause(InputAction.CallbackContext _)
        {
            _isGamePaused = !_isGamePaused;
            Time.timeScale = _isGamePaused ? 0.0f : 1.0f;
            onPauseChanged?.Invoke(_isGamePaused);
        }

        void Start()
        {
            _isGamePaused = false;
            _controls = new Controls();
            PlayerActionMap = _controls.Player;
            GameOverActionMap = _controls.GameOver;
            _currentActionMap = PlayerActionMap;

            _fireSource.onNoCombustibleLeft -= InvokeGameOver;
            _fireSource.onNoCombustibleLeft += InvokeGameOver;

            _controls.GameOver.Retry.performed -= InvokeRestartGame;
            _controls.GameOver.Retry.performed += InvokeRestartGame;

            _controls.Player.TogglePause.performed -= TogglePause;
            _controls.Player.TogglePause.performed += TogglePause;

            _currentActionMap.Enable();

        }

        void Update()
        {
            float timeIncrement = Time.deltaTime * Time.timeScale;
            if (timeIncrement > 0.0f)
            {
                _gameTime += timeIncrement;
                onTimeUpdated?.Invoke();
            }
        }
    }
}