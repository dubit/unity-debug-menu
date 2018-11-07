using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Navigation
{
	/// <summary>
	/// NavigationBuilder:
	/// Place this script on a list, each time the children change (when buttons are instantiated)
	/// it will rebuild the navigation connections.
	/// (currently only supporting vertical).
	/// </summary>
	public class NavigationBuilder : MonoBehaviour
	{
		[SerializeField]
		private Selectable topElement;

		private void OnTransformChildrenChanged()
		{
			if (!enabled) return;

			bool foundFirst = false;

			var parent = transform;
			for (var i = 1; i < parent.childCount - 1; i++)
			{
				var child = parent.GetChild(i);
				if (!child.gameObject.activeSelf)
				{
					continue;
				}

				var next = parent.GetChild(i + 1);
				var thisItem = child.GetComponent<Selectable>();
				var nextItem = next.GetComponent<Selectable>();

				var thisNavigation = thisItem.navigation;
				var nextNavigation = nextItem.navigation;

				thisNavigation.mode = nextNavigation.mode = UnityEngine.UI.Navigation.Mode.Explicit;
				thisNavigation.selectOnDown = nextItem;
				nextNavigation.selectOnUp = thisItem;

				if (!foundFirst)
				{
					foundFirst = true;
					thisNavigation.selectOnUp = topElement;
					var logsButtonNavigation = topElement.navigation;
					logsButtonNavigation.selectOnDown = thisItem;
					topElement.navigation = logsButtonNavigation;
				}

				thisItem.navigation = thisNavigation;
				nextItem.navigation = nextNavigation;
			}
		}
	}
}