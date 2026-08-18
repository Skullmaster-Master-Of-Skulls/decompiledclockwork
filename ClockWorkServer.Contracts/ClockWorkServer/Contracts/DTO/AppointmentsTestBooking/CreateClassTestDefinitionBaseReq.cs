using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0B RID: 2571
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClassTestDefinitionBaseReq : BaseMessageReq
	{
		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x00019E58 File Offset: 0x00018058
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x00019E60 File Offset: 0x00018060
		[DataMember]
		public ClassTestBaseDTO ClassTestBase { get; set; }
	}
}
