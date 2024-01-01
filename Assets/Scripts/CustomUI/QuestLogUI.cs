using Naninovel.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VN.UI
{
    public class QuestLogUI : CustomUI
    {
        [Space, SerializeField]
        private QuestLogTab _questLogTab;

        public void AddLog(string textId)
        {
            _questLogTab.AddText(textId);
        }

        public void ClearLog()
        {
            _questLogTab.Clear();
        }
    }
}