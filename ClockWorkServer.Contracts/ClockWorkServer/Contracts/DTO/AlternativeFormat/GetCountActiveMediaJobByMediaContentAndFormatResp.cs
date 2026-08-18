using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB1 RID: 2993
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCountActiveMediaJobByMediaContentAndFormatResp
	{
		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x0001F2C2 File Offset: 0x0001D4C2
		// (set) Token: 0x06003F54 RID: 16212 RVA: 0x0001F2CA File Offset: 0x0001D4CA
		[DataMember]
		public int CountActiveJobs { get; set; }
	}
}
