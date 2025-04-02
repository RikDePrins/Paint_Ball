using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winnerText = null;

    private void Awake()
    {
        if (GameData.Instance == null)
        {
            return;
        }

        List<Color> colors = GameData.Instance.Colors;

        if (GameData.Instance.Scores[0] > GameData.Instance.Scores[1])
        {
            _winnerText.text = "Ball 1";
            _winnerText.color = colors[0];
        }
        else
        {
            _winnerText.text = "Ball 2";
            _winnerText.color = colors[1];
        }
    }
}