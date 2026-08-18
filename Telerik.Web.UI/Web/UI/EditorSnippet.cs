using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200107D RID: 4221
	[ParseChildren(true, "Value")]
	public class EditorSnippet : EditorNameValueItem
	{
		// Token: 0x0600A9DF RID: 43487 RVA: 0x0024DDDE File Offset: 0x0024BFDE
		public EditorSnippet()
		{
		}

		// Token: 0x0600A9E0 RID: 43488 RVA: 0x0024DDE6 File Offset: 0x0024BFE6
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorSnippet(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x1700368A RID: 13962
		// (get) Token: 0x0600A9E1 RID: 43489 RVA: 0x0024DDFC File Offset: 0x0024BFFC
		// (set) Token: 0x0600A9E2 RID: 43490 RVA: 0x0024DE04 File Offset: 0x0024C004
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}
	}
}
