using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000181 RID: 385
	[DataContract(Namespace = "http://tpro.ca")]
	public class FillReq : BaseMessageReq
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00004198 File Offset: 0x00002398
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x000041A0 File Offset: 0x000023A0
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }
	}
}
