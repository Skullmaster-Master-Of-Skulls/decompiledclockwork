using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200053D RID: 1341
	public sealed class EnumerableProxy : IEnumerable
	{
		// Token: 0x06002E27 RID: 11815 RVA: 0x0011D2E8 File Offset: 0x0011C2E8
		public EnumerableProxy(IEnumerable inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			this.inner = inner;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x0011D305 File Offset: 0x0011C305
		public IEnumerator GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x04001FF8 RID: 8184
		private readonly IEnumerable inner;
	}
}
