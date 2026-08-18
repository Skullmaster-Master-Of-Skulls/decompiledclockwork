using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000EC RID: 236
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseHashAuthMessageReq : BaseMessageReq
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000290C File Offset: 0x00000B0C
		[DataMember]
		public ClockWorkHashAuthenticationDTO HashAuthentication { get; set; }
	}
}
