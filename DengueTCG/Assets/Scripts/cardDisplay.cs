using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class cardDisplay : MonoBehaviour
{
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

    [System.NonSerialized]
    public Vector3 positionToGoBack;
    private bool voltarAoLugar;


    private Transform parent;

    //Pesquisar como receber os textos do textpro
    // Start is called before the first frame update

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


    public void onHold()
    {
       if (showCard)
        {
            sizeCard.DOMove(new Vector3(0, .6f), .30f);
            //sizeCard.position = new Vector3(0, .6f);
            sizeCard.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
    }

    public void onExit()
    {
        if (showCard)
        {
            sizeCard.DOMove(originalPos.position, .30f);
            //sizeCard.position = originalPos.position;
            sizeCard.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }
    
    
    public void OnDrag()
    {
        showCard = false;
        
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        sizeCard.position = transform.position;


        sizeCard.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      
    }
    public void OnEndDrag()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        Vector3 aux =  Camera.main.ScreenToWorldPoint(screenPoint);

        if (!RectTransformUtility.RectangleContainsScreenPoint(enemyTarget, aux))
        {
            showCard = true;
            transform.position = positionToGoBack;
        }
        else
        {
           //Destroir a carta aqui e executar a ação 
           //Buga no parent do Deck 
        }
        
       
        //se n tiver encostando no inimigo voltar para posiçao original, se encostar ,da dano e deletar o display e adicionar a carta na pilha de descarte
    }

    //Para Teste
    void Start()
    {
        var enemy = GameObject.Find("Enemy Image");
        enemyTarget = enemy.GetComponent<RectTransform>();

        originalPos = GetComponent<RectTransform>(); //Recebe a posição e tamanho inicial da carta
        LoadCard(card);
    }

    private void Update()
    {
        //Checa se recebeu o valor final da carta na mao para permitir aumentar a carta
        if (!showCard)
        {
            
            if(transform.parent.name.Equals("Player Hand"))
            {
                parent = transform.parent;
                showCard = true;
            }
        }






    }

}
