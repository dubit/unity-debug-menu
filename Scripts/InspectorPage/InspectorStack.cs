using System;
using System.Collections.Generic;
using DUCK.DebugMenu.InspectorPage.Config;
using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage
{
	public class InspectorStack : MonoBehaviour
	{
		[SerializeField]
		private Inspector inspectorTemplate;

		private readonly List<Inspector> stack = new List<Inspector>();

		public void InspectObject(object targetObject, InspectorConfig config = null)
		{
			if (targetObject == null) throw new ArgumentNullException(nameof(targetObject));
			if (targetObject.GetType().IsPrimitive)
			{
				throw new ArgumentException("Target object cannot be a primitive type", nameof(targetObject));
			}

			var inspector = Instantiate(inspectorTemplate, inspectorTemplate.transform.parent);
			stack.Add(inspector);
			inspector.gameObject.SetActive(true);
			inspector.InspectObject(targetObject, config);
			inspector.BackButton.onClick.AddListener(Back);
			inspector.HomeButton.onClick.AddListener(ClearStack);
		}

		public void Back()
		{
			if (stack.Count > 0)
			{
				var obj = stack[stack.Count - 1];
				stack.Remove(obj);
				Destroy(obj.gameObject);
			}
		}

		public void ClearStack()
		{
			foreach (var buttonList in stack)
			{
				Destroy(buttonList.gameObject);
			}

			stack.Clear();
		}
	}
}
