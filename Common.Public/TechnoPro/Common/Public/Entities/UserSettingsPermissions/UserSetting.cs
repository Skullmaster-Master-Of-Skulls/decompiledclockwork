using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000122 RID: 290
	public class UserSetting : BusinessBase<LookupUserSetting>
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		public LookupUserSetting LookupSetting
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public object Value { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public string UserComment { get; set; }

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000FB08 File Offset: 0x0000DD08
		public override string ToString()
		{
			return (this.Value != null) ? this.Value.ToString() : string.Empty;
		}
	}
}
