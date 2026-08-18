using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000131 RID: 305
	public class OldUserSettingReportForUser
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001015B File Offset: 0x0000E35B
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x00010163 File Offset: 0x0000E363
		public eSettingCode SettingCode { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001016C File Offset: 0x0000E36C
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x00010174 File Offset: 0x0000E374
		public IList<OldUserSettingReportForUserItem> Items { get; set; }
	}
}
