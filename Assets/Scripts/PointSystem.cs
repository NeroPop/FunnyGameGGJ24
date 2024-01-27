using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;


public class PointSystem : MonoBehaviour
{
    //Input for number of players
    [SerializeField] private int playersNumber;

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

    //Is the timer paused?
    [SerializeField] private bool _PauseTimer = false;

    //List of Player Names
    [SerializeField] private List<string> _PlayerNames = new List<string>();

    //References to UI elments
    [SerializeField] private Slider timerBar;
    [SerializeField] private string[] Themes;
    [SerializeField] private TMP_Text _themeText;
    [SerializeField] private VerticalLayoutGroup _layoutGroup;
    [SerializeField] private VerticalLayoutGroup _winLooseLayoutgroup;
    [SerializeField] private Button PlayerButton;
    [SerializeField] private GameObject _nameUI;
    [SerializeField] private GameObject _guessUi;
    [SerializeField] private GameObject _passUi;
    [SerializeField] private TMP_Text _passText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] protected TMP_Text _winnerText;
    [SerializeField] private TMP_InputField _NameInput;

    // Start is called before the first frame update
    private void Start()
    {
        _nameUI.gameObject.SetActive(true);
    }

    //BeginGame is called once the number of players and their names have been assigned
    public void BeginGame()
    {
        //sets timebar maxvalue to maxtime
        timerBar.maxValue = maxTime;
       
        //add a elment to the points list for every player in the game
        for (int i = 0; i < playersNumber; i++)
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
        _nameUI.gameObject.SetActive(false);
        _PauseTimer = false;

    }

    // Update is called once per frame
    void Update()
    {
        //only runs the following code when pause time is not true.
        if (!_PauseTimer)
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

            //Ends game if currant round is greater than max round 
            if (_currantRound >= _maxRound)
            {
                gameOver();
            }
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
            
        _PauseTimer = true;

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
        _PauseTimer = false;
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
        _PauseTimer = true;
    }

    //restarts game
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void AddName()
    {
        // Get the text from the TMP Input Field
        string playerName = _NameInput.text;

        // Check if the input is not empty
        if (!string.IsNullOrEmpty(playerName))
        {
            // Add the player name to the list
            _PlayerNames.Add(playerName);

            // Update the number of players
            playersNumber = _PlayerNames.Count;

            // Optionally, you can print the names and number of players
            Debug.Log("Player Names: " + string.Join(", ", _PlayerNames.ToArray()));
            Debug.Log("Number of Players: " + playersNumber);
        }
        else
        {
            // Handle empty input
            Debug.LogWarning("Please enter a valid player name");
        }

        // Clear the TMP Input Field after adding the name
        _NameInput.text = "";
    }
}
