using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020001F8 RID: 504
	public class CamelliaWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0006F8B5 File Offset: 0x0006E8B5
		public CamelliaWrapEngine() : base(new CamelliaEngine())
		{
		}
	}
}
