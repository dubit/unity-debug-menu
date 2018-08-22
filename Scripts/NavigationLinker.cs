using System;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu
{
	public class NavigationLinker : MonoBehaviour
	{
		enum Direction
		{
			Up,
			Down,
			Left,
			Right,
		}

		[SerializeField]
		private Direction direction;

		[SerializeField]
		private Selectable from;

		[SerializeField]
		private Selectable to;

		private void OnEnable()
		{
			var navigation = from.navigation;

			switch (direction)
			{
				case Direction.Up:
					navigation.selectOnUp = to;
					break;
				case Direction.Down:
					navigation.selectOnDown = to;
					break;
				case Direction.Left:
					navigation.selectOnLeft = to;
					break;
				case Direction.Right:
					navigation.selectOnRight = to;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			from.navigation = navigation;
		}
	}
}