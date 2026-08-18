using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.OldSettingsAndPermissions
{
	// Token: 0x020001C2 RID: 450
	public class SnapshotSettings
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000145B3 File Offset: 0x000127B3
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000145BB File Offset: 0x000127BB
		public int SettingId { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000145C4 File Offset: 0x000127C4
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x000145CC File Offset: 0x000127CC
		public int PersonId { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000145D5 File Offset: 0x000127D5
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x000145DD File Offset: 0x000127DD
		public int SettingCode { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000145E6 File Offset: 0x000127E6
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x000145EE File Offset: 0x000127EE
		public int SettingValue { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000145F7 File Offset: 0x000127F7
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000145FF File Offset: 0x000127FF
		public string SettingStringValue { get; set; }
	}
}
