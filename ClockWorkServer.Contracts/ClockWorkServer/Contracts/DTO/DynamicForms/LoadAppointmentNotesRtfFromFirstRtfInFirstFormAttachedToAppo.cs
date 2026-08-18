using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062C RID: 1580
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq : BaseMessageReq
	{
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0000E98F File Offset: 0x0000CB8F
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0000E997 File Offset: 0x0000CB97
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0000E9B1 File Offset: 0x0000CBB1
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
