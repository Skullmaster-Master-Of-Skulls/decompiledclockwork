using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001073 RID: 4211
	public class EditorFont : EditorValueItem
	{
		// Token: 0x0600A9BA RID: 43450 RVA: 0x0024DB1B File Offset: 0x0024BD1B
		public EditorFont()
		{
		}

		// Token: 0x0600A9BB RID: 43451 RVA: 0x0024DB23 File Offset: 0x0024BD23
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorFont(string name)
		{
			this.Value = name;
		}
	}
}
