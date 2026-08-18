using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200012B RID: 299
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupSettingsResp
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000034AE File Offset: 0x000016AE
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000034B6 File Offset: 0x000016B6
		[DataMember]
		public IList<OldUserSettingDTO> Settings { get; set; }
	}
}
