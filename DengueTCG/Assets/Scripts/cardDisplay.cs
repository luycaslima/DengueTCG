using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class cardDisplay : MonoBehaviour
{
    //O que é um modelo anemico e rico ? 

    [Header("Referencia do Texto e Arte:")]
    //Mostra os dados da carta na UI
    public Card card;

    public Text title;
    public Text cost;
    public Text description;
    public Text flavorText;
    public Text effects;

    /*Bom mas causa queda de perfomance
    public TextMeshProUGUI title;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI description;
    public TextMeshProUGUI flavorText;
    public TextMeshProUGUI effects;*/

    public Image artwork;
    public Image background; //simbolo do efeito da carta


    public RectTransform sizeCard; //Pega transform a ser alterado da carta
    private RectTransform originalPos; //armazena o estado inicial do transform para retornar ao normal
    public bool showCard;

    private RectTransform enemyTarget; //Recebe a posição da imagem do inimigo
    private GameObject discardPile;
    private RectTransform discardPilePos;

    [System.NonSerialized]
    public Vector3 positionToGoBack;
    private bool voltarAoLugar;

    [System.NonSerialized]
    public Transform parent;
    private int position = 0; //Posição que fica na fila objetos filhos

    public void LoadCard(Card c)
    {
        if (c == null)
            return;

        card = c;

        //title.text = c.title;
        title.text = c.title;
        cost.text = c.cost.ToString();
        description.text = c.description;

        //Checa se tem flavor text na string e desativa
        if (string.IsNullOrEmpty(c.flavorText))
        {
            //Aumentar a fonte da descrição aqui
            flavorText.gameObject.SetActive(false);
        }
        else
        {
            flavorText.gameObject.SetActive(true);
            flavorText.text = c.flavorText;
        }

        switch (c.type)
        {
            //Se o tipo da carta for especial, desativa o valor de efeitos
            case 2:
                background.gameObject.SetActive(false);
                break;
            default:
                effects.text = c.effect.ToString();
                //Seta o background pra defesa e ataque
                break;
        } 
    
        //artwork.sprite = c.artwork;
        //background.sprite = c.backgroundEffect[0];
    }


    //utilizava on pointer enter  e exit para aumentar e diminuir a carta(faz mais sentido no mouse)
    public void onHold()
    {
       if (showCard)
        {
            transform.SetAsLastSibling(); //Posiciona como ultimo filho da lista de cartas para renderizar por cima
            sizeCard.DOMove(new Vector3(0, .6f), .30f);
            //sizeCard.position = new Vector3(0, .6f);
            sizeCard.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
    }

    public void onExit()
    {
        if (showCard)
        {
            transform.SetSiblingIndex(position); //Posiciona como ultimo filho da lista de cartas para renderizar por cima

            
            sizeCard.DOMove(originalPos.position, .30f);
            //sizeCard.position = originalPos.position;
            sizeCard.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }
    
    
    public void OnDrag()
    {
        transform.SetAsLastSibling(); //Posiciona como ultimo filho da lista de cartas para renderizar por cima

        showCard = false;
        
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        sizeCard.position = transform.position;
        
        sizeCard.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      
    }

    //se n tiver encostando no inimigo voltar para posiçao original, se encostar ,da dano e deletar o display e adicionar a carta na pilha de descarte
    public void OnEndDrag()
    {
        if(position == transform.childCount)
        {
            transform.SetAsLastSibling();
        }
        else
        {
            transform.SetSiblingIndex(position);
        }

        //Cálculo para pegar o ponto do mouse na tela e adaptar ao espaço na camera
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        Vector3 aux =  Camera.main.ScreenToWorldPoint(screenPoint);

        //Checa se a carta não está emcima do inimigo
        if (!RectTransformUtility.RectangleContainsScreenPoint(enemyTarget, aux))
        {
            //Se tiver emcima da pilha de descarte
            if (RectTransformUtility.RectangleContainsScreenPoint(discardPilePos, aux))
            {

            }
            else//Senao volta ao lugar original
            {
                showCard = true;
                transform.position = positionToGoBack;
            }
        }
        else
        {
           
           //"Destruir o display" e  armazenar a carta na pilha aqui e executar a ação 
           //Buga no parent do Deck 
        }
        
    }

    //Para Teste
    void Start()
    {
        var enemy = GameObject.Find("Enemy Image");
        enemyTarget = enemy.GetComponent<RectTransform>();

        discardPile = GameObject.Find("Discard Pile");
        discardPilePos = discardPile.GetComponent<RectTransform>();

        originalPos = GetComponent<RectTransform>(); //Recebe a posição e tamanho inicial da carta
        LoadCard(card);
    }

    private void Update()
    {
        //Checa se recebeu o valor final da carta na mao para permitir aumentar a carta
        if (!showCard)
        {
            
            if(transform.parent.name.Equals("Player Hand") && Vector3.Distance(transform.position, positionToGoBack) < 0.1f)
            {
                //parent = transform.parent; //setando isso no DECK
                position = transform.GetSiblingIndex();
                showCard = true;
            }
        }






    }

}
