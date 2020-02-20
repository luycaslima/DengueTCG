using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Deck playerDeck;
    public PlayerHandController myHand;
 

    public Text nameText;
    public Text hpText;
    public Text numberOfCards;
    public Text actualCostText;
    public Text totalCostText;

    public int max_HP = 15;
    private int hp;
    public int shield = 0;
    public int costs = 3;

    // Start is called before the first frame update
    void Start()
    {
        max_HP = hp;
        playerDeck.DeckSetup(this);//setando que este baralho é meu   
        myHand.setPlayer(this);
    }

    
    public void Damage(int damage)
    {
        if(shield > 0)
        {
            shield = shield - damage;
        }
        else
        {
            hp = hp - damage;
        }

    }

    public void ShieldUp(int shieldValor)
    {
        shield = shield + shieldValor;
    }

    public void useCost(int cost)
    {
        costs = costs - cost;
    }

    public void RecoverCost(int cost)
    {
        costs = costs + cost;
    }

    //comandos lançados ao game system ou devolta usar o listener do bamghosts 
    public void Dead()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
