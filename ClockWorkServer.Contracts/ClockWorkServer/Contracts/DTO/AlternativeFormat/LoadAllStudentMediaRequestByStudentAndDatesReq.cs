using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C43 RID: 3139
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentMediaRequestByStudentAndDatesReq : BaseReportMessageReq
	{
		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x0002014D File Offset: 0x0001E34D
		// (set) Token: 0x0600419C RID: 16796 RVA: 0x00020155 File Offset: 0x0001E355
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x0002015E File Offset: 0x0001E35E
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x00020166 File Offset: 0x0001E366
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x0002016F File Offset: 0x0001E36F
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x00020177 File Offset: 0x0001E377
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
