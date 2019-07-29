using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DUCK.DebugMenu
{
	public class ButtonList : MonoBehaviour
	{
		[SerializeField]
		private Button actionButtonTemplate;

		public Button AddButton(string text, UnityAction onClick)
		{
			var actionButton = Instantiate(actionButtonTemplate, actionButtonTemplate.transform.parent);
			var label = actionButton.GetComponentInChildren<Text>();
			actionButton.onClick.AddListener(onClick);
			label.text = text;
			actionButton.gameObject.SetActive(true);
			return actionButton;
		}

		public void Clear()
		{
			foreach (Transform child in actionButtonTemplate.transform.parent)
			{
				if (child != actionButtonTemplate.transform)
				{
					Destroy(child.gameObject);
				}
			}
		}
	}
}