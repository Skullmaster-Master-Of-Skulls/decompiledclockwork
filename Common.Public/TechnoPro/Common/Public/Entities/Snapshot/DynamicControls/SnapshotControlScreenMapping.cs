using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.DynamicControls
{
	// Token: 0x020001C7 RID: 455
	public class SnapshotControlScreenMapping
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0001484A File Offset: 0x00012A4A
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00014852 File Offset: 0x00012A52
		public int DynamicScreenControlID { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001485B File Offset: 0x00012A5B
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00014863 File Offset: 0x00012A63
		public int ScreenNum { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0001486C File Offset: 0x00012A6C
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00014874 File Offset: 0x00012A74
		public int ControlId { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0001487D File Offset: 0x00012A7D
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00014885 File Offset: 0x00012A85
		public int OrderNum { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0001488E File Offset: 0x00012A8E
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00014896 File Offset: 0x00012A96
		public bool IsActive { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0001489F File Offset: 0x00012A9F
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x000148A7 File Offset: 0x00012AA7
		public string P { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000148B0 File Offset: 0x00012AB0
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x000148B8 File Offset: 0x00012AB8
		public bool StatsHolding { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000148C1 File Offset: 0x00012AC1
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x000148C9 File Offset: 0x00012AC9
		public string ControlGroup { get; set; }
	}
}
