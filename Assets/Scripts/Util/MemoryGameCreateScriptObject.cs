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
            Debug.Log(locationDataList.locations);
            foreach (LocationData locationData in locationDataList.locations)
            {
                // Cria um novo ScriptableObject e atribui os dados do JSON a ele
                CardScriptObject cardScriptObject = ScriptableObject.CreateInstance<CardScriptObject>();
                cardScriptObject.nameCard = locationData.name;
                cardScriptObject.sprite = Resources.Load<Sprite>(locationData.path); // Certifique-se de ter a imagem no Resources
                cardScriptObject.description = locationData.description;

                // Salva o ScriptableObject como um arquivo asset no projeto
                UnityEditor.AssetDatabase.CreateAsset(cardScriptObject, $"Assets/Resources/ScriptObject/MemoryGame/{cardScriptObject.nameCard}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
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
    public string description;
    public string path;
}

