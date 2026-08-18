using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F9 RID: 2553
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllScribesResp
	{
		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x00019D26 File Offset: 0x00017F26
		// (set) Token: 0x06003517 RID: 13591 RVA: 0x00019D2E File Offset: 0x00017F2E
		[DataMember]
		public List<ProctorDTO> Proctors { get; set; }
	}
}
