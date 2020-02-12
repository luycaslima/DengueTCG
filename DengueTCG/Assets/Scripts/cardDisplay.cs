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
    public TextMeshProUGUI effects;

    public Image artwork;
    public Image background; //simbolo do efeito da carta

    //Pesquisar como receber os textos do textpro
    // Start is called before the first frame update
    void Start()
    {
       
        title.text = card.title;
        cost.text = card.cost.ToString();
        description.text = card.description;
        effects.text = card.effect.ToString();

        //artwork.sprite = card.artwork;
        //background.sprite = card.backgroundEffect;
    }

}
