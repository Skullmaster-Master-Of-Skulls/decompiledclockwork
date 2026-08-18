using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200016F RID: 367
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class DirectiveAttribute : Attribute
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x000534E2 File Offset: 0x000516E2
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x000534EA File Offset: 0x000516EA
		public bool Culture { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000534F3 File Offset: 0x000516F3
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x000534FB File Offset: 0x000516FB
		public string BuilderType { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00053504 File Offset: 0x00051704
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0005350C File Offset: 0x0005170C
		public bool AllowedOnMobilePages { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00053515 File Offset: 0x00051715
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x0005351D File Offset: 0x0005171D
		public string RenameType { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00053526 File Offset: 0x00051726
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0005352E File Offset: 0x0005172E
		public bool ServerLanguageExtensions { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00053537 File Offset: 0x00051737
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0005353F File Offset: 0x0005173F
		public bool ServerLanguageNames { get; set; }

		// Token: 0x06000D1D RID: 3357 RVA: 0x00053548 File Offset: 0x00051748
		public DirectiveAttribute()
		{
			this.AllowedOnMobilePages = true;
		}
	}
}
