using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DUCK.DebugMenu.Logger
{
	public class LogTypeData
	{
		public Sprite Icon { get; private set; }
		public bool isVisible { get; private set; }
		private readonly List<LogEntryElement> logs;
		private readonly CanvasGroup toggleFilterCanvasGroup;

		public LogTypeData(Button toggleFilterButton, Sprite icon)
		{
			Icon = icon;
			toggleFilterButton.onClick.AddListener(HandleToggleFilter);
			toggleFilterCanvasGroup = toggleFilterButton.GetComponent<CanvasGroup>();
			isVisible = true;
			logs = new List<LogEntryElement>();
		}

		public void AddLog(LogEntryElement logEntryElement)
		{
			logs.Add(logEntryElement);
		}

		private void HandleToggleFilter()
		{
			isVisible = !isVisible;
			toggleFilterCanvasGroup.alpha = isVisible ? 1f : 0.5f;
			foreach (var logEntryElement in logs)
			{
				logEntryElement.gameObject.SetActive(isVisible);
			}
		}

		public void Clear()
		{
			foreach (var logEntryElement in logs)
			{
				Object.Destroy(logEntryElement.gameObject);
			}

			logs.Clear();
		}
	}
}
