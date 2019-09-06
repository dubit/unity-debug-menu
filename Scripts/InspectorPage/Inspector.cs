using System;
using DUCK.DebugMenu.InspectorPage.Config;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage
{
	public class Inspector : MonoBehaviour
	{
		[SerializeField]
		private Button homeButton;
		public Button HomeButton => homeButton;

		[SerializeField]
		private Button backButton;
		public Button BackButton => backButton;

		[SerializeField]
		private Transform parent;

		public void InspectObject(object targetObject, InspectorConfig config = null)
		{
			if (targetObject == null) throw new ArgumentNullException(nameof(targetObject));
			if (targetObject.GetType().IsPrimitive)
			{
				throw new ArgumentException("Target object cannot be a primitive type", nameof(targetObject));
			}

			if (config == null) config = InspectorConfig.ConfigSelector.Select(targetObject);
			BuildInspector(targetObject, config);
		}

		private void BuildInspector(object targetObject, InspectorConfig config)
		{
			ClearInspector();

			var instructions = config.GetDrawInstructions(targetObject);

			foreach (var instruction in instructions)
			{
				instruction.Run(parent);
			}
		}

		private void ClearInspector()
		{
			foreach (Transform child in parent)
			{
				Destroy(child.gameObject);
			}
		}
	}
}