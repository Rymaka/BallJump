using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] PlatformSpawner _spawner;

    [Header("Ads Settings :")]
    [SerializeField] int _interstitialAdInterval = 3;
    public static int _gameplayCount;

    public static event Action OnStartGame;
    public static event Action<bool> OnEndGame;

    private void Awake()
    {
        _player.OnFirstJump += StartGame;
    }
    void StartGame()
    {
        OnStartGame?.Invoke();
    }





}