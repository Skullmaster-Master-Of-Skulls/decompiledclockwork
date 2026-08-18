using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A4 RID: 1188
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractUniqueCodesReq : BaseReportMessageReq
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x0000BBAC File Offset: 0x00009DAC
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		[DataMember]
		public string Template { get; set; }
	}
}
