using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Logger
{
	public class LogEntryElement : MonoBehaviour
	{
		public Text TextComponent { get { return text; } }
		public Image ImageComponent { get { return icon; } }

		[SerializeField]
		private Image icon;
		[SerializeField]
		private Text text;
	}
}
