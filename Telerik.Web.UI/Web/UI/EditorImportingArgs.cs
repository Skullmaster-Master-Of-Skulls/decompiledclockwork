using System;

namespace Telerik.Web.UI
{
	// Token: 0x020002A2 RID: 674
	public class EditorImportingArgs : EventArgs
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0004F659 File Offset: 0x0004D859
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0004F661 File Offset: 0x0004D861
		public object RadFlowDocument { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0004F66A File Offset: 0x0004D86A
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0004F672 File Offset: 0x0004D872
		public object HtmlFormatProvider { get; set; }

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004F67B File Offset: 0x0004D87B
		public EditorImportingArgs(object radFlowDocument, object htmlFormatProvider)
		{
			this.RadFlowDocument = radFlowDocument;
			this.HtmlFormatProvider = htmlFormatProvider;
		}
	}
}
