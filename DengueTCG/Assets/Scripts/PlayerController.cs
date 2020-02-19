using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Deck playerDeck;
    public GameObject hand;
    private int max_HP;
    public int HP = 30;

    // Start is called before the first frame update
    void Start()
    {
        playerDeck.DeckSetup(this);//setando que este baralho é meu   
    }

    //comandos lançados ao game system ou devolta usar o listener do bamghosts 
    public void Damage(int damage)
    {

    }


    public void Dead()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
