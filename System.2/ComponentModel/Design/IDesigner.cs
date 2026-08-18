using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E6 RID: 1510
	[ComVisible(true)]
	public interface IDesigner : IDisposable
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060037F4 RID: 14324
		IComponent Component { get; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060037F5 RID: 14325
		DesignerVerbCollection Verbs { get; }

		// Token: 0x060037F6 RID: 14326
		void DoDefaultAction();

		// Token: 0x060037F7 RID: 14327
		void Initialize(IComponent component);
	}
}
