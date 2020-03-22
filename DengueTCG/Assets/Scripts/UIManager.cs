using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// Classe que administra,chama e esconde as telas de menu do jogo e a tela de batalha
/// Possui funções para os botões de cada tela
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 25/02/2020 
/// </summary>

public class UIManager : MonoBehaviour
{
    [Header("Janelas do Jogo:")]
    public RectTransform PauseMenu;
    public RectTransform OptionMenu;
    public RectTransform MainMenu;
    public RectTransform BattleScene;

    [Header("Tela de Vitória:")]
    public RectTransform EndBattleScreen;

    public RectTransform prizeBattle;
    public Text EndBattleStatusText;
    public Text EndBattleWinDescriptionText;

    [Header("Fundo dos Menus:")]
    public GameObject BackGround;

    [Header("Referência ao Prefab da Carta:")]
    public GameObject cardDisplay;
    
    //Chama o menu de pausa
    public void PauseButton()
    {
        PauseMenu.gameObject.SetActive(true);
        PauseMenu.DOAnchorPos(Vector2.zero, .30f);
        BackGround.SetActive(true);
    }

    //Esconde o menu de pausa e volta ao jogo
    public void ResumeButton()
    {
        PauseMenu.DOAnchorPos(new Vector2(0,600f), .30f);
        BackGround.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
    }

    //Mostra o menu de fim de batalha
    private void ShowEndBattleStatus()
    {

        EndBattleScreen.gameObject.SetActive(true);
        EndBattleScreen.DOAnchorPos(Vector2.zero, .30f);
        BackGround.SetActive(true);
    }

    //Mostra a vitória no menu de resultado de batalha e os prêmios a serem escolhidos
    public void Victory(List<Card> prizes)
    {
        for (int i = 0; i < prizes.Count; i++)
        {
            CreatePrizes(prizes[i]);
        }

        EndBattleWinDescriptionText.gameObject.SetActive(true);
        prizeBattle.gameObject.SetActive(true);
        EndBattleStatusText.text = "VITÓRIA";
        
        ShowEndBattleStatus();
    }

    //Mostra a derrota no menu de resultado de batalha
    public void Defeat()
    {
        EndBattleStatusText.text = "DERROTA";
        ShowEndBattleStatus();

    }

    //Cria as cartas prêmio na tela para o usuário escolher
    public void CreatePrizes(Card prize)
    {
       
        GameObject realCard = Instantiate(cardDisplay);
        realCard.GetComponent<cardDisplay>().card = prize;
        realCard.GetComponent<cardDisplay>().isPrizeCard = true;

        realCard.transform.SetParent(prizeBattle);
        realCard.GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;

    }


    //Recomeça o jogo
    public void RestartGame()
    {
        //Para debug, criar um metódo melhor para recomeçar a fase ou batalha
        SceneManager.LoadScene("GameScene");
    }

}
