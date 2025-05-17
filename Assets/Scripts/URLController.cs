using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLController : MonoBehaviour
{
    public const string INSTA_URL = "https://www.instagram.com/asaweroman/";
    public const string EMAIL_URL = "Info@asawer.om";
    public const string MOBILE_URL = "tel:80009008";

    public void OpenInsta_URL()
    {
        Application.OpenURL(INSTA_URL);
    }

    public void SensEmail_URL()
    {
        string email = EMAIL_URL;
        string subject = MyEscapeURL("Asawer realstate");
        string body = MyEscapeURL("I am interested in your services and would like to get in touch with you.");

        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    public void CallPhoneNumber()
    {
        Application.OpenURL(MOBILE_URL);
    }

}
