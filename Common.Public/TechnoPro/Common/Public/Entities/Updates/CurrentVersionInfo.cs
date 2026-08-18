using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200014F RID: 335
	public class CurrentVersionInfo : BusinessBase<string>
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000115BC File Offset: 0x0000F7BC
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Version
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

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000115D4 File Offset: 0x0000F7D4
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000115DC File Offset: 0x0000F7DC
		public string SecondaryVersion { get; set; }
	}
}
