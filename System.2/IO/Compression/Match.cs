using System;

namespace System.IO.Compression
{
	// Token: 0x02000435 RID: 1077
	internal class Match
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000B9D3A File Offset: 0x000B7F3A
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x000B9D42 File Offset: 0x000B7F42
		internal MatchState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x000B9D4B File Offset: 0x000B7F4B
		// (set) Token: 0x06002864 RID: 10340 RVA: 0x000B9D53 File Offset: 0x000B7F53
		internal int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000B9D5C File Offset: 0x000B7F5C
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x000B9D64 File Offset: 0x000B7F64
		internal int Length
		{
			get
			{
				return this.len;
			}
			set
			{
				this.len = value;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x000B9D6D File Offset: 0x000B7F6D
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x000B9D75 File Offset: 0x000B7F75
		internal byte Symbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				this.symbol = value;
			}
		}

		// Token: 0x04002232 RID: 8754
		private MatchState state;

		// Token: 0x04002233 RID: 8755
		private int pos;

		// Token: 0x04002234 RID: 8756
		private int len;

		// Token: 0x04002235 RID: 8757
		private byte symbol;
	}
}
