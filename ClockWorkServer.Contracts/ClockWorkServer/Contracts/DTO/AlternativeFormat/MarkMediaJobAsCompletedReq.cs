using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BDC RID: 3036
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkMediaJobAsCompletedReq : BaseMessageReq
	{
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0001F702 File Offset: 0x0001D902
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x0001F70A File Offset: 0x0001D90A
		[DataMember]
		public MediaJobDTO MediaJob { get; set; }

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x0001F713 File Offset: 0x0001D913
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x0001F71B File Offset: 0x0001D91B
		[DataMember]
		public string CompletedNotes { get; set; }

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x0001F724 File Offset: 0x0001D924
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x0001F72C File Offset: 0x0001D92C
		[DataMember]
		public DateTime AvailableStartTime { get; set; }

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0001F735 File Offset: 0x0001D935
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x0001F73D File Offset: 0x0001D93D
		[DataMember]
		public DateTime AvailableEndTime { get; set; }
	}
}
