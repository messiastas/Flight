using System.Collections;
using UnityEngine;

class SharedVars
{
	
	public static SharedVars Inst = new SharedVars();

    //Данные
    public bool isMobile = false;
    public bool isTestBuild = false;

	public string lastPlayedLevel = "";
	public string currentInterrogationStatus = "";
	public string resultFirstLevel = "";

    public void SavePlayerData()
    {
       // PlayerPrefs.SetInt("tribeSize", SharedVars.Inst.tribeSize);

       // PlayerPrefs.SetFloat("animalDeath", SharedVars.Inst.animalDeath);

       // PlayerPrefs.SetString("language", SharedVars.Inst.language);

       // PlayerPrefs.SetString("isLevelsShuffled", SaveBool(SharedVars.Inst.isLevelsShuffled));

       // for (int i = 0; i < SharedVars.Inst.levelsPosition.Length; i++)
       // {
       //     PlayerPrefs.SetInt("levelsPosition"+i.ToString(), SharedVars.Inst.levelsPosition[i]);
       // }

       // PlayerPrefs.Save();
    }

    public void LoadSettingsData()
    {
       // SharedVars.Inst.language = SharedVars.Inst.LoadString("language", SharedVars.Inst.language);
       // SharedVars.Inst.volume = SharedVars.Inst.LoadBool("volume", SharedVars.Inst.volume);
    }

    public void LoadPlayerData()
    {
       /* SharedVars.Inst.tribeSize = SharedVars.Inst.LoadInt("tribeSize", SharedVars.Inst.tribeSize);

        SharedVars.Inst.animalDeath = SharedVars.Inst.LoadFloat("animalDeath", SharedVars.Inst.animalDeath);

        SharedVars.Inst.language = SharedVars.Inst.LoadString("language", SharedVars.Inst.language);

        SharedVars.Inst.isLevelsShuffled = SharedVars.Inst.LoadBool("isLevelsShuffled", SharedVars.Inst.isLevelsShuffled);

        for (int i = 0; i < SharedVars.Inst.levelsPosition.Length; i++)
        {
            SharedVars.Inst.levelsPosition[i] = SharedVars.Inst.LoadInt("levelsPosition" + i.ToString(), SharedVars.Inst.levelsPosition[i]);
        }
        */
    }

    public void DeleteLastRunPlayerData()
    {
      /*  SharedVars.Inst.DeleteKey("tribeSize");
        for (int i = 0; i < SharedVars.Inst.levelsPosition.Length; i++)
        {
            SharedVars.Inst.DeleteKey("levelsPosition" + i.ToString());
        }
        SharedVars.Inst.isSomeDataMissed = true;*/
    }

    public int LoadInt(string key, int currentAmount)
    {
        int result = int.MinValue;
        if(PlayerPrefs.HasKey(key))
        {
            result = PlayerPrefs.GetInt(key);
        } else
        {
           // SharedVars.Inst.isSomeDataMissed = false;
            result = currentAmount;
        }
        return result;
    }

    public float LoadFloat(string key, float currentAmount)
    {
        float result = float.MinValue;
        if (PlayerPrefs.HasKey(key))
        {
            result = PlayerPrefs.GetFloat(key);
        }
        else
        {
           // SharedVars.Inst.isSomeDataMissed = false;
            result = currentAmount;
        }
        return result;
    }

    public string LoadString(string key, string currentAmount)
    {
        string result = "";
        if (PlayerPrefs.HasKey(key))
        {
            result = PlayerPrefs.GetString(key);
        }
        else
        {
            //SharedVars.Inst.isSomeDataMissed = false;
            result = currentAmount;
        }
        return result;
    }

    public bool LoadBool(string key, bool currentAmount)
    {
        bool result = false;
        if (PlayerPrefs.HasKey(key))
        {
            if(PlayerPrefs.GetString(key)=="true")
            {
                result = true;
            } else
            {
                result = false;
            }
        }
        else
        {
            //SharedVars.Inst.isSomeDataMissed = false;
            result = currentAmount;
        }
        return result;
    }

    public string SaveBool(bool currentAmount)
    {
        string result = "false";
        if (currentAmount)
            {
                result = "true";
            }
        return result;
    }

    public void DeleteKey(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }




}