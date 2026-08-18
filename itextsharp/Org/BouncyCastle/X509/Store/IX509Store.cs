using System;
using System.Collections;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000FA RID: 250
	public interface IX509Store
	{
		// Token: 0x060009D8 RID: 2520
		ICollection GetMatches(IX509Selector selector);
	}
}
