using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VN.UI;

namespace VN.Services
{
    [InitializeAtRuntime]
    [Naninovel.Commands.Goto.DontReset]
    public class QuestLogService : IEngineService
    {
        private readonly List<int> _log;
        private readonly UIManager _uIManager;

        public QuestLogService(UIManager uIManager)
        {
            _log = new List<int>();
            _uIManager = uIManager;
        }

        public void DestroyService()
        {
            _log.Clear();
        }

        public void ResetService()
        {
            _log.Clear();
        }

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void Add(int questStage)
        {
            _log.Add(questStage);
            _uIManager.GetUI<QuestLogUI>().AddLog(GetQuestStageTextId(questStage));

            string GetQuestStageTextId(int questStage)
            {
                return $"-- Add log for quest stage {questStage} --";
            }
        }

        public void Clear()
        {
            _log.Clear();
            _uIManager.GetUI<QuestLogUI>().ClearLog();
        }
    }
}