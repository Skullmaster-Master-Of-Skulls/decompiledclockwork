using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200057A RID: 1402
	[ComVisible(true)]
	public interface ITypeDescriptorContext : IServiceProvider
	{
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060033F4 RID: 13300
		IContainer Container { get; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060033F5 RID: 13301
		object Instance { get; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060033F6 RID: 13302
		PropertyDescriptor PropertyDescriptor { get; }

		// Token: 0x060033F7 RID: 13303
		bool OnComponentChanging();

		// Token: 0x060033F8 RID: 13304
		void OnComponentChanged();
	}
}
