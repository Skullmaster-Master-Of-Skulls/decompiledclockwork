using System;
using System.Configuration;
using System.Threading;

namespace LzmaAlone.Properties
{
	// Token: 0x0200001C RID: 28
	public partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000060B0 File Offset: 0x000042B0
		public static Settings Value
		{
			get
			{
				if (Settings.m_Value == null)
				{
					Monitor.Enter(Settings.m_SyncObject);
					if (Settings.m_Value == null)
					{
						try
						{
							Settings.m_Value = new Settings();
						}
						finally
						{
							Monitor.Exit(Settings.m_SyncObject);
						}
					}
				}
				return Settings.m_Value;
			}
		}

		// Token: 0x040000B1 RID: 177
		private static Settings m_Value;

		// Token: 0x040000B2 RID: 178
		private static object m_SyncObject = new object();
	}
}
