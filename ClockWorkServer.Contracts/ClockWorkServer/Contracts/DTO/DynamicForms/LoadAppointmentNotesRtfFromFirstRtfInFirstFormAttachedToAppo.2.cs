using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062D RID: 1581
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp
	{
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0000E9C2 File Offset: 0x0000CBC2
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		[DataMember]
		public string NotesRtf { get; set; }
	}
}
