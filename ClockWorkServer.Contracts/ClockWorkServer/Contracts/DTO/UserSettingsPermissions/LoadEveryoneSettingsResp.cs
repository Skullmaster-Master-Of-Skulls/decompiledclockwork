using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200012E RID: 302
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadEveryoneSettingsResp
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000034D0 File Offset: 0x000016D0
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x000034D8 File Offset: 0x000016D8
		[DataMember]
		public IList<OldUserSettingDTO> Settings { get; set; }
	}
}
