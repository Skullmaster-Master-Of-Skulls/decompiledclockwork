using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A08 RID: 2568
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClassTestDefinitionBaseReq : BaseMessageReq
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x00019E25 File Offset: 0x00018025
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x00019E2D File Offset: 0x0001802D
		[DataMember]
		public ClassTestBaseDTO ClassTest { get; set; }
	}
}
