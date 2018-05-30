using UnityEngine;

namespace DUCK.DebugMenu.CloudBuild
{
	public class CloudBuildManifest
	{
		public string ScmCommitId { get { return scmCommitId; } }
		public string ScmBranch { get { return scmBranch; } }
		public string BuildNumber { get { return buildNumber; } }
		public string BuildStartTime { get { return buildStartTime; } }
		public string ProjectId { get { return projectId; } }
		public string BundleId { get { return bundleId; } }
		public string UnityVersion { get { return unityVersion; } }
		public string XcodeVersion { get { return xcodeVersion; } }
		public string CloudBuildTargetName { get { return cloudBuildTargetName; } }
		
		[SerializeField]
		private string scmCommitId;
		[SerializeField]
		private string scmBranch;
		[SerializeField]
		private string buildNumber;
		[SerializeField]
		private string buildStartTime;
		[SerializeField]
		private string projectId;
		[SerializeField]
		private string bundleId;
		[SerializeField]
		private string unityVersion;
		[SerializeField]
		private string xcodeVersion;
		[SerializeField]
		private string cloudBuildTargetName;
	}
}
