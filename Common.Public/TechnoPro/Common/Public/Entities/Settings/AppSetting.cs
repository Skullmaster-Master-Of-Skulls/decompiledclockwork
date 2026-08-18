using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001CF RID: 463
	public class AppSetting : BusinessBase<LookupSetting>
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00014F78 File Offset: 0x00013178
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x00014F90 File Offset: 0x00013190
		public LookupSetting LookupSetting
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00014F9B File Offset: 0x0001319B
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00014FA3 File Offset: 0x000131A3
		public object Value { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00014FAC File Offset: 0x000131AC
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00014FB4 File Offset: 0x000131B4
		public string UserComment { get; set; }

		// Token: 0x06000D57 RID: 3415 RVA: 0x00014FC0 File Offset: 0x000131C0
		public override string ToString()
		{
			return (this.Value != null) ? this.Value.ToString() : string.Empty;
		}
	}
}
