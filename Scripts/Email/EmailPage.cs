using System;
using System.Linq;
using System.Text;
using DUCK.DebugMenu.CloudBuild;
using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.Email
{
	public class EmailPage : MonoBehaviour
	{
		[SerializeField]
		private CloudBuildPage cloudBuildPage;
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
			bodyText.text = GenerateBody(body);
		}

		private void SendEmail()
		{
			var email = emailDropdown.options[emailDropdown.value].text;
			var mailTo = string.Format("mailto:{0}?subject={1}&body={2}", email, Uri.EscapeUriString(subjectText.text), Uri.EscapeUriString(bodyText.text));
			Application.OpenURL(mailTo);
		}

		private string GenerateBody(string submittedBody)
		{
			var body = new StringBuilder(GetCloudBuildData());
			body.Append(submittedBody);
			body.Append(GenerateDeviceInfo());
			return body.ToString();
		}

		private string GetCloudBuildData()
		{
			var buildManifest = cloudBuildPage.BuildManifest;
			var buildManifestIsAvailable = buildManifest != null;
			var stringBuilder = new StringBuilder("----------------------------------\n");

			stringBuilder.Append("Cloud Build Target Name: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.CloudBuildTargetName);
			stringBuilder.Append("\n");

			stringBuilder.Append("Project Id: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.ProjectId);
			stringBuilder.Append("\n");

			stringBuilder.Append("Bundle Id: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.BundleId);
			stringBuilder.Append("\n");

			stringBuilder.Append("Build Version: ");
			stringBuilder.Append(Application.version);
			stringBuilder.Append("\n");

			stringBuilder.Append("Build Number: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.BuildNumber);
			stringBuilder.Append("\n");

			stringBuilder.Append("Branch: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.ScmBranch);
			stringBuilder.Append("\n");

			stringBuilder.Append("Unity Version: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.UnityVersion);
			stringBuilder.Append("\n");

#if !UNITY_IPHONE
			stringBuilder.Append("xCode Version: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.XcodeVersion);
			stringBuilder.Append("\n");
#endif
			stringBuilder.Append("Commit Id: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.ScmCommitId);
			stringBuilder.Append("\n");

			stringBuilder.Append("Build Start Time: ");
			if (buildManifestIsAvailable) stringBuilder.Append(buildManifest.BuildStartTime);
			stringBuilder.Append("\n");
			stringBuilder.Append("\n");

			return stringBuilder.ToString();
		}

		private string GenerateDeviceInfo()
		{
			var stringBuilder = new StringBuilder("----------------------------------\n");

			stringBuilder.Append("Device Name: ");
			stringBuilder.Append(SystemInfo.deviceName);
			stringBuilder.Append("\n");

			stringBuilder.Append("Device Model: ");
			stringBuilder.Append(SystemInfo.deviceModel);
			stringBuilder.Append("\n");

			stringBuilder.Append("Device Type: ");
			stringBuilder.Append(SystemInfo.deviceType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Operating System: ");
			stringBuilder.Append(SystemInfo.operatingSystem);
			stringBuilder.Append("\n");

			stringBuilder.Append("Operating System Family: ");
			stringBuilder.Append(SystemInfo.operatingSystemFamily);
			stringBuilder.Append("\n");

			stringBuilder.Append("System Memory Size: ");
			stringBuilder.Append(SystemInfo.systemMemorySize);
			stringBuilder.Append("\n");

			stringBuilder.Append("Processor Type: ");
			stringBuilder.Append(SystemInfo.processorType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Processor Count: ");
			stringBuilder.Append(SystemInfo.processorCount);
			stringBuilder.Append("\n");

			stringBuilder.Append("Screen Resolution: ");
			stringBuilder.Append(Screen.width);
			stringBuilder.Append("x");
			stringBuilder.Append(Screen.height);
			stringBuilder.Append("\n");

			stringBuilder.Append("Screen Aspect Ratio: ");
			stringBuilder.Append((float)Screen.width / Screen.height);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Device Name: ");
			stringBuilder.Append(SystemInfo.graphicsDeviceName);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics DeviceType: ");
			stringBuilder.Append(SystemInfo.graphicsDeviceType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Device Vendor: ");
			stringBuilder.Append(SystemInfo.graphicsDeviceVendor);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Memory Size: ");
			stringBuilder.Append(SystemInfo.graphicsMemorySize);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Shader Level: ");
			stringBuilder.Append(SystemInfo.graphicsShaderLevel);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Multi Threaded: ");
			stringBuilder.Append(SystemInfo.graphicsMultiThreaded);
			stringBuilder.Append("\n");

			stringBuilder.Append("Battery Status: ");
			stringBuilder.Append(SystemInfo.batteryStatus);
			stringBuilder.Append("\n");

			stringBuilder.Append("Battery Level: ");
			stringBuilder.Append(SystemInfo.batteryLevel * 100);
			stringBuilder.Append("%");
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Vibration: ");
			stringBuilder.Append(SystemInfo.supportsVibration);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Shadows: ");
			stringBuilder.Append(SystemInfo.supportsShadows);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Instancing: ");
			stringBuilder.Append(SystemInfo.supportsInstancing);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Location Service: ");
			stringBuilder.Append(SystemInfo.supportsLocationService);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Accelerometer: ");
			stringBuilder.Append(SystemInfo.supportsAccelerometer);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Gyroscope: ");
			stringBuilder.Append(SystemInfo.supportsGyroscope);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Audio: ");
			stringBuilder.Append(SystemInfo.supportsAudio);
			stringBuilder.Append("\n");

			return stringBuilder.ToString();
		}
	}
}
