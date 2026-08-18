using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000136 RID: 310
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingValueStringResp
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000359C File Offset: 0x0000179C
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x000035A4 File Offset: 0x000017A4
		[DataMember]
		public string SettingValue { get; set; }
	}
}
