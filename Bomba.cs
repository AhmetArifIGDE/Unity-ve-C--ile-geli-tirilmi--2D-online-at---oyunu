using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    float darbegucu;


    GameObject gameKontrol;
    GameObject oyuncu;
    AudioSource YokOlmaSesi;
    PhotonView pw;

    void Start()
    {
        darbegucu = 20;
        gameKontrol = GameObject.FindWithTag("GameKontrol");
        pw.GetComponent<PhotonView>();
        YokOlmaSesi.GetComponent<AudioSource>().Play();

    }
    [PunRPC]
    public void TagAktar(string gelentag)
    {
        oyuncu = GameObject.FindWithTag(gelentag);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("OrtadakiKutular"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("darbeal",RpcTarget.All,darbegucu);
            oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("KutuCarpma", transform.position, transform.rotation, 0, null);
                YokOlmaSesi.Play();
                PhotonNetwork.Destroy(gameObject);
            }



        }
        if (collision.gameObject.CompareTag("Oyuncu2_Kule") || collision.gameObject.CompareTag("Oyuncu2"))
        {

            gameKontrol.GetComponent<PhotonView>().RPC("DarbeVur", RpcTarget.All, 2, darbegucu);
            oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("KutuCarpma", transform.position, transform.rotation, 0, null);
                YokOlmaSesi.Play();
                PhotonNetwork.Destroy(gameObject);
            }


        }
        if (collision.gameObject.CompareTag("Oyuncu1_Kule") || collision.gameObject.CompareTag("Oyuncu1"))
        {

            gameKontrol.GetComponent<PhotonView>().RPC("DarbeVur", RpcTarget.All, 1, darbegucu);
            oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("KutuCarpma", transform.position, transform.rotation, 0, null);
                YokOlmaSesi.Play();
                PhotonNetwork.Destroy(gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Zemin"))
        {

            oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            if (pw.IsMine)
            {
                PhotonNetwork.Instantiate("KutuCarpma", transform.position, transform.rotation, 0, null);
                YokOlmaSesi.Play();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }




}
