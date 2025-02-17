using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerActionManager : StateMachine
{
    public GameObject[] towers;

    public SkillTree mySkillTree;
    public GameObject passivTreePanel;
    private bool passivTreeShowed;

    public SoundManager mySounds;

    public float golds;

    public int hp;

    public Tile bridgeTile;
    public Tile bridgeTileVertical;
    public Tile bridgeTileHorizontal;
    
    public GameObject emptySprite;

    public GameObject notEnoughGolds;

    public GameObject ghostRange;

    public float rangeBeetweenTowers;

    public bool isPaused;

    private GameObject wave;

    private float timer = 10;
    private bool timerRunning;

    public GameObject gameOverPanel;

    private bool turretMode = false;

    public Sprite turret;
    public Sprite barreTurret;
    
    public Sprite construction;
    public Sprite barreConstruction;

    public bool mouseOnUiElement;

    public int bridgesLeft;

    public AudioClip switchSound;
    public AudioClip posePont;

    public bool x2ButtonPressed = false;

    [TextArea(14,10)] public string tuto;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        gears.GetComponent<Gears>().playerActionManager = this;
        
        Pause();

        notEnoughGolds.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);

        wave = GameObject.Find("Path");
    }

    new void Update()
    {
        currentState?.Tick();

        if (gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().goldText != null)
        {
            UpdateText(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().goldText, ""//"Gold : "
                , (int) golds); //pas mettre dans update +opti
        }

        if (gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().hpText != null)
        {
            UpdateText(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().hpText, ""//"Hp : "
                , hp);
        }


        if (Input.GetButton("Fire2"))
        {
            SetState(null);
        }


        if (Input.GetButtonDown("P"))
        {
            ShowHidePassivSkillTree();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        
        Wave();

    }

    public void GetGold()
    {
        golds += 100;
    }

    public void PlaceTower(int i)
    {
        if (!isPaused && gears.GetComponent<Gears>().playerI.GetComponent<Player>().currentState?.GetType().Name != typeof(PlayerBuildingState).Name)
        {
            SetState(new PlaceTowerState(this, towers[i]));
        }
    }

    public void EnterSellTowerMod()
    {
        if (!isPaused)
        {
            SetState(new SellTowerState(this));
        }
    }

    public void EnterPlaceBridgeMode()
    {
        if (!isPaused && bridgesLeft > 0)
        {
            SetState(new PlaceBridgeState(this));
        }
    }

    public void UpdateText(TextMeshProUGUI toUpdate, string s, int i) //update les texts : golds, hp
    {
        toUpdate.text = s + i;
    }

    public void Pause()
    {
        if (!isPaused)
        {
            SetState(null);
            Time.timeScale = 0f;
            Gears.gears.uIManager.GetComponent<UIManager>().pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Gears.gears.uIManager.GetComponent<UIManager>().pausePanel.SetActive(false);
        }

        isPaused = !isPaused;
    }


    public void Wave()
    {
        
        
        if (wave.GetComponent<WaveManagerB>().waveDone)
        {
            timerRunning = true;
            gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().waverTimer.text =
                " Prochaine Vague : \n " + timer.ToString("F0") + " seconde";
            timer -= Time.deltaTime;
            if (timer < 0 && timerRunning)
            {
                timerRunning = false;
                timer = 10;
                wave.GetComponent<WaveManagerB>().waveDone = false;

                gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().waverTimer.text =  " Prochaine Vague : \n En Cours";
                   

                if (wave.GetComponent<WaveManagerB>().firstWaveDone)
                {
                    wave.GetComponent<WaveManagerB>().currentWave++;
                }

                gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().isRunning = true;
            }
        }
        
        if (Input.GetButtonDown("Submit") || x2ButtonPressed  &&
            gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().isRunning == false && timerRunning == false) //lancer la vague
        {
            if (wave.GetComponent<WaveManagerB>().firstWaveDone)
            {
                wave.GetComponent<WaveManagerB>().currentWave++;
            }

            gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().waverTimer.text = " Prochaine Vague : \n En Cours";
            gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().isRunning = true;
        }
        
        UpdateText(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().waveText, "Vague N° ",
            wave.GetComponent<WaveManagerB>().currentWave + 1);
    }
    public void ShowHidePassivSkillTree()
    {
        if (!passivTreeShowed)
        {
            passivTreePanel?.SetActive(true);

            mouseOnUiElement = true;
        }
        else
        {
            passivTreePanel?.SetActive(false);
            
            mouseOnUiElement = false;
        }

        passivTreeShowed = !passivTreeShowed;
    }

    public IEnumerator ShowNotEnoughGoldsWorld(float time)
    {
        notEnoughGolds.SetActive(true);
        notEnoughGolds.transform.position = Input.mousePosition + new Vector3(30,30,0);
        
        yield return new WaitForSeconds(time);
        
        notEnoughGolds.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Test");
    }

    public void InterfaceSwitch()
    {
        if (turretMode)
        {
            turretMode = false;
            GameObject.Find("Turret Mode Button").GetComponent<Image>().sprite = construction;
            GameObject.Find("Image").GetComponent<Image>().sprite = barreConstruction;
            AudioSource.PlayClipAtPoint(switchSound, new Vector3(transform.position.x,transform.position.y,-8), 1.0f);

        }
        else
        {
            turretMode = true;
            GameObject.Find("Turret Mode Button").GetComponent<Image>().sprite = turret;
            GameObject.Find("Image").GetComponent<Image>().sprite = barreTurret;
            AudioSource.PlayClipAtPoint(switchSound, new Vector3(transform.position.x,transform.position.y,-8), 1.0f);

        }
    }
    
    void OnGUI()
    {
        /*if (isPaused)
        {
            // Si on clique sur le bouton alors isPaused devient faux donc le jeu reprend
            if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.4f, Screen.width * 0.15f, Screen.height * 0.1f), "Demarrer/Reprendre"))
            {
                isPaused = false;
            }

            if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.55f, Screen.width * 0.15f, Screen.height * 0.1f), "Relancer"))
            {
               ReloadLevel(); // Charge le menu principal
            }

            if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.7f, Screen.width * 0.15f, Screen.height * 0.1f), "Quitter"))
            {
                Application.Quit(); // Ferme le jeu
            }

            GUI.TextArea(new Rect(Screen.width * 0.05f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.6f), tuto);
        }*/
    }
}