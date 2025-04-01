using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _scoreTexts = null;

    [SerializeField]
    private BallManager _ballManager = null;

    private List<Color> _colors = null;
    private List<int> _scores = new();

    private void Awake()
    {
        _colors = _ballManager.Colors;

        foreach (TextMeshProUGUI scoreText in _scoreTexts)
        {
            _scores.Add(0);
        }
    }

    public void OnOrbEnter(Color otherColor)
    {
        for (int index = 0; index < _colors.Count; ++index)
        {
            if (_colors[index] == otherColor)
            {
                ++_scores[index];
                _scoreTexts[index].text = _scores[index].ToString();
            }
        }
    }

    public void OnOrbExit(Color otherColor)
    {
        for (int index = 0; index < _colors.Count; ++index)
        {
            if (_colors[index] == otherColor)
            {
                --_scores[index];
                _scoreTexts[index].text = _scores[index].ToString();
            }
        }
    }
}