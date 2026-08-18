using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBD RID: 3005
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobByIdResp
	{
		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x0001F39F File Offset: 0x0001D59F
		// (set) Token: 0x06003F7A RID: 16250 RVA: 0x0001F3A7 File Offset: 0x0001D5A7
		[DataMember]
		public CompletedMediaJobDTO MediaJob { get; set; }
	}
}
