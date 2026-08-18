using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000342 RID: 834
	[DataContract(Namespace = "http://tpro.ca")]
	public class ColumnFormattingRuleDTO
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00008F05 File Offset: 0x00007105
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00008F0D File Offset: 0x0000710D
		[DataMember]
		public string ColumnName { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00008F16 File Offset: 0x00007116
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00008F1E File Offset: 0x0000711E
		[DataMember]
		public string FormattingString { get; set; }
	}
}
