using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097B RID: 2427
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateShowTimeAsTypeReq : BaseMessageReq
	{
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x00018149 File Offset: 0x00016349
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x00018151 File Offset: 0x00016351
		[DataMember]
		public AppShowTimeAsTypeDTO AppShowTimeAsType { get; set; }
	}
}
