using System;
using System.Collections.Generic;
using DUCK.DebugMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage
{
	public class ShelfView : MonoBehaviour
	{
		[SerializeField]
		private Button homeButton;

		[SerializeField]
		private ButtonList buttonList;

		public void Init(
			IEnumerable<InspectorShelf.InspectorShelfItem> objects,
			Action<object> onInspectionRequested,
			UnityAction home)
		{
			homeButton.onClick.AddListener(home);

			buttonList.Clear();

			foreach (var element in objects)
			{
				var obj = element;
				buttonList.AddButton(obj.Name, () => onInspectionRequested(obj.Object));
			}
		}
	}
}