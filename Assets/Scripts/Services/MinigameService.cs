using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using DTT.MinigameBase.LevelSelect;
using UnityEngine.SceneManagement;
using DTT.MinigameMemory;

namespace VN.Services
{
    [InitializeAtRuntime]
    [Naninovel.Commands.Goto.DontReset]
    public class MinigameService : IEngineService
    {
        private const string LEVEL_DATABASE_PATH = "Minigame/LevelDatabase";
        private const string LEVEL_SETTINGS = "Minigame/DemoEasy";
        private const string MINIGAME_SCENE_NAME = "Demo";

        private LevelDatabase _levelDatabase;
        private MemoryGameSettings _levelSettings;
        private MinigameManager _minigameManager;

        public void DestroyService()
        {
            if (_levelDatabase != null)
                Resources.UnloadAsset(_levelDatabase);
            if (_levelSettings != null)
                Resources.UnloadAsset(_levelSettings);

            _minigameManager?.Dispose();
        }

        public void ResetService() { }

        public async UniTask InitializeServiceAsync()
        {
            ResourceRequest databaseRequest = Resources.LoadAsync<LevelDatabase>(LEVEL_DATABASE_PATH);
            ResourceRequest dataRequest = Resources.LoadAsync<MemoryGameSettings>(LEVEL_SETTINGS);
            await UniTask.WhenAll(databaseRequest.ToUniTask(), dataRequest.ToUniTask());

            _levelDatabase = databaseRequest.asset as LevelDatabase;
            _levelSettings = dataRequest.asset as MemoryGameSettings;

            if (_levelDatabase == null)
                throw new System.Exception("Failed to load level database!");
            if (_levelSettings == null)
                throw new System.Exception("Failed to load medium level data!");
        }

        public async UniTask LoadMinigameAsync()
        {
            if (SceneManager.GetSceneByName(MINIGAME_SCENE_NAME).IsValid())
            {
                Debug.LogWarning("Minigame scene already loaded!");
                return;
            }

            var op = SceneManager.LoadSceneAsync(MINIGAME_SCENE_NAME, LoadSceneMode.Additive);
            await op.ToUniTask();
            _minigameManager = new MinigameManager(Object.FindObjectOfType<MemoryGameManager>(), _levelSettings);
        }

        public UniTask UnloadMinigameAsync()
        {
            if (!SceneManager.GetSceneByName(MINIGAME_SCENE_NAME).IsValid())
            {
                Debug.LogWarning("Minigame scene already unloaded!");
                return UniTask.CompletedTask;
            }
            _minigameManager?.ForceFinish();
            _minigameManager?.Dispose();

            var op = SceneManager.UnloadSceneAsync(MINIGAME_SCENE_NAME);
            return op.ToUniTask();
        }

        public void StartGame()
        {
            _minigameManager.StartGameAsync();
        }

        public UniTask StartGameAsync()
        {
            return _minigameManager.StartGameAsync();
        }

        public void FinishGame()
        {
            _minigameManager.ForceFinish();
        }
    }

    class MinigameManager : System.IDisposable
    {
        private readonly MemoryGameManager _memoryGameManager;
        private readonly MemoryGameSettings _memoryGameSettings;
        private readonly UniTaskCompletionSource _completionSource;
        private bool _started;

        public MinigameManager(MemoryGameManager memoryGameManager, MemoryGameSettings memoryGameSettings)
        {
            _memoryGameManager = memoryGameManager;
            _memoryGameSettings = memoryGameSettings;
            _completionSource = new UniTaskCompletionSource();
            _memoryGameManager.Finish += FinishGameCallback;
        }

        public UniTask StartGameAsync()
        {
            if (!_started)
            {
                _memoryGameManager.StartGame(_memoryGameSettings);
                _started = true;
            }

            return _completionSource.Task;
        }

        public void ForceFinish()
        {
            if (!_started)
                return;

            _memoryGameManager.ForceFinish();
        }

        private void FinishGameCallback(MemoryGameResults results)
        {
            _completionSource.TrySetResult();
        }

        public void Dispose()
        {
            _memoryGameManager.Finish -= FinishGameCallback;
        }
    }
}