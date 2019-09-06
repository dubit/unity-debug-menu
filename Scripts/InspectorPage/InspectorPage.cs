using DUCK.DebugMenu;
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
		private SceneHierarchyView sceneView;

		[SerializeField]
		private ShelfView shelfView;

		[SerializeField]
		private InspectorStack inspectorStack;

		[SerializeField]
		private InspectorPrefabs builtInPrefabs;

		[SerializeField]
		private Button sceneViewButton;

		[SerializeField]
		private Button shelfViewButton;

		private void Awake()
		{
			InspectorConfig.Init(builtInPrefabs);

			sceneView.OnInspectionRequested += HandleSceneViewInspectionRequested;

			sceneViewButton.onClick.AddListener(ShowSceneView);
			shelfViewButton.onClick.AddListener(ShowPoolView);
		}

		private void ShowSceneView()
		{
			sceneView.gameObject.SetActive(true);

			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				var scene = SceneManager.GetSceneAt(i);
				var rootGameObjects = scene.GetRootGameObjects();
				sceneView.ShowObjects(rootGameObjects);
			}
		}

		private void ShowPoolView()
		{
			shelfView.gameObject.SetActive(true);

			shelfView.Init(
				Shelf.Items,
				obj => inspectorStack.InspectObject(obj),
				() => shelfView.gameObject.SetActive(false));
		}

		private void OnEnable()
		{
			sceneView.ClearStack();
			inspectorStack.ClearStack();
		}

		private void HandleSceneViewInspectionRequested(object obj)
		{
			inspectorStack.InspectObject(obj);
		}
	}
}