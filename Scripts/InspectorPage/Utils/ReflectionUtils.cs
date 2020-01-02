using System;
using System.Reflection;

namespace DUCK.DebugMenu.InspectorPage.Utils
{
	internal static class ReflectionUtils
	{
		public static bool IsDeprecated(this MemberInfo member)
		{
			return member.GetCustomAttribute<ObsoleteAttribute>() != null;
		}
	}
}
