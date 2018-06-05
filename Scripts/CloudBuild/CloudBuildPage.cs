using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.CloudBuild
{
	public class CloudBuildPage : MonoBehaviour
	{
		public CloudBuildManifest BuildManifest { get; private set; }

		[Header("Cloud Build Manifest Info Text")]
		[SerializeField]
		private Text scmCommitId;
		[SerializeField]
		private Text scmBranch;
		[SerializeField]
		private Text buildNumber;
		[SerializeField]
		private Text buildStartTime;
		[SerializeField]
		private Text projectId;
		[SerializeField]
		private Text bundleId;
		[SerializeField]
		private Text unityVersion;
		[SerializeField]
		private Text xcodeVersion;
		[SerializeField]
		private Text cloudBuildTargetName;

		public void Awake()
		{
			var buildManifestJson = Resources.Load<TextAsset>("UnityCloudBuildManifest.json");
			if (!buildManifestJson) return;

			BuildManifest = JsonUtility.FromJson<CloudBuildManifest>(buildManifestJson.text);
			if (BuildManifest != null)
			{
				scmCommitId.text = BuildManifest.ScmCommitId;
				scmBranch.text = BuildManifest.ScmBranch;
				buildNumber.text = BuildManifest.BuildNumber;
				buildStartTime.text = BuildManifest.BuildStartTime;
				projectId.text = BuildManifest.ProjectId;
				bundleId.text = BuildManifest.BundleId;
				unityVersion.text = BuildManifest.UnityVersion;
				xcodeVersion.text = BuildManifest.XcodeVersion;
				cloudBuildTargetName.text = BuildManifest.CloudBuildTargetName;
#if !UNITY_IPHONE
				xcodeVersion.gameObject.SetActive(false);
#endif
			}
		}
	}
}
