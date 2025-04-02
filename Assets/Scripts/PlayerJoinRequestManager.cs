using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerJoinRequestManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timer;
    [SerializeField]
    int _numberOfPlayers = 2;
    [SerializeField]
    private float _countdownTime = 3f;
    [SerializeField]
    private PlayerInputManager _playerInputManager;
    private bool _isCountingDown = false;

    void Update()
    {
        if (!_isCountingDown && FindObjectsByType<BallController>(FindObjectsSortMode.None).Length == _numberOfPlayers)
        {
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        _isCountingDown = true;
        float timeLeft = _countdownTime;

        while (timeLeft > 0)
        {
            _timer.text = timeLeft.ToString(); // Display one decimal place
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        _timer.text = "0";
        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
        foreach (var item in FindObjectsByType<BallController>(FindObjectsSortMode.None))
        {
            item.StartGame();
        }
        foreach (var item in FindObjectsByType<GameManager>(FindObjectsSortMode.None))
        {
            item.StartTimer();
        }
        _playerInputManager.DisableJoining();
        this.gameObject.SetActive(false);
    }
}
