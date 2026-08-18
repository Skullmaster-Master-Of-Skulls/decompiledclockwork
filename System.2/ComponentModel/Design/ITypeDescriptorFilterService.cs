using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F9 RID: 1529
	public interface ITypeDescriptorFilterService
	{
		// Token: 0x06003861 RID: 14433
		bool FilterAttributes(IComponent component, IDictionary attributes);

		// Token: 0x06003862 RID: 14434
		bool FilterEvents(IComponent component, IDictionary events);

		// Token: 0x06003863 RID: 14435
		bool FilterProperties(IComponent component, IDictionary properties);
	}
}
