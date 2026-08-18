using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200055E RID: 1374
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		// Token: 0x06003393 RID: 13203
		void Add(IComponent component);

		// Token: 0x06003394 RID: 13204
		void Add(IComponent component, string name);

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003395 RID: 13205
		ComponentCollection Components { get; }

		// Token: 0x06003396 RID: 13206
		void Remove(IComponent component);
	}
}
