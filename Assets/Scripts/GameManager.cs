using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool player1Ready;
    public bool player2Ready;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerReady(int playerID, bool isReady)
    {
        if (playerID == 1) player1Ready = isReady;
        if (playerID == 2) player2Ready = isReady;
    }
}
