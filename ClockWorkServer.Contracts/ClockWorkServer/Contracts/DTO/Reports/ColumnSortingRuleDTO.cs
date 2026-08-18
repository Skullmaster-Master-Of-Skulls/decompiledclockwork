using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000343 RID: 835
	[DataContract(Namespace = "http://tpro.ca")]
	public class ColumnSortingRuleDTO
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00008F27 File Offset: 0x00007127
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x00008F2F File Offset: 0x0000712F
		[DataMember]
		public string ColumnName { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00008F38 File Offset: 0x00007138
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x00008F40 File Offset: 0x00007140
		[DataMember]
		public bool SortDescending { get; set; }
	}
}
