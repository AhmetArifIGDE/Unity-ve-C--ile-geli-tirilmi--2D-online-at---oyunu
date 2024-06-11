using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SunucuYonetim : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Servera Baðlandý");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    
    {

        Debug.Log("Lobiye Baðlandý");
    }
    public void RandomGiris()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();
    }
    public void OdaKur()
    {
        PhotonNetwork.LoadLevel(1);
        string odaadi= Random.Range(0,9614).ToString();
        PhotonNetwork.JoinOrCreateRoom(odaadi, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu",Vector3.zero,Quaternion.identity,0,null);
        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("KullaniciAdi");
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            objem.gameObject.tag = "Oyuncu2";
        }
        
    }
    public override void OnLeftRoom()
    {
        //odadan 

    }
    public override void OnLeftLobby()
    {
        //lobiden
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //bir oyuncu girdiyse
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //bir oyuncu çýktýysa 
        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
       //odaya girilemedi ise
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    //random odaya girilemediyse
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //oda oluþturulamadýysa
    }
    public void BilgileriKontrolEt()
    {
        if(PhotonNetwork.PlayerList.Length==2)
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(false);
            GameObject.FindWithTag("Oyuncu1_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncu2_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("BilgileriKontrolEt");
        }
        else
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(true);
            GameObject.FindWithTag("Oyuncu1_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncu2_isim").GetComponent<TextMeshProUGUI>().text = "...............";
        }
    }
}
