using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    public GameObject top;
    public GameObject TopCikisnoktasi;
    public ParticleSystem TopAtisEfekt;
    public AudioSource TopAtmaSesi;
    float AtisYonu;
    [Header("Güç Barý")]
    Image PowerBar;
    float powerSayi;
    bool sonaGeldimi=false;

    Coroutine powerDongu;

    PhotonView pw;
    void Start()
    {
       
       pw = GetComponent<PhotonView>();
        if(pw.IsMine)
        {
            PowerBar = GameObject.FindWithTag("PowerBar").GetComponent<Image>();
            //GetComponent<Oyuncu>().enabled = true;
            if (PhotonNetwork.IsMasterClient)
            {
                //gameObject.tag = "Oyuncu1";
                transform.position = GameObject.FindWithTag("OlusacakNokta1").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta1").transform.rotation;
                AtisYonu = 2f;
            }
            else
            {
                //gameObject.tag = "Oyuncu2";
                transform.position = GameObject.FindWithTag("OlusacakNokta2").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta2").transform.rotation;
                AtisYonu = -2f;
            }
        }
        InvokeRepeating("OyunBasladimi",0,0.5f);
    }
    public void OyunBasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                powerDongu = StartCoroutine(PowerBarCalistir());
                CancelInvoke("OyunBasladimi");
                
            }
            else
            {
                StopAllCoroutines();
            }
            
        }
     
    }
    IEnumerator PowerBarCalistir()
    {
        sonaGeldimi = false;
        PowerBar.fillAmount = 0;
        while (true)
        {
            if (PowerBar.fillAmount < 1 && !sonaGeldimi)
            {
                powerSayi = 0.01f;
                PowerBar.fillAmount += powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
            else
            {
                sonaGeldimi = true;
                powerSayi = 0.01f;
                PowerBar.fillAmount -= powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
                if (PowerBar.fillAmount == 0)
                {
                    sonaGeldimi= false;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(pw.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate("Patlama", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation,0,null);
                Instantiate(TopAtisEfekt, TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation);
                TopAtmaSesi.Play();
                GameObject topobjem = PhotonNetwork.Instantiate("Bomba", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation,0,null);

                topobjem.GetComponent<PhotonView>().RPC("TagAktar",RpcTarget.All,gameObject.tag);
                Rigidbody2D rg = topobjem.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(AtisYonu, 0f) * (PowerBar.fillAmount * 13f), ForceMode2D.Impulse);
                StopCoroutine(powerDongu);
            }
        }
        
        
    }
    public void PowerOynasin()
    {

        powerDongu=StartCoroutine(PowerBarCalistir());
    }
}
