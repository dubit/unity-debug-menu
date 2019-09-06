using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DUCK.DebugMenu.Actions
{
	public class DebugMenuActionsPage : MonoBehaviour
	{
		[SerializeField]
		private string backCharacter = "←";

		[SerializeField]
		private string folderArrowCharacter = "▶";

		[SerializeField]
		private DebugMenu debugMenu;

		[SerializeField]
		private ButtonList buttonList;

		private DebugMenuItemNode rootNode;
		private DebugMenuItemNode currentPageNode;

		public void Init()
		{
			rootNode = new DebugMenuItemNode();
			currentPageNode = rootNode;
		}

		private void OnEnable()
		{
			currentPageNode = rootNode;
			SetupPage(currentPageNode);
		}

		/// <summary>
		/// Adds a new button to the debug menu that will invoke the specified action when clicked
		/// </summary>
		/// <param name="path">The path for the button, must be unique</param>
		/// <param name="action">The action to invoke when clicked</param>
		/// <param name="hideDebugMenuOnClick">A boolean used to control if the debug menu should hide when the button is clicked (defaults to true)</param>
		public void AddButton(string path, Action action, bool hideDebugMenuOnClick = true)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
			if (action == null) throw new ArgumentNullException(nameof(action));

			if (ButtonPathExists(path))
			{
				throw new Exception($"Cannot add button at {path}. A button/folder already exists!");
			}

			// Add the button!
			var parts = GetParts(path);
			var currentNode = rootNode;
			for (var i = 0; i < parts.Length; i++)
			{
				var isLast = i == parts.Length - 1;
				var part = parts[i];
				if (currentNode.ContainsChild(part))
				{
					currentNode = currentNode.GetChild(part);
				}
				else
				{
					currentNode =
						isLast ?
							currentNode.AddButton(part, action, hideDebugMenuOnClick) :
							currentNode.AddFolder(part);
				}
			}
		}

		/// <summary>
		/// Removes a button from the debug menu.
		/// </summary>
		/// <param name="path">The path of of the button to remove</param>
		public void RemoveButton(string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

			if (!ButtonPathExists(path))
			{
				throw new Exception($"Cannot remove the button {path}. It does not exist.");
			}

			var parts = GetParts(path);
			var nodes = new List<DebugMenuItemNode>();
			var currentNode = rootNode;
			nodes.Add(currentNode);
			foreach (var part in parts)
			{
				currentNode = currentNode.GetChild(part);
				nodes.Add(currentNode);
			}

			for (var i = nodes.Count - 2; i >= 0; i--)
			{
				var parent = nodes[i];
				var child = nodes[i + 1];
				if (child.ChildCount == 0)
				{
					parent.RemoveChild(child.Name);
				}
			}
		}

		private void SetupPage(DebugMenuItemNode node)
		{
			currentPageNode = node;

			buttonList.Clear();


			if (node.Parent != null)
			{
				buttonList.AddButton($"{backCharacter} Back", () => { SetupPage(node.Parent); });
			}

			foreach (var childNode in node.Children.Values)
			{
				var isFolder = childNode.IsFolder;
				var buttonText = childNode.Name + (isFolder ? $" {folderArrowCharacter}" : "");

				UnityAction actionFunc = () =>
				{
					if (isFolder)
					{
						SetupPage(childNode);
					}
					else
					{
						childNode.Action.Invoke();
						if (childNode.HideOnClick) debugMenu.Hide();
					}
				};

				buttonList.AddButton(buttonText, actionFunc);
			}
		}

		private bool ButtonPathExists(string path)
		{
			var parts = GetParts(path);
			var currentNode = rootNode;
			foreach (var part in parts)
			{
				if (currentNode.ContainsChild(part))
				{
					currentNode = currentNode.GetChild(part);
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		private string[] GetParts(string path)
		{
			return path.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}