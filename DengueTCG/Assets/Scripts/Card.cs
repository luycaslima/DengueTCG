using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card" , menuName = "Card")]
public class Card : ScriptableObject {
    
    //Cria um objeto que armazena os dados da carta
    public new string title;
    public int cost;
    public Sprite artwork;
    [TextArea]
    public string description;
    [TextArea]
    public string flavorText;
    [Range(0,2)]
    public int type; //tipo da carta 0 para ataque , 1 para defesa , 2 para situacional

    public Sprite backgroundEffect; //O simbolo atras do valor de efeito muda de arcodor com tipo de carta?(ataque, defesa)
    public int effect;


}
