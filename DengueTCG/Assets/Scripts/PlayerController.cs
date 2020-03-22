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

    //Usa a energia guardada para ativar uma carta
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

    //Aumenta a energia máximo temporariamente ao descartar carta
    public void GrowCost(int value)
    {
        currentMaxCost = status.max_Cost + value;
        currentCost = currentCost + value;
        UpdateCostText();
    }

    //Reseta o valor da energia pro máximo normal ao fim do turno
    public void ResetCost()
    {
        currentMaxCost = status.max_Cost;
        currentCost = currentMaxCost;
        ResetCostText();
    }
    //atualiza o texto na tela da energia do jogador
    public void UpdateCostText()
    {
        totalCostText.text = currentMaxCost.ToString();
        actualCostText.text = currentCost.ToString();
    }

    //Reseta os valores da energia devolta ao normal
    public void ResetCostText()
    {
        totalCostText.text = currentMaxCost.ToString();
        actualCostText.text = currentMaxCost.ToString();
    }

    //Atualiza os pontos de vida na tela do jogador
    public void UpdateHpText()
    {
        hpText.text = currentHp.ToString();
    }

    //Atualiza o texto do escudo na tela do jogador
    public void UpdateShieldText()
    {
       shieldText.text = currentShield.ToString();
    }

 
    //Pega todas as cartas da pilha de descarte e coloca no baralho de volta(deck)
    public void ResetDeck()
    {
        playerDeck.deck.AddRange(discardPile);

        discardPile.Clear();
        playerDeck.UpdateNumberOfCardsDeck();
    }


}
