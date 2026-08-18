using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200017F RID: 383
	[SchemaElementName("Master")]
	internal class Master4_0 : Master
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00053F86 File Offset: 0x00052186
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x00053F8E File Offset: 0x0005218E
		[Browsable(false)]
		[Filterable(false)]
		public string Description { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00053F97 File Offset: 0x00052197
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00053F9F File Offset: 0x0005219F
		[DefaultValue("Inherit")]
		public ClientIDMode ClientIDMode { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00053FA8 File Offset: 0x000521A8
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00053FB0 File Offset: 0x000521B0
		public ViewStateMode ViewStateMode { get; set; }
	}
}
