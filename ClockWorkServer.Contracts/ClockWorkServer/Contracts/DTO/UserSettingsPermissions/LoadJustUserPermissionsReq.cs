using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013A RID: 314
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadJustUserPermissionsReq : BaseMessageReq
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00003668 File Offset: 0x00001868
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00003670 File Offset: 0x00001870
		[DataMember]
		public int ForPersonId { get; set; }
	}
}
