using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;

[System.Serializable] 
public class SavesData : MonoBehaviour
{
    public int level;
    
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] InputField integerText;
    [SerializeField] InputField stringifyText;
    [SerializeField] Text systemSavesText;
    [SerializeField] Toggle[] booleanArrayToggle;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
        
    }
    public void Save()
    {
        YandexGame.savesData.money = CoinManager.Instance.Coins;
        YandexGame.savesData.money = int.Parse(integerText.text); 

        YandexGame.SaveProgress();
        YandexGame.savesData.level = level;

        Text.text = YandexGame.savesData.money.ToString();
    }

    public void GetLoad()
    {
        integerText.text = string.Empty;
        
        stringifyText.text = string.Empty;

        integerText.placeholder.GetComponent<Text>().text = YandexGame.savesData.money.ToString();

        systemSavesText.text = $"Language - {YandexGame.savesData.language}\n" +
        $"First Session - {YandexGame.savesData.isFirstSession}\n" +
        $"Prompt Done - {YandexGame.savesData.promptDone}\n";
    }
}
