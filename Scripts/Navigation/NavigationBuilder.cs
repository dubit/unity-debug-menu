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
		private Selectable[] topElements;

		[SerializeField]
		private bool topElementsJumpToBottomOfList;

		[SerializeField]
		private Selectable leftElement;

		[SerializeField]
		private Selectable rightElement;

		private void OnTransformChildrenChanged()
		{
			if (!enabled) return;

			Selectable previousItem = null;

			var parent = transform;
			for (var i = 1; i < parent.childCount; i++)
			{
				var child = parent.GetChild(i);

				var thisItem = child.GetComponent<Selectable>();
				var thisNavigation = thisItem.navigation;

				if (previousItem == null)
				{
					// no previous (this is the first element)
					thisNavigation.selectOnUp = topElements[0];
					if (!topElementsJumpToBottomOfList)
					{
						HookTopElementsDownTo(thisItem);
					}
				}
				else
				{
					var previousNavigation = previousItem.navigation;
					previousNavigation.mode = thisNavigation.mode = UnityEngine.UI.Navigation.Mode.Explicit;
					previousNavigation.selectOnDown = thisItem;
					thisNavigation.selectOnUp = previousItem;

					thisNavigation.selectOnLeft = leftElement;
					thisNavigation.selectOnRight = rightElement;
					previousItem.navigation = previousNavigation;
				}

				thisItem.navigation = thisNavigation;
				previousItem = thisItem;
			}

			if (topElementsJumpToBottomOfList)
			{
				HookTopElementsDownTo(previousItem);
			}
		}

		private void HookTopElementsDownTo(Selectable item)
		{
			foreach (var topElement in topElements)
			{
				var topElementNavigation = topElement.navigation;
				topElementNavigation.selectOnDown = item;
				topElement.navigation = topElementNavigation;
			}
		}
	}
}