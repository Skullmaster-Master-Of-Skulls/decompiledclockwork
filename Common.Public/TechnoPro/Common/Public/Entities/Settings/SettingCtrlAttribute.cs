using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D8 RID: 472
	public class SettingCtrlAttribute : Attribute
	{
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000159D2 File Offset: 0x00013BD2
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x000159DA File Offset: 0x00013BDA
		public string Type { get; protected set; }

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000159E3 File Offset: 0x00013BE3
		public SettingCtrlAttribute(string type)
		{
			this.Type = type;
		}
	}
}
