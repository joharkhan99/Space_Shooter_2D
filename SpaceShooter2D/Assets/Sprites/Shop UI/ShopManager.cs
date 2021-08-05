using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.Networking;


public class ShopManager : MonoBehaviour
{
    public Text PlayerShopName;
    public Text PlayerShopScore;
    public Text PlayerShopDiamond;
    public Text PlayerShopGold;
    public Button guiAlertButton;
    public GameObject InternetConnPanel;
    public GameObject InternetConnPanel2;

    void Start()
    {
        InvokeRepeating("InternetpanelShowHide", 0, 2f);


        if (PlayerPrefs.GetString("MAP1") == "map1"){
            HidePriceButtonIfBuy(1,"Map");
        }if (PlayerPrefs.GetString("MAP2") == "map2"){
            HidePriceButtonIfBuy(2, "Map");
        }if (PlayerPrefs.GetString("MAP3") == "map3"){
            HidePriceButtonIfBuy(3, "Map");
        }if (PlayerPrefs.GetString("MAP4") == "map4"){
            HidePriceButtonIfBuy(4, "Map");
        }if (PlayerPrefs.GetString("MAP5") == "map5"){
            HidePriceButtonIfBuy(5, "Map");
        }if (PlayerPrefs.GetString("MAP6") == "map6"){
            HidePriceButtonIfBuy(6, "Map");
        }if (PlayerPrefs.GetString("MAP7") == "map7"){
            HidePriceButtonIfBuy(7, "Map");
        }if (PlayerPrefs.GetString("MAP8") == "map8"){
            HidePriceButtonIfBuy(8, "Map");
        }if (PlayerPrefs.GetString("MAP9") == "map9"){
            HidePriceButtonIfBuy(9, "Map");
        }

        // ships
        if (PlayerPrefs.GetString("SHIP1") == "ship1")
        {
            HidePriceButtonIfBuy(1, "Ship");
        }
        if (PlayerPrefs.GetString("SHIP2") == "ship2")
        {
            HidePriceButtonIfBuy(2, "Ship");
        }
        if (PlayerPrefs.GetString("SHIP3") == "ship3")
        {
            HidePriceButtonIfBuy(3, "Ship");
        }
        if (PlayerPrefs.GetString("SHIP4") == "ship4")
        {
            HidePriceButtonIfBuy(4, "Ship");
        }

    }

    public void HidePriceButtonIfBuy(int number, string type)
    {
        GameObject.Find(type + "" + number + "PriceButton").SetActive(false);
        GameObject.Find(type + "" + number + "").GetComponent<Button>().enabled = false;
    }

    void Update()
    {
        string Player_prefs_USERNAME = PlayerPrefs.GetString("USERNAME");
        int Player_prefs_SCORE = PlayerPrefs.GetInt("HIGHSCORE"),
            Player_prefs_DIAMONDS = PlayerPrefs.GetInt("DIAMOND"),
            Player_prefs_GOLDS = PlayerPrefs.GetInt("GOLD");

        PlayerShopName.GetComponent<Text>().text = Player_prefs_USERNAME;
        PlayerShopScore.GetComponent<Text>().text = Player_prefs_SCORE.ToString();
        PlayerShopDiamond.GetComponent<Text>().text = Player_prefs_DIAMONDS.ToString();
        PlayerShopGold.GetComponent<Text>().text = Player_prefs_GOLDS.ToString();

    }

    public void InternetpanelShowHide()
    {
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                InternetConnPanel.SetActive(false);
                InternetConnPanel2.SetActive(false);
            }
            else
            {
                InternetConnPanel.SetActive(true);
                InternetConnPanel2.SetActive(true);
            }
        }));
    }

    IEnumerator CheckInternetConnection(System.Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.infologysolutions.com/");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }



    public void ExitShop()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GeneralBuyMap(string currency, int mapNumber)
    {
        int Player_GOLD = PlayerPrefs.GetInt(currency);
        int MapPrice = int.Parse(GameObject.Find("Map"+mapNumber+"Price").GetComponent<Text>().text);
        if (Player_GOLD < MapPrice)
        {
            StartCoroutine(ShowMessage("Not Enough " + currency.ToLower() + " To Buy!", 2));
        }
        else
        {
            int payment = Player_GOLD - MapPrice;
            PlayerPrefs.SetInt(currency, payment);
            StartCoroutine(ShowMessage("Hurray!", 2));

            GameObject.Find("Map" + mapNumber + "PriceButton").SetActive(false);
            GameObject.Find("Map" + mapNumber + "").GetComponent<Button>().enabled = false;

            GameObject.Find("Map" + mapNumber + "SelectButton").SetActive(true);
            PlayerPrefs.SetString("MAP" + mapNumber + "", "map" + mapNumber + "");
        }
    }

    public void BuyMap1()
    {
        GeneralBuyMap("GOLD", 1);
    }
    public void BuyMap2()
    {
        GeneralBuyMap("GOLD", 2);
    }    
    
    public void BuyMap3()
    {
        GeneralBuyMap("DIAMOND", 3);
    }

    public void BuyMap4()
    {
        GeneralBuyMap("GOLD", 4);
    }

    public void BuyMap5()
    {
        GeneralBuyMap("GOLD", 5);
    }

    public void BuyMap6()
    {
        GeneralBuyMap("DIAMOND", 6);
    }

    public void BuyMap7()
    {
        GeneralBuyMap("GOLD", 7);
    }

    public void BuyMap8()
    {
        GeneralBuyMap("GOLD", 8);
    }

    public void BuyMap9()
    {
        GeneralBuyMap("DIAMOND", 9);
    }


    public void SelectMap(int mapNumber)
    {
        PlayerPrefs.SetString("SELECTMAP", "map" + mapNumber);
        StartCoroutine(ShowMessage("Map "+mapNumber+" Selected!", 2));
    }


    // SHIPS
    public void SelectShip(int shipNumber)
    {
        PlayerPrefs.SetString("SELECTSHIP", "ship" + shipNumber);
        StartCoroutine(ShowMessage("Ship " + shipNumber + " Selected!", 2));
    }

    public void GeneralBuyShip(string currency, int shipNumber)
    {
        int Player_GOLD = PlayerPrefs.GetInt(currency);
        int ShipPrice = int.Parse(GameObject.Find("Ship" + shipNumber + "Price").GetComponent<Text>().text);
        if (Player_GOLD < ShipPrice)
        {
            StartCoroutine(ShowMessage("Not Enough " + currency.ToLower() + " To Buy!", 2));
        }
        else
        {
            int payment = Player_GOLD - ShipPrice;
            PlayerPrefs.SetInt(currency, payment);
            StartCoroutine(ShowMessage("Hurray!", 2));

            GameObject.Find("Ship" + shipNumber + "PriceButton").SetActive(false);
            GameObject.Find("Ship" + shipNumber + "").GetComponent<Button>().enabled = false;

            GameObject.Find("Ship" + shipNumber + "SelectButton").SetActive(true);
            PlayerPrefs.SetString("SHIP" + shipNumber + "", "ship" + shipNumber + "");
        }
    }

    public void BuyShip1()
    {
        GeneralBuyShip("GOLD", 1);
    }
    public void BuyShip2()
    {
        GeneralBuyShip("GOLD", 2);
    }

    public void BuyShip3()
    {
        GeneralBuyShip("DIAMOND", 3);
    }

    public void BuyShip4()
    {
        GeneralBuyShip("GOLD", 4);
    }




    IEnumerator ShowMessage(string message, float delay)
    {
        guiAlertButton.GetComponentInChildren<Text>().text = message;
        guiAlertButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        guiAlertButton.GetComponentInChildren<Text>().text = "";
        guiAlertButton.gameObject.SetActive(false);
    }

}
