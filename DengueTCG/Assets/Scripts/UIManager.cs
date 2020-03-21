using System.Collections;
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
    public RectTransform PauseMenu, OptionMenu, MainMenu,BattleScene;
    public GameObject BackGround;
    

    public void PauseButton()
    {
        PauseMenu.DOAnchorPos(Vector2.zero, .30f);
        BackGround.SetActive(true);
    }

    public void ResumeButton()
    {
        PauseMenu.DOAnchorPos(new Vector2(0,600f), .30f);
        BackGround.SetActive(false);
    }

}
