using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C63 RID: 3171
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsMediaContentAlreadyRequestedResp
	{
		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x00020439 File Offset: 0x0001E639
		// (set) Token: 0x06004214 RID: 16916 RVA: 0x00020441 File Offset: 0x0001E641
		[DataMember]
		public bool WasRequested { get; set; }
	}
}
