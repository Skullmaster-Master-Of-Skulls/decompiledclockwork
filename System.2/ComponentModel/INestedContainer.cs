using System;

namespace System.ComponentModel
{
	// Token: 0x02000566 RID: 1382
	public interface INestedContainer : IContainer, IDisposable
	{
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060033B3 RID: 13235
		IComponent Owner { get; }
	}
}
