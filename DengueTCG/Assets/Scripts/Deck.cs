using System.Collections;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Deck : MonoBehaviour
{
    
    public List<Card> deck = new List<Card>();

    public GameObject cardPrefab;
    public GameObject myHandsGrid; //Lugar onde vai instanciar as cartas 
    private PlayerController player;

    

    //Criar classe altera o tamanho das cartas da ui e deixa eu clicar em cada uma delas e executa a ação dela
    //Essa mesma classe recebe o texto mostrando quanto de custo ainda possuo para usar as cartas e atualiza na tela
    public void PickUpCards(int numberOfCards)
    {
        //Checa se o deck possui cards ainda
        if (deck.Any()) { 
            for (int i = 0; i < numberOfCards; i++)
            {
                int rndCardIndex = Random.Range(0, deck.Count);
                Card pickedCard = deck[rndCardIndex];
                deck.RemoveAt(rndCardIndex);

                var cardInstanciado = Instantiate(cardPrefab, myHandsGrid.transform);
                //cardInstanciado.transform.SetParent(myHandsGrid.transform);
                cardInstanciado.GetComponent<cardDisplay>().card = pickedCard;
                //cardInstanciado.GetComponent<RectTransform>().DOAnchorPos(destinyPos.position, .30f);
            }
            
        }
        //Pega um número de cartas e instancia na mão do player
        
    }
    public void DeckSetup(PlayerController playerToSet)
    {
        player = playerToSet;
    } 
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PickUpCards(1);
        }
    }

}
