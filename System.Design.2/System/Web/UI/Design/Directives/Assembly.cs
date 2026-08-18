using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000175 RID: 373
	internal class Assembly
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00053BC7 File Offset: 0x00051DC7
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x00053BCF File Offset: 0x00051DCF
		[Filterable(false)]
		public string Name { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00053BD8 File Offset: 0x00051DD8
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x00053BE0 File Offset: 0x00051DE0
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string Src { get; set; }
	}
}
