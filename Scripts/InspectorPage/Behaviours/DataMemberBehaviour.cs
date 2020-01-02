using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InspectorPage.Behaviours
{
	public class DataMemberBehaviour : MonoBehaviour
	{
		[SerializeField]
		private Text memberNameText;

		[SerializeField]
		private Text memberValueText;

		public void Init(MemberInfo member, object targetObject)
		{
			memberNameText.text = member.Name;

			try
			{
				switch (member)
				{
					case FieldInfo fieldInfo:
						memberValueText.text = $"{fieldInfo.GetValue(targetObject)}";
						break;
					case PropertyInfo propertyInfo:
						memberValueText.text = $"{propertyInfo.GetValue(targetObject)}";
						break;
				}
			}
			catch (Exception e)
			{
				memberValueText.text = e.Message;
			}
		}
	}
}
