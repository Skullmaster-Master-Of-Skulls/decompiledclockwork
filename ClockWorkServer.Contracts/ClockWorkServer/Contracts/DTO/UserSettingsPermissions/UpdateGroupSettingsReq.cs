using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000125 RID: 293
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupSettingsReq : BaseMessageReq
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00003448 File Offset: 0x00001648
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00003450 File Offset: 0x00001650
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00003459 File Offset: 0x00001659
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00003461 File Offset: 0x00001661
		[DataMember]
		public List<OldUserSettingDTO> Settings { get; set; }
	}
}
