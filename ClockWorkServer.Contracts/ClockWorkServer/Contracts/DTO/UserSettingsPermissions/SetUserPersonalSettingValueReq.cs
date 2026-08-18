using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000131 RID: 305
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetUserPersonalSettingValueReq : BaseMessageReq
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00003514 File Offset: 0x00001714
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0000351C File Offset: 0x0000171C
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00003525 File Offset: 0x00001725
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0000352D File Offset: 0x0000172D
		[DataMember]
		public eSettingCode SettingCode { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00003536 File Offset: 0x00001736
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0000353E File Offset: 0x0000173E
		[DataMember]
		public int IntVal { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00003547 File Offset: 0x00001747
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0000354F File Offset: 0x0000174F
		[DataMember]
		public string StringVal { get; set; }
	}
}
