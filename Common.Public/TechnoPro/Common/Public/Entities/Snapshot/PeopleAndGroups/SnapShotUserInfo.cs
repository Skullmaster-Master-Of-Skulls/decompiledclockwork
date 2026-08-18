using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.PeopleAndGroups
{
	// Token: 0x020001BF RID: 447
	public class SnapShotUserInfo
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000144B4 File Offset: 0x000126B4
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x000144BC File Offset: 0x000126BC
		public byte[] Username { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x000144C5 File Offset: 0x000126C5
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x000144CD File Offset: 0x000126CD
		public int PersonId { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000144D6 File Offset: 0x000126D6
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x000144DE File Offset: 0x000126DE
		public byte[] Pass { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000144E7 File Offset: 0x000126E7
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000144EF File Offset: 0x000126EF
		public bool RequirePasswordChange { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000144F8 File Offset: 0x000126F8
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00014500 File Offset: 0x00012700
		public DateTime? LastPasswordChangeDate { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00014509 File Offset: 0x00012709
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00014511 File Offset: 0x00012711
		public DateTime? PasswordExpiryDate { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0001451A File Offset: 0x0001271A
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00014522 File Offset: 0x00012722
		public bool IsEncrypted { get; set; }
	}
}
