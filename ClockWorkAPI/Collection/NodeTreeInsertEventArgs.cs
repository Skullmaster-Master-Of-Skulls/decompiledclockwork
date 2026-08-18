using System;

namespace ClockWorkAPI.Collection
{
	// Token: 0x0200003D RID: 61
	public class NodeTreeInsertEventArgs<T> : EventArgs
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00011424 File Offset: 0x00010424
		public NodeTreeInsertOperation Operation
		{
			get
			{
				return this._Operation;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0001143C File Offset: 0x0001043C
		public INode<T> Node
		{
			get
			{
				return this._Node;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011454 File Offset: 0x00010454
		public NodeTreeInsertEventArgs(NodeTreeInsertOperation operation, INode<T> node)
		{
			this._Operation = operation;
			this._Node = node;
		}

		// Token: 0x04000183 RID: 387
		private NodeTreeInsertOperation _Operation;

		// Token: 0x04000184 RID: 388
		private INode<T> _Node = null;
	}
}
