using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000129 RID: 297
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonSettingsResp
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0000348C File Offset: 0x0000168C
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00003494 File Offset: 0x00001694
		[DataMember]
		public IList<OldUserSettingDTO> Settings { get; set; }
	}
}
