using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB5 RID: 2997
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobsByExpiredInLessThanResp
	{
		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0001F317 File Offset: 0x0001D517
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x0001F31F File Offset: 0x0001D51F
		[DataMember]
		public IList<MediaJobDTO> MediaJobList { get; set; }
	}
}
