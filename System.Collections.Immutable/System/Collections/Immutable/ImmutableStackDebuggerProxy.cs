using System;
using System.Diagnostics;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000036 RID: 54
	internal class ImmutableStackDebuggerProxy<T>
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00009265 File Offset: 0x00007465
		public ImmutableStackDebuggerProxy(ImmutableStack<T> stack)
		{
			Requires.NotNull<ImmutableStack<T>>(stack, "stack");
			this._stack = stack;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000927F File Offset: 0x0000747F
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = this._stack.ToArray<T>();
				}
				return this._contents;
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly ImmutableStack<T> _stack;

		// Token: 0x04000044 RID: 68
		private T[] _contents;
	}
}
