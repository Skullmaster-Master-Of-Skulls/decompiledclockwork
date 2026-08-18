using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E9 RID: 2537
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingsResp
	{
		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x00019BD2 File Offset: 0x00017DD2
		// (set) Token: 0x060034DF RID: 13535 RVA: 0x00019BDA File Offset: 0x00017DDA
		[DataMember]
		public List<SittingDTO> Sittings { get; set; }
	}
}
