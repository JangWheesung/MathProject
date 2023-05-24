using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator GameOver(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("End");
    }

    public void GameClear(float time)
    {
        StartCoroutine(GameClearing(time));
    }

    private IEnumerator GameClearing(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Claer");
    }
}
