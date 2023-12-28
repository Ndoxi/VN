using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VN.Services;

namespace VN.Commands
{
    [CommandAlias("loadMinigame")]
    public class LoadMinigameCommand : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            Debug.Log($"Loading minigame...");
            MinigameService minigameService = Engine.GetService<MinigameService>();
            await minigameService.LoadMinigameAsync();

            Debug.Log("Minigame loaded!");
        }
    }

    [CommandAlias("unloadMinigame")]
    public class UnloadMinigameCommand : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            Debug.Log($"Unloading minigame...");
            MinigameService minigameService = Engine.GetService<MinigameService>();
            await minigameService.UnloadMinigameAsync();

            Debug.Log("Minigame unloaded!");
        }
    }

    [CommandAlias("startMinigame")]
    public class StartMinigameCommand : Command
    {
        public IntegerParameter Level;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            Debug.Log($"Starting level {Level}...");
            MinigameService minigameService = Engine.GetService<MinigameService>();
            return minigameService.StartGameAsync();
        }
    }
}