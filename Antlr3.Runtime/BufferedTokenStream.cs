using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class BufferedTokenStream : ITokenStream, IIntStream, ITokenStreamInformation
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000038C9 File Offset: 0x00001AC9
		public BufferedTokenStream()
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000038E5 File Offset: 0x00001AE5
		public BufferedTokenStream(ITokenSource tokenSource)
		{
			this._tokenSource = tokenSource;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003908 File Offset: 0x00001B08
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003910 File Offset: 0x00001B10
		public virtual ITokenSource TokenSource
		{
			get
			{
				return this._tokenSource;
			}
			set
			{
				this._tokenSource = value;
				this._tokens.Clear();
				this._p = -1;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000392B File Offset: 0x00001B2B
		public virtual int Index
		{
			get
			{
				return this._p;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003933 File Offset: 0x00001B33
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000393B File Offset: 0x00001B3B
		public virtual int Range { get; protected set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003944 File Offset: 0x00001B44
		public virtual int Count
		{
			get
			{
				return this._tokens.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003951 File Offset: 0x00001B51
		public virtual string SourceName
		{
			get
			{
				return this._tokenSource.SourceName;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000395E File Offset: 0x00001B5E
		public virtual IToken LastToken
		{
			get
			{
				return this.LB(1);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003968 File Offset: 0x00001B68
		public virtual IToken LastRealToken
		{
			get
			{
				int num = 0;
				IToken token;
				do
				{
					num++;
					token = this.LB(num);
				}
				while (token != null && token.Line <= 0);
				return token;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003990 File Offset: 0x00001B90
		public virtual int MaxLookBehind
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003997 File Offset: 0x00001B97
		public virtual int Mark()
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			this._lastMarker = this.Index;
			return this._lastMarker;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000039BA File Offset: 0x00001BBA
		public virtual void Release(int marker)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000039BC File Offset: 0x00001BBC
		public virtual void Rewind(int marker)
		{
			this.Seek(marker);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000039C5 File Offset: 0x00001BC5
		public virtual void Rewind()
		{
			this.Seek(this._lastMarker);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000039D3 File Offset: 0x00001BD3
		public virtual void Reset()
		{
			this._p = 0;
			this._lastMarker = 0;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000039E3 File Offset: 0x00001BE3
		public virtual void Seek(int index)
		{
			this._p = index;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000039EC File Offset: 0x00001BEC
		public virtual void Consume()
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			this._p++;
			this.Sync(this._p);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003A18 File Offset: 0x00001C18
		protected virtual void Sync(int i)
		{
			int num = i - this._tokens.Count + 1;
			if (num > 0)
			{
				this.Fetch(num);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A40 File Offset: 0x00001C40
		protected virtual void Fetch(int n)
		{
			for (int i = 0; i < n; i++)
			{
				IToken token = this.TokenSource.NextToken();
				token.TokenIndex = this._tokens.Count;
				this._tokens.Add(token);
				if (token.Type == -1)
				{
					return;
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A8C File Offset: 0x00001C8C
		public virtual IToken Get(int i)
		{
			if (i < 0 || i >= this._tokens.Count)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"token index ",
					i,
					" out of range 0..",
					this._tokens.Count - 1
				}));
			}
			return this._tokens[i];
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public virtual int LA(int i)
		{
			return this.LT(i).Type;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003B06 File Offset: 0x00001D06
		protected virtual IToken LB(int k)
		{
			if (this._p - k < 0)
			{
				return null;
			}
			return this._tokens[this._p - k];
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003B28 File Offset: 0x00001D28
		public virtual IToken LT(int k)
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			if (k == 0)
			{
				return null;
			}
			if (k < 0)
			{
				return this.LB(-k);
			}
			int num = this._p + k - 1;
			this.Sync(num);
			if (num >= this._tokens.Count)
			{
				return this._tokens[this._tokens.Count - 1];
			}
			if (num > this.Range)
			{
				this.Range = num;
			}
			return this._tokens[this._p + k - 1];
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003BB4 File Offset: 0x00001DB4
		protected virtual void Setup()
		{
			this.Sync(0);
			this._p = 0;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public virtual List<IToken> GetTokens()
		{
			return this._tokens;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003BCC File Offset: 0x00001DCC
		public virtual List<IToken> GetTokens(int start, int stop)
		{
			return this.GetTokens(start, stop, null);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public virtual List<IToken> GetTokens(int start, int stop, BitSet types)
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			if (stop >= this._tokens.Count)
			{
				stop = this._tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > stop)
			{
				return null;
			}
			List<IToken> list = new List<IToken>();
			for (int i = start; i <= stop; i++)
			{
				IToken token = this._tokens[i];
				if (types == null || types.Member(token.Type))
				{
					list.Add(token);
				}
			}
			if (list.Count == 0)
			{
				list = null;
			}
			return list;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C60 File Offset: 0x00001E60
		public virtual List<IToken> GetTokens(int start, int stop, IEnumerable<int> types)
		{
			return this.GetTokens(start, stop, new BitSet(types));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003C70 File Offset: 0x00001E70
		public virtual List<IToken> GetTokens(int start, int stop, int ttype)
		{
			return this.GetTokens(start, stop, BitSet.Of(ttype));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C80 File Offset: 0x00001E80
		public override string ToString()
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			this.Fill();
			return this.ToString(0, this._tokens.Count - 1);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003CAC File Offset: 0x00001EAC
		public virtual string ToString(int start, int stop)
		{
			if (start < 0 || stop < 0)
			{
				return null;
			}
			if (this._p == -1)
			{
				this.Setup();
			}
			if (stop >= this._tokens.Count)
			{
				stop = this._tokens.Count - 1;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = start; i <= stop; i++)
			{
				IToken token = this._tokens[i];
				if (token.Type == -1)
				{
					break;
				}
				stringBuilder.Append(token.Text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D2A File Offset: 0x00001F2A
		public virtual string ToString(IToken start, IToken stop)
		{
			if (start != null && stop != null)
			{
				return this.ToString(start.TokenIndex, stop.TokenIndex);
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003D48 File Offset: 0x00001F48
		public virtual void Fill()
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			if (this._tokens[this._p].Type == -1)
			{
				return;
			}
			int num = this._p + 1;
			this.Sync(num);
			while (this._tokens[num].Type != -1)
			{
				num++;
				this.Sync(num);
			}
		}

		// Token: 0x0400001C RID: 28
		private ITokenSource _tokenSource;

		// Token: 0x0400001D RID: 29
		[CLSCompliant(false)]
		protected List<IToken> _tokens = new List<IToken>(100);

		// Token: 0x0400001E RID: 30
		private int _lastMarker;

		// Token: 0x0400001F RID: 31
		[CLSCompliant(false)]
		protected int _p = -1;
	}
}
