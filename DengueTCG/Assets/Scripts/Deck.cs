using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    Stack<Card> baralhoPilha = new Stack<Card>();
    public List<Card> deck = new List<Card>();

    public Card[] baralho;
    private Card tempGO;

    public GameObject myHandsGrid; //Lugar onde vai instanciar as cartas 

    //Adaptar para uma lista
    public void Shuffle()
    {
        for (int i = 0; i < baralho.Length; i++)
        {
            int rnd = Random.Range(0, baralho.Length);
            tempGO = baralho[rnd];
            baralho[rnd] = baralho[i];
            baralho[i] = tempGO;
        }
    }

    //Criar classe altera o tamanho das cartas da ui e deixa eu clicar em cada uma delas e executa a ação dela
    //Essa mesma classe recebe o texto mostrando quanto de custo ainda possuo para usar as cartas e atualiza na tela
    public void PickUpCards(int numberOfCards)
    {
        //Pega um número de cartas e instancia na mão do player
        
    }

}
