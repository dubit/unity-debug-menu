using System.Text;
using DUCK.DebugMenu.CloudBuild;
using UnityEngine;

namespace DUCK.DebugMenu.Email.Body
{
	public static class CloudBuildInfo
	{
		private static string CACHED;

		public static string Generate(CloudBuildManifest buildManifest)
		{
			if (!string.IsNullOrEmpty(CACHED))
			{
				return CACHED;
			}

			var buildManifestIsAvailable = buildManifest != null;
			var stringBuilder = new StringBuilder(EmailPage.SEPERATOR);
			stringBuilder.Append("\n");

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

			return CACHED = stringBuilder.ToString();
		}
	}
}
