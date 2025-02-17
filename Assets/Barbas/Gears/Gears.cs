using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gears : MonoBehaviour
{
    public static Gears gears;
    
    public KeyCode up { get; set; }
    
    public Camera cam;
    
    public SoundManager SoundManager;

    public PlayerActionManager playerActionManager;

    public TileManager tileManager;

    public GameObject uIManager;

    public GameObject pathManager;

    public EventManager eventManager;

    public GameObject playerI;

    public LayerMask ennemisLayer;

    public LayerMask TowerLayer;

    void Awake()
    {
        if (gears == null)
        {
            DontDestroyOnLoad(gameObject);
            gears = this;
        }else if (gears != null)
        {
            Destroy(gameObject);
        }

        //up = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"));
        up = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Z");
    }
    
    void Start()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        if (cam != Camera.main)
        {
            cam = Camera.main;
        }
    }
    
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LvlSelectionScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
