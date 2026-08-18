using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000044 RID: 68
	public class BufferedTreeNodeStream : ITreeNodeStream, IIntStream, ITokenStreamInformation
	{
		// Token: 0x06000334 RID: 820 RVA: 0x00008859 File Offset: 0x00006A59
		public BufferedTreeNodeStream(object tree) : this(new CommonTreeAdaptor(), tree)
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008867 File Offset: 0x00006A67
		public BufferedTreeNodeStream(ITreeAdaptor adaptor, object tree) : this(adaptor, tree, 100)
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008874 File Offset: 0x00006A74
		public BufferedTreeNodeStream(ITreeAdaptor adaptor, object tree, int initialBufferSize)
		{
			this.root = tree;
			this.adaptor = adaptor;
			this.nodes = new List<object>(initialBufferSize);
			this.down = adaptor.Create(2, "DOWN");
			this.up = adaptor.Create(3, "UP");
			this.eof = adaptor.Create(-1, "EOF");
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000088DE File Offset: 0x00006ADE
		public virtual int Count
		{
			get
			{
				if (this.p == -1)
				{
					throw new InvalidOperationException("Cannot determine the Count before the buffer is filled.");
				}
				return this.nodes.Count;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000088FF File Offset: 0x00006AFF
		public virtual object TreeSource
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008907 File Offset: 0x00006B07
		public virtual string SourceName
		{
			get
			{
				return this.TokenStream.SourceName;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00008914 File Offset: 0x00006B14
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000891C File Offset: 0x00006B1C
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00008925 File Offset: 0x00006B25
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000892D File Offset: 0x00006B2D
		public virtual ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this.adaptor;
			}
			set
			{
				this.adaptor = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00008936 File Offset: 0x00006B36
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000893E File Offset: 0x00006B3E
		public virtual bool UniqueNavigationNodes
		{
			get
			{
				return this.uniqueNavigationNodes;
			}
			set
			{
				this.uniqueNavigationNodes = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00008947 File Offset: 0x00006B47
		public virtual IToken LastToken
		{
			get
			{
				return this.TreeAdaptor.GetToken(this.LB(1));
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000895C File Offset: 0x00006B5C
		public virtual IToken LastRealToken
		{
			get
			{
				int num = 0;
				IToken token;
				do
				{
					num++;
					token = this.TreeAdaptor.GetToken(this.LB(num));
				}
				while (token != null && token.Line <= 0);
				return token;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000898F File Offset: 0x00006B8F
		public virtual int MaxLookBehind
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008996 File Offset: 0x00006B96
		protected virtual void FillBuffer()
		{
			this.FillBuffer(this.root);
			this.p = 0;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000089AC File Offset: 0x00006BAC
		public virtual void FillBuffer(object t)
		{
			bool flag = this.adaptor.IsNil(t);
			if (!flag)
			{
				this.nodes.Add(t);
			}
			int childCount = this.adaptor.GetChildCount(t);
			if (!flag && childCount > 0)
			{
				this.AddNavigationNode(2);
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this.FillBuffer(child);
			}
			if (!flag && childCount > 0)
			{
				this.AddNavigationNode(3);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008A20 File Offset: 0x00006C20
		protected virtual int GetNodeIndex(object node)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			for (int i = 0; i < this.nodes.Count; i++)
			{
				object obj = this.nodes[i];
				if (obj == node)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008A68 File Offset: 0x00006C68
		protected virtual void AddNavigationNode(int ttype)
		{
			object value;
			if (ttype == 2)
			{
				if (this.UniqueNavigationNodes)
				{
					value = this.adaptor.Create(2, "DOWN");
				}
				else
				{
					value = this.down;
				}
			}
			else if (this.UniqueNavigationNodes)
			{
				value = this.adaptor.Create(3, "UP");
			}
			else
			{
				value = this.up;
			}
			this.nodes.Add(value);
		}

		// Token: 0x170000A8 RID: 168
		public virtual object this[int i]
		{
			get
			{
				if (this.p == -1)
				{
					throw new InvalidOperationException("Cannot get the node at index i before the buffer is filled.");
				}
				return this.nodes[i];
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public virtual object LT(int k)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (k < 0)
			{
				return this.LB(-k);
			}
			if (this.p + k - 1 >= this.nodes.Count)
			{
				return this.eof;
			}
			return this.nodes[this.p + k - 1];
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008B55 File Offset: 0x00006D55
		public virtual object GetCurrentSymbol()
		{
			return this.LT(1);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008B5E File Offset: 0x00006D5E
		protected virtual object LB(int k)
		{
			if (k == 0)
			{
				return null;
			}
			if (this.p - k < 0)
			{
				return null;
			}
			return this.nodes[this.p - k];
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008B85 File Offset: 0x00006D85
		public virtual void Consume()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.p++;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public virtual int LA(int i)
		{
			return this.adaptor.GetType(this.LT(i));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public virtual int Mark()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.lastMarker = this.Index;
			return this.lastMarker;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008BDB File Offset: 0x00006DDB
		public virtual void Release(int marker)
		{
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00008BDD File Offset: 0x00006DDD
		public virtual int Index
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008BE5 File Offset: 0x00006DE5
		public virtual void Rewind(int marker)
		{
			this.Seek(marker);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008BEE File Offset: 0x00006DEE
		public virtual void Rewind()
		{
			this.Seek(this.lastMarker);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008BFC File Offset: 0x00006DFC
		public virtual void Seek(int index)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.p = index;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008C14 File Offset: 0x00006E14
		public virtual void Push(int index)
		{
			if (this.calls == null)
			{
				this.calls = new Stack<int>();
			}
			this.calls.Push(this.p);
			this.Seek(index);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008C44 File Offset: 0x00006E44
		public virtual int Pop()
		{
			int num = this.calls.Pop();
			this.Seek(num);
			return num;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008C65 File Offset: 0x00006E65
		public virtual void Reset()
		{
			this.p = 0;
			this.lastMarker = 0;
			if (this.calls != null)
			{
				this.calls.Clear();
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008C88 File Offset: 0x00006E88
		public virtual IEnumerator<object> Iterator()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return new BufferedTreeNodeStream.StreamIterator(this);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008C9F File Offset: 0x00006E9F
		public virtual void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			if (parent != null)
			{
				this.adaptor.ReplaceChildren(parent, startChildIndex, stopChildIndex, t);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public virtual string ToTokenTypeString()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.nodes.Count; i++)
			{
				object t = this.nodes[i];
				stringBuilder.Append(" ");
				stringBuilder.Append(this.adaptor.GetType(t));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00008D20 File Offset: 0x00006F20
		public virtual string ToTokenString(int start, int stop)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num < this.nodes.Count && num <= stop)
			{
				object t = this.nodes[num];
				stringBuilder.Append(" ");
				stringBuilder.Append(this.adaptor.GetToken(t));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008D90 File Offset: 0x00006F90
		public virtual string ToString(object start, object stop)
		{
			Console.Out.WriteLine("toString");
			if (start == null || stop == null)
			{
				return null;
			}
			if (this.p == -1)
			{
				throw new InvalidOperationException("Buffer is not yet filled.");
			}
			if (start is CommonTree)
			{
				Console.Out.Write("toString: " + ((CommonTree)start).Token + ", ");
			}
			else
			{
				Console.Out.WriteLine(start);
			}
			if (stop is CommonTree)
			{
				Console.Out.WriteLine(((CommonTree)stop).Token);
			}
			else
			{
				Console.Out.WriteLine(stop);
			}
			if (this.tokens != null)
			{
				int tokenStartIndex = this.adaptor.GetTokenStartIndex(start);
				int stop2 = this.adaptor.GetTokenStopIndex(stop);
				if (this.adaptor.GetType(stop) == 3)
				{
					stop2 = this.adaptor.GetTokenStopIndex(start);
				}
				else if (this.adaptor.GetType(stop) == -1)
				{
					stop2 = this.Count - 2;
				}
				return this.tokens.ToString(tokenStartIndex, stop2);
			}
			int i;
			for (i = 0; i < this.nodes.Count; i++)
			{
				object obj = this.nodes[i];
				if (obj == start)
				{
					break;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (object obj = this.nodes[i]; obj != stop; obj = this.nodes[i])
			{
				string text = this.adaptor.GetText(obj);
				if (text == null)
				{
					text = " " + this.adaptor.GetType(obj).ToString();
				}
				stringBuilder.Append(text);
				i++;
			}
			string text2 = this.adaptor.GetText(stop);
			if (text2 == null)
			{
				text2 = " " + this.adaptor.GetType(stop).ToString();
			}
			stringBuilder.Append(text2);
			return stringBuilder.ToString();
		}

		// Token: 0x04000095 RID: 149
		public const int DEFAULT_INITIAL_BUFFER_SIZE = 100;

		// Token: 0x04000096 RID: 150
		public const int INITIAL_CALL_STACK_SIZE = 10;

		// Token: 0x04000097 RID: 151
		protected object down;

		// Token: 0x04000098 RID: 152
		protected object up;

		// Token: 0x04000099 RID: 153
		protected object eof;

		// Token: 0x0400009A RID: 154
		protected IList nodes;

		// Token: 0x0400009B RID: 155
		protected object root;

		// Token: 0x0400009C RID: 156
		protected ITokenStream tokens;

		// Token: 0x0400009D RID: 157
		private ITreeAdaptor adaptor;

		// Token: 0x0400009E RID: 158
		private bool uniqueNavigationNodes;

		// Token: 0x0400009F RID: 159
		protected int p = -1;

		// Token: 0x040000A0 RID: 160
		protected int lastMarker;

		// Token: 0x040000A1 RID: 161
		protected Stack<int> calls;

		// Token: 0x02000045 RID: 69
		protected sealed class StreamIterator : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x0600035B RID: 859 RVA: 0x00008F66 File Offset: 0x00007166
			public StreamIterator(BufferedTreeNodeStream outer)
			{
				this._outer = outer;
				this._index = -1;
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600035C RID: 860 RVA: 0x00008F7C File Offset: 0x0000717C
			public object Current
			{
				get
				{
					if (this._index < this._outer.nodes.Count)
					{
						return this._outer.nodes[this._index];
					}
					return this._outer.eof;
				}
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00008FB8 File Offset: 0x000071B8
			public void Dispose()
			{
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00008FBA File Offset: 0x000071BA
			public bool MoveNext()
			{
				if (this._index < this._outer.nodes.Count)
				{
					this._index++;
				}
				return this._index < this._outer.nodes.Count;
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00008FFA File Offset: 0x000071FA
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x040000A2 RID: 162
			private BufferedTreeNodeStream _outer;

			// Token: 0x040000A3 RID: 163
			private int _index;
		}
	}
}
