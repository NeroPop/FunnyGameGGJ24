using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public int _buttInt;
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manger");
    }

    public void Win()
    {
        gameManager.GetComponent<PointSystem>().playerThatGuessed = _buttInt;
        gameManager.GetComponent<PointSystem>().loose();
    }

}
