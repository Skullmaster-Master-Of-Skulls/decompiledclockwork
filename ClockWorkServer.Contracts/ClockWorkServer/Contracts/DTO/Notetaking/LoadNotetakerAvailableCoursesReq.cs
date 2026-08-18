using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044C RID: 1100
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerAvailableCoursesReq : BaseReportMessageReq
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0000AE39 File Offset: 0x00009039
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x0000AE41 File Offset: 0x00009041
		[DataMember]
		public int NotetakerId { get; set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0000AE4A File Offset: 0x0000904A
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0000AE52 File Offset: 0x00009052
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0000AE5B File Offset: 0x0000905B
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0000AE63 File Offset: 0x00009063
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
