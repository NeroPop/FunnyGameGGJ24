using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    public GameObject UIToHide;
    public GameObject UIToShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UISwitchButton()
    {
        UIToHide.SetActive(false);
        UIToShow.SetActive(true);

    }
}
