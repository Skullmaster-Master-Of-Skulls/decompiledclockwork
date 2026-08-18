using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000144 RID: 324
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserPermissionSetDTO
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00003975 File Offset: 0x00001B75
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0000397D File Offset: 0x00001B7D
		public int PersonId { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00003986 File Offset: 0x00001B86
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0000398E File Offset: 0x00001B8E
		public IList<UserPermissionDTO> PersonPermissions { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00003997 File Offset: 0x00001B97
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0000399F File Offset: 0x00001B9F
		public IList<UserPermissionDTO> GroupPermissions { get; set; }
	}
}
