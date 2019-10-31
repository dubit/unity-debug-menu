using System.Collections.Generic;
using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage.Config
{
	public class GameObjectInspectorConfig : InspectorConfig
	{
		public GameObject LabelPrefab { get; set; }
		public GameObject DividerPrefab { get; set; }

		public override IEnumerable<IDrawInstruction> GetDrawInstructions(object targetObject)
		{
			var result = new List<IDrawInstruction>();

			var gameObject = (GameObject) targetObject;

			var components = gameObject.GetComponents<Component>();

			foreach (var component in components)
			{
				result.Add(new SimpleDrawInstruction{ Prefab = DividerPrefab });
				result.Add(new LabelDrawInstruction{ Prefab = LabelPrefab, Text = component.GetType().Name });
				result.AddRange(base.GetDrawInstructions(component));
			}

			return result;
		}
	}
}
