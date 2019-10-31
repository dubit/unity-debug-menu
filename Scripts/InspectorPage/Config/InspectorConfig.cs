using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DUCK.DebugMenu.InspectorPage.Utils;
using UnityEngine;

namespace DUCK.DebugMenu.InspectorPage.Config
{
	public class InspectorConfig
	{
		public static Selector<object, InspectorConfig> ConfigSelector { get; } = new Selector<object, InspectorConfig>();

		public static void Init(InspectorPrefabs prefabs)
		{
			ConfigSelector.AddMatcher(o => true, GetDefaultConfig(prefabs));
			ConfigSelector.AddMatcher(o => o is GameObject, GetGameObjectConfig(prefabs));
		}

		public bool IncludeFields { get; set; } = true;
		public bool IncludeProperties { get; set; } = true;

		public Selector<MemberInfo, GameObject> PrefabSelector { get; } = new Selector<MemberInfo, GameObject>();

		public virtual IEnumerable<IDrawInstruction> GetDrawInstructions(object targetObject)
		{
			var result = new List<IDrawInstruction>();

			var drawableMembers = GetDrawableMembers(targetObject);

			foreach (var member in drawableMembers)
			{
				var prefab = PrefabSelector.Select(member);

				var instruction = new MemberDrawInstruction
				{
					Member = member,
					Prefab = prefab,
					TargetObject = targetObject
				};

				result.Add(instruction);
			}

			return result;
		}

		private IEnumerable<MemberInfo> GetDrawableMembers(object targetObject)
		{
			var result = new List<MemberInfo>();

			var members = GetAllInstanceMembers(targetObject.GetType());

			if (IncludeProperties)
			{
				result.AddRange(
					members.Where(m =>
						m is PropertyInfo propertyInfo && propertyInfo.GetIndexParameters().Length == 0));
			}

			if (IncludeFields)
			{
				result.AddRange(members.Where(m => m.MemberType == MemberTypes.Field));
			}

			return result
				.Where(m => !m.IsDeprecated());
		}

		private List<MemberInfo> GetAllInstanceMembers(Type type)
		{
			var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

			var fieldInfos = type.GetMembers(bindingFlags).ToList();

			var currentType = type;
			while (currentType.BaseType != typeof(object))
			{
				if (currentType.BaseType == null) break;
				fieldInfos.AddRange(currentType.BaseType.GetFields(bindingFlags));
				currentType = currentType.BaseType;
			}

			return fieldInfos;
		}

		private static InspectorConfig GetDefaultConfig(InspectorPrefabs prefabs)
		{
			var config = new InspectorConfig();
			config.PrefabSelector.AddMatcher(m => true, prefabs.SimpleDataMember);
			return config;
		}

		private static InspectorConfig GetGameObjectConfig(InspectorPrefabs prefabs)
		{
			var config = new GameObjectInspectorConfig();
			config.DividerPrefab = prefabs.DividerPrefab;
			config.LabelPrefab = prefabs.LabelPrefab;
			config.PrefabSelector.AddMatcher(m => true, prefabs.SimpleDataMember);
			return config;
		}
	}
}