using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000142 RID: 322
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserPermissionIsAllowedDTO
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000038ED File Offset: 0x00001AED
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x000038F5 File Offset: 0x00001AF5
		[DataMember]
		public UserPermissionEnum Permission { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000038FE File Offset: 0x00001AFE
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00003906 File Offset: 0x00001B06
		[DataMember]
		public bool IsAllowed { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0000390F File Offset: 0x00001B0F
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00003917 File Offset: 0x00001B17
		[DataMember]
		public eUserPermissionType PermissionType { get; set; }
	}
}
