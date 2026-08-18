using System;

namespace System.Configuration
{
	// Token: 0x020006FC RID: 1788
	public interface IPersistComponentSettings
	{
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600372B RID: 14123
		// (set) Token: 0x0600372C RID: 14124
		bool SaveSettings { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600372D RID: 14125
		// (set) Token: 0x0600372E RID: 14126
		string SettingsKey { get; set; }

		// Token: 0x0600372F RID: 14127
		void LoadComponentSettings();

		// Token: 0x06003730 RID: 14128
		void SaveComponentSettings();

		// Token: 0x06003731 RID: 14129
		void ResetComponentSettings();
	}
}
