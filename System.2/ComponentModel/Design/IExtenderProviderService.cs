using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005EF RID: 1519
	public interface IExtenderProviderService
	{
		// Token: 0x06003833 RID: 14387
		void AddExtenderProvider(IExtenderProvider provider);

		// Token: 0x06003834 RID: 14388
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
