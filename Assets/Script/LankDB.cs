using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LankDB : MonoBehaviour {

    private static LankDB singleton;
    public static LankDB GetInstance() { return singleton; }

    public const int MaxLank = 10;

    [HideInInspector]
    public string LoginCount;
    [HideInInspector]
    public string[] lank;

    private bool isFirstLogin;

    private void Awake()
    {
        if(singleton != this && singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            //DontDestroyOnLoad(this);

            InitData();
        }
    }

    public int getMaxLank()
    {
        return MaxLank;
    }

    public void SaveData_TypeInt(string keyString, int intData)
    {
        PlayerPrefs.SetInt(keyString, intData);
    }

    public int LoadData_TypeInt(string keyString)
    {
        return PlayerPrefs.GetInt(keyString);
    }

    void InitData()
    {
        lank = new string[MaxLank];

        for(int i = 0; i<MaxLank;i++)
        {
            // 0 - 9
            lank[i] = "lank" + i.ToString();
        }

        if (LoadData_TypeInt(LoginCount) == 0)
        {
            isFirstLogin = true;

            SaveData_TypeInt(LoginCount, 1);

            // 저장할때
            for (int i = 0; i < lank.Length; ++i)
            {
                // 1 - 10
                SaveData_TypeInt(lank[i], 0);
            }
        
        }
        else
        {
            isFirstLogin = false;
            SaveData_TypeInt(LoginCount, LoadData_TypeInt(LoginCount) + 1);
        }
    }

    public bool IsFirstLogin()
    {
        return isFirstLogin;
    }
    
    public int[] loadLanking()
    {
        int[] lanking = new int[MaxLank];


        for(int i = 0; i<MaxLank; ++i)
        {
            lanking[i] = LoadData_TypeInt(lank[i]);
        }


        return lanking;
    }
    
    public void deleteDB()
    {
        // 원래 경고뜸
        PlayerPrefs.DeleteAll();
    }


    public void saveAllLank(int[] array)
    {
        for (int i = 0; i < MaxLank; ++i)
        {
            SaveData_TypeInt(lank[i], array[i]);
        }
    }
    

}
