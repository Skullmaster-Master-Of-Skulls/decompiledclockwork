using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200054B RID: 1355
	public interface IWebPartRow
	{
		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060044F3 RID: 17651
		PropertyDescriptorCollection Schema { get; }

		// Token: 0x060044F4 RID: 17652
		void GetRowData(RowCallback callback);
	}
}
