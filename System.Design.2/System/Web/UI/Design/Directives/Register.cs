using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200018A RID: 394
	internal class Register
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00054555 File Offset: 0x00052755
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0005455D File Offset: 0x0005275D
		[Filterable(false)]
		public string Assembly { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00054566 File Offset: 0x00052766
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0005456E File Offset: 0x0005276E
		[Filterable(false)]
		public string Namespace { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00054577 File Offset: 0x00052777
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0005457F File Offset: 0x0005277F
		[Filterable(false)]
		[UrlProperty("*.ascx")]
		public string Src { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00054588 File Offset: 0x00052788
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00054590 File Offset: 0x00052790
		[Filterable(false)]
		public string TagName { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00054599 File Offset: 0x00052799
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x000545A1 File Offset: 0x000527A1
		[Filterable(false)]
		public string TagPrefix { get; set; }
	}
}
