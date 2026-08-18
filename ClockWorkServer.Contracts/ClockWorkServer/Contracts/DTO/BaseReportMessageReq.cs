using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000EB RID: 235
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseReportMessageReq : BaseMessageReq
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000028F3 File Offset: 0x00000AF3
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x000028FB File Offset: 0x00000AFB
		[DataMember]
		public string BinPath { get; set; }
	}
}
