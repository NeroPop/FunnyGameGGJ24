using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
        reRoll();
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
            reRoll();
        }

        if (_currantRound >= _maxRound)
        {
            print("Game End");
        }
    }

    
    void reRoll()
    {
        if (_currantRound >= _maxRound)
            return;

        _buttons[currantPlayer].GetComponent<Image>().color = Color.white;
        _timer = 0;
        _themeText.SetText(Themes[Random.Range(0, Themes.Length)]);



        if (currantPlayer == _points.Count - 1)
        {
            currantPlayer = 0;
            _currantRound++;
        }
        else
            currantPlayer++;
        _buttons[currantPlayer].GetComponent<Image>().color = Color.green;
    }

    public void win()
    {
        reRoll();
        _points[currantPlayer]++;
        _winLooseLayoutgroup.gameObject.SetActive(true);
        _layoutGroup.gameObject.SetActive(false);
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
}
