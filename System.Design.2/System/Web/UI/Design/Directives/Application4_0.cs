using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000174 RID: 372
	[SchemaElementName("Application")]
	internal class Application4_0 : Application
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00053BB6 File Offset: 0x00051DB6
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00053BBE File Offset: 0x00051DBE
		[Browsable(false)]
		[Filterable(false)]
		public string Description { get; set; }
	}
}
