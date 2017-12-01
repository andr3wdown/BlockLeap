using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text title;
    public Text hScore;
    float scaler = 1;
    public float maxScaler = 1.3f;
    public float minScaler = 0.9f;
    bool dir = false;
    int hscore;
	void Start ()
    {
        hscore = PlayerPrefs.GetInt("HighScore");
        FindObjectOfType<BlockSpawner>().isStarted = true;
	}
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (!dir)
        {
            scaler += Time.deltaTime * 0.25f;
            if(scaler > maxScaler)
            {
                scaler = maxScaler;
                dir = true;
            }
        }
        else
        {
            scaler -= Time.deltaTime * 0.25f;
            if (scaler < minScaler)
            {
                scaler = minScaler;
                dir = false;
            }
        }

        title.rectTransform.localScale = new Vector3(scaler, scaler, scaler);
        hScore.text = "" + hscore;
    }
}
