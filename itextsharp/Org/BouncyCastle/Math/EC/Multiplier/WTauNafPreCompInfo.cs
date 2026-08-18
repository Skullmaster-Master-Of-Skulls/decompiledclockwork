using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000117 RID: 279
	internal class WTauNafPreCompInfo : PreCompInfo
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x00037904 File Offset: 0x00036904
		internal WTauNafPreCompInfo(F2mPoint[] preComp)
		{
			this.preComp = preComp;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00037913 File Offset: 0x00036913
		internal F2mPoint[] GetPreComp()
		{
			return this.preComp;
		}

		// Token: 0x0400086E RID: 2158
		private readonly F2mPoint[] preComp;
	}
}
