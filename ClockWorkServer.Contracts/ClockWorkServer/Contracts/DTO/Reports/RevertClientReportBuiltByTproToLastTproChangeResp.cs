using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000327 RID: 807
	[DataContract(Namespace = "http://tpro.ca")]
	public class RevertClientReportBuiltByTproToLastTproChangeResp
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00008885 File Offset: 0x00006A85
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0000888D File Offset: 0x00006A8D
		[DataMember]
		public bool WasReverted { get; set; }
	}
}
