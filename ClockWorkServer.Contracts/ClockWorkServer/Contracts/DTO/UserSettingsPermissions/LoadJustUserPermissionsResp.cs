using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200013B RID: 315
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadJustUserPermissionsResp
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00003679 File Offset: 0x00001879
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00003681 File Offset: 0x00001881
		[DataMember]
		public UserOrGroupJustPermissionSetDTO PermissionSet { get; set; }
	}
}
