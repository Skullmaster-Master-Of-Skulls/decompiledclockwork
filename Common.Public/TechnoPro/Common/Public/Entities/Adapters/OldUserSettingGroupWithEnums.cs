using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C8 RID: 1480
	public class OldUserSettingGroupWithEnums
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x0000D55A File Offset: 0x0000B75A
		public OldUserSettingGroupWithEnums()
		{
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000373F4 File Offset: 0x000355F4
		public OldUserSettingGroupWithEnums(eOldUserSettingGroup group, OldUserSettingGroupAttribute groupAttribute, IList<OldUserSettingWithAttribute> settings)
		{
			this.Group = eOldUserSettingGroup.System;
			this.GroupAttribute = groupAttribute;
			this.Settings = settings;
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x00037417 File Offset: 0x00035617
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x0003741F File Offset: 0x0003561F
		public eOldUserSettingGroup Group { get; set; }

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x00037428 File Offset: 0x00035628
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x00037430 File Offset: 0x00035630
		public OldUserSettingGroupAttribute GroupAttribute { get; set; }

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x00037439 File Offset: 0x00035639
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x00037441 File Offset: 0x00035641
		public IList<OldUserSettingWithAttribute> Settings { get; set; }
	}
}
