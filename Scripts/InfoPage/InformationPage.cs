using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InfoPage
{
	public class InformationPage : AbstractDebugMenuTabPage
	{
		[Header("Navigation Buttons")]
		[SerializeField]
		private Button buildInfoButton;
		[SerializeField]
		private Button deviceInfoButton;
		[FormerlySerializedAs("cloudBuildInfoPage")]
		[Header("Pages")]
		[SerializeField]
		private AbstractInfoSubPage buildInfoPage;
		[SerializeField]
		private DeviceInfoPage deviceInfoPage;

		private void Awake()
		{
			buildInfoButton.onClick.AddListener(ShowBuildInfo);
			deviceInfoButton.onClick.AddListener(ShowDeviceInfo);

			ShowBuildInfo();

			deviceInfoPage.Initialize();
			buildInfoPage.Initialize();
		}

		private void ShowBuildInfo()
		{
			buildInfoPage.gameObject.SetActive(true);
			deviceInfoPage.gameObject.SetActive(false);
			buildInfoButton.interactable = false;
			deviceInfoButton.interactable = true;
		}

		private void ShowDeviceInfo()
		{
			buildInfoPage.gameObject.SetActive(false);
			deviceInfoPage.gameObject.SetActive(true);
			buildInfoButton.interactable = true;
			deviceInfoButton.interactable = false;
		}
	}
}
