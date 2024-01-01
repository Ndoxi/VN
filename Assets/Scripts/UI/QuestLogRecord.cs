using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VN.UI
{
    public class QuestLogRecord : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        public void SetData(string textId)
        {
            _textMesh.text = textId;
        }
    }
}