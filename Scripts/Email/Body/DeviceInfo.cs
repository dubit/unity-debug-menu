using System.Text;
using DUCK.DebugMenu.InfoPage;

namespace DUCK.DebugMenu.Email.Body
{
	public static class DeviceInfo
	{
		public static string Generate(DeviceInfoManifest deviceInfo)
		{
			var stringBuilder = new StringBuilder(EmailPage.SEPERATOR);
			stringBuilder.Append("\n");

			stringBuilder.Append("Device Name: ");
			stringBuilder.Append(deviceInfo.DeviceName);
			stringBuilder.Append("\n");

			stringBuilder.Append("Device Model: ");
			stringBuilder.Append(deviceInfo.DeviceModel);
			stringBuilder.Append("\n");

			stringBuilder.Append("Device Type: ");
			stringBuilder.Append(deviceInfo.DeviceType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Operating System: ");
			stringBuilder.Append(deviceInfo.OperatingSystem);
			stringBuilder.Append("\n");

			stringBuilder.Append("Operating System Family: ");
			stringBuilder.Append(deviceInfo.OperatingSystemFamily);
			stringBuilder.Append("\n");

			stringBuilder.Append("System Memory Size: ");
			stringBuilder.Append(deviceInfo.SystemMemorySize);
			stringBuilder.Append("\n");

			stringBuilder.Append("Processor Type: ");
			stringBuilder.Append(deviceInfo.ProcessorType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Processor Count: ");
			stringBuilder.Append(deviceInfo.ProcessorCount);
			stringBuilder.Append("\n");

			stringBuilder.Append("Screen Resolution: ");
			stringBuilder.Append(deviceInfo.ScreenResolution);
			stringBuilder.Append("\n");

			stringBuilder.Append("Screen Aspect Ratio: ");
			stringBuilder.Append(deviceInfo.AspectRatio);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Device Name: ");
			stringBuilder.Append(deviceInfo.GraphicsDeviceName);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics DeviceType: ");
			stringBuilder.Append(deviceInfo.GraphicsDeviceType);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Device Vendor: ");
			stringBuilder.Append(deviceInfo.GraphicsDeviceVendor);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Memory Size: ");
			stringBuilder.Append(deviceInfo.GraphicsMemorySize);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Shader Level: ");
			stringBuilder.Append(deviceInfo.GraphicsShaderLevel);
			stringBuilder.Append("\n");

			stringBuilder.Append("Graphics Multi Threaded: ");
			stringBuilder.Append(deviceInfo.GraphicsMultiThreaded);
			stringBuilder.Append("\n");

			stringBuilder.Append("Battery Status: ");
			stringBuilder.Append(deviceInfo.BatteryStatus);
			stringBuilder.Append("\n");

			stringBuilder.Append("Battery Level: ");
			stringBuilder.Append(deviceInfo.BatteryLevel);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Vibration: ");
			stringBuilder.Append(deviceInfo.SupportsVibration);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Shadows: ");
			stringBuilder.Append(deviceInfo.SupportsShadows);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Instancing: ");
			stringBuilder.Append(deviceInfo.SupportsInstancing);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Location Service: ");
			stringBuilder.Append(deviceInfo.SupportsLocationService);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Accelerometer: ");
			stringBuilder.Append(deviceInfo.SupportsAccelerometer);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Gyroscope: ");
			stringBuilder.Append(deviceInfo.SupportsGyroscope);
			stringBuilder.Append("\n");

			stringBuilder.Append("Supports Audio: ");
			stringBuilder.Append(deviceInfo.SupportsAudio);
			stringBuilder.Append("\n");

			return stringBuilder.ToString();
		}
	}
}
