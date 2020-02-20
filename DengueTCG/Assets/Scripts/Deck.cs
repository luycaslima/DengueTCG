using System.Collections;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public float timeShowPlayer;
    private float currentTimeShow;
    bool moveToHand;
    bool chegou;


    public GameObject cardPrefab;
    public GameObject myHandsGrid; //onde as Cartas ficam

    public RectTransform initialDeckPos;
    public RectTransform showCardPos;

    public List<Card> deck = new List<Card>();

    private PlayerController player;

    private Vector3 targetPosition;
    private Vector3 handPosition;

    private GameObject cardInstanciacao;

   
    
    //Criar classe altera o tamanho das cartas da ui e deixa eu clicar em cada uma delas e executa a ação dela
    //Essa mesma classe recebe o texto mostrando quanto de custo ainda possuo para usar as cartas e atualiza na tela
    public void PickUpCards(int numberOfCards)
    {
        //Checa se o deck possui cards ainda
        if (deck.Any()) {

            chegou = false;
            //Se a ultima carta n foi chamada como parente , é chamad aqui
            if (cardInstanciacao != null)
            {
                
                cardInstanciacao.transform.SetParent(myHandsGrid.transform);
            }
            
            for (int i = 0; i < numberOfCards; i++)
            {

                int rndCardIndex = Random.Range(0, deck.Count);
                Card pickedCard = deck[rndCardIndex];
                deck.RemoveAt(rndCardIndex);

                /*//Metodo usando o grid layout do UI
                var cardInstanciado = Instantiate(cardPrefab, initialPos) as GameObject;
                cardInstanciado.GetComponent<cardDisplay>().card = pickedCard;
                cardInstanciado.transform.SetParent(myHandsGrid.transform);
                */
                
                cardInstanciacao = Instantiate(cardPrefab, initialDeckPos);
                cardInstanciacao.transform.localPosition = Vector3.zero;

                cardInstanciacao.GetComponent<cardDisplay>().card = pickedCard;
                cardInstanciacao.GetComponent<cardDisplay>().showCard = false; //Permite passar o mouse sob a carta

                moveToHand = true;
                

                targetPosition = showCardPos.transform.position; //vai pra posição de show
                currentTimeShow = 0;

                player.myHand.AddCard(cardInstanciacao);
            }
            
        }
        
        
    }

    //ligando o player a esse deck
    public void DeckSetup(PlayerController playerToSet)
    {
        player = playerToSet;
    } 
    private void Start()
    {
        
    }

    private void Update()
    {
        
        if (moveToHand && cardInstanciacao != null)
        {
            currentTimeShow += Time.deltaTime;
            if(currentTimeShow >= timeShowPlayer)
            {
                handPosition = player.myHand.nextCard; //pega a próxima posição na mão
                targetPosition = handPosition;

                cardInstanciacao.transform.SetParent(myHandsGrid.transform); //Ao terminar a animaçao , seta como parente
                cardInstanciacao.GetComponent<cardDisplay>().positionToGoBack = handPosition; //Da a posição atual pra carta ficar
            }

            //cardInstanciacao.transform.position = Vector3.Lerp(cardInstanciacao.transform.position, targetPosition, 10 * Time.deltaTime);
            

            if (targetPosition == showCardPos.position)
            {
                   cardInstanciacao.transform.position = Vector3.Lerp(cardInstanciacao.transform.position, targetPosition, 10 * Time.deltaTime);

            }else if (targetPosition == handPosition)
            {
                if(Vector3.Distance(cardInstanciacao.transform.position, targetPosition) > 0.1 && !chegou)
                {
                    cardInstanciacao.transform.position = Vector3.Lerp(cardInstanciacao.transform.position, targetPosition, 10 * Time.deltaTime);
                }
                else{
                    chegou = true;
                }

            }


        }


        if (Input.GetButtonDown("Jump"))
        {
            PickUpCards(1);

        }
    }

}
