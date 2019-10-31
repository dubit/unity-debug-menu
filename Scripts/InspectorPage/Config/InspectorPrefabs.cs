using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage.Config
{
	[CreateAssetMenu(menuName = "Create Built in prefabs")]
	public class InspectorPrefabs : ScriptableObject
	{
		[SerializeField]
		private GameObject simpleDataMember;
		public GameObject SimpleDataMember => simpleDataMember;

		[SerializeField]
		private GameObject dividerPrefab;
		public GameObject DividerPrefab => dividerPrefab;

		[SerializeField]
		private GameObject labelPrefab;
		public GameObject LabelPrefab => labelPrefab;
	}
}
