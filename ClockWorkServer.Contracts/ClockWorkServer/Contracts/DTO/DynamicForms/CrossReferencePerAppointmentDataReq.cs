using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063C RID: 1596
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferencePerAppointmentDataReq : BaseMessageReq
	{
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x0000ECE2 File Offset: 0x0000CEE2
		[DataMember]
		public DataTable TableWithData { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x0000ECEB File Offset: 0x0000CEEB
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x0000ECF3 File Offset: 0x0000CEF3
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
