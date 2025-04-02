using TMPro;
using UnityEngine;

using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerJoinRequestManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timer;
    [SerializeField]
    int _numberOfPlayers = 2;
    [SerializeField]
    private float _countdownTime = 3f;
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
        this.gameObject.SetActive(false);
    }
}
