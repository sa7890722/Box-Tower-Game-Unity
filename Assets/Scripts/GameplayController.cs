using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public BoxSpawner box_Spawner;
    public bool gameDone;
    public Transform player;
    public Text scoreText;
    public Text highScore;

    [HideInInspector]
    public BoxScript currentBox;
    public BoxScript box_Script;
    public int count;

    public CameraFollow cameraScript;
    private int moveCount;
    //public int count = 0;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        box_Spawner.SpawnBox();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if(Input.anyKeyDown)
        {
            currentBox.DropBox();
        }
    }



    public void SpawnNewBox()
    {
        Invoke("NewBox", 2.5f);
    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount+=1;
        count++;
        if (moveCount == 1)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 1f;
        }
        int vv = count * 100;
        scoreText.text = vv.ToString();

        if (vv > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", vv);
            highScore.text = vv.ToString();
        }
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
