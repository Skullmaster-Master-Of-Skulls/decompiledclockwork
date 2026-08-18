using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000006 RID: 6
	public interface IX509Selector : ICloneable
	{
		// Token: 0x06000019 RID: 25
		bool Match(object obj);
	}
}
