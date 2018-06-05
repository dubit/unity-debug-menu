using System.Globalization;
using UnityEngine;

namespace DUCK.DebugMenu.InfoPage
{
	public class DeviceInfoManifest
	{
		public string DeviceName { get; private set; }
		public string DeviceModel { get; private set; }
		public string DeviceType { get; private set; }
		public string OperatingSystem { get; private set; }
		public string OperatingSystemFamily { get; private set; }
		public string SystemMemorySize { get; private set; }
		public string ProcessorType { get; private set; }
		public string ProcessorCount { get; private set; }
		public string ScreenResolution { get { return string.Format("{0}x{1} ({2})", Screen.width, Screen.height, Screen.height > Screen.width ? "Portrait" : "Landscape"); } }
		public string AspectRatio { get; private set; }
		public string ScreenDPI { get; private set; }
		public string GraphicsDeviceName { get; private set; }
		public string GraphicsDeviceType { get; private set; }
		public string GraphicsDeviceVendor { get; private set; }
		public string GraphicsMemorySize { get; private set; }
		public string GraphicsShaderLevel { get; private set; }
		public string GraphicsMultiThreaded { get; private set; }
		public string BatteryStatus { get { return SystemInfo.batteryStatus.ToString(); } }
		public string BatteryLevel { get { return SystemInfo.batteryLevel * 100 + "%"; } }
		public string SupportsVibration { get; private set; }
		public string SupportsShadows { get; private set; }
		public string SupportsInstancing { get; private set; }
		public string SupportsLocationService { get; private set; }
		public string SupportsAccelerometer { get; private set; }
		public string SupportsGyroscope { get; private set; }
		public string SupportsAudio { get; private set; }

		public DeviceInfoManifest()
		{
			DeviceName = SystemInfo.deviceName;
			DeviceModel = SystemInfo.deviceModel;
			DeviceType = SystemInfo.deviceType.ToString();
			OperatingSystem = SystemInfo.operatingSystem;
			OperatingSystemFamily = SystemInfo.operatingSystemFamily.ToString();
			SystemMemorySize = SystemInfo.systemMemorySize.ToString();
			ProcessorType = SystemInfo.processorType;
			ProcessorCount = SystemInfo.processorCount.ToString();
			AspectRatio = Screen.height > Screen.width
				? ((float) Screen.height / Screen.width).ToString(CultureInfo.InvariantCulture)
				: ((float) Screen.width / Screen.height).ToString(CultureInfo.InvariantCulture);
			ScreenDPI = Screen.dpi.ToString(CultureInfo.InvariantCulture);
			GraphicsDeviceName = SystemInfo.graphicsDeviceName;
			GraphicsDeviceType = SystemInfo.graphicsDeviceType.ToString();
			GraphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
			GraphicsMemorySize = SystemInfo.graphicsMemorySize.ToString();
			GraphicsShaderLevel = SystemInfo.graphicsShaderLevel.ToString();
			GraphicsMultiThreaded = SystemInfo.graphicsMultiThreaded.ToString();
			SupportsVibration = SystemInfo.supportsVibration.ToString();
			SupportsShadows = SystemInfo.supportsShadows.ToString();
			SupportsInstancing = SystemInfo.supportsInstancing.ToString();
			SupportsLocationService = SystemInfo.supportsLocationService.ToString();
			SupportsAccelerometer = SystemInfo.supportsAccelerometer.ToString();
			SupportsGyroscope = SystemInfo.supportsGyroscope.ToString();
			SupportsAudio = SystemInfo.supportsAudio.ToString();
		}
	}
}
