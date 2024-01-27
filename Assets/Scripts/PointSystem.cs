using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PointSystem : MonoBehaviour
{
    //Input for number of players
    [SerializeField] private int players;

    //player points system
    private List<int> _points = new List<int>();
    
    //currant time on timer
    private float _timer;

    //max time/ timelimit
    [SerializeField] private float maxTime;

    //currantly selected player
    private int currantPlayer;
  
    //player point give buttons
    private List<Button> _buttons = new List<Button>();

    //Currant round
    private int _currantRound;

    //Max number of rounds
    [SerializeField] private int _maxRound;

    //References to UI elments
    [SerializeField] private Slider timerBar;
    [SerializeField] private string[] Themes;
    [SerializeField] private TMP_Text _themeText;
    [SerializeField] private VerticalLayoutGroup _layoutGroup;
    [SerializeField] private VerticalLayoutGroup _winLooseLayoutgroup;
    [SerializeField] private Button PlayerButton;
    [SerializeField] private GameObject _guessUi;
    [SerializeField] private GameObject _passUi;
    [SerializeField] private TMP_Text _passText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] protected TMP_Text _winnerText;

    // Start is called before the first frame update
    void Start()
    {
        //sets timebar maxvalue to maxtime
        timerBar.maxValue = maxTime;
       
        //add a elment to the points list for every player in the game
        for (int i = 0; i < players; i++)
        {
            _points.Add(0);
            Button newButton = Instantiate(PlayerButton, _layoutGroup.transform);
            newButton.GetComponent<ButtonClick>()._buttInt = i;
            newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "" + i;
            _buttons.Add(newButton);
        }
        pass();

        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //progresses timer
        _timer += 1 * Time.deltaTime;
        timerBar.value = _timer;

        //resets timer if it is greater than max value ending turn and moveing to next pass stage
        if (_timer > maxTime)
        {
            _timer = 0;
            pass();
        }

        //Ends game is currant round is greater than max round 
        if (_currantRound >= _maxRound)
        {
            gameOver();
        }

    }

    
    //preps game for next round
    void reRoll()
    {
        if (_currantRound >= _maxRound) 
            return;
 
        _timer = 0;
        _themeText.SetText(Themes[Random.Range(0, Themes.Length)]);

        _buttons[currantPlayer].GetComponent<Image>().color = Color.green;
    }

    //awards points to player who won
    public void win()
    {
        pass();
        _points[currantPlayer]++;
        
    }

    //Don't think this dose anything anymore but to afried to get rid of it...
    public void fail()
    {
        reRoll();
    }

    //dissables buffer button activating player buttons
    public void laugh()
    {
        _winLooseLayoutgroup.gameObject.SetActive(false);
        _layoutGroup.gameObject.SetActive(true);
    }

    //enter pass phase allowing players to pass phone between each other
    void pass()
    {
        if (_currantRound >= _maxRound)
        {
            gameOver();
            return;
        }
            


        _buttons[currantPlayer].GetComponent<Image>().color = Color.white;
        if (currantPlayer == _points.Count - 1)
        {
            currantPlayer = 0;
            _currantRound++;
        }
        else
            currantPlayer++;

        _passUi.SetActive(true);
        _guessUi.SetActive(false);
        _passText.SetText(currantPlayer + "!");
    }

    //progresses to next phase when player click redy button in pase phase
    public void ready()
    {
        _passUi.SetActive(false);
        _guessUi.SetActive(true);
        reRoll();
        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);
    }

    //end of game, shows winners and gives player options to play again or go back to main menu
    void gameOver()
    {
        string winners = "";
        _guessUi.SetActive(false);
        _passUi.SetActive(false);
        gameOverUI.SetActive(true);
        var maxValue = Mathf.Max(_points.ToArray());
        for (int i = 0; i < _points.Count; i++)
        {
            if (_points[i] == maxValue)
            {
                winners += i + " ";
            }
        }
        _winnerText.SetText(winners + "!!!");
    }

    //restarts game
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
