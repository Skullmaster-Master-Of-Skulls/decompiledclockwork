using System;

namespace System.Net
{
	// Token: 0x0200020D RID: 525
	internal class SpnToken
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00067150 File Offset: 0x00065350
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x00067158 File Offset: 0x00065358
		internal bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
			set
			{
				this.isTrusted = false;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00067161 File Offset: 0x00065361
		internal string Spn
		{
			get
			{
				return this.spn;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00067169 File Offset: 0x00065369
		internal SpnToken(string spn) : this(spn, true)
		{
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00067173 File Offset: 0x00065373
		internal SpnToken(string spn, bool trusted)
		{
			this.spn = spn;
			this.isTrusted = trusted;
		}

		// Token: 0x0400156E RID: 5486
		private readonly string spn;

		// Token: 0x0400156F RID: 5487
		private bool isTrusted;
	}
}
