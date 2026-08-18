using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAF RID: 2991
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCountActiveMediaJobByMediaContentPerFormatIdResp
	{
		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x0001F27E File Offset: 0x0001D47E
		// (set) Token: 0x06003F4A RID: 16202 RVA: 0x0001F286 File Offset: 0x0001D486
		[DataMember]
		public int CountActiveJobs { get; set; }
	}
}
