using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void PlayButton()
    {

        // Load Essential scene
        SceneManager.LoadScene("Essential");

        // Load LowerIsland scene
        SceneManager.LoadScene("LowerIsland", LoadSceneMode.Additive);
    }
}
