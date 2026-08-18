using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001059 RID: 4185
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class EditorValueItem : StateManager
	{
		// Token: 0x0600A905 RID: 43269 RVA: 0x0024B8D8 File Offset: 0x00249AD8
		public EditorValueItem()
		{
		}

		// Token: 0x0600A906 RID: 43270 RVA: 0x0024B8E0 File Offset: 0x00249AE0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorValueItem(string value)
		{
			this.Value = value;
		}

		// Token: 0x1700363E RID: 13886
		// (get) Token: 0x0600A907 RID: 43271 RVA: 0x0024B8EF File Offset: 0x00249AEF
		// (set) Token: 0x0600A908 RID: 43272 RVA: 0x0024B91E File Offset: 0x00249B1E
		public virtual string Value
		{
			get
			{
				if (base.ViewState["Value"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Value"];
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
