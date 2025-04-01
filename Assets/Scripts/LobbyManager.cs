using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private bool player1Ready = false;
    private bool player2Ready = false;

    public void Player1Ready()
    {
        player1Ready = true;
        CheckStartGame();
    }

    public void Player2Ready()
    {
        player2Ready = true;
        CheckStartGame();
    }

    private void CheckStartGame()
    {
        if (player1Ready && player2Ready)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}