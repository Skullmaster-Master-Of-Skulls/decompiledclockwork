using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C65 RID: 3173
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentRequestedInfoByIdResp
	{
		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x0002045B File Offset: 0x0001E65B
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x00020463 File Offset: 0x0001E663
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
