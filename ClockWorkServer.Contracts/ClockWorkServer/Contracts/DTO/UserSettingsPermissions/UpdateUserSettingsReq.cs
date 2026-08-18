using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000124 RID: 292
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateUserSettingsReq : BaseMessageReq
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00003426 File Offset: 0x00001626
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0000342E File Offset: 0x0000162E
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00003437 File Offset: 0x00001637
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0000343F File Offset: 0x0000163F
		[DataMember]
		public List<OldUserSettingDTO> Settings { get; set; }
	}
}
