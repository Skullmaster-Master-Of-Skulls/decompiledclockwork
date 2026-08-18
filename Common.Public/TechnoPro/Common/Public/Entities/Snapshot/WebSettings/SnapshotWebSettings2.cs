using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.WebSettings
{
	// Token: 0x020001B5 RID: 437
	public class SnapshotWebSettings2
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00013FFD File Offset: 0x000121FD
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00014005 File Offset: 0x00012205
		public int WebSettingId { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001400E File Offset: 0x0001220E
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00014016 File Offset: 0x00012216
		public string InstanceName { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001401F File Offset: 0x0001221F
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00014027 File Offset: 0x00012227
		public int SettingCode { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00014030 File Offset: 0x00012230
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x00014038 File Offset: 0x00012238
		public byte[] SettingStringValue { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00014041 File Offset: 0x00012241
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x00014049 File Offset: 0x00012249
		public string UserComment { get; set; }
	}
}
