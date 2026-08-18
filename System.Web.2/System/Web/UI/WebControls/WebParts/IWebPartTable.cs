using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200054C RID: 1356
	public interface IWebPartTable
	{
		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060044F5 RID: 17653
		PropertyDescriptorCollection Schema { get; }

		// Token: 0x060044F6 RID: 17654
		void GetTableData(TableCallback callback);
	}
}
