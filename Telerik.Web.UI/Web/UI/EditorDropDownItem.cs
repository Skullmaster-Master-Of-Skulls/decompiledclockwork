using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x020019EC RID: 6636
	public class EditorDropDownItem : EditorNameValueItem
	{
		// Token: 0x060100E1 RID: 65761 RVA: 0x0039A34E File Offset: 0x0039854E
		public EditorDropDownItem()
		{
		}

		// Token: 0x060100E2 RID: 65762 RVA: 0x0039A356 File Offset: 0x00398556
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorDropDownItem(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
