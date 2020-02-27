using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
