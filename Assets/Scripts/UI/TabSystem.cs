using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    public Tab[] tabs;
    private Tab activeTab;
    public Image content;

    private void Start()
    {
        activeTab = tabs[0];
        foreach (Tab t in tabs)
        {
            t.RegisterCallback(SwitchTab);
        }
    }

    private void SwitchTab(Tab t)
    {
        activeTab = t;
        content.color = activeTab.color;
    }
}
