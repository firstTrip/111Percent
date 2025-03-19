using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    Canvas uiCanvas;

    [SerializeField]
    MagicPanel magicPanel;

    [SerializeField]
    TabManager tabManager;

    public Canvas GetUICanvas()
    {
       return uiCanvas;
    }

    public MagicPanel GetMagicPanel()
    {
        return magicPanel;
    }

    public TabManager GetTabManager()
    {
        return tabManager;
    }
}
