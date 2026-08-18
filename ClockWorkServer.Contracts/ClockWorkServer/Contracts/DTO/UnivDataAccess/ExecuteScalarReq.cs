using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000183 RID: 387
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteScalarReq : BaseMessageReq
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000041BA File Offset: 0x000023BA
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000041C2 File Offset: 0x000023C2
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }
	}
}
