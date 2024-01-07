using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSkript : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGem()
    {
        Application.Quit();
    }
}
