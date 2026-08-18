using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000179 RID: 377
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesTableExistReq : BaseMessageReq
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x000040DD File Offset: 0x000022DD
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x000040E5 File Offset: 0x000022E5
		[DataMember]
		public string TableName { get; set; }
	}
}
