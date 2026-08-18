using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000632 RID: 1586
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentExtendedInfosReq : BaseMessageReq
	{
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0000EA7D File Offset: 0x0000CC7D
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x0000EA85 File Offset: 0x0000CC85
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
