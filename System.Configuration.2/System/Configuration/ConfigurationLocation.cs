using System;

namespace System.Configuration
{
	// Token: 0x0200002A RID: 42
	public class ConfigurationLocation
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		internal ConfigurationLocation(Configuration config, string locationSubPath)
		{
			this._config = config;
			this._locationSubPath = locationSubPath;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000FA0A File Offset: 0x0000DC0A
		public string Path
		{
			get
			{
				return this._locationSubPath;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000FA12 File Offset: 0x0000DC12
		public Configuration OpenConfiguration()
		{
			return this._config.OpenLocationConfiguration(this._locationSubPath);
		}

		// Token: 0x040001CD RID: 461
		private Configuration _config;

		// Token: 0x040001CE RID: 462
		private string _locationSubPath;
	}
}
