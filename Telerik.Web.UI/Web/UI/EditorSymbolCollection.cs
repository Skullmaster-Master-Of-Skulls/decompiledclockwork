using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001080 RID: 4224
	public sealed class EditorSymbolCollection : EditorValueItemCollection<EditorSymbol>
	{
		// Token: 0x0600A9EA RID: 43498 RVA: 0x0024DE9B File Offset: 0x0024C09B
		internal EditorSymbolCollection()
		{
		}

		// Token: 0x0600A9EB RID: 43499 RVA: 0x0024DEA3 File Offset: 0x0024C0A3
		internal override object GetItemValue(EditorSymbol item)
		{
			return item.Value;
		}
	}
}
