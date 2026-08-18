using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000325 RID: 805
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateClientReportBuiltByTproIsNotTamperedWithResp
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00008863 File Offset: 0x00006A63
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x0000886B File Offset: 0x00006A6B
		[DataMember]
		public bool IsValidated { get; set; }
	}
}
