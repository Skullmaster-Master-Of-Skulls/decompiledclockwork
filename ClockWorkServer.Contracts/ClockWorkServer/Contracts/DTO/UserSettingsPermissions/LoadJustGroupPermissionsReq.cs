using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013C RID: 316
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadJustGroupPermissionsReq : BaseMessageReq
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0000368A File Offset: 0x0000188A
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00003692 File Offset: 0x00001892
		[DataMember]
		public int ForGroupId { get; set; }
	}
}
