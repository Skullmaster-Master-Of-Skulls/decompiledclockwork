using System;

namespace System.Web.UI
{
	// Token: 0x0200029E RID: 670
	public interface IDataBindingsAccessor
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001F82 RID: 8066
		DataBindingCollection DataBindings { get; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001F83 RID: 8067
		bool HasDataBindings { get; }
	}
}
