using UnityEngine;

namespace DUCK.DebugMenu.InfoPage
{
	public abstract class AbstractInfoSubPage : MonoBehaviour
	{
		private const int DEFAULT_CHILDREN = 2;

		[SerializeField]
		private RectTransform container;
		[SerializeField]
		private InfoElement elementTemplate;
		[SerializeField]
		private GameObject seperator;

		public abstract void Initialize();

		protected virtual void Awake()
		{
			elementTemplate.gameObject.SetActive(false);
			seperator.gameObject.SetActive(false);
		}

		public void AddElement(string title, string value)
		{
			if (seperator && container.transform.childCount > DEFAULT_CHILDREN)
			{
				var newSeperator = Instantiate(seperator, container);
				newSeperator.gameObject.SetActive(true);
			}

			var infoElement = Instantiate(elementTemplate, container);
			infoElement.Title.text = title;
			if (!string.IsNullOrEmpty(value))
			{
				infoElement.Value.text = value;
			}

			infoElement.gameObject.SetActive(true);
		}
	}
}
