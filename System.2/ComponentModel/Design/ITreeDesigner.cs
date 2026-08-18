using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F8 RID: 1528
	public interface ITreeDesigner : IDesigner, IDisposable
	{
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600385F RID: 14431
		ICollection Children { get; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003860 RID: 14432
		IDesigner Parent { get; }
	}
}
