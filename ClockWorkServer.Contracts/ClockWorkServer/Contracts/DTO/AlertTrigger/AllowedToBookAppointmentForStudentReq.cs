using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C7A RID: 3194
	[DataContract(Namespace = "http://tpro.ca")]
	public class AllowedToBookAppointmentForStudentReq : BaseMessageReq
	{
		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x00020821 File Offset: 0x0001EA21
		// (set) Token: 0x0600428F RID: 17039 RVA: 0x00020829 File Offset: 0x0001EA29
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
