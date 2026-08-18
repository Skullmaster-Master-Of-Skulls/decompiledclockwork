using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.DynamicControls
{
	// Token: 0x020001CA RID: 458
	public class SnapshotLookupLists
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00014ABF File Offset: 0x00012CBF
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00014AC7 File Offset: 0x00012CC7
		public int LookupListId { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00014AD0 File Offset: 0x00012CD0
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public int LookupGroupId { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00014AE1 File Offset: 0x00012CE1
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00014AE9 File Offset: 0x00012CE9
		public string LookupText { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00014AF2 File Offset: 0x00012CF2
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00014AFA File Offset: 0x00012CFA
		public int OrderNum { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00014B03 File Offset: 0x00012D03
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00014B0B File Offset: 0x00012D0B
		public string LookupValue { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00014B14 File Offset: 0x00012D14
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00014B1C File Offset: 0x00012D1C
		public bool Visible { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00014B25 File Offset: 0x00012D25
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00014B2D File Offset: 0x00012D2D
		public string Children { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00014B36 File Offset: 0x00012D36
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00014B3E File Offset: 0x00012D3E
		public string XmlParams { get; set; }
	}
}
