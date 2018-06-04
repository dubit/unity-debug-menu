namespace DUCK.DebugMenu.InfoPage
{
	public class DeviceInfoPage : AbstractInfoSubPage
	{
		public DeviceInfoManifest DeviceInfoManifest { get; private set; }

		public void Initialize()
		{
			DeviceInfoManifest = new DeviceInfoManifest();
		}

		protected override void Awake()
		{
			base.Awake();

			AddElement("Device Name", DeviceInfoManifest.DeviceName);
			AddElement("Device Model", DeviceInfoManifest.DeviceModel);
			AddElement("Device Type", DeviceInfoManifest.DeviceType);
			AddElement("Operating System", DeviceInfoManifest.OperatingSystem);
			AddElement("Operating System Family", DeviceInfoManifest.OperatingSystemFamily);
			AddElement("System Memory Size", DeviceInfoManifest.SystemMemorySize);
			AddElement("Processor Type", DeviceInfoManifest.ProcessorType);
			AddElement("Processor Count", DeviceInfoManifest.ProcessorCount);
			AddElement("Screen Resolution", DeviceInfoManifest.ScreenResolution);
			AddElement("Screen Aspect Ratio", DeviceInfoManifest.AspectRatio);
			AddElement("Graphics Device Name", DeviceInfoManifest.GraphicsDeviceName);
			AddElement("Graphics Device Type", DeviceInfoManifest.DeviceType);
			AddElement("Graphics Device Vendor", DeviceInfoManifest.GraphicsDeviceVendor);
			AddElement("Graphics Memory Size", DeviceInfoManifest.GraphicsMemorySize);
			AddElement("Graphics Shader Level", DeviceInfoManifest.GraphicsShaderLevel);
			AddElement("Graphics Multi Threaded", DeviceInfoManifest.GraphicsMultiThreaded);
			AddElement("Battery Status", DeviceInfoManifest.BatteryStatus);
			AddElement("Battery Level", DeviceInfoManifest.BatteryLevel);
			AddElement("Supports Vibration", DeviceInfoManifest.SupportsVibration);
			AddElement("Supports Shadows", DeviceInfoManifest.SupportsShadows);
			AddElement("Supports Instancing", DeviceInfoManifest.SupportsInstancing);
			AddElement("Supports Location Services", DeviceInfoManifest.SupportsLocationService);
			AddElement("Supports Accelerometer", DeviceInfoManifest.SupportsAccelerometer);
			AddElement("Supports Gyroscope", DeviceInfoManifest.SupportsGyroscope);
			AddElement("Supports Audio", DeviceInfoManifest.SupportsAudio);
		}
	}
}
