using System;

namespace Antlr.Runtime
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class CommonTokenStream : BufferedTokenStream
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00004343 File Offset: 0x00002543
		public CommonTokenStream()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000434B File Offset: 0x0000254B
		public CommonTokenStream(ITokenSource tokenSource) : this(tokenSource, 0)
		{
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004355 File Offset: 0x00002555
		public CommonTokenStream(ITokenSource tokenSource, int channel) : base(tokenSource)
		{
			this._channel = channel;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004365 File Offset: 0x00002565
		public int Channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000436D File Offset: 0x0000256D
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004375 File Offset: 0x00002575
		public override ITokenSource TokenSource
		{
			get
			{
				return base.TokenSource;
			}
			set
			{
				base.TokenSource = value;
				this._channel = 0;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004385 File Offset: 0x00002585
		public override void Consume()
		{
			if (this._p == -1)
			{
				this.Setup();
			}
			this._p++;
			this._p = this.SkipOffTokenChannels(this._p);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000043B8 File Offset: 0x000025B8
		protected override IToken LB(int k)
		{
			if (k == 0 || this._p - k < 0)
			{
				return null;
			}
			int num = this._p;
			for (int i = 1; i <= k; i++)
			{
				num = this.SkipOffTokenChannelsReverse(num - 1);
			}
			if (num < 0)
			{
				return null;
			}
			return this._tokens[num];
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004404 File Offset: 0x00002604
		public override IToken LT(int k)
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
			int num = this._p;
			for (int i = 1; i < k; i++)
			{
				num = this.SkipOffTokenChannels(num + 1);
			}
			if (num > this.Range)
			{
				this.Range = num;
			}
			return this._tokens[num];
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000446B File Offset: 0x0000266B
		protected virtual int SkipOffTokenChannels(int i)
		{
			this.Sync(i);
			while (this._tokens[i].Channel != this._channel)
			{
				i++;
				this.Sync(i);
			}
			return i;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000449C File Offset: 0x0000269C
		protected virtual int SkipOffTokenChannelsReverse(int i)
		{
			while (i >= 0 && this._tokens[i].Channel != this._channel)
			{
				i--;
			}
			return i;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000044C3 File Offset: 0x000026C3
		public override void Reset()
		{
			base.Reset();
			this._p = this.SkipOffTokenChannels(0);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000044D8 File Offset: 0x000026D8
		protected override void Setup()
		{
			this._p = 0;
			this._p = this.SkipOffTokenChannels(this._p);
		}

		// Token: 0x04000034 RID: 52
		private int _channel;
	}
}
