using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000180 RID: 384
	[Directive(AllowedOnMobilePages = false)]
	internal class MasterType
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00053FB9 File Offset: 0x000521B9
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00053FC1 File Offset: 0x000521C1
		[Filterable(false)]
		public string TypeName { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00053FCA File Offset: 0x000521CA
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00053FD2 File Offset: 0x000521D2
		[Filterable(false)]
		[UrlProperty("*.master")]
		public string VirtualPath { get; set; }
	}
}
