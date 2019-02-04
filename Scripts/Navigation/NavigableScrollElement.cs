using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Navigation
{
	public class NavigableScrollElement : MonoBehaviour, ISelectHandler
	{
		private static NavigableScrollElement tweeningElement;

		[SerializeField]
		private float scrollSpeed = 1000f;

		private ScrollRect scrollRect;
		private RectTransform rootCanvasTransform;
		private RectTransform scrollRectTransform;
		private float scrollRectTop;
		private float scrollRectBottom;

		private void Start()
		{
			scrollRect = GetComponentInParent<ScrollRect>();
			rootCanvasTransform = (RectTransform) GetComponentInParent<Canvas>().transform;
			scrollRectTransform = ((RectTransform) scrollRect.transform);

			// get world space corners of the scroll rect
			Vector3[] corners = new Vector3[4];
			scrollRectTransform.GetWorldCorners(corners);

			// now we know the min/max y values of the scroll rect in "canvas space"
			scrollRectTop = rootCanvasTransform.InverseTransformPoint(corners[1]).y;
			scrollRectBottom = rootCanvasTransform.InverseTransformPoint(corners[0]).y;
		}

		public void OnSelect(BaseEventData eventData)
		{
			// get world space corners of our own rect
			var corners = new Vector3[4];
			((RectTransform) transform).GetWorldCorners(corners);

			// get top and bottom y values
			var bottom = rootCanvasTransform.InverseTransformPoint(corners[0]).y;
			var top = rootCanvasTransform.InverseTransformPoint(corners[1]).y;

			// if it's not on screen then start tweening the scroll rect to a visible position
			if (top > scrollRectTop || bottom < scrollRectBottom)
			{
				StartCoroutine(TweenScrollRect(scrollRectTop - top, bottom - scrollRectBottom));
			}
		}

		private IEnumerator TweenScrollRect(float topDifference, float bottomDifference)
		{
			tweeningElement = this;

			var totalDelta = 0f;
			if (topDifference < 0f)
			{
				totalDelta = topDifference;
			}
			else if (bottomDifference < 0f)
			{
				totalDelta = -bottomDifference;
			}

			var startPosition = scrollRect.content.position.y;
			var endPosition = scrollRect.content.position.y + totalDelta;

			var currentPosition = scrollRect.content.position;
			var duration = Mathf.Abs(totalDelta) / scrollSpeed;
			var currentTime = 0f;

			while (currentTime < duration)
			{
				// If another element started to tween then let's break out.
				if (tweeningElement != this)
				{
					break;
				}
				currentTime += Time.deltaTime;

				var t = currentTime / duration;
				currentPosition.y = Mathf.Lerp(startPosition, endPosition, t);
				scrollRect.content.position = currentPosition;

				yield return null;
			}

			if (tweeningElement == this)
			{
				tweeningElement = null;
			}
			yield return null;
		}
	}
}