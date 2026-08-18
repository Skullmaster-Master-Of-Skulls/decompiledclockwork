using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020003D2 RID: 978
	internal class WNafPreCompInfo : PreCompInfo
	{
		// Token: 0x06002207 RID: 8711 RVA: 0x000CE08C File Offset: 0x000CD08C
		internal ECPoint[] GetPreComp()
		{
			return this.preComp;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000CE094 File Offset: 0x000CD094
		internal void SetPreComp(ECPoint[] preComp)
		{
			this.preComp = preComp;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000CE09D File Offset: 0x000CD09D
		internal ECPoint GetTwiceP()
		{
			return this.twiceP;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000CE0A5 File Offset: 0x000CD0A5
		internal void SetTwiceP(ECPoint twiceThis)
		{
			this.twiceP = twiceThis;
		}

		// Token: 0x04001755 RID: 5973
		private ECPoint[] preComp;

		// Token: 0x04001756 RID: 5974
		private ECPoint twiceP;
	}
}
