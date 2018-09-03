using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DUCK.DebugMenu.Email;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Logger
{
	/// <summary>
	/// Manages a list of log entries displayed in the debug menu
	/// </summary>
	public class DebugLogPage : MonoBehaviour
	{
		private struct PendingLog
		{
			public readonly string LogMessage;
			public readonly string StackTrace;
			public readonly LogType LogType;

			public PendingLog(string logMessage, string stackTrace, LogType logType)
			{
				LogMessage = logMessage;
				LogType = logType;
				StackTrace = stackTrace;
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
		[Header("StackTrace")]
		[SerializeField]
		private GameObject stackTraceContainer;
		[SerializeField]
		private Button stackTraceBackButton;
		[SerializeField]
		private Text stackTraceText;
		[SerializeField]
		private Text stackTraceLogText;
		[SerializeField]
		private Button emailStackTraceButton;

		private readonly Queue<PendingLog> pendingLogs = new Queue<PendingLog>();
		private readonly Color oddBackgroundColor = new Color(0.95f, 0.95f, 0.95f);
		private readonly List<LogEntryElement> allLogs = new List<LogEntryElement>();
		private Dictionary<LogType, LogTypeData> logTypeDatas;

		private void Awake()
		{
			entryPrefab.gameObject.SetActive(false);
			scrollRect.gameObject.SetActive(false);
			stackTraceContainer.gameObject.SetActive(false);
			clearButton.interactable = false;
			clearButton.onClick.AddListener(Clear);

			var errorLogData = new LogTypeData(toggleErrorButton, errorIcon);
			logTypeDatas = new Dictionary<LogType, LogTypeData>
			{
				{LogType.Log, new LogTypeData(toggleInfoButton, infoIcon)},
				{LogType.Warning, new LogTypeData(toggleWarningsButton, warningIcon)},
				{LogType.Error, errorLogData},
				{LogType.Assert, errorLogData},
				{LogType.Exception, errorLogData}
			};

			toggleInfoButton.onClick.AddListener(UpdateLogEntryBackgrounds);
			toggleWarningsButton.onClick.AddListener(UpdateLogEntryBackgrounds);
			toggleErrorButton.onClick.AddListener(UpdateLogEntryBackgrounds);

			stackTraceBackButton.onClick.AddListener(() =>
			{
				scrollRect.gameObject.SetActive(true);
				stackTraceContainer.gameObject.SetActive(false);
			});
			emailStackTraceButton.onClick.AddListener(() =>
			{
				gameObject.SetActive(false);
				const string emailSubject = "Unity DebugMenu has reported a log.";
				var emailBody = GenerateEmailBody(stackTraceLogText.text, stackTraceText.text);
				DebugMenu.Instance.EmailPage.Show(emailSubject, emailBody, () => gameObject.SetActive(true));
			});
		}

		public void Initialize()
		{
			Application.logMessageReceived += HandleLog;
		}

		private void HandleLog(string logString, string stackTrace, LogType type)
		{
			pendingLogs.Enqueue(new PendingLog(logString, stackTrace, type));
		}

		private void UpdateLogEntryBackgrounds()
		{
			var activeLogs = allLogs
				.Where(l => l.gameObject.activeSelf)
				.OrderBy(l => l.transform.GetSiblingIndex())
				.ToArray();

			for (var i = 0; i < activeLogs.Length; i++)
			{
				activeLogs[i].Background.color = i % 2 != 0 ? oddBackgroundColor : Color.white;
			}

			scrollRect.gameObject.SetActive(activeLogs.Length > 0);

			StartCoroutine(ScrollToBottom());
		}

		private void OnEnable()
		{
			StartCoroutine(ScrollToBottom());
		}

		private void Clear()
		{
			foreach (var logTypeData in logTypeDatas)
			{
				logTypeData.Value.Clear();
			}

			allLogs.Clear();

			clearButton.interactable = false;
			scrollRect.gameObject.SetActive(false);
		}

		private void AddLogEntry(string text, string stackTrace, LogType logType)
		{
			if (!scrollRect.gameObject.activeSelf)
			{
				scrollRect.gameObject.SetActive(true);
			}

			clearButton.interactable = true;

			var logTypeData = logTypeDatas[logType];
			var newLogEntry = Instantiate(entryPrefab, container, false);
			newLogEntry.transform.SetAsLastSibling();
			newLogEntry.ButtonComponent.onClick.AddListener(() => HandleStackTrace(text, stackTrace));
			newLogEntry.TextComponent.text = text;
			newLogEntry.IconComponent.sprite = logTypeDatas[logType].Icon;
			newLogEntry.gameObject.SetActive(logTypeData.isVisible);
			logTypeData.AddLog(newLogEntry);
			allLogs.Add(newLogEntry);

			UpdateLogEntryBackgrounds();
		}

		private void HandleStackTrace(string log, string stackTrace)
		{
			scrollRect.gameObject.SetActive(false);
			stackTraceContainer.gameObject.SetActive(true);
			stackTraceLogText.text = log;
			stackTraceText.text = stackTrace;
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
				AddLogEntry(pendingLog.LogMessage, pendingLog.StackTrace, pendingLog.LogType);
			}

			StartCoroutine(ScrollToBottom());
		}

		private static string GenerateEmailBody(string log, string stackTrace)
		{
			var body = new StringBuilder(EmailPage.SEPERATOR);
			body.Append("\n");
			body.Append(log);
			body.Append("\n");
			body.Append("\n");
			body.Append(EmailPage.SEPERATOR);
			body.Append("\n");
			body.Append(stackTrace);
			body.Append("\n");

			return body.ToString();
		}

		private void OnDestroy()
		{
			Application.logMessageReceived -= HandleLog;
		}
	}
}
