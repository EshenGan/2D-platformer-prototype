using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Manager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private GameObject Play; //button
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject PauseWhilePlaying;
    [SerializeField] private GameObject Resume;
    [SerializeField] private GameObject RL;
    [SerializeField] private GameObject RG;
    [SerializeField] private GameObject RLWhilePlaying;
    [SerializeField] private GameObject RGWhilePlaying;
    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject ExitWhilePlaying;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject skiplvl;
    [SerializeField] private GameObject skiplvlWhilePlaying;

    [SerializeField] private List<FallingSpikes> fallingspikes;

    private void Awake()
    {
        levelReset();
        Play.SetActive(true);
        gameOver.SetActive(false);
        Pause.SetActive(false);
        RL.SetActive(false);
        RG.SetActive(false);
        Resume.SetActive(false);
        PauseWhilePlaying.SetActive(false);
        skiplvlWhilePlaying.SetActive(false);
        RLWhilePlaying.SetActive(false);
        RGWhilePlaying.SetActive(false);
        Exit.SetActive(true);
        skiplvl.SetActive(true);
        ExitWhilePlaying.SetActive(false);
        victory.SetActive(false);


    }

    public void play()
    {
        Play.SetActive(false);
        Pause.SetActive(false);
        RL.SetActive(false);
        RG.SetActive(false);
        Resume.SetActive(false);
        PauseWhilePlaying.SetActive(true);
        RLWhilePlaying.SetActive(true);
        RGWhilePlaying.SetActive(true);
        skiplvlWhilePlaying.SetActive(true);
        Exit.SetActive(false);
        skiplvl.SetActive(false);
        ExitWhilePlaying.SetActive(true);
        gameOver.SetActive(false);
        victory.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("2D_platform_survival_level1")) {
            for (int i = 0; i < fallingspikes.Count; i++)
            {
                fallingspikes[i].enabled = true;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level3")) {

            Pillars[] pillars = FindObjectsOfType<Pillars>();

            for (int i = 0; i < pillars.Length; i++) // destroy old pillars from previous game session
            {
                Destroy(pillars[i].gameObject);
            }
        }
    

    }
    public void levelReset()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("2D_platform_survival_level1"))
        {
            for (int i = 0; i < fallingspikes.Count; i++)
            {
                fallingspikes[i].enabled = false;
            }
        }



    }

    public void GameOver()
    {
        bool check = player.lives.All(b => b.gameObject.activeSelf == false);
        if (check == true) {
            gameOver.SetActive(true);
            Play.SetActive(false);
            Pause.SetActive(false);
            RL.SetActive(true);
            RG.SetActive(true);
            PauseWhilePlaying.SetActive(false);
            RLWhilePlaying.SetActive(false);
            RGWhilePlaying.SetActive(false);
            Exit.SetActive(true);
            ExitWhilePlaying.SetActive(false);
            victory.SetActive(false);
            skiplvlWhilePlaying.SetActive(false);
            skiplvl.SetActive(true);
            levelReset();
        }

    }

    public void pause() 
    {
        Time.timeScale = 0f;
        gameOver.SetActive(false);
        Resume.SetActive(true);
        RL.SetActive(true);
        RG.SetActive(true);
        Play.SetActive(false);
        PauseWhilePlaying.SetActive(false);
        RLWhilePlaying.SetActive(false);
        RGWhilePlaying.SetActive(false);
        Exit.SetActive(true);
        ExitWhilePlaying.SetActive(false);
        skiplvlWhilePlaying.SetActive(false);
        skiplvl.SetActive(true);
        victory.SetActive(false);
    }

    public void resume() 
    {
        Time.timeScale = 1f;
        gameOver.SetActive(false);
        Resume.SetActive(false);
        Pause.SetActive(false);
        RL.SetActive(false);
        RG.SetActive(false);
        Play.SetActive(false);
        Exit.SetActive(false);
        PauseWhilePlaying.SetActive(true);
        RLWhilePlaying.SetActive(true);
        RGWhilePlaying.SetActive(true);
        ExitWhilePlaying.SetActive(true);
        skiplvlWhilePlaying.SetActive(true);
        skiplvl.SetActive(false);
        victory.SetActive(false);

    }

    public void restartGame()
    {            

        Time.timeScale = 0f;
        player.enabled = false;
        SceneManager.LoadScene(0);
        play();

    }

    public void restartLevel() 
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level1")) 
        {

            Time.timeScale = 0f;
            player.enabled = false;
            SceneManager.LoadScene(0);
            play();

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level2")) 
        {
            levelReset();
            SceneManager.LoadScene(1);
            play();

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level3")) 
        {
            levelReset();
            SceneManager.LoadScene(2);
            play();

        }

    }

    public void ExitGame()
    {
        Application.Quit();
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
            Debug.Log("exit executable");
        }
    }

    public void GameWon() {

        levelReset();
        victory.SetActive(true);
        gameOver.SetActive(false);
        Play.SetActive(false);
        Pause.SetActive(false);
        RL.SetActive(false);
        RG.SetActive(true);
        PauseWhilePlaying.SetActive(false);
        RLWhilePlaying.SetActive(false);
        RGWhilePlaying.SetActive(false);
        Exit.SetActive(true);
        ExitWhilePlaying.SetActive(false);
        skiplvlWhilePlaying.SetActive(false);
        skiplvl.SetActive(true);


    }

    public void skiplevel() {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level1")) {
            levelReset();
            SceneManager.LoadScene(1);
            play();

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level2")) {

            levelReset();
            SceneManager.LoadScene(2);
            play();
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2D_platform_survival_level3")) {

            levelReset();
            SceneManager.LoadScene(0);
            play();
        }
    
    }



}
