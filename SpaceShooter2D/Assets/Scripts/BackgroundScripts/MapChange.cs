using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChange : MonoBehaviour
{
    private SpriteRenderer Image;
    private Sprite Map1, Map2, Map3, Map4, Map5, Map6, Map7, Map8, Map9;

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<SpriteRenderer>();
        Map1 = Resources.Load<Sprite>("Map 1");
        Map2 = Resources.Load<Sprite>("Map 2");
        Map3 = Resources.Load<Sprite>("Map 3");
        Map4 = Resources.Load<Sprite>("Map 4");
        Map5 = Resources.Load<Sprite>("Map 5");
        Map6 = Resources.Load<Sprite>("Map 6");
        Map7 = Resources.Load<Sprite>("Map 7");
        Map8 = Resources.Load<Sprite>("Map 8");
        Map9 = Resources.Load<Sprite>("Map 9");

        string SelectedMap = PlayerPrefs.GetString("SELECTMAP");
        switch (SelectedMap)
        {
            case "map1":
                Image.sprite = Map1;
                break;
            case "map2":
                Image.sprite = Map2;
                break;
            case "map3":
                Image.sprite = Map3;
                break;
            case "map4":
                Image.sprite = Map4;
                break;
            case "map5":
                Image.sprite = Map5;
                break;
            case "map6":
                Image.sprite = Map6;
                break;
            case "map7":
                Image.sprite = Map7;
                break;
            case "map8":
                Image.sprite = Map8;
                break;
            case "map9":
                Image.sprite = Map9;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
