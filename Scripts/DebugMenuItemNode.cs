using System;
using System.Collections.Generic;

namespace DUCK.DebugMenu
{
	public enum NodeType
	{
		Button,
		Folder,
	}

	/// <summary>
	/// A node used to build a hierarchy of debug menu items
	/// </summary>
	public class DebugMenuItemNode
	{
		public string Name { get; }
		public NodeType Type { get; }
		public bool IsFolder => Type == NodeType.Folder;
		public bool IsButton => Type == NodeType.Button;
		public Dictionary<string, DebugMenuItemNode> Children { get; private set; }
		public int ChildCount => Children.Count;
		public DebugMenuItemNode Parent { get; }
		public Action Action { get; }
		public bool HideOnClick { get; private set; }
		public bool ContainsChild(string part) => Children.ContainsKey(part);
		public DebugMenuItemNode GetChild(string part) => Children[part];

		public DebugMenuItemNode(NodeType type = NodeType.Folder, string name = null, DebugMenuItemNode parent = null, Action action = null)
		{
			Type = type;
			Children = new Dictionary<string, DebugMenuItemNode>();
			Parent = parent;
			Name = name;
			Action = action;
		}

		public DebugMenuItemNode AddFolder(string name)
		{
			if (ContainsChild(name))
			{
				throw new Exception($"Cannot add with the name \"{name}\", it already exists");
			}

			var child = new DebugMenuItemNode(NodeType.Folder, name, this);
			Children.Add(name, child);
			return child;
		}

		public DebugMenuItemNode AddButton(string name, Action action, bool hideDebugMenuOnClick = true)
		{
			if (ContainsChild(name))
			{
				throw new Exception($"Cannot add with the name \"{name}\", it already exists");
			}

			var child = new DebugMenuItemNode(NodeType.Button, name, this, action);
			child.HideOnClick = hideDebugMenuOnClick;
			Children.Add(name, child);
			return child;
		}

		public void RemoveChild(string childName)
		{
			if (!ContainsChild(childName))
			{
				throw new Exception($"Cannot remove child {childName}, it does not exist");
			}

			Children.Remove(childName);
		}
	}
}