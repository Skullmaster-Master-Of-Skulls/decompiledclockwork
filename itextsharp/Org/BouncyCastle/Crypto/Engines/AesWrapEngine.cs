using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020004CA RID: 1226
	public class AesWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x060029DE RID: 10718 RVA: 0x000FF9D3 File Offset: 0x000FE9D3
		public AesWrapEngine() : base(new AesEngine())
		{
		}
	}
}
