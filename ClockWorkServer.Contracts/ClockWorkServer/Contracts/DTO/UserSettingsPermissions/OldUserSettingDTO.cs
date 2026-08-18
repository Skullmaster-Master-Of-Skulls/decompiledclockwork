using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000137 RID: 311
	[DataContract(Namespace = "http://tpro.ca")]
	public class OldUserSettingDTO
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000035AD File Offset: 0x000017AD
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x000035B5 File Offset: 0x000017B5
		[DataMember]
		public int SettingIdOrSettingGroupId { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x000035C6 File Offset: 0x000017C6
		[DataMember]
		public eSettingCode SettingCode { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000035CF File Offset: 0x000017CF
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000035D7 File Offset: 0x000017D7
		[DataMember]
		public string StringVal { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000035E0 File Offset: 0x000017E0
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x000035E8 File Offset: 0x000017E8
		[DataMember]
		public int IntVal { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000035F1 File Offset: 0x000017F1
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x000035F9 File Offset: 0x000017F9
		[DataMember]
		public eDataItemModificationStatus ModificationStatus { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00003602 File Offset: 0x00001802
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0000360A File Offset: 0x0000180A
		[DataMember]
		public int PersonOrGroupId { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00003613 File Offset: 0x00001813
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0000361B File Offset: 0x0000181B
		[DataMember]
		public eOldUserSettingType SettingType { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0000362C File Offset: 0x0000182C
		[DataMember]
		public int OrderNum { get; set; }
	}
}
