using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Linq;
using Unity.VisualScripting;
public class LandmineUILogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<BallController> _Players = new List<BallController>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_Players.Count);

        // Always find and add players
        var players = FindObjectsByType<BallController>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if (!_Players.Contains(player))
            {
                _Players.Add(player);
            }
        }

        // Check mine timer for each player
        foreach (var item in _Players)
        {
           if (gameObject.name == "RedMine")
            {
                if (item.) ;
            }
            
        }
    }
}
