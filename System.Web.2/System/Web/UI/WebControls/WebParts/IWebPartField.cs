using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000548 RID: 1352
	public interface IWebPartField
	{
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x060044DE RID: 17630
		PropertyDescriptor Schema { get; }

		// Token: 0x060044DF RID: 17631
		void GetFieldValue(FieldCallback callback);
	}
}
