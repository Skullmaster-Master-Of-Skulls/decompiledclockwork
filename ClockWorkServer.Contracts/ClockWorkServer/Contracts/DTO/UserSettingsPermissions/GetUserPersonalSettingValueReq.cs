using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200012F RID: 303
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUserPersonalSettingValueReq : BaseMessageReq
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000034E1 File Offset: 0x000016E1
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x000034E9 File Offset: 0x000016E9
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000034F2 File Offset: 0x000016F2
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x000034FA File Offset: 0x000016FA
		[DataMember]
		public eSettingCode SettingCode { get; set; }
	}
}
