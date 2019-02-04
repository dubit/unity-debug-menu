using System;
using System.Collections.Generic;
using DUCK.DebugMenu.Email;
using DUCK.DebugMenu.InfoPage;
using DUCK.DebugMenu.Logger;
using DUCK.DebugMenu.Navigation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DUCK.DebugMenu
{
	/// <summary>
	/// A script that provides access and a simple API to control the debug menu.
	///
	/// DUCK ships with a ready-made prefab with this script already configured, just drop it into your startup scene,
	/// then access it via the singleton.
	///
	/// To bring up the DebugMenu use an IDebugMenuSummoner and register it with the DebugMenu via AddSummoner,
	/// or add it to the same object as the debug menu, and it will find it on Start(). The ready-ade prefab that ships
	/// with DUCK, has a DefaultDebugMenuSummoner. If you want to use your own logic to summon the debug menu then
	/// implement your own IDebugMenuSummoner and register it.
	/// </summary>
	public class DebugMenu : MonoBehaviour
	{
		private static DebugMenu instance;
		public static DebugMenu Instance
		{
			get
			{
				if (instance != null)
				{
					return instance;
				}

				throw new Exception("Instance is null (it must be initialized first)");
			}
		}

		public EmailPage EmailPage { get { return emailPage; } }

		[SerializeField]
		private GameObject rootObject;

		[Header("Buttons")]
		[SerializeField]
		private Button closeButton;

		[SerializeField]
		private Button infoPageButton;

		[SerializeField]
		private Button logPageButton;

		[SerializeField]
		private Button actionButtonTemplate;

		[Header("Pages")]
		[SerializeField]
		private InformationPage infoPage;
		[SerializeField]
		private DebugLogPage logPage;
		[SerializeField]
		private EmailPage emailPage;

		[Header("Navigation")]
		[SerializeField]
		private bool useNavigation;

		private DebugMenuItemNode rootNode;
		private DebugMenuItemNode currentPageNode;

		public event Action OnShow;
		public event Action OnHide;

		private void Awake()
		{
			if (instance != null) throw new Exception("Cannot have more than one Instance of DebugMenu active");
			instance = this;

			rootObject.gameObject.SetActive(false);
			rootNode = new DebugMenuItemNode();
			currentPageNode = rootNode;

			// When running tests you cannot use DontDestroyOnLoad in editor mode
			if (Application.isPlaying)
			{
				DontDestroyOnLoad(this);
			}
		}

		private void Start()
		{
			// Find any summoners added to the same object
			var summoners = GetComponents<IDebugMenuSummoner>();

			foreach (var summoner in summoners)
			{
				AddSummoner(summoner);
			}

			logPage.Initialize();
			infoPage.Initialize();

			if (!useNavigation)
			{
				var navComponents = new List<MonoBehaviour>();
				navComponents.AddRange(GetComponentsInChildren<NavigationBuilder>(true));
				navComponents.AddRange(GetComponentsInChildren<NavigationFocus>(true));
				navComponents.AddRange(GetComponentsInChildren<NavigationLinker>(true));
				navComponents.AddRange(GetComponentsInChildren<NavigableScrollElement>(true));
				navComponents.ForEach(c => c.enabled = false);
			}
		}

		/// <summary>
		/// Shows the debug menu
		/// </summary>
		public void Show()
		{
			rootObject.SetActive(true);

			currentPageNode = rootNode;
			SetupPage(currentPageNode);

			if (useNavigation)
			{
				EventSystem.current.SetSelectedGameObject(closeButton.gameObject);
			}

			if (OnShow != null)
			{
				OnShow.Invoke();
			}
		}

		/// <summary>
		/// Hides the debug menu
		/// </summary>
		public void Hide()
		{
			rootObject.SetActive(false);

			if (OnHide != null)
			{
				OnHide.Invoke();
			}
		}

		public void ShowInfo()
		{
			infoPage.gameObject.SetActive(true);
		}

		/// <summary>
		/// Displays the game log
		/// </summary>
		public void ShowGameLog()
		{
			logPage.gameObject.SetActive(true);
		}

		/// <summary>
		/// Adds a new debug menu summoner
		/// </summary>
		public void AddSummoner(IDebugMenuSummoner summoner)
		{
			if (summoner == null) throw new ArgumentNullException("summoner");
			summoner.OnSummonRequested += Show;
		}

		/// <summary>
		/// Removes a debug menu summoner
		/// </summary>
		public void RemoveSummoner(IDebugMenuSummoner summoner)
		{
			if (summoner == null) throw new ArgumentNullException("summoner");
			summoner.OnSummonRequested -= Show;
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

		private string[] GetParts(string path)
		{
			return path.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);
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

		private void SetupPage(DebugMenuItemNode node)
		{
			currentPageNode = node;
			foreach (Transform child in actionButtonTemplate.transform.parent)
			{
				if (child != actionButtonTemplate.transform)
				{
					Destroy(child.gameObject);
				}
			}

			GameObject firstChild = null;

			if (node.Parent != null)
			{
				var backButton = Instantiate(actionButtonTemplate, actionButtonTemplate.transform.parent, false);
				backButton.GetComponentInChildren<Text>().text = "← Back";
				backButton.onClick.AddListener(() => { SetupPage(node.Parent); });
				backButton.gameObject.SetActive(true);

				firstChild = backButton.gameObject;
			}

			foreach (var button in node.Children.Values)
			{
				var actionButton = Instantiate(actionButtonTemplate, actionButtonTemplate.transform.parent, false);
				if (button.IsFolder)
				{
					actionButton.onClick.AddListener(() => { SetupPage(button); });
				}
				else
				{
					actionButton.onClick.AddListener(() =>
					{
						button.Action.Invoke();
						if (button.HideOnClick) Hide();
					});
				}

				var label = actionButton.GetComponentInChildren<Text>();
				label.text = button.Name + (button.IsFolder ? " ▶" : "");
				actionButton.gameObject.SetActive(true);

				firstChild = (firstChild != null) ? firstChild : actionButton.gameObject;
			}

			if (useNavigation && firstChild != null)
			{
				EventSystem.current.SetSelectedGameObject(firstChild);
			}
		}
	}
}

