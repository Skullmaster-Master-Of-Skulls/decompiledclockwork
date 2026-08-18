using System;
using System.Collections.Generic;
using System.Text;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class CommonTreeNodeStream : LookaheadStream<object>, ITreeNodeStream, IIntStream, IPositionTrackingStream
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00009597 File Offset: 0x00007797
		public CommonTreeNodeStream(object tree) : this(new CommonTreeAdaptor(), tree)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000095A5 File Offset: 0x000077A5
		public CommonTreeNodeStream(ITreeAdaptor adaptor, object tree)
		{
			this._root = tree;
			this._adaptor = adaptor;
			this._it = new TreeIterator(adaptor, this._root);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000095CD File Offset: 0x000077CD
		public virtual string SourceName
		{
			get
			{
				if (this.TokenStream == null)
				{
					return null;
				}
				return this.TokenStream.SourceName;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000095E4 File Offset: 0x000077E4
		// (set) Token: 0x0600038B RID: 907 RVA: 0x000095EC File Offset: 0x000077EC
		public virtual ITokenStream TokenStream
		{
			get
			{
				return this.tokens;
			}
			set
			{
				this.tokens = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000095F5 File Offset: 0x000077F5
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000095FD File Offset: 0x000077FD
		public virtual ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this._adaptor;
			}
			set
			{
				this._adaptor = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00009606 File Offset: 0x00007806
		public virtual object TreeSource
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000960E File Offset: 0x0000780E
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00009611 File Offset: 0x00007811
		public virtual bool UniqueNavigationNodes
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009613 File Offset: 0x00007813
		public override void Reset()
		{
			base.Reset();
			this._it.Reset();
			this._hasNilRoot = false;
			this._level = 0;
			this._previousLocationElement = null;
			if (this._calls != null)
			{
				this._calls.Clear();
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009650 File Offset: 0x00007850
		public override object NextElement()
		{
			this._it.MoveNext();
			object obj = this._it.Current;
			if (obj == this._it.up)
			{
				this._level--;
				if (this._level == 0 && this._hasNilRoot)
				{
					this._it.MoveNext();
					return this._it.Current;
				}
			}
			else if (obj == this._it.down)
			{
				this._level++;
			}
			if (this._level == 0 && this.TreeAdaptor.IsNil(obj))
			{
				this._hasNilRoot = true;
				this._it.MoveNext();
				obj = this._it.Current;
				this._level++;
				this._it.MoveNext();
				obj = this._it.Current;
			}
			return obj;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009734 File Offset: 0x00007934
		public override object Dequeue()
		{
			object result = base.Dequeue();
			if (this._p == 0 && this.HasPositionInformation(base.PreviousElement))
			{
				this._previousLocationElement = base.PreviousElement;
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000976B File Offset: 0x0000796B
		public override bool IsEndOfFile(object o)
		{
			return this.TreeAdaptor.GetType(o) == -1;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000977C File Offset: 0x0000797C
		public virtual int LA(int i)
		{
			return this.TreeAdaptor.GetType(this.LT(i));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009790 File Offset: 0x00007990
		public virtual void Push(int index)
		{
			if (this._calls == null)
			{
				this._calls = new Stack<int>();
			}
			this._calls.Push(this._p);
			this.Seek(index);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000097C0 File Offset: 0x000079C0
		public virtual int Pop()
		{
			int num = this._calls.Pop();
			this.Seek(num);
			return num;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000097E4 File Offset: 0x000079E4
		public object GetKnownPositionElement(bool allowApproximateLocation)
		{
			object obj = this._data[this._p];
			if (this.HasPositionInformation(obj))
			{
				return obj;
			}
			if (!allowApproximateLocation)
			{
				return null;
			}
			for (int i = this._p - 1; i >= 0; i--)
			{
				obj = this._data[i];
				if (this.HasPositionInformation(obj))
				{
					return obj;
				}
			}
			return this._previousLocationElement;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009844 File Offset: 0x00007A44
		public bool HasPositionInformation(object node)
		{
			IToken token = this.TreeAdaptor.GetToken(node);
			return token != null && token.Line > 0;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000986F File Offset: 0x00007A6F
		public virtual void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			if (parent != null)
			{
				this.TreeAdaptor.ReplaceChildren(parent, startChildIndex, stopChildIndex, t);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009884 File Offset: 0x00007A84
		public virtual string ToString(object start, object stop)
		{
			return "n/a";
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000988C File Offset: 0x00007A8C
		public virtual string ToTokenTypeString()
		{
			this.Reset();
			StringBuilder stringBuilder = new StringBuilder();
			object t = this.LT(1);
			for (int type = this.TreeAdaptor.GetType(t); type != -1; type = this.TreeAdaptor.GetType(t))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(type);
				this.Consume();
				t = this.LT(1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000AD RID: 173
		public const int DEFAULT_INITIAL_BUFFER_SIZE = 100;

		// Token: 0x040000AE RID: 174
		public const int INITIAL_CALL_STACK_SIZE = 10;

		// Token: 0x040000AF RID: 175
		private readonly object _root;

		// Token: 0x040000B0 RID: 176
		protected ITokenStream tokens;

		// Token: 0x040000B1 RID: 177
		[NonSerialized]
		private ITreeAdaptor _adaptor;

		// Token: 0x040000B2 RID: 178
		private readonly TreeIterator _it;

		// Token: 0x040000B3 RID: 179
		private Stack<int> _calls;

		// Token: 0x040000B4 RID: 180
		private bool _hasNilRoot;

		// Token: 0x040000B5 RID: 181
		private int _level;

		// Token: 0x040000B6 RID: 182
		private object _previousLocationElement;
	}
}
