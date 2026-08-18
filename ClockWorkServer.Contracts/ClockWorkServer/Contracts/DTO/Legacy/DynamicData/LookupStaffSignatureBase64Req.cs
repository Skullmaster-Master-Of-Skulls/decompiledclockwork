using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E3 RID: 1251
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupStaffSignatureBase64Req : BaseMessageReq
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		[DataMember]
		public int PersonId { get; set; }
	}
}
