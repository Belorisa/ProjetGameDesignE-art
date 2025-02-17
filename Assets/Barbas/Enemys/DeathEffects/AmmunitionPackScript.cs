using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionPackScript : MonoBehaviour
{
    private GameObject gears;
    
    public AudioClip deathSound;
    public float volume;
    
    public int ammunition;
    
    public SoundManager soundManager;

    public int nbrOfJumps;

    public float dropRange;

    public float parabolaHeight;

    public float speed;

    private Vector2 targetedPos;

    private Vector2 startPos;

    private Vector2 dir;
    
    private float progression;

    private bool _notMoving;

    private Vector3 _startScale;

    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        _startScale = transform.localScale;
        
        Destroy(gameObject, 30);
        
        //soundManager.PlaySound("SpawnSound");

        startPos = new Vector2(transform.position.x, transform.position.y);

        float i = Random.Range(0, 2 * Mathf.PI);

        float x = Mathf.Cos(i) * dropRange;
        float y = Mathf.Sin(i) * dropRange;
        
        targetedPos = new Vector2(x, y) + new Vector2(transform.position.x, transform.position.y);

        dir = targetedPos - new Vector2(transform.position.x, transform.position.y);
    }
    
    void Update()
    {
        progression += Time.deltaTime * speed;

        progression = Mathf.Clamp(progression, 0f, 1f);

        if (!_notMoving)
        {
            transform.position = new Vector3(MathParabola.Parabola(startPos, targetedPos, parabolaHeight, progression).x, 
                MathParabola.Parabola(startPos, targetedPos, parabolaHeight, progression).y, transform.position.z);
        }

        if (Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                targetedPos) <= 0.05f && nbrOfJumps > 0)
        {
            progression = 0;

            startPos = new Vector2(transform.position.x, transform.position.y);

            dropRange *= 0.1f;

            parabolaHeight *= 0.1f;
            
            targetedPos = new Vector2(transform.position.x, transform.position.y) + dir.normalized * dropRange;

            nbrOfJumps--;

            if (nbrOfJumps <= 0)
            {
                _notMoving = true;
            }
        }

        if (Vector2.Distance(transform.position, gears.GetComponent<Gears>().playerI.GetComponent<Player>().feets.transform.position) <
            gears.GetComponent<Gears>().playerI.GetComponent<Player>().magnetAmmPackRange && _notMoving)
        {
            float prog = 0.1f / Vector2.Distance(transform.position, gears.GetComponent<Gears>().playerI.GetComponent<Player>().feets.transform.position) * Time.deltaTime * 80;
            
            transform.position = Vector2.Lerp(transform.position, gears.GetComponent<Gears>().playerI.GetComponent<Player>().feets.transform.position, prog);

            transform.localScale = new Vector3(transform.localScale.x - prog, transform.localScale.y - prog, transform.localScale.z);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Add: " + ammunition);
            
            //soundManager.PlaySound("ReloadSound");
            col.gameObject.GetComponent<Player>().ammunition += ammunition;

            col.gameObject.GetComponent<Player>().ammunition = Mathf.Clamp(col.gameObject.GetComponent<Player>().ammunition, 0, 100);
            
            gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().ammunitionIndicator.GetComponent<AmmunitionIndicator>().UpdateAmmunition();

            AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);

            Destroy(gameObject);
        }
    }
}
