using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000132 RID: 306
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveSettingsReq : BaseMessageReq
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00003560 File Offset: 0x00001760
		[DataMember]
		public IList<OldUserSettingDTO> Settings { get; set; }
	}
}
