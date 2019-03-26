using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Navigation
{
	public class NavigationListBuilder : MonoBehaviour
	{
		private enum NavigationDirection
		{
			Vertical,
			Horizontal,
		}

		[SerializeField]
		private NavigationDirection direction;

		[SerializeField, Tooltip("Represents the element to select, to the left of the list in vertical mode, and below the list in horizontal mode")]
		private Selectable negativeSideElement;

		[SerializeField, Tooltip("Represents the element to select, to the right of the list in vertical mode, and above the list in horizontal mode")]
		private Selectable positiveSideElement;

		private void OnTransformChildrenChanged()
		{
			if (!enabled) return;

			Selectable previousItem = null;

			var parent = transform;
			for (var i = 0; i < parent.childCount; i++)
			{
				var child = parent.GetChild(i);
				if (!child.gameObject.activeSelf) continue;
				var thisItem = child.GetComponent<Selectable>();
				if (thisItem == null) continue;

				// first item in the list...
				if (previousItem == null)
				{
					HookFirstItemToStartElements(thisItem);
				}
				else
				{
					HookItemToPrevious(thisItem, previousItem);
				}

				previousItem = thisItem;
			}

			// Now finally link out of the end of the list
			if (previousItem != null)
			{
				// Hook to top of the list or top elements
			}
		}

		private void HookItemToPrevious(Selectable thisItem, Selectable previousItem)
		{
			var thisNavigation = thisItem.navigation;
			var previousNavigation = previousItem.navigation;

			// set both to explicit
			previousNavigation.mode = thisNavigation.mode = UnityEngine.UI.Navigation.Mode.Explicit;

			if (direction == NavigationDirection.Vertical)
			{
				previousNavigation.selectOnDown = thisItem;
				thisNavigation.selectOnUp = previousItem;

				thisNavigation.selectOnLeft = negativeSideElement;
				thisNavigation.selectOnRight = positiveSideElement;
			}
			else
			{	previousNavigation.selectOnRight = thisItem;
				thisNavigation.selectOnLeft = previousItem;

				thisNavigation.selectOnDown = negativeSideElement;
				thisNavigation.selectOnUp = positiveSideElement;
			}

			previousItem.navigation = previousNavigation;
			thisItem.navigation = thisNavigation;
		}

		private void HookFirstItemToStartElements(Selectable thisItem)
		{
			//
		}
	}
}