using System.Text;
using UnityEngine;

namespace DUCK.DebugMenu.Email.Body
{
	public static class DeviceInfo
	{
		private static string CACHED;

		public static string Generate()
		{
			if (!string.IsNullOrEmpty(CACHED))
			{
				return CACHED;
			}

			var stringBuilder = new StringBuilder(EmailPage.SEPERATOR);
			stringBuilder.Append("\n");

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
			stringBuilder.Append((float) Screen.width / Screen.height);
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

			return CACHED = stringBuilder.ToString();
		}
	}
}
