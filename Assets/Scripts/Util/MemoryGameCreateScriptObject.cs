using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameCreateScriptObject : MonoBehaviour
{
    // Referência ao TextAsset contendo o JSON
    public TextAsset jsonTextAsset;

    void Start()
    {
        if (jsonTextAsset != null)
        {
            // Converte o JSON para o objeto desejado
            LocationDataList locationDataList = JsonUtility.FromJson<LocationDataList>(jsonTextAsset.text);
            foreach (LocationData locationData in locationDataList.locations)
            {
                // Cria um novo ScriptableObject e atribui os dados do JSON a ele
                CardScriptObject cardScriptObject = ScriptableObject.CreateInstance<CardScriptObject>();
                cardScriptObject.nameCard = locationData.name;
                cardScriptObject.pairName = locationData.pairName;
                cardScriptObject.sprite = Resources.Load<Sprite>(locationData.path); 
                cardScriptObject.description = locationData.description;

                // Salva o ScriptableObject como um arquivo asset no projeto
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(cardScriptObject, $"Assets/Resources/ScriptObject/MemoryGame/{cardScriptObject.nameCard}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
#endif                
            }
        }
        else
        {
            Debug.LogError("TextAsset não foi atribuído.");
        }
    }
}

[System.Serializable]
public class LocationDataList
{
    public LocationData[] locations;
}

[System.Serializable]
public class LocationData
{
    public string name;
    public string pairName;
    public string description;
    public string path;
}

