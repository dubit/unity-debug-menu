using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace DUCK.DebugMenu.Logger
{
	public class LogTypeData
	{
		public Sprite Icon { get; private set; }
		private bool isVisible;
		private readonly List<LogEntryElement> logs;
		private readonly CanvasGroup toggleFilterCanvasGroup;
		private readonly Color oddBackgroundColor;

		public LogTypeData(Button toggleFilterButton, Sprite icon)
		{
			Icon = icon;
			toggleFilterButton.onClick.AddListener(HandleToggleFilter);
			toggleFilterCanvasGroup = toggleFilterButton.GetComponent<CanvasGroup>();
			isVisible = true;
			logs = new List<LogEntryElement>();
			oddBackgroundColor = new Color(0.8f, 0.8f, 0.8f);
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

		public void UpdateBackground(ref int count)
		{
			if (!isVisible) return;

			foreach (var logEntryElement in logs)
			{
				logEntryElement.Background.color = count++ % 2 != 0 ? oddBackgroundColor : Color.white;
			}
		}
	}
}
