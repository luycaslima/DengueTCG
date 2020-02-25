using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Fonte: http://ilkinulas.github.io/programming/unity/2016/03/18/unity_ui_drag_threshold.html

public class DragTreshold : MonoBehaviour
{
    //Calcula o ponto minimo de pixels para interpretar que eu cometi um drag no objeto 
    void Start()
    {
        int defaultValue = EventSystem.current.pixelDragThreshold;
        EventSystem.current.pixelDragThreshold =
                Mathf.Max(
                     defaultValue,
                     (int)(defaultValue * Screen.dpi / 160f));
    }
}
