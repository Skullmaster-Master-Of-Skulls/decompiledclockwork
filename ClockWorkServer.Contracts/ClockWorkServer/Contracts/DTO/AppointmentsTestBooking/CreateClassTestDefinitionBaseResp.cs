using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0A RID: 2570
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClassTestDefinitionBaseResp
	{
		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x00019E47 File Offset: 0x00018047
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x00019E4F File Offset: 0x0001804F
		[DataMember]
		public int ExamId { get; set; }
	}
}
