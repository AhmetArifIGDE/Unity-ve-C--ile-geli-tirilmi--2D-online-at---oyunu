using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anamenukontrol : MonoBehaviour
{
    public GameObject ilkpanel;
    public GameObject ikincipanel;
    public InputField KullaniciAd;
    public Text VarolanKullaniciAdi;
    void Start()
    {
        if (!PlayerPrefs.HasKey("KullaniciAdi"))
        {
            ilkpanel.SetActive(true);
        }
        else
        {
            ikincipanel.SetActive(true);
            VarolanKullaniciAdi.text = PlayerPrefs.GetString("KullaniciAdi");
        }
    }

    public void kullaniciAdiKaydet()
    {
        PlayerPrefs.SetString("KullaniciAdi", KullaniciAd.text);
        ilkpanel.SetActive(false);
        ikincipanel.SetActive(true);
        VarolanKullaniciAdi.text = KullaniciAd.text;
    }
}
