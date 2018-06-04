using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InfoPage
{
	public class InfoElement : MonoBehaviour
	{
		public Text Title { get { return title; } }
		public Text Value { get { return value; } }

		[SerializeField]
		private Text title;
		[SerializeField]
		private Text value;
	}
}
