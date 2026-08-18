using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000141 RID: 321
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserPermissionDTO
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00003898 File Offset: 0x00001A98
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x000038A0 File Offset: 0x00001AA0
		[DataMember]
		public int PersonOrGroupId { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000038A9 File Offset: 0x00001AA9
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000038B1 File Offset: 0x00001AB1
		[DataMember]
		public eUserPermissionType PermissionType { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000038BA File Offset: 0x00001ABA
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x000038C2 File Offset: 0x00001AC2
		[DataMember]
		public UserPermissionEnum Permission { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x000038CB File Offset: 0x00001ACB
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x000038D3 File Offset: 0x00001AD3
		[DataMember]
		public int PermissionValue { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x000038DC File Offset: 0x00001ADC
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x000038E4 File Offset: 0x00001AE4
		[DataMember]
		public int OrderNum { get; set; }
	}
}
