using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000187 RID: 391
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReaderReq : BaseMessageReq
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x000041FE File Offset: 0x000023FE
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00004206 File Offset: 0x00002406
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }
	}
}
