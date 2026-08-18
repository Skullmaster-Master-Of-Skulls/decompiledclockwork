using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000630 RID: 1584
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0000EA39 File Offset: 0x0000CC39
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x0000EA41 File Offset: 0x0000CC41
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0000EA4A File Offset: 0x0000CC4A
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x0000EA52 File Offset: 0x0000CC52
		[DataMember]
		public int PrimaryStudentPersonId { get; set; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0000EA5B File Offset: 0x0000CC5B
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x0000EA63 File Offset: 0x0000CC63
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
