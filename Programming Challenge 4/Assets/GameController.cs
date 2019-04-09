using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject userStart;
    public GameObject pokeGame;

    private void Start()
    {
        pokeGame.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void StartGame()
    {
        userStart.SetActive(false);
        pokeGame.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
