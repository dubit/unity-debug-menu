using UnityEngine;
using UnityEngine.UI;

namespace DUCK.DebugMenu.CloudBuild
{
	public class CloudBuildPage : MonoBehaviour
	{
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

			var buildManifest = JsonUtility.FromJson<CloudBuildManifest>(buildManifestJson.text);
			if (buildManifest != null)
			{
				scmCommitId.text = buildManifest.ScmCommitId;
				scmBranch.text = buildManifest.ScmBranch;
				buildNumber.text = buildManifest.BuildNumber;
				buildStartTime.text = buildManifest.BuildStartTime;
				projectId.text = buildManifest.ProjectId;
				bundleId.text = buildManifest.BundleId;
				unityVersion.text = buildManifest.UnityVersion;
				xcodeVersion.text = buildManifest.XcodeVersion;
				cloudBuildTargetName.text = buildManifest.CloudBuildTargetName;
#if !UNITY_IPHONE
				xcodeVersion.gameObject.SetActive(false);
#endif
			}
		}
	}
}
