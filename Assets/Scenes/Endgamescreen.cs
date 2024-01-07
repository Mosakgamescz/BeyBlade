using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Endgamescreen : MonoBehaviour
{
    public TextMeshProUGUI txt;

    private void Start()
    {
        if (Endgameholder.status == "lost")
        {
            txt.text = "You lost!";
        }
        if (Endgameholder.status == "won")
        {
            txt.text = "You won!";
        }
    }

    public void GotoMenu()
    {
        Debug.Log("gg");
        SceneManager.LoadScene(0);
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
