using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000135 RID: 309
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingValueStringReq : BaseMessageReq
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000358B File Offset: 0x0000178B
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00003593 File Offset: 0x00001793
		[DataMember]
		public eSettingCode SettingCode { get; set; }
	}
}
