using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013D RID: 317
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadJustGroupPermissionsResp
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0000369B File Offset: 0x0000189B
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x000036A3 File Offset: 0x000018A3
		[DataMember]
		public UserOrGroupJustPermissionSetDTO PermissionSet { get; set; }
	}
}
