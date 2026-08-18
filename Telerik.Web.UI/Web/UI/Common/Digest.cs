using System;

namespace Telerik.Web.UI.Common
{
	// Token: 0x0200014C RID: 332
	internal sealed class Digest
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x0002F3B9 File Offset: 0x0002D5B9
		public Digest()
		{
			this.A = 1732584193U;
			this.B = 4023233417U;
			this.C = 2562383102U;
			this.D = 271733878U;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
		public override string ToString()
		{
			return SafeMD5Helper.ReverseByte(this.A).ToString("X8") + SafeMD5Helper.ReverseByte(this.B).ToString("X8") + SafeMD5Helper.ReverseByte(this.C).ToString("X8") + SafeMD5Helper.ReverseByte(this.D).ToString("X8");
		}

		// Token: 0x0400033E RID: 830
		public uint A;

		// Token: 0x0400033F RID: 831
		public uint B;

		// Token: 0x04000340 RID: 832
		public uint C;

		// Token: 0x04000341 RID: 833
		public uint D;
	}
}
