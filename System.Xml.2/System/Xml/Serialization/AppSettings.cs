using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x0200012D RID: 301
	internal static class AppSettings
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00061231 File Offset: 0x0005F431
		internal static bool? UseLegacySerializerGeneration
		{
			get
			{
				AppSettings.EnsureSettingsLoaded();
				return AppSettings.useLegacySerializerGeneration;
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00061240 File Offset: 0x0005F440
		private static void EnsureSettingsLoaded()
		{
			if (!AppSettings.settingsInitalized)
			{
				object obj = AppSettings.appSettingsLock;
				lock (obj)
				{
					if (!AppSettings.settingsInitalized)
					{
						NameValueCollection nameValueCollection = null;
						try
						{
							nameValueCollection = ConfigurationManager.AppSettings;
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							bool value;
							if (nameValueCollection == null || !bool.TryParse(nameValueCollection["System:Xml:Serialization:UseLegacySerializerGeneration"], out value))
							{
								AppSettings.useLegacySerializerGeneration = null;
							}
							else
							{
								AppSettings.useLegacySerializerGeneration = new bool?(value);
							}
							AppSettings.settingsInitalized = true;
						}
					}
				}
			}
		}

		// Token: 0x04000A4C RID: 2636
		private const string UseLegacySerializerGenerationAppSettingsString = "System:Xml:Serialization:UseLegacySerializerGeneration";

		// Token: 0x04000A4D RID: 2637
		private static bool? useLegacySerializerGeneration;

		// Token: 0x04000A4E RID: 2638
		private static volatile bool settingsInitalized = false;

		// Token: 0x04000A4F RID: 2639
		private static object appSettingsLock = new object();
	}
}
