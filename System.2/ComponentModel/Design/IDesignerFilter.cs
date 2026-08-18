using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E8 RID: 1512
	public interface IDesignerFilter
	{
		// Token: 0x06003802 RID: 14338
		void PostFilterAttributes(IDictionary attributes);

		// Token: 0x06003803 RID: 14339
		void PostFilterEvents(IDictionary events);

		// Token: 0x06003804 RID: 14340
		void PostFilterProperties(IDictionary properties);

		// Token: 0x06003805 RID: 14341
		void PreFilterAttributes(IDictionary attributes);

		// Token: 0x06003806 RID: 14342
		void PreFilterEvents(IDictionary events);

		// Token: 0x06003807 RID: 14343
		void PreFilterProperties(IDictionary properties);
	}
}
