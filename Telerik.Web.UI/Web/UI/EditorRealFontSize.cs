using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200107B RID: 4219
	public class EditorRealFontSize : EditorValueItem
	{
		// Token: 0x0600A9DC RID: 43484 RVA: 0x0024DDBF File Offset: 0x0024BFBF
		public EditorRealFontSize()
		{
		}

		// Token: 0x0600A9DD RID: 43485 RVA: 0x0024DDC7 File Offset: 0x0024BFC7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorRealFontSize(string value)
		{
			this.Value = value;
		}
	}
}
