using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000344 RID: 836
	[DataContract(Namespace = "http://tpro.ca")]
	public class RowFormattingDTO
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00008F49 File Offset: 0x00007149
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00008F51 File Offset: 0x00007151
		[DataMember]
		public string ColumnName { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00008F5A File Offset: 0x0000715A
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00008F62 File Offset: 0x00007162
		[DataMember]
		public eRowFormattingConditionType ConditionType { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00008F6B File Offset: 0x0000716B
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x00008F73 File Offset: 0x00007173
		[DataMember]
		public string ConditionValue { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00008F7C File Offset: 0x0000717C
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x00008F84 File Offset: 0x00007184
		[DataMember]
		public int BackColourArgB { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00008F8D File Offset: 0x0000718D
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x00008F95 File Offset: 0x00007195
		[DataMember]
		public int ForeColourArgB { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00008F9E File Offset: 0x0000719E
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x00008FA6 File Offset: 0x000071A6
		[DataMember]
		public bool ApplyToRow { get; set; }
	}
}
