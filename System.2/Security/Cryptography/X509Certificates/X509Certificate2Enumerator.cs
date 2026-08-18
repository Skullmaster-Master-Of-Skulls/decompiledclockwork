using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046A RID: 1130
	public sealed class X509Certificate2Enumerator : IEnumerator
	{
		// Token: 0x06002A20 RID: 10784 RVA: 0x000C0706 File Offset: 0x000BE906
		private X509Certificate2Enumerator()
		{
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000C070E File Offset: 0x000BE90E
		internal X509Certificate2Enumerator(X509Certificate2Collection mappings)
		{
			this.baseEnumerator = ((IEnumerable)mappings).GetEnumerator();
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000C0722 File Offset: 0x000BE922
		public X509Certificate2 Current
		{
			get
			{
				return (X509Certificate2)this.baseEnumerator.Current;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000C0734 File Offset: 0x000BE934
		object IEnumerator.Current
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000C0741 File Offset: 0x000BE941
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000C074E File Offset: 0x000BE94E
		bool IEnumerator.MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000C075B File Offset: 0x000BE95B
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000C0768 File Offset: 0x000BE968
		void IEnumerator.Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x040025E2 RID: 9698
		private IEnumerator baseEnumerator;
	}
}
