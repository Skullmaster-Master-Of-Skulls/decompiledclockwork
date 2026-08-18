using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005C7 RID: 1479
	public abstract class TypeDescriptionProviderService
	{
		// Token: 0x06003755 RID: 14165
		public abstract TypeDescriptionProvider GetProvider(object instance);

		// Token: 0x06003756 RID: 14166
		public abstract TypeDescriptionProvider GetProvider(Type type);
	}
}
