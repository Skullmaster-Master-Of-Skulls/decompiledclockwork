using System;

namespace Antlr.Runtime
{
	// Token: 0x0200000A RID: 10
	public class ParserRuleReturnScope<TToken> : IRuleReturnScope<TToken>, IRuleReturnScope
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002561 File Offset: 0x00000761
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002569 File Offset: 0x00000769
		public TToken Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002572 File Offset: 0x00000772
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000257A File Offset: 0x0000077A
		public TToken Stop
		{
			get
			{
				return this._stop;
			}
			set
			{
				this._stop = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002583 File Offset: 0x00000783
		object IRuleReturnScope.Start
		{
			get
			{
				return this.Start;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002590 File Offset: 0x00000790
		object IRuleReturnScope.Stop
		{
			get
			{
				return this.Stop;
			}
		}

		// Token: 0x0400000D RID: 13
		private TToken _start;

		// Token: 0x0400000E RID: 14
		private TToken _stop;
	}
}
