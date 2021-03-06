﻿using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Logger
{
	public class LogEntryElement : MonoBehaviour
	{
		public Image Background { get { return background; } }
		public Button ButtonComponent { get { return button; } }
		public Text TextComponent { get { return text; } }
		public Image IconComponent { get { return icon; } }

		[SerializeField]
		private Image icon;
		[SerializeField]
		private Text text;
		[SerializeField]
		private Image background;
		[SerializeField]
		private Button button;
	}
}
