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
		private enum LogType
		{
			Default = 12171705,
			Warning = 16753920,
			Error = 16711680
		}

		private class LogTypeConfig
		{
			public Color Color { get; private set; }
			public Sprite Icon { get; private set; }

			public LogTypeConfig(Color color, Sprite icon)
			{
				Color = color;
				Icon = icon;
			}
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

		[Header("Log Type Icons")]
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
		[SerializeField]
		private Button clearButton;

		private Queue<PendingLog> pendingLogs;
		private Dictionary<LogType, LogTypeConfig> logTypeConfigs;

		private static DebugLogPage instance;

		private void Awake()
		{
			entryPrefab.gameObject.SetActive(false);
			clearButton.interactable = false;

			logTypeConfigs = new Dictionary<LogType, LogTypeConfig>
			{
				{LogType.Default, new LogTypeConfig(new Color(185, 185, 185), infoIcon)},
				{LogType.Warning, new LogTypeConfig(new Color(255, 165, 0), warningIcon)},
				{LogType.Error, new LogTypeConfig(new Color(255, 0, 0), errorIcon)}
			};

			Initialise();
		}

		private void OnEnable()
		{
			StartCoroutine(ScrollToBottom());
		}

		public static void Log(string text)
		{
			Log(text, LogType.Default);
		}

		public static void LogWarning(string text)
		{
			Log(text, LogType.Warning);
		}

		public static void LogError(string text)
		{
			Log(text, LogType.Error);
		}

		/// <summary>
		/// The logs are passed back to the main thread through a queue as instantiating the new log
		/// must be done on the main thread, however the queue itself is not properly concurrent.
		/// </summary>
		private static void Log(string text, LogType logType)
		{
			if (instance == null) return;

			instance.pendingLogs.Enqueue(new PendingLog(text, logType));
		}

		public void Initialise()
		{
			if (instance == null)
			{
				instance = this;
				pendingLogs = new Queue<PendingLog>();
			}
		}

		/// <summary>
		/// Called from a Clear button OnClick in the prefab
		/// </summary>
		public void Clear()
		{
			foreach (var rectTransform in container.GetComponentsInChildren<RectTransform>())
			{
				if (rectTransform != container && rectTransform != entryPrefab)
				{
					Destroy(rectTransform.gameObject);
				}
			}

			clearButton.interactable = false;
		}

		private void AddLogEntry(string text, LogType logType)
		{
			clearButton.interactable = true;

			var logTypeConfig = logTypeConfigs[logType];

			var newLogEntry = Instantiate(entryPrefab, container, false);
			newLogEntry.transform.SetAsLastSibling();
			newLogEntry.TextComponent.text = text;
			newLogEntry.ImageComponent.sprite = logTypeConfig.Icon;
			newLogEntry.gameObject.SetActive(true);
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
