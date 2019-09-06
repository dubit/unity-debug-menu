using System;
using System.Collections.Generic;
using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage
{
	public class SceneHierarchyView : MonoBehaviour
	{
		[SerializeField]
		private SceneHierarchyViewStackElement template;

		private readonly List<SceneHierarchyViewStackElement> stack = new List<SceneHierarchyViewStackElement>();

		public event Action<object> OnInspectionRequested;

		public void ShowObjects(IEnumerable<GameObject> objects)
		{
			var stackElement = Instantiate(template, template.transform.parent);
			stackElement.gameObject.SetActive(true);

			stackElement.Init(
				objects,
				HandleObjectSelected,
				InspectObject,
				Back,
				ClearStack);

			stack.Add(stackElement);
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

		private void HandleObjectSelected(GameObject target)
		{
			var children = GetChildren(target);
			if (children.Count > 0)
			{
				ShowObjects(children);
			}
		}

		private void InspectObject(GameObject target)
		{
			OnInspectionRequested?.Invoke(target);
		}

		public void ClearStack()
		{
			for (var i = stack.Count - 1; i >= 0; i--)
			{
				var buttonList = stack[i];
				Destroy(buttonList.gameObject);
				stack.Remove(buttonList);
			}
		}

		private static List<GameObject> GetChildren(GameObject obj)
		{
			var result = new List<GameObject>();
			foreach (Transform child in obj.transform)
			{
				result.Add(child.gameObject);
			}

			return result;
		}
	}
}
