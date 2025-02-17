using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemis : DebuffStates
{
    public EnnemiStats myStats;

    public float percentChanceToDropAmmunition;

    public int nbrAmmunitionToDrop;

    public GameObject ammunitionPack;

    public List<DeathEffect> deathEffects = new List<DeathEffect>();

    new void Start()
    {
        StartForEvryOne();
    }
    
    new void Update()
    {
        if (myStats.hp <= 0)
        {
           Death();
        }
        
        currentState.Tick();
        
        for (int i = debuffs.Count - 1; i >= 0; i--)
        {
            debuffs[i].Tick();
        }
    }

    public void StartForEvryOne()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        ChanceToDropAmmunition(percentChanceToDropAmmunition);

        SetState(new EnemiState(this));

        for (int i = 0; i < Gears.gears.pathManager.GetComponent<WaveManagerB>().nbrOfLoops; i++)
        {
            //Debug.Log("increase enemy hp");
            
            myStats.hp *= Gears.gears.pathManager.GetComponent<WaveManagerB>().scalingPerLoops;
        }
    }

    public virtual void ChanceToDropAmmunition(float chance)
    {
        if (Random.Range(1, 100) < chance + gears.GetComponent<Gears>().eventManager.bonusChanceToDropAmmunitionPack)
        { 
            //Debug.Log("Base chance to drop ammunition");
            
           deathEffects.Add(new DropAmmunitionDeathRattle(this, nbrAmmunitionToDrop, ammunitionPack));

           gears.GetComponent<Gears>().eventManager.bonusChanceToDropAmmunitionPack = 0;
        }
        else
        {
            gears.GetComponent<Gears>().eventManager.bonusChanceToDropAmmunitionPack++; //+ 0.5f?
        }
    }

    public void TakeDamage(int dmg) //changer int en une class bulletstats? pour + d'info genre le type de dégat
    {
        myStats.hp -= dmg;
    }

    public void Death()
    {
        if (deathEffects.Count > 0)
        {
            foreach (var effect in deathEffects)
            {
                effect.DeathRattle();
            }
        }

        gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds += myStats.goldValue;
        Destroy(gameObject);
    }
}

[System.Serializable]
public class EnnemiStats
{
    public float hp;

    public float speed;
    
    public int hpValue;
    
    public int goldValue;

    //public int resistance;
}
