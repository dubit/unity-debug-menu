using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DUCK.DebugMenu
{
	/// <summary>
	/// Navigation Focus:
	/// This will focus the event systems selected item on the specified target object.
	/// Can be configured to happen automatically OnEnabled is called
	/// </summary>
	public class NavigationFocus : MonoBehaviour
	{
		[SerializeField]
		private GameObject target;

		[SerializeField]
		private bool focusOnEnable = true;

		private void OnEnable()
		{
			if (focusOnEnable)
			{
				Focus();
			}
		}

		public void Focus()
		{
			StartCoroutine(FocusCoroutine());
		}

		private IEnumerator FocusCoroutine()
		{
			yield return new WaitForEndOfFrame();
			EventSystem.current.SetSelectedGameObject(target);
		}
	}
}
