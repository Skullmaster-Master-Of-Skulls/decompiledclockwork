using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000979 RID: 2425
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteShowTimeAsTypeByAppCodeReq : BaseMessageReq
	{
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x00018127 File Offset: 0x00016327
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x0001812F File Offset: 0x0001632F
		[DataMember]
		public int AppCode { get; set; }
	}
}
