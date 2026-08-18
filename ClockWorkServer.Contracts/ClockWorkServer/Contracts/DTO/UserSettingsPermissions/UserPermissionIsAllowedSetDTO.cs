using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000143 RID: 323
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserPermissionIsAllowedSetDTO
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00003920 File Offset: 0x00001B20
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00003928 File Offset: 0x00001B28
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00003931 File Offset: 0x00001B31
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00003939 File Offset: 0x00001B39
		[DataMember]
		public IList<UserPermissionIsAllowedDTO> GeneralPermissionsAllowed { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00003942 File Offset: 0x00001B42
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0000394A File Offset: 0x00001B4A
		[DataMember]
		public IList<int> ScreenNumsAllowedViewScreen { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00003953 File Offset: 0x00001B53
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0000395B File Offset: 0x00001B5B
		[DataMember]
		public IList<int> ScreenNumsAllowedModifyScreen { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00003964 File Offset: 0x00001B64
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0000396C File Offset: 0x00001B6C
		[DataMember]
		public IList<int> ScreenNumsAllowedCreateScreen { get; set; }
	}
}
