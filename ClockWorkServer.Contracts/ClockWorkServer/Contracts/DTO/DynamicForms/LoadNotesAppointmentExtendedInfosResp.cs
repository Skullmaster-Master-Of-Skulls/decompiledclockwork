using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000633 RID: 1587
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentExtendedInfosResp
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x0000EA96 File Offset: 0x0000CC96
		[DataMember]
		public IList<NotesAppointmentExtendedInfoDTO> NotesAppointmentExtendedInfos { get; set; }
	}
}
