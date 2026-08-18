using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E6E RID: 3694
	public class TimeZoneNamePair
	{
		// Token: 0x17002C50 RID: 11344
		// (get) Token: 0x06008C25 RID: 35877 RVA: 0x001FD108 File Offset: 0x001FB308
		// (set) Token: 0x06008C26 RID: 35878 RVA: 0x001FD110 File Offset: 0x001FB310
		public string Id { get; set; }

		// Token: 0x17002C51 RID: 11345
		// (get) Token: 0x06008C27 RID: 35879 RVA: 0x001FD119 File Offset: 0x001FB319
		// (set) Token: 0x06008C28 RID: 35880 RVA: 0x001FD121 File Offset: 0x001FB321
		public string DisplayName { get; set; }

		// Token: 0x06008C29 RID: 35881 RVA: 0x001FD12A File Offset: 0x001FB32A
		public TimeZoneNamePair()
		{
		}

		// Token: 0x06008C2A RID: 35882 RVA: 0x001FD132 File Offset: 0x001FB332
		public TimeZoneNamePair(string id, string displayName)
		{
			this.Id = id;
			this.DisplayName = displayName;
		}
	}
}
