using System.Collections;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public float timeShowPlayer;
    private float currentTimeShow;
    bool moveToHand;
    bool chegou;

    [Tooltip("Base da Carta")]
    public GameObject cardPrefab;   //Prefab do display da carta
    
    [Header("Posições da Carta")]
    public GameObject myHandsGrid;       //Posição da mão onde as Cartas ficam
    public RectTransform initialDeckPos; //Posição inicial onde a carta surge
    public RectTransform showCardPos;    //Posição que a carta mostra pro usuário

    [Header("Texto número de cartas")]
    public Text numberOfCardsText;
    private int numberOfCards = 0;

    public List<Card> deck = new List<Card>();

    private PlayerController player;

    private Vector3 targetPosition; //Armazena a posição que a carta vai no momento
    private Vector3 handPosition;   //Posição que recebe a posição da mão que a carta vai fica

    [SerializeField]
    private GameObject cardInstanciacao; //Instancia da carta criada

   
    
    //Criar classe altera o tamanho das cartas da ui e deixa eu clicar em cada uma delas e executa a ação dela
    //Essa mesma classe recebe o texto mostrando quanto de custo ainda possuo para usar as cartas e atualiza na tela
    public void PickUpCards(int pickedCards)
    {
        //Checa se o deck possui cards ainda
        if (deck.Any()) {

            
            chegou = false;
            //Se a ultima carta n foi chamada como parente ainda , é chamada aqui
            if (cardInstanciacao != null)
            {
                cardInstanciacao.GetComponent<cardDisplay>().parent = myHandsGrid;
                cardInstanciacao.transform.SetParent(myHandsGrid.transform);
            }
            
            //Pega uma carta
            for (int i = 0; i < pickedCards; i++)
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
            numberOfCards = numberOfCards - pickedCards;
            numberOfCardsText.text = numberOfCards.ToString();


        }
        
        
    }

    //ligando o player a esse deck
    public void DeckSetup(PlayerController playerToSet)
    {
        player = playerToSet;
    } 


    
    private void Start()
    {
        numberOfCards = deck.Count;
        numberOfCardsText.text = numberOfCards.ToString();
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
                
                //Otimizar essas calls , podem ser custosas no futuro
                cardInstanciacao.transform.SetParent(myHandsGrid.transform); //Ao terminar a animaçao , seta como parente
                cardInstanciacao.GetComponent<cardDisplay>().parent = myHandsGrid;
                cardInstanciacao.GetComponent<cardDisplay>().positionToGoBack = handPosition; //Da a posição atual pra carta ficar
            }


            if (targetPosition == showCardPos.position )
            {
                   cardInstanciacao.transform.position = Vector3.Lerp(cardInstanciacao.transform.position, targetPosition, 10 * Time.deltaTime);

            }
            else if (targetPosition == handPosition )
            {
                if(Vector3.Distance(cardInstanciacao.transform.position, targetPosition) > 0.1 && !chegou)
                {
                    cardInstanciacao.transform.position = Vector3.Lerp(cardInstanciacao.transform.position, targetPosition, 10 * Time.deltaTime);
                }
                else
                {
                    cardInstanciacao = null;
                    chegou = true;
                }

            }

        }




    }

}
