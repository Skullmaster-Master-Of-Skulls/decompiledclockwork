using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017D RID: 381
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSQLCommandParametersFilledInReq : BaseMessageReq
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00004132 File Offset: 0x00002332
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0000413A File Offset: 0x0000233A
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }
	}
}
