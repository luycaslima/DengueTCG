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
    private bool podeEncerrarTurno;

    [Header("Referência dos Textos:")]
    public Text nameText;
    public Text hpText;
    public Text totalCostText;
    public Text actualCostText;

    [Header("Discard Pile:")]
    public List<Card> discardPile = new List<Card>();


    //Colocar o Inimigo aqui?

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

    public void Damage(int damage)
    {
        //Configurar se passar do escudo dar dano no player tbm
        if(currentShield > 0)
        {
            currentShield = currentShield - damage;
        }
        else
        {
            currentHp = currentHp - damage;
        }

    }

    public void ShieldUp(int shieldValor)
    {
        if(shieldValor  + currentShield < status.max_Shield)
        {
            currentShield = currentShield + shieldValor;
        }
        else
        {
            currentShield = status.max_Shield;
        }
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

    public void RecoverCost(int cost)
    {
        if(currentCost + cost < status.max_Cost)
        {
            currentCost = currentCost + cost;
        }
        else
        {
            currentCost = status.max_Cost;
        }
    }

    //Aumenta o Custo máximo temporariamente ao descartar carta
    public void GrowCost(int value)
    {
        currentMaxCost = status.max_Cost + value;
        currentCost = currentCost + value;
        UpdateText();
    }

    public void ResetCost()
    {
        currentMaxCost = status.max_Cost;
        currentCost = currentMaxCost;
        UpdateText();
    }

    public void UpdateText()
    {
        totalCostText.text = currentMaxCost.ToString();
        actualCostText.text = currentMaxCost.ToString();
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
