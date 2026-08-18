using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD7 RID: 3031
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobResp
	{
		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x0001F6AD File Offset: 0x0001D8AD
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x0001F6B5 File Offset: 0x0001D8B5
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
