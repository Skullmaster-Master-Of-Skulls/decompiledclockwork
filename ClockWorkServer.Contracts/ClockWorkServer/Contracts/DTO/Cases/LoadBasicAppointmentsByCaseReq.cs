using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A4 RID: 2212
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicAppointmentsByCaseReq : BaseMessageReq
	{
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x00015386 File Offset: 0x00013586
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x0001538E File Offset: 0x0001358E
		[DataMember]
		public int CaseId { get; set; }
	}
}
