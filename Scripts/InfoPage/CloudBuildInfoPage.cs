using UnityEngine;

namespace DUCK.DebugMenu.InfoPage
{
	public class CloudBuildInfoPage : AbstractInfoSubPage
	{
		public CloudBuildManifest BuildManifest { get; private set; }

		public override void Initialize()
		{
			var buildManifestJson = Resources.Load<TextAsset>("UnityCloudBuildManifest.json");
			BuildManifest = buildManifestJson
				? JsonUtility.FromJson<CloudBuildManifest>(buildManifestJson.text)
				: new CloudBuildManifest();
		}

		protected override void Awake()
		{
			base.Awake();

			if (BuildManifest != null)
			{
				AddElement("Cloud Build Target Name", BuildManifest.CloudBuildTargetName);
				AddElement("Project Id", BuildManifest.ProjectId);
				AddElement("Bundle Id", BuildManifest.BundleId);
				AddElement("Version", Application.version);
				AddElement("Build Number", BuildManifest.BuildNumber);
				AddElement("Branch", BuildManifest.ScmBranch);
				AddElement("Unity Version", BuildManifest.UnityVersion);
#if !UNITY_IPHONE
				AddElement("xCode Version", BuildManifest.XcodeVersion);
#endif
				AddElement("Commit Id", BuildManifest.ScmCommitId);
				AddElement("Build Start Time", BuildManifest.BuildStartTime);
			}
		}
	}
}
