using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsReminder
{
	// Token: 0x02000AAF RID: 2735
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsAppointmentReminderEnableResp
	{
		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		// (set) Token: 0x06003A0F RID: 14863 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		[DataMember]
		public bool IsEnable { get; set; }
	}
}
