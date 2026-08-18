using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess
{
	// Token: 0x02000414 RID: 1044
	[DataContract(Namespace = "http://tpro.ca")]
	public class MultiAccessContext
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0000A8BE File Offset: 0x00008ABE
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x0000A8C6 File Offset: 0x00008AC6
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0000A8CF File Offset: 0x00008ACF
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
