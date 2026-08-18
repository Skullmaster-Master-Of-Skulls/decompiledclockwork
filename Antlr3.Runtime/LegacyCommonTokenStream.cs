using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class LegacyCommonTokenStream : ITokenStream, IIntStream
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000505B File Offset: 0x0000325B
		public LegacyCommonTokenStream()
		{
			this.tokens = new List<IToken>(500);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000507A File Offset: 0x0000327A
		public LegacyCommonTokenStream(ITokenSource tokenSource) : this()
		{
			this._tokenSource = tokenSource;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005089 File Offset: 0x00003289
		public LegacyCommonTokenStream(ITokenSource tokenSource, int channel) : this(tokenSource)
		{
			this.channel = channel;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005099 File Offset: 0x00003299
		public virtual int Index
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000050A1 File Offset: 0x000032A1
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000050A9 File Offset: 0x000032A9
		public virtual int Range { get; protected set; }

		// Token: 0x0600018D RID: 397 RVA: 0x000050B2 File Offset: 0x000032B2
		public virtual void SetTokenSource(ITokenSource tokenSource)
		{
			this._tokenSource = tokenSource;
			this.tokens.Clear();
			this.p = -1;
			this.channel = 0;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000050D4 File Offset: 0x000032D4
		public virtual void FillBuffer()
		{
			if (this.p != -1)
			{
				return;
			}
			int num = 0;
			IToken token = this._tokenSource.NextToken();
			while (token != null && token.Type != -1)
			{
				bool flag = false;
				int num2;
				if (this.channelOverrideMap != null && this.channelOverrideMap.TryGetValue(token.Type, out num2))
				{
					token.Channel = num2;
				}
				if (this.discardSet != null && this.discardSet.Contains(token.Type))
				{
					flag = true;
				}
				else if (this.discardOffChannelTokens && token.Channel != this.channel)
				{
					flag = true;
				}
				if (!flag)
				{
					token.TokenIndex = num;
					this.tokens.Add(token);
					num++;
				}
				token = this._tokenSource.NextToken();
			}
			this.p = 0;
			this.p = this.SkipOffTokenChannels(this.p);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000051A9 File Offset: 0x000033A9
		public virtual void Consume()
		{
			if (this.p < this.tokens.Count)
			{
				this.p++;
				this.p = this.SkipOffTokenChannels(this.p);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000051E0 File Offset: 0x000033E0
		protected virtual int SkipOffTokenChannels(int i)
		{
			int count = this.tokens.Count;
			while (i < count && this.tokens[i].Channel != this.channel)
			{
				i++;
			}
			return i;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000521E File Offset: 0x0000341E
		protected virtual int SkipOffTokenChannelsReverse(int i)
		{
			while (i >= 0 && this.tokens[i].Channel != this.channel)
			{
				i--;
			}
			return i;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005245 File Offset: 0x00003445
		public virtual void SetTokenTypeChannel(int ttype, int channel)
		{
			if (this.channelOverrideMap == null)
			{
				this.channelOverrideMap = new Dictionary<int, int>();
			}
			this.channelOverrideMap[ttype] = channel;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005267 File Offset: 0x00003467
		public virtual void DiscardTokenType(int ttype)
		{
			if (this.discardSet == null)
			{
				this.discardSet = new List<int>();
			}
			this.discardSet.Add(ttype);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005288 File Offset: 0x00003488
		public virtual void SetDiscardOffChannelTokens(bool discardOffChannelTokens)
		{
			this.discardOffChannelTokens = discardOffChannelTokens;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005291 File Offset: 0x00003491
		public virtual IList<IToken> GetTokens()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return this.tokens;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000052A8 File Offset: 0x000034A8
		public virtual IList<IToken> GetTokens(int start, int stop)
		{
			return this.GetTokens(start, stop, null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000052B4 File Offset: 0x000034B4
		public virtual IList<IToken> GetTokens(int start, int stop, BitSet types)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (stop >= this.tokens.Count)
			{
				stop = this.tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > stop)
			{
				return null;
			}
			IList<IToken> list = new List<IToken>();
			for (int i = start; i <= stop; i++)
			{
				IToken token = this.tokens[i];
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

		// Token: 0x06000198 RID: 408 RVA: 0x0000533C File Offset: 0x0000353C
		public virtual IList<IToken> GetTokens(int start, int stop, IList<int> types)
		{
			return this.GetTokens(start, stop, new BitSet(types));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000534C File Offset: 0x0000354C
		public virtual IList<IToken> GetTokens(int start, int stop, int ttype)
		{
			return this.GetTokens(start, stop, BitSet.Of(ttype));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000535C File Offset: 0x0000355C
		public virtual IToken LT(int k)
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
			if (this.p + k - 1 >= this.tokens.Count)
			{
				return this.tokens[this.tokens.Count - 1];
			}
			int num = this.p;
			for (int i = 1; i < k; i++)
			{
				num = this.SkipOffTokenChannels(num + 1);
			}
			if (num >= this.tokens.Count)
			{
				return this.tokens[this.tokens.Count - 1];
			}
			if (num > this.Range)
			{
				this.Range = num;
			}
			return this.tokens[num];
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000541C File Offset: 0x0000361C
		protected virtual IToken LB(int k)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (this.p - k < 0)
			{
				return null;
			}
			int num = this.p;
			for (int i = 1; i <= k; i++)
			{
				num = this.SkipOffTokenChannelsReverse(num - 1);
			}
			if (num < 0)
			{
				return null;
			}
			return this.tokens[num];
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005479 File Offset: 0x00003679
		public virtual IToken Get(int i)
		{
			return this.tokens[i];
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005487 File Offset: 0x00003687
		public virtual int LA(int i)
		{
			return this.LT(i).Type;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005495 File Offset: 0x00003695
		public virtual int Mark()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.lastMarker = this.Index;
			return this.lastMarker;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000054B8 File Offset: 0x000036B8
		public virtual void Release(int marker)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000054BA File Offset: 0x000036BA
		public virtual int Count
		{
			get
			{
				return this.tokens.Count;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000054C7 File Offset: 0x000036C7
		public virtual void Rewind(int marker)
		{
			this.Seek(marker);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000054D0 File Offset: 0x000036D0
		public virtual void Rewind()
		{
			this.Seek(this.lastMarker);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000054DE File Offset: 0x000036DE
		public virtual void Reset()
		{
			this.p = 0;
			this.lastMarker = 0;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000054EE File Offset: 0x000036EE
		public virtual void Seek(int index)
		{
			this.p = index;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000054F7 File Offset: 0x000036F7
		public virtual ITokenSource TokenSource
		{
			get
			{
				return this._tokenSource;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000054FF File Offset: 0x000036FF
		public virtual string SourceName
		{
			get
			{
				return this.TokenSource.SourceName;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000550C File Offset: 0x0000370C
		public override string ToString()
		{
			if (this.p == -1)
			{
				throw new InvalidOperationException("Buffer is not yet filled.");
			}
			return this.ToString(0, this.tokens.Count - 1);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005538 File Offset: 0x00003738
		public virtual string ToString(int start, int stop)
		{
			if (start < 0 || stop < 0)
			{
				return null;
			}
			if (this.p == -1)
			{
				throw new InvalidOperationException("Buffer is not yet filled.");
			}
			if (stop >= this.tokens.Count)
			{
				stop = this.tokens.Count - 1;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = start; i <= stop; i++)
			{
				IToken token = this.tokens[i];
				stringBuilder.Append(token.Text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000055B2 File Offset: 0x000037B2
		public virtual string ToString(IToken start, IToken stop)
		{
			if (start != null && stop != null)
			{
				return this.ToString(start.TokenIndex, stop.TokenIndex);
			}
			return null;
		}

		// Token: 0x0400004D RID: 77
		[NonSerialized]
		private ITokenSource _tokenSource;

		// Token: 0x0400004E RID: 78
		protected List<IToken> tokens;

		// Token: 0x0400004F RID: 79
		protected IDictionary<int, int> channelOverrideMap;

		// Token: 0x04000050 RID: 80
		protected List<int> discardSet;

		// Token: 0x04000051 RID: 81
		protected int channel;

		// Token: 0x04000052 RID: 82
		protected bool discardOffChannelTokens;

		// Token: 0x04000053 RID: 83
		protected int lastMarker;

		// Token: 0x04000054 RID: 84
		protected int p = -1;
	}
}
