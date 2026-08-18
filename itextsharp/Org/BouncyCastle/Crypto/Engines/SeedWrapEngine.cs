using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200024C RID: 588
	public class SeedWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00082F59 File Offset: 0x00081F59
		public SeedWrapEngine() : base(new SeedEngine())
		{
		}
	}
}
