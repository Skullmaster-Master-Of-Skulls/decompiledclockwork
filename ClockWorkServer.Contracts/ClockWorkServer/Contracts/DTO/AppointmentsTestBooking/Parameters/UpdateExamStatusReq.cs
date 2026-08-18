using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A60 RID: 2656
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateExamStatusReq : BaseMessageReq
	{
		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x0001B13F File Offset: 0x0001933F
		// (set) Token: 0x060037B8 RID: 14264 RVA: 0x0001B147 File Offset: 0x00019347
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x0001B150 File Offset: 0x00019350
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x0001B158 File Offset: 0x00019358
		[DataMember]
		public int NewExamStatusLookupId { get; set; }
	}
}
