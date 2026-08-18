using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x02000576 RID: 1398
	[ComVisible(true)]
	public interface ISite : IServiceProvider
	{
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060033E6 RID: 13286
		IComponent Component { get; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060033E7 RID: 13287
		IContainer Container { get; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060033E8 RID: 13288
		bool DesignMode { get; }

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060033E9 RID: 13289
		// (set) Token: 0x060033EA RID: 13290
		string Name { get; set; }
	}
}
