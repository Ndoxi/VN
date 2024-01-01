using Naninovel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VN.UI
{
    public class QuestLogRecord : MonoBehaviour
    {
        private const string QUEST_CATEGORY = "UIText";
        private const string QUEST_STAGE_KEY = "QuestStage_";
        private const string DEFAULT_VALUE = "Localisation error";
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        private int _stageId;

        private void Awake()
        {
            var localizationService = Engine.GetService<ILocalizationManager>();
            localizationService.OnLocaleChanged += _ => UpdateText();
        }

        public void SetData(int stageId)
        {
            _stageId = stageId;
            UpdateText();
        }

        private void UpdateText()
        {
            UpdateTextInternal(Engine.GetService<ITextManager>()
                                     .GetRecordValue($"{QUEST_STAGE_KEY}{_stageId}", QUEST_CATEGORY));

            void UpdateTextInternal(string text)
            {
                if (text != null)
                    _textMesh.text = text;
                else
                    _textMesh.text = DEFAULT_VALUE;
            }
        }
    }
}