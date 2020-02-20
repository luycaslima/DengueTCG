using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    
    public Transform positionToShowCard;
    public int maxCardOnHand = 7;


    public Vector3 rangedCardPosition;
    [System.NonSerialized]
    public Vector3 nextCard;

    private PlayerController player;
    private  Vector3 minPosition;
    private Vector3 maxPosition;

    

    public List<GameObject> cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        minPosition = transform.position - rangedCardPosition;
        maxPosition = transform.position + rangedCardPosition;
    }
    //Ligando essa mão ao player
    public void setPlayer(PlayerController playerToSet)
    {
        player = playerToSet;
    }

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

    private Vector3 CalculateDistanceHandPosition(int indice, int limit)
    {
       
        float distance = indice /(float)(limit);

        return Vector3.Lerp(minPosition, maxPosition, distance);

    }

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
        
    }
}
