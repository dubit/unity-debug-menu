using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.InfoPage
{
	public class InformationPage : MonoBehaviour
	{
		[Header("Navigation Buttons")]
		[SerializeField]
		private Button buildInfoButton;
		[SerializeField]
		private Button deviceInfoButton;
		[Header("Pages")]
		[SerializeField]
		private CloudBuildInfoPage cloudBuildInfoPage;
		[SerializeField]
		private DeviceInfoPage deviceInfoPage;

		private void Awake()
		{
			buildInfoButton.onClick.AddListener(ShowBuildInfo);
			deviceInfoButton.onClick.AddListener(ShowDeviceInfo);

			ShowBuildInfo();
		}

		public void Initialize()
		{
			deviceInfoPage.Initialize();
			cloudBuildInfoPage.Initialize();
		}

		private void ShowBuildInfo()
		{
			cloudBuildInfoPage.gameObject.SetActive(true);
			deviceInfoPage.gameObject.SetActive(false);
			buildInfoButton.interactable = false;
			deviceInfoButton.interactable = true;
		}

		private void ShowDeviceInfo()
		{
			cloudBuildInfoPage.gameObject.SetActive(false);
			deviceInfoPage.gameObject.SetActive(true);
			buildInfoButton.interactable = true;
			deviceInfoButton.interactable = false;
		}
	}
}
