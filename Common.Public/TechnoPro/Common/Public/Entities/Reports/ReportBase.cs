using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000212 RID: 530
	public class ReportBase : BusinessBase<int>
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0001754C File Offset: 0x0001574C
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ReportId
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

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00017564 File Offset: 0x00015764
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0001756C File Offset: 0x0001576C
		public string ReportTitle { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00017575 File Offset: 0x00015775
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0001757D File Offset: 0x0001577D
		public string ReportDescription { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00017586 File Offset: 0x00015786
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0001758E File Offset: 0x0001578E
		public Guid ReportUniqueId { get; set; }
	}
}
