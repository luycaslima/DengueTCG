using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    //Mostra os dados da carta na UI
    public Card card;

    public TextMeshProUGUI title;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI description;
    public TextMeshProUGUI flavorText;
    public TextMeshProUGUI effects;

    public Image artwork;
    public Image background; //simbolo do efeito da carta

    //Pesquisar como receber os textos do textpro
    // Start is called before the first frame update

    public void LoadCard(Card c)
    {
        if (c == null)
            return;

        card = c;

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

    //Para Teste
    void Start()
    {
        LoadCard(card);
    }

}
