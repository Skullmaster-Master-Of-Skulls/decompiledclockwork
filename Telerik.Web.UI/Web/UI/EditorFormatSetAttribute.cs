using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000B42 RID: 2882
	public class EditorFormatSetAttribute : EditorNameValueItem
	{
		// Token: 0x06006CBA RID: 27834 RVA: 0x001939FE File Offset: 0x00191BFE
		public EditorFormatSetAttribute()
		{
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x00193A06 File Offset: 0x00191C06
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorFormatSetAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
