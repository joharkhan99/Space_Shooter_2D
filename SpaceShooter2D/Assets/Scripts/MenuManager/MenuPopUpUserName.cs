using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class MenuPopUpUserName : MonoBehaviour
{
    public Text guiText;
    public Text Username_field;
    private const string UsernameURL = "https://infologysolutions.com/spaceshooter_api/index.php";
    private string checkUsername = "";
    public GameObject Panel;


    // Start is called before the first frame update
    void Start()
    {
        string Player_prefs_USERNAME = PlayerPrefs.GetString("USERNAME");
        if (PlayerPrefs.HasKey("USERNAME") && Player_prefs_USERNAME != null)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
    }

    public void HideShowUserNamePanel()
    {

    }

    IEnumerator ShowMessage(string message, float delay)
    {
        guiText.GetComponent<Text>().text = message;
        guiText.enabled = true;
        yield return new WaitForSeconds(delay);
        guiText.GetComponent<Text>().text = "";
        guiText.enabled = false;
    }

    public void OnSumbitButton()
    {
        string UsernameField= Username_field.text.ToString().Trim();

        if (UsernameField.Equals("") || UsernameField==null)
        {
            StartCoroutine(ShowMessage("Please Enter Username", 3));
        }else if (UsernameField.Length > 10)
        {
            StartCoroutine(ShowMessage("Username should be less than 13 characters", 3));
        }
        else
        { 
            StartCoroutine(GetUsername(UsernameField));
        }
    }

    IEnumerator GetUsername(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("CheckUserExists_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post(UsernameURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string contents = www.downloadHandler.text;
                using (StringReader reader = new StringReader(contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        checkUsername = line;
                        if (checkUsername == "yes")
                        {
                            StartCoroutine(ShowMessage("Username Already Taken", 2));
                        }
                        else if (checkUsername == "no")
                        {
                            StartCoroutine(ShowMessage("Username Created", 2));
                            AddNewUser(name, 0);
                        }
                    }
                }
            }
        }
    }

    public void AddNewUser(string name, int score)
    {
        StartCoroutine(AddUser(name, score));
        PlayerPrefs.SetString("USERNAME" , name);
        Panel.gameObject.SetActive(false);

        PlayerPrefs.SetInt("DIAMOND", 10);
        PlayerPrefs.SetInt("GOLD", 500);
    }

    IEnumerator AddUser(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("post_leaderboard_ZErXPnRu6PI1ka2kjes2EvGUat8Q7fhN", "true");
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(UsernameURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
            }
        }
    }








}
