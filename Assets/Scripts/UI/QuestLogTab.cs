using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VN.UI
{
    public class QuestLogTab : MonoBehaviour
    {
        [SerializeField] private ScrollRect _recordsList;
        [SerializeField] private LayoutGroup _contentLayoutGroup;
        [SerializeField] private QuestLogRecord _recordPrefab;
        private readonly List<QuestLogRecord> _logRecords = new List<QuestLogRecord>();

        public void AddLog(int logId)
        {
            QuestLogRecord newRecord = GameObject.Instantiate(_recordPrefab, _recordsList.content.transform);
            newRecord.SetData(logId);
            _logRecords.Add(newRecord);
            UpdateLayout();
        }

        public void Clear()
        {
            foreach (QuestLogRecord record in _logRecords)
            {
                Destroy(record.gameObject);
            }
            _logRecords.Clear();
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            Canvas.ForceUpdateCanvases();
            _contentLayoutGroup.enabled = false;
            _contentLayoutGroup.enabled = true;
        }
    }
}