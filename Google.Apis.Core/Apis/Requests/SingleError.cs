using System;

namespace Google.Apis.Requests
{
	// Token: 0x02000014 RID: 20
	public class SingleError
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E44 File Offset: 0x00001044
		public string Domain { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E4D File Offset: 0x0000104D
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002E55 File Offset: 0x00001055
		public string Reason { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E5E File Offset: 0x0000105E
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E66 File Offset: 0x00001066
		public string Message { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E6F File Offset: 0x0000106F
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002E77 File Offset: 0x00001077
		public string LocationType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E80 File Offset: 0x00001080
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002E88 File Offset: 0x00001088
		public string Location { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002E91 File Offset: 0x00001091
		public override string ToString()
		{
			return string.Format("Message[{0}] Location[{1} - {2}] Reason[{3}] Domain[{4}]", new object[]
			{
				this.Message,
				this.Location,
				this.LocationType,
				this.Reason,
				this.Domain
			});
		}
	}
}
