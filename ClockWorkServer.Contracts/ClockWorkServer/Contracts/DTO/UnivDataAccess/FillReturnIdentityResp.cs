using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000180 RID: 384
	[DataContract(Namespace = "http://tpro.ca")]
	public class FillReturnIdentityResp
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00004187 File Offset: 0x00002387
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0000418F File Offset: 0x0000238F
		[DataMember]
		public QueryResultDTO QueryResult { get; set; }
	}
}
