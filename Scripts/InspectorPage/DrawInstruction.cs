using System.Reflection;
using DUCK.DebugMenu.InspectorPage.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage
{
	public interface IDrawInstruction
	{
		void Run(Transform parent);
	}

	public class MemberDrawInstruction : IDrawInstruction
	{
		public GameObject Prefab { get; set; }
		public MemberInfo Member { get; set; }
		public object TargetObject { get; set; }

		public void Run(Transform parent)
		{
			var prefab = Object.Instantiate(Prefab, parent);
			var memberFieldBehaviour = prefab.GetComponent<DataMemberBehaviour>();
			memberFieldBehaviour.Init(Member, TargetObject);
		}
	}

	public class SimpleDrawInstruction : IDrawInstruction
	{
		public GameObject Prefab { get; set; }

		public void Run(Transform parent)
		{
			Object.Instantiate(Prefab, parent);
		}
	}

	public class LabelDrawInstruction : IDrawInstruction
	{
		public GameObject Prefab { get; set; }
		public string Text { get; set; }

		public void Run(Transform parent)
		{
			var prefab = Object.Instantiate(Prefab, parent);
			var text = prefab.GetComponentInChildren<Text>();
			text.text = Text;
		}
	}
}