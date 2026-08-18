using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A49 RID: 2633
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFilesByExamReq : BaseMessageReq
	{
		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x0001AEFD File Offset: 0x000190FD
		// (set) Token: 0x0600375D RID: 14173 RVA: 0x0001AF05 File Offset: 0x00019105
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x0001AF0E File Offset: 0x0001910E
		// (set) Token: 0x0600375F RID: 14175 RVA: 0x0001AF16 File Offset: 0x00019116
		[DataMember]
		public bool IncludeDeletedFiles { get; set; }

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x0001AF1F File Offset: 0x0001911F
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x0001AF27 File Offset: 0x00019127
		[DataMember]
		public bool LoadFileData { get; set; }
	}
}
