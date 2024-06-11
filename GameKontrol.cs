using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameKontrol : MonoBehaviour
{
    PhotonView pw;
    [Header("OYUNCU AYARLARI VE")]
    public Image Oyuncu1_SaglikBar;
    public Image Oyuncu2_SaglikBar;
    float oyuncu1_saglik=100;
    float oyuncu2_saglik=100;
    void Start()
    {
        pw.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    [PunRPC]
    public void DarbeVur(int kriter, float darbegucu)
    {

        switch (kriter)
        {

            case 1:
                oyuncu1_saglik -= darbegucu;
                Oyuncu1_SaglikBar.fillAmount = oyuncu1_saglik / 100;
                if (oyuncu1_saglik <= 0)
                {
                    Debug.Log("1. oyuncu yenildi");
                }
                break;
            case 2:
                oyuncu2_saglik -= darbegucu;
                Oyuncu2_SaglikBar.fillAmount = oyuncu2_saglik / 100;
                if (oyuncu2_saglik <= 0)
                {
                    Debug.Log("2. oyuncu yenildi");
                }
                break;

        }

    }
}
