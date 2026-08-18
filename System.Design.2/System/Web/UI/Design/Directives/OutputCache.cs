using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000182 RID: 386
	internal class OutputCache
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00053FFD File Offset: 0x000521FD
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x00054005 File Offset: 0x00052205
		public string CacheProfile { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0005400E File Offset: 0x0005220E
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x00054016 File Offset: 0x00052216
		[Filterable(false)]
		public int Duration { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0005401F File Offset: 0x0005221F
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x00054027 File Offset: 0x00052227
		[Filterable(false)]
		public OutputCacheLocation Location { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00054030 File Offset: 0x00052230
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00054038 File Offset: 0x00052238
		[Filterable(false)]
		public bool NoStore { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00054041 File Offset: 0x00052241
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00054049 File Offset: 0x00052249
		[Filterable(false)]
		public string SqlDependency { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00054052 File Offset: 0x00052252
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0005405A File Offset: 0x0005225A
		[Filterable(false)]
		public string VaryByControl { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00054063 File Offset: 0x00052263
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0005406B File Offset: 0x0005226B
		[Filterable(false)]
		public string VaryByContentEncoding { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00054074 File Offset: 0x00052274
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0005407C File Offset: 0x0005227C
		[Filterable(false)]
		public string VaryByCustom { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00054085 File Offset: 0x00052285
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0005408D File Offset: 0x0005228D
		[Filterable(false)]
		public string VaryByHeader { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00054096 File Offset: 0x00052296
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0005409E File Offset: 0x0005229E
		[Filterable(false)]
		public string VaryByParam { get; set; }
	}
}
