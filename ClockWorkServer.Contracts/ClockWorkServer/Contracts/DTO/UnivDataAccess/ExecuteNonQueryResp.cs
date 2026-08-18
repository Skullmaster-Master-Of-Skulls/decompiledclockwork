using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000186 RID: 390
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteNonQueryResp
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000041ED File Offset: 0x000023ED
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x000041F5 File Offset: 0x000023F5
		[DataMember]
		public QueryResultDTO QueryResult { get; set; }
	}
}
