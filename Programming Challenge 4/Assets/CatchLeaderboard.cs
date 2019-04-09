using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchLeaderboard : MonoBehaviour
{
    public CatchSettings cs;

    public void PostHighScore()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate() { StatisticName = "total_pokemon_caught", Value = cs.totalPokemonCaught}
                }
            },
            OnUpdatePlayerStatisticsResponse,
            OnPlayFabError
        );
    }

    public void OnUpdatePlayerStatisticsResponse(UpdatePlayerStatisticsResult response)
    {
        Debug.Log("User statistics updated");
    }

    public void OnPlayFabError(PlayFabError error)
    {
        Debug.LogError("PlayFab Error: " + error.GenerateErrorReport());
    }
}
