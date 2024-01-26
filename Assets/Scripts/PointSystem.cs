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

    // Start is called before the first frame update
    void Start()
    {
        reRoll();

        timerBar.maxValue = maxTime;
       
        for (int i = 0; i < players; i++)
        {
            _points.Add(0);
            Button newButton = Instantiate(PlayerButton, _layoutGroup.transform);
            //newButton.onClick.AddListener(GameObject.Find("Game Manger").GetComponent<PointSystem>().win());
        }

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
        _timer = 0;
        _themeText.SetText(Themes[Random.Range(0, Themes.Length)]);

        currantPlayer++;
        if (currantPlayer > _points.Count)
        {
            currantPlayer = 0;
        }
    }

    public void win()
    {
        reRoll();
        _points[currantPlayer]++;
    }

    public void fail()
    {
        reRoll();
        print("You scuk!");
    }
}
