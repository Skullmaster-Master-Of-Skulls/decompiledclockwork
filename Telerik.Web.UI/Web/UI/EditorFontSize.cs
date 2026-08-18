using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001075 RID: 4213
	public class EditorFontSize : EditorValueItem
	{
		// Token: 0x0600A9BD RID: 43453 RVA: 0x0024DB3A File Offset: 0x0024BD3A
		public EditorFontSize()
		{
		}

		// Token: 0x0600A9BE RID: 43454 RVA: 0x0024DB42 File Offset: 0x0024BD42
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorFontSize(string value)
		{
			this.Value = value;
		}
	}
}
