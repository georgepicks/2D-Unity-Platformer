using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Screens
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenuScreen;
    [SerializeField] private GameObject startMenuScreen;
    [SerializeField] private GameObject gameWinScreen;

    //Game over audio
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        startMenuScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        gameWinScreen.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gameOverSound;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Displays the pause menu when escape is pressed
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenuScreen.SetActive(true);
    }


    //Unpauses and starts the game
    public void StartGame()
    {
        Time.timeScale = 1f;
        startMenuScreen.SetActive(false);
    }

    //Unpauses the game when the player selects resume in the pause menu
    public void UnpauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }
    
    //Called when the player runs out of lives
    public void GameOver()
    {
        audioSource.Play();
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    //Called when the player reaches the end of the game
    public void GameWon()
    {
        Time.timeScale = 0f;
        gameWinScreen.SetActive(true);
    }
}
