using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [SerializeField]
    private Material[] _materials = null;

    private List<int> _scores = new();

    public List<int> Scores
    {
        get
        {
            return _scores;
        }
    }

    public Material[] Materials
    {
        get
        {
            return _materials;
        }
    }

    public List<Color> Colors
    {
        get
        {
            List<Color> colors = new();

            foreach (Material material in _materials)
            {
                colors.Add(material.color);
            }

            return colors;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Material material in _materials)
            {
                _scores.Add(0);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}