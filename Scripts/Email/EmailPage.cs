using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Email
{
	public class EmailPage : MonoBehaviour
	{
		[SerializeField]
		private Button backButton;
		[SerializeField]
		private Button sendEmailButton;
		[SerializeField]
		private Dropdown emailDropdown;
		[SerializeField]
		private TextAsset emailAddressesJson;
		[SerializeField]
		private InputField subjectText;
		[SerializeField]
		private Text bodyText;
		[SerializeField]
		private Image bodyImage;

		private Action onBack;

		private string[] emailAddresses;

		private void Awake()
		{
			var emailAddressesModel = JsonUtility.FromJson<EmailAddressesModel>(emailAddressesJson.text);
			emailAddresses = emailAddressesModel.emails;

			emailDropdown.AddOptions(emailAddresses.ToList());

			backButton.onClick.AddListener(() =>
			{
				gameObject.SetActive(false);
				onBack();
			});

			sendEmailButton.onClick.AddListener(SendEmail);
		}

		public void Show(string subject, string body, Action onBack)
		{
			this.onBack = onBack;
			gameObject.SetActive(true);
			subjectText.text = subject;
			bodyText.text = body;
		}

		public void Show(string subject, Texture body)
		{
		}

		private void SendEmail()
		{
			var email = emailDropdown.options[emailDropdown.value].text;

			var body = new StringBuilder();
			body.Append(bodyText.text);
			var mailTo = string.Format("mailto:{0}?subject={1}&body={2}", email, subjectText.text, bodyText.text);
			Application.OpenURL(mailTo);
		}
	}
}
