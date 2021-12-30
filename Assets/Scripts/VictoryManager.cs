using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager instance;
    
    public GameObject winPanel;
    public GameObject whiteTeam;
    public GameObject blackTeam;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine("CheckVictory");

        /*public void heckVictory()
        {
            if(Board.instance.blackTeamParent.childCount == 0)
                Victory(true);
            else if(Board.instance.whiteTeamParent.childCount == 0)
                Victory(false);
        }*/
    }

    private void Victory(bool isWhite)
    {
        winPanel.SetActive(true);

        if (isWhite)
            whiteTeam.SetActive(true);
        else
            blackTeam.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator CheckVictory()
    {
        yield return new WaitForSeconds(1f);
        
        while (!winPanel.activeSelf)
        {
            if(Board.instance.blackTeamParent.childCount == 0)
                Victory(true);
            else if(Board.instance.whiteTeamParent.childCount == 0)
                Victory(false);
        
            print("Work");

            yield return new WaitForSeconds(.25f);
        }
    }
}
