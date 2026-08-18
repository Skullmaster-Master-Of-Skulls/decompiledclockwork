using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044F RID: 1103
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueStudentsReceivingNotesResp
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0000AE9F File Offset: 0x0000909F
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x0000AEA7 File Offset: 0x000090A7
		[DataMember]
		public IList<ServiceRequestBaseDTO> Assignments { get; set; }
	}
}
