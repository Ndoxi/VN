using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VN.Services;

namespace VN.Commands
{
    [CommandAlias("addQuestLog")]
    public class AddQuestLog : Command
    {
        [RequiredParameter]
        public IntegerParameter StageId;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestLogService questLogService = Engine.GetService<QuestLogService>();
            questLogService.Add(StageId);

            return UniTask.CompletedTask;
        }
    }

    [CommandAlias("clearQuestLog")]
    public class ClearQuestLog : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestLogService questLogService = Engine.GetService<QuestLogService>();
            questLogService.Clear();

            return UniTask.CompletedTask;
        }
    }
}