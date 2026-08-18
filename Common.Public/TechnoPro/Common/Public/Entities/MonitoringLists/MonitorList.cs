using System;

namespace TechnoPro.Common.Public.Entities.MonitoringLists
{
	// Token: 0x02000192 RID: 402
	public class MonitorList : BusinessBase<string>
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00013332 File Offset: 0x00011532
		public MonitorList()
		{
			this.Title = "";
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00013348 File Offset: 0x00011548
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string UniqueName
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00013360 File Offset: 0x00011560
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00013368 File Offset: 0x00011568
		public int ReportId { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00013371 File Offset: 0x00011571
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00013379 File Offset: 0x00011579
		public int SubReportId { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00013382 File Offset: 0x00011582
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0001338A File Offset: 0x0001158A
		public bool IsVisible { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00013393 File Offset: 0x00011593
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0001339B File Offset: 0x0001159B
		public bool IsActive { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000133A4 File Offset: 0x000115A4
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x000133AC File Offset: 0x000115AC
		public string Title { get; set; }
	}
}
