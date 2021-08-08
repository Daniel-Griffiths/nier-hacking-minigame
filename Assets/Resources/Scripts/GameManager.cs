using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip[] tracks;
    public GameObject gameOverScreen;
    private bool _gameHasEnded = false;

    void Start()
    {
        GetComponent<AudioSource>().clip = tracks[Random.Range(0, tracks.Length)];   
        GetComponent<AudioSource>().Play();
    }

    void Update() 
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        if (_gameHasEnded == false) {
            _gameHasEnded = true;
            gameOverScreen.SetActive(true);
        }
    }
}
