using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200012A RID: 298
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupSettingsReq : BaseMessageReq
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0000349D File Offset: 0x0000169D
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x000034A5 File Offset: 0x000016A5
		[DataMember]
		public int GroupId { get; set; }
	}
}
