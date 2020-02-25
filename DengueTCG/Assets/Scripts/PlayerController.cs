using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Partes do Player:")]
    public Deck playerDeck;
    public PlayerHandController myHand;
    public RectTransform BotaoEncerraTurno;
    //public DiscardPile myDiscard;

    [Header("Status do Player:")]
    public Entidade status;

    [Header("Status Atual:")]
    public int currentHp;
    public int currentShield;
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
        currentCost = status.max_cost;

        totalCostText.text = status.max_cost.ToString();
        actualCostText.text = currentCost.ToString();
        hpText.text = currentHp.ToString();


        //myDiscard.DiscardSetup(this);
        playerDeck.DeckSetup(this);//setando que este baralho é meu   
        myHand.setPlayer(this);
    }

    //Ser comandos override da interfae Entidade
    
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
        if(currentCost + cost < status.max_cost)
        {
            currentCost = currentCost + cost;
        }
        else
        {
            currentCost = status.max_cost;
        }
    }

    //comandos lançados ao game system ou devolta usar o listener do bamghosts 
    public void Dead()
    {

    }

    public void EncerrarTurno()
    {
        //Resetar o valor dos custos aqui
        EsconderBotaoTurno();
        //podeEncerrarTurno = false;
    }

    private void EsconderBotaoTurno()
    {
        BotaoEncerraTurno.DOKill();
        BotaoEncerraTurno.DOAnchorPos(new Vector2(300f,0), 0.30f);
    }

    private void MostrarBotaoTurno()
    {
        BotaoEncerraTurno.DOAnchorPos( Vector2.zero, 0.30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!podeEncerrarTurno)
        {
            if (currentCost == 0 /*|| myHand.cards.Count == 0 Chechar se eu não puxei cartas antes pra n da positivo logo de primeira*/ )
            {

                MostrarBotaoTurno();
                podeEncerrarTurno = true;

            }
        }

        
    }
}
