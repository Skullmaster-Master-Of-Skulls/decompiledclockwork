using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200006D RID: 109
	public sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x06000396 RID: 918 RVA: 0x00013859 File Offset: 0x00012859
		private EmptyEnumerator()
		{
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013861 File Offset: 0x00012861
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013864 File Offset: 0x00012864
		public void Reset()
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00013866 File Offset: 0x00012866
		public object Current
		{
			get
			{
				throw new InvalidOperationException("No elements");
			}
		}

		// Token: 0x040001F3 RID: 499
		public static readonly IEnumerator Instance = new EmptyEnumerator();
	}
}
