using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Essa Clase, calcula o ponto minimo de pixels para o jogo entender que eu arrastei(drag) uma carta na tela do telefone
/// O objetivo dela é tornar menos sensivel a interpretação de arrastar carta quando dedo encosta
/// 
/// Data de criação: 21/02/2020
/// Autor: Lucas Lima da Silva Santos
/// 
/// Utilizando a solução daqui
///Fonte: http://ilkinulas.github.io/programming/unity/2016/03/18/unity_ui_drag_threshold.html
/// </summary>


public class DragTreshold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int defaultValue = EventSystem.current.pixelDragThreshold;
        EventSystem.current.pixelDragThreshold =
                Mathf.Max(
                     defaultValue,
                     (int)(defaultValue * Screen.dpi / 160f));
    }
}
