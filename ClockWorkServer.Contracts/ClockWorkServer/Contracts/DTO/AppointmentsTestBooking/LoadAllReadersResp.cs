using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F7 RID: 2551
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllReadersResp
	{
		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x00019D15 File Offset: 0x00017F15
		// (set) Token: 0x06003513 RID: 13587 RVA: 0x00019D1D File Offset: 0x00017F1D
		[DataMember]
		public List<ProctorDTO> Proctors { get; set; }
	}
}
