using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000185 RID: 389
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteNonQueryReq : BaseMessageReq
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000041DC File Offset: 0x000023DC
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x000041E4 File Offset: 0x000023E4
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }
	}
}
