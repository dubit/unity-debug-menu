using System;
using System.Collections.Generic;

namespace DUCK.DebugMenu.InspectorPage.Utils
{
	public class Selector<TIn, TOut>
	{
		private readonly Dictionary<Func<TIn, bool>, TOut> matchingFuncs =
			new Dictionary<Func<TIn, bool>, TOut>();
		private List<Func<TIn, bool>> matchinFuncsOrder = new List<Func<TIn, bool>>();

		public void AddMatcher(Func<TIn, bool> matchingFunc, TOut result)
		{
			matchingFuncs.Add(matchingFunc, result);
			matchinFuncsOrder.Insert(0, matchingFunc);
		}

		public void RemoveMatcher(Func<TIn, bool> matchingFunc)
		{
			matchingFuncs.Remove(matchingFunc);
			matchinFuncsOrder.Remove(matchingFunc);
		}

		public TOut Select(TIn input)
		{
			foreach (var matchinfFunc in matchinFuncsOrder)
			{
				if (matchinfFunc(input))
				{
					return matchingFuncs[matchinfFunc];
				}
			}

			return default(TOut);
		}
	}
}
