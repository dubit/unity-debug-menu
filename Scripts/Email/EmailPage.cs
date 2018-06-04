using System;
using System.Linq;
using System.Text;
using DUCK.DebugMenu.Email.Body;
using DUCK.DebugMenu.InfoPage;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Email
{
	public class EmailPage : MonoBehaviour
	{
		public const string SEPERATOR = "----------------------------------";

		[Header("Email Addresses")]
		[SerializeField]
		private TextAsset emailAddressesJson;
		[Header("Components")]
		[SerializeField]
		private CloudBuildInfoPage cloudBuildInfoPage;
		[SerializeField]
		private DeviceInfoPage deviceInfoPage;
		[SerializeField]
		private Button backButton;
		[SerializeField]
		private Button sendEmailButton;
		[SerializeField]
		private Dropdown emailDropdown;
		[SerializeField]
		private InputField subjectText;
		[SerializeField]
		private Text bodyText;

		private Action onBack;

		private void Awake()
		{
			var emailAddressesModel = JsonUtility.FromJson<EmailAddressesModel>(emailAddressesJson.text);
			var emailAddresses = emailAddressesModel.Emails;

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
			bodyText.text = GenerateBody(body);
		}

		private string GenerateBody(string submittedBody)
		{
			var body = new StringBuilder();
			body.Append(CloudBuildInfo.Generate(cloudBuildInfoPage.BuildManifest));
			body.Append(submittedBody);
			body.Append(DeviceInfo.Generate(deviceInfoPage.DeviceInfoManifest));
			return body.ToString();
		}

		private void SendEmail()
		{
			var email = emailDropdown.options[emailDropdown.value].text;
			var mailTo = string.Format("mailto:{0}?subject={1}&body={2}", email, Uri.EscapeUriString(subjectText.text), Uri.EscapeUriString(bodyText.text));
			Application.OpenURL(mailTo);
		}
	}
}
