using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongineUI : MonoBehaviour
{
    [SerializeField]private PlayPanle Play;
    [SerializeField]private StarPanle StarPanle;
    [SerializeField]private RegistPanle RegistPanle;
    
    public void ShowStarPanel()
    {
        StarPanle.gameObject.SetActive(true);
    }

    public void ShowRegistPanel()
    {
        RegistPanle.gameObject.SetActive(true);
    }
}
