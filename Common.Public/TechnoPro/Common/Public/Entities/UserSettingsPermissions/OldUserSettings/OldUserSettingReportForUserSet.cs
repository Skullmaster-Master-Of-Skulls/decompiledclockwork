using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000133 RID: 307
	public class OldUserSettingReportForUserSet : BusinessBase<int>
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000101C4 File Offset: 0x0000E3C4
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000101DC File Offset: 0x0000E3DC
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public IList<OldUserSettingReportForUser> SettingsWithReports { get; set; }
	}
}
