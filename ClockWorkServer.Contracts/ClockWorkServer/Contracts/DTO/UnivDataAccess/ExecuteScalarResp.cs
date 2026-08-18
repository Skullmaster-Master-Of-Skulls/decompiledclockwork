using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000184 RID: 388
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteScalarResp
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000041CB File Offset: 0x000023CB
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x000041D3 File Offset: 0x000023D3
		[DataMember]
		public QueryResultDTO QueryResult { get; set; }
	}
}
