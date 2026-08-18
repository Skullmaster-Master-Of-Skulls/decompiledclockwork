using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000173 RID: 371
	[SchemaElementName("Application")]
	internal class Application2_0 : Application
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00053B9D File Offset: 0x00051D9D
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00053BA5 File Offset: 0x00051DA5
		[Filterable(false)]
		public string Description { get; set; }
	}
}
