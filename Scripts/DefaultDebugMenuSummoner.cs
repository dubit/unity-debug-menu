using System;
using UnityEngine;

namespace DUCK.DebugMenu
{
	/// <summary>
	/// A simple implementation of IDebugMenuSummoner, that summons the debug menu when F7 is pressed, or on a device,
	/// when 6 fingers are pressed down
	/// </summary>
	public class DefaultDebugMenuSummoner : MonoBehaviour, IDebugMenuSummoner
	{
		private const float TOUCH_TIME_OUT = 2;
		private const int TOUCH_AMOUNT = 5;
		private const KeyCode ACTIVATE_KEY = KeyCode.F7;
		private float touchTime;
		private int index;

		public event Action OnSummonRequested;

		private bool IsHeld()
		{
			var touches = Input.touches;

			foreach (var touch in touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					index++;
				}
				else if (touch.phase == TouchPhase.Ended)
				{
					index = 0;
				}
			}
			
			if(index == TOUCH_AMOUNT)
			{
				touchTime += Time.deltaTime;

				if (touchTime >= TOUCH_TIME_OUT)
				{
					index = 0;
					touchTime = 0;
					
					return true;
				}
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
