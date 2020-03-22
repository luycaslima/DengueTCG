using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



/// <summary>
/// Classe que controla todas as ações que envolvem o player, sua mão e baralho(deck)
/// seus atributos consistem em mostrar os status atual do player na tela, administra a pilha de descarte e servir de meio de interação entre o baralho e a mão
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 02/02/2020
/// 
/// </summary>


public class PlayerController : MonoBehaviour
{
    [Header("Referência a Batalha:")]
    public BattleManager battle;

    [Header("Partes do Player:")]
    public Deck playerDeck;
    public PlayerHandController myHand;

    [Header("Status do Player:")]
    public Entidade status;

    [Header("Status Atual:")]
    public int currentHp;
    public int currentShield;
    public int currentMaxCost;
    public int currentCost;

    [Header("Referência dos Textos:")]
    public Text nameText;
    public Text hpText;
    public Text shieldText;
    public Text totalCostText;
    public Text actualCostText;

    [Header("Discard Pile:")]
    public List<Card> discardPile = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {

        currentHp = status.max_HP;
        currentShield = 0;
        currentMaxCost = status.max_Cost;
        currentCost = status.max_Cost;

        totalCostText.text = status.max_Cost.ToString();
        actualCostText.text = currentCost.ToString();
        hpText.text = currentHp.ToString();


        playerDeck.DeckSetup(this);//Configurando que este baralho é meu   
        myHand.setPlayer(this); //Configurando que está mao é minha
    }

    

    //Da outra olhada nisso
    public void useCost(int cost)
    {
        if(currentCost - cost >= 0)
        {
            currentCost = currentCost - cost;
        }
        else
        {
            currentCost = 0;
        }
        actualCostText.text = currentCost.ToString();
    }

    //Aumenta o Custo máximo temporariamente ao descartar carta
    public void GrowCost(int value)
    {
        currentMaxCost = status.max_Cost + value;
        currentCost = currentCost + value;
        UpdateCostText();
    }

    public void ResetCost()
    {
        currentMaxCost = status.max_Cost;
        currentCost = currentMaxCost;
        ResetCostText();
    }
    public void UpdateCostText()
    {
        totalCostText.text = currentMaxCost.ToString();
        actualCostText.text = currentCost.ToString();
    }

    public void ResetCostText()
    {
        totalCostText.text = currentMaxCost.ToString();
        actualCostText.text = currentMaxCost.ToString();
    }

    public void UpdateHpText()
    {
        hpText.text = currentHp.ToString();
    }

    public void UpdateShieldText()
    {
       shieldText.text = currentShield.ToString();
    }

 

    public void ResetDeck()
    {
        playerDeck.deck.AddRange(discardPile);
        //playerDeck.deck = discardPile;

        discardPile.Clear();
        playerDeck.UpdateNumberOfCardsDeck();
    }


    // Update is called once per frame
    void Update()
    {
    

        
    }
}
