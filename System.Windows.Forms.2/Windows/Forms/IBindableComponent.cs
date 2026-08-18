using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000288 RID: 648
	public interface IBindableComponent : IComponent, IDisposable
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600298E RID: 10638
		ControlBindingsCollection DataBindings { get; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600298F RID: 10639
		// (set) Token: 0x06002990 RID: 10640
		BindingContext BindingContext { get; set; }
	}
}
