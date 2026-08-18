using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE6 RID: 3046
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobStatusByGroupReq : BaseMessageReq
	{
		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0001F8DE File Offset: 0x0001DADE
		// (set) Token: 0x06004041 RID: 16449 RVA: 0x0001F8E6 File Offset: 0x0001DAE6
		[DataMember]
		public MediaJobStatusGroup MediaJobStatusGroup { get; set; }
	}
}
