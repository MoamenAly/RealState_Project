using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] CanvasGroup ui;

    public void _ShowHideUI()
    {
        if (ui != null)
        {
            if (ui.alpha == 0)
            {
                ui.alpha = 1;
            }
            else
            {
                ui.alpha = 0;
            }
        }
    }
}
