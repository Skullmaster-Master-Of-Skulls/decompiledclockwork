using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000324 RID: 804
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateClientReportBuiltByTproIsNotTamperedWithReq : BaseReportMessageReq
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00008852 File Offset: 0x00006A52
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0000885A File Offset: 0x00006A5A
		[DataMember]
		public int ReportId { get; set; }
	}
}
