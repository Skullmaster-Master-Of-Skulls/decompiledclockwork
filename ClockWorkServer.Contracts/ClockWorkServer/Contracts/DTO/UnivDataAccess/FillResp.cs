using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000182 RID: 386
	[DataContract(Namespace = "http://tpro.ca")]
	public class FillResp
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000041A9 File Offset: 0x000023A9
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000041B1 File Offset: 0x000023B1
		[DataMember]
		public QueryResultDTO QueryResult { get; set; }
	}
}
