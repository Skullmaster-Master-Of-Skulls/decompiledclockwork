using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000132 RID: 306
	public class OldUserSettingReportForUserItem
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001017D File Offset: 0x0000E37D
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00010185 File Offset: 0x0000E385
		public int PersonOrGroupId { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001018E File Offset: 0x0000E38E
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00010196 File Offset: 0x0000E396
		public eOldUserSettingType SettingType { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001019F File Offset: 0x0000E39F
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x000101A7 File Offset: 0x0000E3A7
		public int IntVal { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000101B0 File Offset: 0x0000E3B0
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public string StringVal { get; set; }
	}
}
