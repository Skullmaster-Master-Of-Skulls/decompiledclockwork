using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001071 RID: 4209
	public class EditorCssClass : EditorNameValueItem
	{
		// Token: 0x0600A9B7 RID: 43447 RVA: 0x0024DAF5 File Offset: 0x0024BCF5
		public EditorCssClass()
		{
		}

		// Token: 0x0600A9B8 RID: 43448 RVA: 0x0024DAFD File Offset: 0x0024BCFD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorCssClass(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
