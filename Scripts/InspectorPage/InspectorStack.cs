using System;
using System.Collections.Generic;
using DUCK.DebugMenu.InspectorPage.Config;
using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage
{
	public class InspectorStack : MonoBehaviour
	{
		[Serializable]
		class Templates
		{
			[SerializeField]
			public SceneHierarchyViewStackElement sceneHierarchyView;

			[SerializeField]
			public InspectorView inspectorView;

			[SerializeField]
			public ShelfView shelfView;
		}

		public bool IsEmpty => stack.Count == 0;

		[SerializeField]
		private Templates templates;

		[SerializeField]
		private Transform parent;

		private readonly List<GameObject> stack = new List<GameObject>();

		public event Action OnStackChanged;

		public void AddInspectorView(object targetObject)
		{
			AddInspectorView(targetObject, null);
		}

		public void AddInspectorView(object targetObject, InspectorConfig config)
		{
			if (targetObject == null) throw new ArgumentNullException(nameof(targetObject));
			if (targetObject.GetType().IsPrimitive)
			{
				throw new ArgumentException("Target object cannot be a primitive type", nameof(targetObject));
			}

			var stackElement = Instantiate(templates.inspectorView, parent);
			stackElement.gameObject.SetActive(true);

			stackElement.InspectObject(targetObject, config);
			stack.Add(stackElement.gameObject);
			OnStackChanged?.Invoke();
		}

		public void AddShelfView(InspectorShelf shelf)
		{
			var stackElement = Instantiate(templates.shelfView, parent);
			stackElement.gameObject.SetActive(true);

			stackElement.Init(shelf.Items, AddInspectorView);

			stack.Add(stackElement.gameObject);
			OnStackChanged?.Invoke();
		}

		public void AddSceneHierarchyView(IEnumerable<GameObject> objects)
		{
			var stackElement = Instantiate(templates.sceneHierarchyView, parent);
			stackElement.gameObject.SetActive(true);

			stackElement.Init(
				objects,
				target =>
				{
					var children = new List<GameObject>();
					foreach (Transform child in target.transform)
					{
						children.Add(child.gameObject);
					}

					if (children.Count > 0)
					{
						AddSceneHierarchyView(children.ToArray());
					}
				},
				AddInspectorView);

			stack.Add(stackElement.gameObject);
			OnStackChanged?.Invoke();
		}

		public void Back()
		{
			if (stack.Count > 0)
			{
				var obj = stack[stack.Count - 1];
				stack.Remove(obj);
				Destroy(obj);
				OnStackChanged?.Invoke();
			}
		}

		public void ClearStack()
		{
			for (var i = stack.Count - 1; i >= 0; i--)
			{
				var element = stack[i];
				Destroy(element);
				stack.Remove(element);
			}
			OnStackChanged?.Invoke();
		}
	}
}