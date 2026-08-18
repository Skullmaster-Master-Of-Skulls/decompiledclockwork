using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A09 RID: 2569
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClassTestDefinitionReq : BaseMessageReq
	{
		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x00019E36 File Offset: 0x00018036
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x00019E3E File Offset: 0x0001803E
		[DataMember]
		public ClassTestDTO ClassTest { get; set; }
	}
}
