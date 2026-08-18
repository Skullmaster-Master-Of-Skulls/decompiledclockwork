using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B7B RID: 2939
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaContentResp
	{
		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x0001E7A7 File Offset: 0x0001C9A7
		// (set) Token: 0x06003E28 RID: 15912 RVA: 0x0001E7AF File Offset: 0x0001C9AF
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
