using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchSettings : MonoBehaviour
{
    public Text pokeNameText;
    public GameObject catchObj;
    public GameObject catchButton;
    public LocationServicesController locServ;

    public int totalPokemonCaught = 0;

    private void Start()
    {
        catchButton.SetActive(false);
        catchObj.SetActive(false);
        totalPokemonCaught = 0;
    }

    private void FixedUpdate()
    {
        if (PlayfabManager.Instance.state == PlayfabManager.LoginStates.Success)
        {
            catchButton.SetActive(true);
        }
    }
    public void OnCatchPokemon()
    {
        PlayFabClientAPI.WritePlayerEvent(
            new WriteClientPlayerEventRequest()
            {
                EventName = "pokemon_caught",
                Body = new Dictionary<string, object>() { { "Pokemon", pokeNameText.text }, { "Location", locServ.coordinates.ToString() } }
            },
            result =>
            {
                Debug.Log("Success sending event: pokemon_caught");
                catchObj.SetActive(true);
                totalPokemonCaught++;
            },
            error =>
            {
                Debug.Log("Failed to send event: pokemon_caught");
            }
        );
    }

}
