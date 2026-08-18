using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F5 RID: 2549
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllProctorsResp
	{
		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x00019D04 File Offset: 0x00017F04
		// (set) Token: 0x0600350F RID: 13583 RVA: 0x00019D0C File Offset: 0x00017F0C
		[DataMember]
		public List<ProctorDTO> Proctors { get; set; }
	}
}
