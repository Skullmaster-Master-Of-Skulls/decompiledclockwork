using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B4 RID: 948
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffWithCommonInfoByIdResp
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00009E40 File Offset: 0x00008040
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00009E48 File Offset: 0x00008048
		[DataMember]
		public StaffWithCommonInfoDTO StaffWithCommonInfo { get; set; }
	}
}
