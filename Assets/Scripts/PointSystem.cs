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

    [SerializeField] private GridLayoutGroup _layoutGroup;

    [SerializeField] private Button PlayerButton;

    [SerializeField] private List<Button> _buttons = new List<Button>();

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
    }

    
    void reRoll()
    {
        
        _buttons[currantPlayer].GetComponent<Image>().color = Color.white;
        _timer = 0;
        _themeText.SetText(Themes[Random.Range(0, Themes.Length)]);



        if (currantPlayer == _points.Count - 1)
        {
            currantPlayer = 0;
        }
        else
            currantPlayer++;
        _buttons[currantPlayer].GetComponent<Image>().color = Color.green;
    }

    public void win()
    {
        reRoll();
        _points[currantPlayer]++;
    }

    public void fail()
    {
        reRoll();
    }
}
