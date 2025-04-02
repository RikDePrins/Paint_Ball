using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winnerText = null;

    [SerializeField]
    private TextMeshProUGUI _notText = null;

    private void Awake()
    {
        if (GameData.Instance == null)
        {
            return;
        }

        List<Color> colors = GameData.Instance.Colors;

        if (GameData.Instance.Scores[0] == GameData.Instance.Scores[1])
        {
            _notText.gameObject.SetActive(true);
            _winnerText.text = "Draw";
            _winnerText.color = new Color(170f, 0f, 255f);
        }
        else if (GameData.Instance.Scores[0] > GameData.Instance.Scores[1])
        {
            _notText.gameObject.SetActive(false);
            _winnerText.text = "Ball 1";
            _winnerText.color = colors[0];
        }
        else
        {
            _notText.gameObject.SetActive(false);
            _winnerText.text = "Ball 2";
            _winnerText.color = colors[1];
        }

        for (int index = 0; index < GameData.Instance.Scores.Count; ++index)
        {
            GameData.Instance.Scores[index] = 0;
        }
    }
}