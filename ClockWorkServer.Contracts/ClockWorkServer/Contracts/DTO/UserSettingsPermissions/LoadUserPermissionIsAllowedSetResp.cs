using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000139 RID: 313
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUserPermissionIsAllowedSetResp
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00003657 File Offset: 0x00001857
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0000365F File Offset: 0x0000185F
		[DataMember]
		public UserPermissionIsAllowedSetDTO IsAllowedSet { get; set; }
	}
}
