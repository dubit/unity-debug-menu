using System.Collections.Generic;
using DUCK.DebugMenu.InspectorPage.Config;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage
{
	public class InspectorPage : AbstractDebugMenuTabPage
	{
		public InspectorShelf Shelf { get; } = new InspectorShelf();

		[SerializeField]
		private InspectorPrefabs builtInPrefabs;

		[SerializeField]
		private InspectorStack stack;

		[SerializeField]
		private Button homeButton;

		[SerializeField]
		private Button backStackButton;

		[SerializeField]
		private Button sceneViewButton;

		[SerializeField]
		private Button shelfViewButton;

		private void Awake()
		{
			InspectorConfig.Init(builtInPrefabs);
		}

		private void Start()
		{
			sceneViewButton.onClick.AddListener(ShowSceneView);
			shelfViewButton.onClick.AddListener(ShowShelfView);
			homeButton.onClick.AddListener(stack.ClearStack);
			backStackButton.onClick.AddListener(stack.Back);
			UpdateButtonStates();

			stack.OnStackChanged += UpdateButtonStates;
		}

		private void UpdateButtonStates()
		{
			backStackButton.interactable = !stack.IsEmpty;
			homeButton.interactable = !stack.IsEmpty;
		}

		private void ShowSceneView()
		{
			var rootGameObjects = new List<GameObject>();
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				rootGameObjects.AddRange(scene.GetRootGameObjects());
			}
			stack.AddSceneHierarchyView(rootGameObjects);
		}

		private void ShowShelfView()
		{
			stack.AddShelfView(Shelf);
		}

		private void OnEnable()
		{
			stack.ClearStack();
		}
	}
}