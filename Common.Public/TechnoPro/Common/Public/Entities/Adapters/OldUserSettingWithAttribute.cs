using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C9 RID: 1481
	public class OldUserSettingWithAttribute
	{
		// Token: 0x06002F9C RID: 12188 RVA: 0x0000D55A File Offset: 0x0000B75A
		public OldUserSettingWithAttribute()
		{
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0003744A File Offset: 0x0003564A
		public OldUserSettingWithAttribute(eSettingCode setting)
		{
			this.Setting = setting;
			this.SettingAttribute = WebSettingGroupWithEnums.GetAttribute<OldUserSettingAttribute>(setting);
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x0003746E File Offset: 0x0003566E
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x00037476 File Offset: 0x00035676
		public eSettingCode Setting { get; set; }

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x0003747F File Offset: 0x0003567F
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x00037487 File Offset: 0x00035687
		public OldUserSettingAttribute SettingAttribute { get; set; }
	}
}
