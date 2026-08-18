using System;
using System.Collections;
using System.Collections.Generic;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000059 RID: 89
	public class TreeIterator : IEnumerator<object>, IDisposable, IEnumerator
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000AB68 File Offset: 0x00008D68
		public TreeIterator(CommonTree tree) : this(new CommonTreeAdaptor(), tree)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000AB78 File Offset: 0x00008D78
		public TreeIterator(ITreeAdaptor adaptor, object tree)
		{
			this.adaptor = adaptor;
			this.tree = tree;
			this.root = tree;
			this.nodes = new Queue<object>();
			this.down = adaptor.Create(2, "DOWN");
			this.up = adaptor.Create(3, "UP");
			this.eof = adaptor.Create(-1, "EOF");
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public object Current { get; private set; }

		// Token: 0x06000408 RID: 1032 RVA: 0x0000ABF9 File Offset: 0x00008DF9
		public void Dispose()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000ABFC File Offset: 0x00008DFC
		public bool MoveNext()
		{
			if (this.firstTime)
			{
				this.firstTime = false;
				if (this.adaptor.GetChildCount(this.tree) == 0)
				{
					this.nodes.Enqueue(this.eof);
				}
				this.Current = this.tree;
			}
			else if (this.nodes != null && this.nodes.Count > 0)
			{
				this.Current = this.nodes.Dequeue();
			}
			else if (this.tree == null)
			{
				this.Current = this.eof;
			}
			else if (this.adaptor.GetChildCount(this.tree) > 0)
			{
				this.tree = this.adaptor.GetChild(this.tree, 0);
				this.nodes.Enqueue(this.tree);
				this.Current = this.down;
			}
			else
			{
				object parent = this.adaptor.GetParent(this.tree);
				while (parent != null && this.adaptor.GetChildIndex(this.tree) + 1 >= this.adaptor.GetChildCount(parent))
				{
					this.nodes.Enqueue(this.up);
					this.tree = parent;
					parent = this.adaptor.GetParent(this.tree);
				}
				if (parent == null)
				{
					this.tree = null;
					this.nodes.Enqueue(this.eof);
					this.Current = this.nodes.Dequeue();
				}
				else
				{
					int i = this.adaptor.GetChildIndex(this.tree) + 1;
					this.tree = this.adaptor.GetChild(parent, i);
					this.nodes.Enqueue(this.tree);
					this.Current = this.nodes.Dequeue();
				}
			}
			bool result = this.Current != this.eof || !this.reachedEof;
			this.reachedEof = (this.Current == this.eof);
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		public void Reset()
		{
			this.firstTime = true;
			this.tree = this.root;
			this.nodes.Clear();
		}

		// Token: 0x040000D2 RID: 210
		protected ITreeAdaptor adaptor;

		// Token: 0x040000D3 RID: 211
		protected object root;

		// Token: 0x040000D4 RID: 212
		protected object tree;

		// Token: 0x040000D5 RID: 213
		protected bool firstTime = true;

		// Token: 0x040000D6 RID: 214
		private bool reachedEof;

		// Token: 0x040000D7 RID: 215
		public object up;

		// Token: 0x040000D8 RID: 216
		public object down;

		// Token: 0x040000D9 RID: 217
		public object eof;

		// Token: 0x040000DA RID: 218
		protected Queue<object> nodes;
	}
}
