using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _scoreTexts = null;

    [SerializeField]
    private TextMeshProUGUI _gameTimeText = null;

    [SerializeField]
    private float _gameTimeInSeconds = 15f;

    [SerializeField]
    private string _endSceneText = "EndScene";

    private List<Color> _colors = null;

    private void Start()
    {
        _colors = GameData.Instance.Colors;
    }

    private void Update()
    {
        if (_gameTimeInSeconds > 0)
        {
            _gameTimeInSeconds -= Time.deltaTime;
            _gameTimeText.text = TimeSpan.FromSeconds(_gameTimeInSeconds).ToString(@"m\:ss");
        }
        else
        {
            SceneManager.LoadScene(_endSceneText);
        }
    }

    public void OnOrbEnter(Color otherColor)
    {
        for (int index = 0; index < _colors.Count; ++index)
        {
            if (_colors[index] == otherColor)
            {
                ++GameData.Instance.Scores[index];
                _scoreTexts[index].text = GameData.Instance.Scores[index].ToString();
            }
        }
    }

    public void OnOrbExit(Color otherColor)
    {
        for (int index = 0; index < _colors.Count; ++index)
        {
            if (_colors[index] == otherColor)
            {
                --GameData.Instance.Scores[index];
                _scoreTexts[index].text = GameData.Instance.Scores[index].ToString();
            }
        }
    }
}