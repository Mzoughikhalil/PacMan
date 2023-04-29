using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//mzoughist
public class GameManager : MonoBehaviour
{
    public GameObject pacman;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;
    public int currentMunich = 0;

    public int score;
    public Text scoretext;
    public bool hadDeathOnThisLevel;
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        currentMunich = 0;
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoretext.text ="Score: " + score.ToString();
    }
    public void CollectedPellet (NodeController nodeController)
    {
        if (currentMunich == 0)
        {
            munch1.Play();
            currentMunich = 1;
        }
        else if (currentMunich ==1)
        {
            munch2.Play();
            currentMunich = 0;
        }

        AddToScore(10);
    }

    /*public IEnumerable PlayerEaten()
    {
        hadDeathOnThisLevel = true;
        StopGame();
        yield return new WaitForSeconds(1);

        
    }*/
    
}
