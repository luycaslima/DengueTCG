using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe que administra a "mão" do player, se conecta a classe PlayerController 
/// Aqui é regorganizado as cartas na mão, chamada a função que escolhe a carta, descarta a carta e puxa uma carta
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 25/02/2020
/// </summary>

public class PlayerHandController : MonoBehaviour
{
    
    public Transform positionToShowCard;
    public int maxCardOnHand = 7;

    public int discardLimit = 1;
    private int actualDiscard = 0;
    private bool podeDescartar;

    public Vector3 rangedCardPosition; // Distancia entre as cartas
    [System.NonSerialized]
    public Vector3 nextCard;

    private PlayerController player;
    private Vector3 minPosition; //ponto mais a esquerda limite
    private Vector3 maxPosition; //ponto mais a direita limite

    //Cartas na mão
    public List<GameObject> cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        minPosition = transform.position - rangedCardPosition;
        maxPosition = transform.position + rangedCardPosition;
        actualDiscard = 0;
        podeDescartar = false;
    }

    //Ligando essa mão ao player
    public void setPlayer(PlayerController playerToSet)
    {
        player = playerToSet;
    }


    //Organiza as cartas na mão toda vez que puxa uma carta
    public void ReOrganizeCards()
    {
        Vector3 handPosition = transform.position;
        
        for (int i = 1 ; i < cards.Count; i++)
        {
            handPosition = CalculateDistanceHandPosition(i , cards.Count + 1);
            
            if (i - 1 < cards.Count)//se não for a ultima carta
            {
                cards[i - 1].transform.position = handPosition; //atualizar a posição atual da carta anterior
                cards[i - 1].GetComponent<cardDisplay>().positionToGoBack = handPosition; //atualizar a posição a carta tem que voltar ao soltar ela
            }
        }
        nextCard = CalculateDistanceHandPosition(cards.Count, cards.Count + 1); //da a posição nova para carta

    }


    //Recalcular as posições das cartas de acordo com as que sobraram
    public void ChoosedCard(GameObject choosedCard)
    {
        Card cardData = choosedCard.GetComponent<cardDisplay>().card;
        //Ve se pode usar senao volta pro lugar
        if(player.currentCost - cardData.cost >= 0)
        {
            player.battle.ApplyCardEffect(cardData); 

            cards.Remove(choosedCard);
            player.discardPile.Add(cardData);
            player.useCost(cardData.cost);
            Destroy(choosedCard);
        }
        else
        {
            choosedCard.transform.position = choosedCard.GetComponent<cardDisplay>().positionToGoBack;
        }
    }

    //descarta uma Carta da mão
    public void DiscardCard(GameObject choosedCard)
    {
        Card cardData = choosedCard.GetComponent<cardDisplay>().card;
        if(actualDiscard < discardLimit)
        {
            cards.Remove(choosedCard);
            player.discardPile.Add(cardData);


            player.GrowCost(cardData.cost);
    

            actualDiscard = actualDiscard + 1;
            podeDescartar = true;

            Destroy(choosedCard);
            //Fazer uma checagem se o valor limite foi atingindo para mudar a cor da pilha de descarte
        }
        else
        {
            choosedCard.transform.position = choosedCard.GetComponent<cardDisplay>().positionToGoBack;
        }
    }

    //Descarta a mão inteira
    public void DiscardHand()
    {
        Card cardData;
        int i = 0;
        while (i < cards.Count)
        {
            cardData = cards[i].GetComponent<cardDisplay>().card;
            player.discardPile.Add(cardData);
            Destroy(cards[i]);
            i++;
        }
        cards.Clear();
    }

    //Calcula a distancia que cada carta vai entre uma e outra
    private Vector3 CalculateDistanceHandPosition(int indice, int limit)
    {
        
        float distance = indice /(float)(limit);

        return Vector3.Lerp(minPosition, maxPosition, distance);

    }

    //Adiciona uma carta na mão
    public void AddCard(GameObject card)
    {
        if(cards.Count < maxCardOnHand)
        {
            cards.Add(card);
            ReOrganizeCards();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (podeDescartar)
        {
            if(actualDiscard == discardLimit)
            {

                //TODO ESCURECER A PILHA DE DESCARTE AQUI
                podeDescartar = false;
            }
        }
        
    }
}
