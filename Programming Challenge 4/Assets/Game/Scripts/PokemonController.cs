using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokemonController : MonoBehaviour
{
    [Serializable]
    public class PokeSprites
    {
        public string front_default;
        public string front_shiny;
    }

    [Serializable]
    public class PokemonInfo
    {
        public string name;
        public PokeSprites sprites;
    }

    public InputField searchText;
    public Text pokeNameText;
    public Image pokeImage;

    void Start()
    { 
    }

    void Update()
    {
    }

    public void SetPokemonInfo()
    {
        PokemonInfo info = getPokemon();
        pokeNameText.text = info.name;

        int rng = UnityEngine.Random.Range(0, 10);

        if (rng < 9)
        {
            StartCoroutine(GetSprite(info.sprites.front_default));
        }
        else
        {
            StartCoroutine(GetSprite(info.sprites.front_shiny));
        }
    }

    IEnumerator GetSprite(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] results = www.downloadHandler.data;
            Texture2D texture = new Texture2D(2,2);
            texture.LoadImage(results);
            
            pokeImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            pokeImage.color = Color.white;
        }
    }

    private PokemonInfo getPokemon()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://pokeapi.co/api/v1/pokemon/{0}", searchText.text));

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        return JsonUtility.FromJson<PokemonInfo>(jsonResponse);

    }

    
}
