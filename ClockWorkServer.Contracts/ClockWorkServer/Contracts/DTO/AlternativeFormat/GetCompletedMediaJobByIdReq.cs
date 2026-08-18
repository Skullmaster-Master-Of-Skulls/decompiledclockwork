using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBC RID: 3004
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobByIdReq : BaseMessageReq
	{
		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x0001F38E File Offset: 0x0001D58E
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x0001F396 File Offset: 0x0001D596
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
