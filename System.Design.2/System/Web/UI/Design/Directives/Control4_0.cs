using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000178 RID: 376
	[SchemaElementName("Control")]
	internal class Control4_0 : Control
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00053D34 File Offset: 0x00051F34
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x00053D3C File Offset: 0x00051F3C
		[Browsable(false)]
		[Filterable(false)]
		public string Description { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00053D45 File Offset: 0x00051F45
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00053D4D File Offset: 0x00051F4D
		[DefaultValue("Inherit")]
		public ClientIDMode ClientIDMode { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00053D56 File Offset: 0x00051F56
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00053D5E File Offset: 0x00051F5E
		public ViewStateMode ViewStateMode { get; set; }
	}
}
