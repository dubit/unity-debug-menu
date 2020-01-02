using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu
{
	public class AbstractDebugMenuTabPage : MonoBehaviour
	{
		public Button TabButton { get; set; }

		public bool HasButton => !string.IsNullOrEmpty(buttonText);

		[SerializeField]
		private string buttonText;
		public string ButtonText => buttonText;

		[SerializeField]
		private Button backButton;
		public Button BackButton => backButton;
	}
}