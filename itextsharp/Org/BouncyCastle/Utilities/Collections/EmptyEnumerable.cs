using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200006C RID: 108
	public sealed class EmptyEnumerable : IEnumerable
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0001383E File Offset: 0x0001283E
		private EmptyEnumerable()
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013846 File Offset: 0x00012846
		public IEnumerator GetEnumerator()
		{
			return EmptyEnumerator.Instance;
		}

		// Token: 0x040001F2 RID: 498
		public static readonly IEnumerable Instance = new EmptyEnumerable();
	}
}
