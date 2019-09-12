using System;
using System.Collections.Generic;
using System.Linq;
using DUCK.DebugMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage
{
	public class SceneHierarchyViewStackElement : MonoBehaviour
	{
		[SerializeField]
		private ButtonList buttonList;

		public void Init(IEnumerable<GameObject> objects,
			Action<GameObject> onSelected,
			Action<GameObject> onInspectionRequested)
		{
			foreach (var element in objects)
			{
				var obj = element;
				var button = buttonList.AddButton(obj.name,
					() => onSelected(obj));
				var inspectButton = button.GetComponentsInChildren<Button>()
					.FirstOrDefault(c => c != button);
				if (inspectButton != null)
				{
					inspectButton.onClick.AddListener(() => onInspectionRequested(obj));
				}
			}
		}
	}
}
