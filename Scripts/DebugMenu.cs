using System;
using System.Collections.Generic;
using DUCK.DebugMenu.Email;
using DUCK.DebugMenu.InfoPage;
using DUCK.DebugMenu.Logger;
using UnityEngine;
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

		[SerializeField]
		private Button actionButtonTemplate;

		[Header("Pages")]
		[SerializeField]
		private InformationPage infoPage;
		[SerializeField]
		private DebugLogPage logPage;
		[SerializeField]
		private EmailPage emailPage;

		private readonly Dictionary<string, Button> buttons = new Dictionary<string, Button>();

		public event Action OnShow;
		public event Action OnHide;

		private void Awake()
		{
			if (instance != null) throw new Exception("Cannot have more than one Instance of DebugMenu active");
			instance = this;

			rootObject.gameObject.SetActive(false);

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
		}

		/// <summary>
		/// Shows the debug menu
		/// </summary>
		public void Show()
		{
			rootObject.SetActive(true);

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

		/// <summary>
		/// Displays the BuildInfo (from build manifest)
		/// </summary>
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
		/// <param name="text">The text for the button, (this works like an id must be unique to all buttons)</param>
		/// <param name="action">The action to invoke when clicked</param>
		/// <param name="hideDebugMenuOnClick">A boolean used to control if the debug menu should hide when the button is clicked (defaults to true)</param>
		public void AddButton(string text, Action action, bool hideDebugMenuOnClick = true)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");
			if (action == null) throw new ArgumentNullException("action");

			if (!buttons.ContainsKey(text))
			{
				var actionButton = Instantiate(actionButtonTemplate);
				actionButton.transform.SetParent(actionButtonTemplate.transform.parent, false);
				actionButton.onClick.AddListener(() =>
				{
					action.Invoke();
					if (hideDebugMenuOnClick)
					{
						Hide();
					}
				});
				actionButton.GetComponentInChildren<Text>().text = text;
				actionButton.gameObject.SetActive(true);
				buttons.Add(text, actionButton);
			}
		}

		/// <summary>
		/// Updates a button in the debug menu.
		/// </summary>
		/// <param name="oldText">The text of the button to update</param>
		/// <param name="newText">The new text of the button</param>
		/// <param name="newAction">An optional replacement of the button onClick</param>
		/// <param name="hideDebugMenuOnClick">Optional bool if the debug menu should hide when the button is clicked</param>
		public void UpdateButton(string oldText, string newText, Action newAction = null, bool hideDebugMenuOnClick = true)
		{
			if (string.IsNullOrEmpty(oldText)) throw new ArgumentNullException("oldText");
			if (string.IsNullOrEmpty(newText)) throw new ArgumentNullException("newText");

			Button button;
			if (!buttons.TryGetValue(oldText, out button)) return;

			buttons.Remove(oldText);
			button.GetComponentInChildren<Text>().text = newText;
			buttons.Add(newText, button);

			if (newAction != null)
			{
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(() =>
				{
					newAction.Invoke();

					if (hideDebugMenuOnClick)
					{
						Hide();
					}
				});
			}
		}

		/// <summary>
		/// Removes a button from the debug menu.
		/// </summary>
		/// <param name="text">The text of the button to remove, (this works like an id must be unique to all buttons)</param>
		public void RemoveButton(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentNullException("text");

			if (buttons.ContainsKey(text))
			{
				Destroy(buttons[text].gameObject);
				buttons.Remove(text);
			}
		}
	}
}
