using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Logger
{
	/// <summary>
	/// Manages a list of log entries displayed in the debug menu
	/// </summary>
	public class DebugLogPage : MonoBehaviour
	{
		public enum LogType
		{
			Default = 12171705,
			Warning = 16753920,
			Error = 16711680
		}

		private struct PendingLog
		{
			public readonly string LogMessage;
			public readonly LogType LogType;

			public PendingLog(string logMessage, LogType logType)
			{
				LogMessage = logMessage;
				LogType = logType;
			}
		}

		[Header("Icons")]
		[SerializeField]
		private Sprite infoIcon;
		[SerializeField]
		private Sprite warningIcon;
		[SerializeField]
		private Sprite errorIcon;
		[Header("Components")]
		[SerializeField]
		private ScrollRect scrollRect;
		[SerializeField]
		private RectTransform container;
		[SerializeField]
		private LogEntryElement entryPrefab;
		[Header("Buttons")]
		[SerializeField]
		private Button clearButton;
		[SerializeField]
		private Button toggleInfoButton;
		[SerializeField]
		private Button toggleWarningsButton;
		[SerializeField]
		private Button toggleErrorButton;

		private readonly Queue<PendingLog> pendingLogs = new Queue<PendingLog>();
		private Dictionary<LogType, LogTypeData> logTypeDatas;

		private void Awake()
		{
			entryPrefab.gameObject.SetActive(false);
			clearButton.interactable = false;

			logTypeDatas = new Dictionary<LogType, LogTypeData>
			{
				{LogType.Default, new LogTypeData(toggleInfoButton, infoIcon)},
				{LogType.Warning, new LogTypeData(toggleWarningsButton, warningIcon)},
				{LogType.Error, new LogTypeData(toggleErrorButton, errorIcon)},
			};

			toggleInfoButton.onClick.AddListener(UpdateLogEntryBackgrounds);
			toggleWarningsButton.onClick.AddListener(UpdateLogEntryBackgrounds);
			toggleErrorButton.onClick.AddListener(UpdateLogEntryBackgrounds);
		}

		private void UpdateLogEntryBackgrounds()
		{
			var count = 0;
			foreach (var logTypeData in logTypeDatas)
			{
				logTypeData.Value.UpdateBackground(ref count);
			}
		}

		private void OnEnable()
		{
			StartCoroutine(ScrollToBottom());
		}

		/// <summary>
		/// The logs are passed back to the main thread through a queue as instantiating the new log
		/// must be done on the main thread, however the queue itself is not properly concurrent.
		/// </summary>
		public void Log(string text, LogType logType)
		{
			pendingLogs.Enqueue(new PendingLog(text, logType));
		}

		/// <summary>
		/// Called from a Clear button OnClick in the prefab
		/// </summary>
		public void Clear()
		{
			foreach (var logTypeData in logTypeDatas)
			{
				logTypeData.Value.Clear();
			}

			clearButton.interactable = false;
		}

		private void AddLogEntry(string text, LogType logType)
		{
			clearButton.interactable = true;

			var newLogEntry = Instantiate(entryPrefab, container, false);
			newLogEntry.transform.SetAsLastSibling();
			newLogEntry.TextComponent.text = text;
			newLogEntry.IconComponent.sprite = logTypeDatas[logType].Icon;
			newLogEntry.gameObject.SetActive(true);
			logTypeDatas[logType].AddLog(newLogEntry);

			UpdateLogEntryBackgrounds();
		}

		private IEnumerator ScrollToBottom()
		{
			yield return new WaitForEndOfFrame();
			scrollRect.verticalNormalizedPosition = 0f;
		}

		private void Update()
		{
			if (pendingLogs.Count == 0) return;

			while (pendingLogs.Count > 0)
			{
				var pendingLog = pendingLogs.Dequeue();
				AddLogEntry(pendingLog.LogMessage, pendingLog.LogType);
			}

			StartCoroutine(ScrollToBottom());
		}
	}
}
