using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000127 RID: 295
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllUserSettingsResp
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000346A File Offset: 0x0000166A
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x00003472 File Offset: 0x00001672
		[DataMember]
		public IList<OldUserSettingDTO> Settings { get; set; }
	}
}
