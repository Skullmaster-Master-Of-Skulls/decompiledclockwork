using System;
using Antlr.Runtime.Misc;

namespace Antlr.Runtime
{
	// Token: 0x02000068 RID: 104
	public class UnbufferedTokenStream : LookaheadStream<IToken>, ITokenStream, IIntStream, ITokenStreamInformation
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public UnbufferedTokenStream(ITokenSource tokenSource)
		{
			this.tokenSource = tokenSource;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000BBDE File Offset: 0x00009DDE
		public ITokenSource TokenSource
		{
			get
			{
				return this.tokenSource;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000BBE6 File Offset: 0x00009DE6
		public string SourceName
		{
			get
			{
				return this.TokenSource.SourceName;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000BBF3 File Offset: 0x00009DF3
		public IToken LastToken
		{
			get
			{
				return this.LB(1);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public IToken LastRealToken
		{
			get
			{
				return this._realTokens.Peek();
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000BC09 File Offset: 0x00009E09
		public int MaxLookBehind
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public override int Mark()
		{
			this._realTokens.Push(this._realTokens.Peek());
			return base.Mark();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000BC2A File Offset: 0x00009E2A
		public override void Release(int marker)
		{
			base.Release(marker);
			this._realTokens.Pop();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000BC3F File Offset: 0x00009E3F
		public override void Clear()
		{
			this._realTokens.Clear();
			this._realTokens.Push(null);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000BC58 File Offset: 0x00009E58
		public override void Consume()
		{
			base.Consume();
			if (base.PreviousElement != null && base.PreviousElement.Line > 0)
			{
				this._realTokens[this._realTokens.Count - 1] = base.PreviousElement;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000BC94 File Offset: 0x00009E94
		public override IToken NextElement()
		{
			IToken token = this.tokenSource.NextToken();
			token.TokenIndex = this.tokenIndex++;
			return token;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		public override bool IsEndOfFile(IToken o)
		{
			return o.Type == -1;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public IToken Get(int i)
		{
			throw new NotSupportedException("Absolute token indexes are meaningless in an unbuffered stream");
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public int LA(int i)
		{
			return this.LT(i).Type;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000BCEA File Offset: 0x00009EEA
		public string ToString(int start, int stop)
		{
			return "n/a";
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000BCF1 File Offset: 0x00009EF1
		public string ToString(IToken start, IToken stop)
		{
			return "n/a";
		}

		// Token: 0x04000100 RID: 256
		[CLSCompliant(false)]
		protected ITokenSource tokenSource;

		// Token: 0x04000101 RID: 257
		protected int tokenIndex;

		// Token: 0x04000102 RID: 258
		protected int channel;

		// Token: 0x04000103 RID: 259
		private readonly ListStack<IToken> _realTokens = new ListStack<IToken>
		{
			null
		};
	}
}
