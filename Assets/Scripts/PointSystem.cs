using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;
using UnityEditor.Animations;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PointSystem : MonoBehaviour
{
    //Input for number of players
    [SerializeField] private int playersNumber;

    //player points system
    [SerializeField] private List<int> _points = new List<int>();
    
    //currant time on timer
    private float _timer;

    //max time/ timelimit
    [SerializeField] private float maxTime;

    //currantly selected player
    private int currantPlayer;
  
    //player point give buttons
    private List<Button> _buttons = new List<Button>();

    //Currant round
    [SerializeField] private int _currantRound;

    //Max number of rounds
    [SerializeField] private int _maxRound;

    //Is the timer paused?
    [SerializeField] private bool _PauseTimer = true;

    //References to UI panels
    [Header ("UI Panels")]
    [SerializeField] private GameObject _nameUI;
    [SerializeField] private GameObject _guessUi;
    [SerializeField] private GameObject _passUi;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject _guideUI;
    [SerializeField] private GameObject _creditsUI;
    [SerializeField] private GameObject _mainMenuUI;

    //References to UI elments
    [Header("UI Elements")]
    [SerializeField] private Slider timerBar;
    [SerializeField] private TMP_Text _themeText;
    [SerializeField] private VerticalLayoutGroup _layoutGroup;
    [SerializeField] private VerticalLayoutGroup _winLooseLayoutgroup;
    [SerializeField] private Button PlayerButton;
    [SerializeField] private TMP_Text _passText;
    [SerializeField] protected TMP_Text _winnerText;
    [SerializeField] private TMP_InputField _NameInput;
    [SerializeField] private TMP_Text _PlayerCountTxt;
    [SerializeField] private TMP_Text _roundNumText;
    [SerializeField] private Slider _roundNumSlider;

    [SerializeField] private GameObject _themeBuffer;
    [SerializeField] private TMP_Text _themeBufferText;

    //List of themes
    [SerializeField] private string[] Themes;

    //List of Player Names
    [SerializeField] private List<string> _PlayerNames = new List<string>();

    private bool _startOfGame = true;

    private string _selectedTheme;

    public int playerThatGuessed;

    // Start is called before the first frame update
    private void Start()
    {
        _mainMenuUI.gameObject.SetActive(true);
        
    }

    public void Play()
    {
        _mainMenuUI.gameObject.SetActive(false);
        _nameUI.gameObject.SetActive(true);
    }

    //BeginGame is called once the number of players and their names have been assigned
    public void BeginGame()
    {
        if (_PlayerNames.Count < 2)
            return;

        //sets timebar maxvalue to maxtime
        timerBar.maxValue = maxTime;

        for (int i = 0; i < playersNumber; i++)
        {
            _points.Add(0);
        }

        pass();

        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);
        _nameUI.gameObject.SetActive(false);
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

            ////Ends game if currant round is greater than max round 
            //if (_currantRound >= _maxRound)
            //{
            //    gameOver();
            //}
        }

    }

    
    //preps game for next round
    void reRoll()
    {
        if (_currantRound >= _maxRound) 
            return;
 
        _timer = 0;
        
    }

    //awards points to player who won
    public void win()
    {
        _points[currantPlayer]++;
        pass();

    }

    public void loose()
    {
        _points[playerThatGuessed]++;
        pass();
    }

    //Don't think this dose anything anymore but to afried to get rid of it...
   

    //dissables buffer button activating player buttons
    public void laugh()
    {
        //insert some code here that gives the current host a point :)
        //_winLooseLayoutgroup.gameObject.SetActive(false);
        //_layoutGroup.gameObject.SetActive(true);
        win();

    }


    public void Guessed()
    { 
        _winLooseLayoutgroup.gameObject.SetActive(false);
        _layoutGroup.gameObject.SetActive(true);
        _PauseTimer = true;
    }

    //enter pass phase allowing players to pass phone between each other
    void pass()
    {

        for (int d = _layoutGroup.transform.childCount - 1; d >= 0; d--)
        {
            Destroy(_layoutGroup.transform.GetChild(d).gameObject);
        }

        
        

        _PauseTimer = true;

        

        

        if (_startOfGame == false)
        {
            if (currantPlayer == _PlayerNames.Count - 1)
            {
                currantPlayer = 0;
                _currantRound++;
            }
            else
                currantPlayer++;
        }
        _startOfGame = false;

        //add a elment to the points list for every player in the game except host
        for (int i = 0; i < playersNumber; i++)
        {
            if (i != currantPlayer)
            {
                Button newButton = Instantiate(PlayerButton, _layoutGroup.transform);
                newButton.GetComponent<ButtonClick>()._buttInt = i;
                newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "" + _PlayerNames[i].ToString();
                _buttons.Add(newButton);
            }
        }


        if (_currantRound >= _maxRound)
        {
            gameOver();
            return;
        }


        _passUi.SetActive(true);
        _guessUi.SetActive(false);
        _passText.SetText(_PlayerNames[currantPlayer].ToString() + "!");

    }

    public void themeBuffer()
    {
        _selectedTheme = Themes[Random.Range(0, Themes.Length)];
        _themeText.SetText(_selectedTheme);
        _themeBufferText.SetText(_selectedTheme);

        _layoutGroup.gameObject.SetActive(false);
        _passUi.SetActive(false);
        
        _themeBuffer.SetActive(true);
    }

    //progresses to next phase when player click redy button in pase phase
    public void ready()
    {
        _themeBuffer.SetActive(false);
        //_passUi.SetActive(false);
        _guessUi.SetActive(true);
        reRoll();
        _winLooseLayoutgroup.gameObject.SetActive(true);
        //_layoutGroup.gameObject.SetActive(false);
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
        for (int i = 0; i < _PlayerNames.Count; i++)
        {
            print(maxValue);
            if (_points[i] == maxValue)
            {
                print(i);
                winners += _PlayerNames[i].ToString() + " ";
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
            //Debug.Log("Player Names: " + string.Join(", ", _PlayerNames.ToArray()));
            //Debug.Log("Number of Players: " + playersNumber);
        }
        else
        {
            // Handle empty input
            //Debug.LogWarning("Please enter a valid player name");
        }

        // Clear the TMP Input Field after adding the name
        _NameInput.text = "";
        _PlayerCountTxt.text = playersNumber.ToString();
    }

    public void roundNumChange()
    {
        _roundNumText.SetText("Rounds: " + _roundNumSlider.value.ToString());
        _maxRound = (int) _roundNumSlider.value;
    }

    public void quit()
    {
        Application.Quit();
    }

    //UI controls
    public void MainMenu()
    {
        _mainMenuUI.gameObject.SetActive(true);
        _creditsUI.gameObject.SetActive(false);
        _guideUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

    }

    public void Credits()
    {
        _mainMenuUI.gameObject.SetActive(false);
        _creditsUI.gameObject.SetActive(true);
    }

    public void Guide()
    {
        _mainMenuUI.gameObject.SetActive(false);
        _guideUI.gameObject.SetActive(true);
    }
}
