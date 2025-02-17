using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject loopPanel;

    public GameObject towerUiContainer;

    public GameObject pausePanel;

    public GameObject[] helpPanels;

    private int _currentIndex;

    public GameObject controlTurretPanel;

    public GameObject controlWalkPanel;

    private GameObject _activeControlPanel;
    
    public Canvas canvasMain;
    
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI hpText;

    public TextMeshProUGUI waveText;

    public TextMeshProUGUI waverTimer;

    public AmmunitionIndicator ammunitionIndicator;

    private bool _helpPanelShowed;

    private bool _controlShowed;

    private bool _controlTurret;

    void Awake()
    {
        Gears.gears.uIManager = gameObject;
    }
    
    void Start()
    {
        _activeControlPanel = controlWalkPanel;
    }
    
    void Update()
    {
        
    }

    public void ShowHelpMenu()
    {
        if (!_helpPanelShowed)
        {
            helpPanels[_currentIndex].SetActive(true);
        }
        else
        {
            helpPanels[_currentIndex].SetActive(false);
        }

        _helpPanelShowed = !_helpPanelShowed;
    }

    public void ReturnHelpPanel()
    {
        if (_currentIndex == 0)
        {
            ShowHelpMenu();
        }
        else
        {
            helpPanels[_currentIndex].SetActive(false);

            _currentIndex--;
        
            helpPanels[_currentIndex].SetActive(true);
        }
    }

    public void NextHelpPanel()
    {
        helpPanels[_currentIndex].SetActive(false);

        _currentIndex++;
        
        helpPanels[_currentIndex].SetActive(true);
    }

    public void ShowControl()
    {
        if (!_controlShowed)
        {
            _activeControlPanel.SetActive(true);
        }
        else
        {
            _activeControlPanel.SetActive(false);
        }

        _controlShowed = !_controlShowed;
    }

    public void SwapControlPanel()
    {
        if (_activeControlPanel == controlWalkPanel)
        {
            controlWalkPanel.SetActive(false);
            
            _activeControlPanel = controlTurretPanel;
            
            _activeControlPanel.SetActive(true);
        }
        else
        {
            controlTurretPanel.SetActive(false);
            
            _activeControlPanel = controlWalkPanel;
            
            _activeControlPanel.SetActive(true);
        }
    }
}
