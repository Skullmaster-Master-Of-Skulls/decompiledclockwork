using System;
using System.Diagnostics;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000042 RID: 66
	internal sealed class AntlrRuntime_BaseTreeDebugView
	{
		// Token: 0x06000329 RID: 809 RVA: 0x000087FA File Offset: 0x000069FA
		public AntlrRuntime_BaseTreeDebugView(BaseTree tree)
		{
			this._tree = tree;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000880C File Offset: 0x00006A0C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public ITree[] Children
		{
			get
			{
				if (this._tree == null || this._tree.Children == null)
				{
					return null;
				}
				ITree[] array = new ITree[this._tree.Children.Count];
				this._tree.Children.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000094 RID: 148
		private readonly BaseTree _tree;
	}
}
