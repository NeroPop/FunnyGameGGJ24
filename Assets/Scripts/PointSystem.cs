using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PointSystem : MonoBehaviour
{
    [SerializeField] private int players;
    [SerializeField] private List<int> _points = new List<int>();
    [SerializeField] private int _scoreTOAdd;
    [SerializeField] private float _timer;
    [SerializeField] private float maxTime;


    [SerializeField] private int currantPlayer;

    [SerializeField] private Slider timerBar;

    [SerializeField] private string[] Themes;

    [SerializeField] private TMP_Text _themeText;

    [SerializeField] private VerticalLayoutGroup _layoutGroup;

    [SerializeField] private VerticalLayoutGroup _winLooseLayoutgroup;

    [SerializeField] private Button PlayerButton;

    [SerializeField] private List<Button> _buttons = new List<Button>();

    [SerializeField] private int _currantRound;
    [SerializeField] private int _maxRound;

    [SerializeField] private GameObject _guessUi;
    [SerializeField] private GameObject _passUi;
    [SerializeField] private TMP_Text _passText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] protected TMP_Text _winnerText;

    // Start is called before the first frame update
    void Start()
    {
        timerBar.maxValue = maxTime;
       
        for (int i = 0; i < players; i++)
        {
            _points.Add(0);
            Button newButton = Instantiate(PlayerButton, _layoutGroup.transform);
            newButton.GetComponent<ButtonClick>()._buttInt = i;
            newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "" + i;
            _buttons.Add(newButton);
        }
        pass();
        //_layoutGroup.cellSize = new Vector2 (_layoutGroup.flexibleWidth, 90);

        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        _timer += 1 * Time.deltaTime;
        timerBar.value = _timer;

        if (_timer > maxTime)
        {
            _timer = 0;
            pass();
        }

        if (_currantRound >= _maxRound)
        {
            gameOver();
        }

    }

    
    
    void reRoll()
    {
        if (_currantRound >= _maxRound) 
            return;
 

        _timer = 0;
        _themeText.SetText(Themes[Random.Range(0, Themes.Length)]);



        
        _buttons[currantPlayer].GetComponent<Image>().color = Color.green;
    }

    public void win()
    {
        pass();
        _points[currantPlayer]++;
        
    }

    public void fail()
    {
        reRoll();
    }

    public void laugh()
    {
        _winLooseLayoutgroup.gameObject.SetActive(false);
        _layoutGroup.gameObject.SetActive(true);
    }

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

    public void ready()
    {
        _passUi.SetActive(false);
        _guessUi.SetActive(true);
        reRoll();
        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);
    }

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

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
