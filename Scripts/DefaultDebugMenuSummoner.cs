using System;
using UnityEngine;

namespace DUCK.DebugMenu
{
	/// <summary>
	/// A simple implementation of IDebugMenuSummoner, that summons the debug menu when F7 is pressed, or on a device,
	/// when 5 fingers are held down
	/// </summary>
	public class DefaultDebugMenuSummoner : MonoBehaviour, IDebugMenuSummoner
	{
		private const float TOUCH_TIME_OUT = 2;
		private const int TOUCH_AMOUNT = 5;
		private const KeyCode ACTIVATE_KEY = KeyCode.F7;
		private float touchTime;
		private int index;

		public event Action OnSummonRequested;

		private int GetTouchCount()
		{
			var result = 0;
			var touchCount = Input.touchCount;

			for (var i = 0; i < touchCount; i++)
			{
				var touch = Input.GetTouch(i);
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					result++;
				}
			}

			return result;
		}
		
		private bool IsHeld()
		{
			touchTime = GetTouchCount() >= TOUCH_AMOUNT
				? touchTime + Time.deltaTime
				: 0;

			if (touchTime >= TOUCH_TIME_OUT)
			{
				touchTime = 0;
				return true;
			}

			return false;
		}

		private void Update()
		{
			if (IsHeld() || Input.GetKeyDown(ACTIVATE_KEY))
			{
				if (OnSummonRequested != null)
				{
					OnSummonRequested.Invoke();
				}
			}
		}
	}
}
