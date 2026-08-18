using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000172 RID: 370
	internal class Application
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00053B59 File Offset: 0x00051D59
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00053B61 File Offset: 0x00051D61
		[Browsable(false)]
		[Filterable(false)]
		public string ClassName { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00053B6A File Offset: 0x00051D6A
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00053B72 File Offset: 0x00051D72
		public string CodeBehind { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00053B7B File Offset: 0x00051D7B
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00053B83 File Offset: 0x00051D83
		[Filterable(false)]
		[Directive(RenameType = "class")]
		public string Inherits { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00053B8C File Offset: 0x00051D8C
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00053B94 File Offset: 0x00051D94
		[Filterable(false)]
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }
	}
}
