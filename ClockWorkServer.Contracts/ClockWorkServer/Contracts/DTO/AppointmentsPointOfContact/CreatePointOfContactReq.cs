using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000919 RID: 2329
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePointOfContactReq : BaseMessageReq
	{
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000167AD File Offset: 0x000149AD
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x000167B5 File Offset: 0x000149B5
		[DataMember]
		public PointOfContactDTO PointOfContact { get; set; }
	}
}
