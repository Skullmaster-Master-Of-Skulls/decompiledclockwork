using System;

namespace ClockWorkAPI.Collection
{
	// Token: 0x0200003B RID: 59
	public class NodeTreeNodeEventArgs<T> : EventArgs
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000113F0 File Offset: 0x000103F0
		public INode<T> Node
		{
			get
			{
				return this._Node;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011408 File Offset: 0x00010408
		public NodeTreeNodeEventArgs(INode<T> node)
		{
			this._Node = node;
		}

		// Token: 0x0400017D RID: 381
		private INode<T> _Node = null;
	}
}
