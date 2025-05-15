using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLController : MonoBehaviour
{
    public const string TALEEL1_URL = "";
    public const string TALEEL2_URL = "";
    public const string ZAHYA1_URL = "";
    public const string ZAHYA2_URL = "";

    public void OpenTaleel1_URL()
    { 
        Application.OpenURL(TALEEL1_URL );
    }

    public void OpenTaleel2_URL()
    {
        Application.OpenURL(TALEEL2_URL);
    }

    public void OpenZahyA1_URL()
    {
        Application.OpenURL(ZAHYA1_URL);
    }

    public void OpenZahyA2_URL()
    {
        Application.OpenURL(ZAHYA2_URL);
    }
}
