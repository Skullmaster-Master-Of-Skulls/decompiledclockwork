using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x0200075F RID: 1887
	internal class EmptyCollection : ICollection, IEnumerable, IEnumerator
	{
		// Token: 0x06005BD5 RID: 23509 RVA: 0x001707AE File Offset: 0x0016F7AE
		private EmptyCollection()
		{
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x001707B6 File Offset: 0x0016F7B6
		internal static EmptyCollection Instance
		{
			get
			{
				return EmptyCollection.s_theEmptyCollection;
			}
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001707BD File Offset: 0x0016F7BD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06005BD8 RID: 23512 RVA: 0x001707C0 File Offset: 0x0016F7C0
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x001707C3 File Offset: 0x0016F7C3
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x001707C6 File Offset: 0x0016F7C6
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x001707C9 File Offset: 0x0016F7C9
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06005BDC RID: 23516 RVA: 0x001707CB File Offset: 0x0016F7CB
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x001707CE File Offset: 0x0016F7CE
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x001707D1 File Offset: 0x0016F7D1
		void IEnumerator.Reset()
		{
		}

		// Token: 0x04003122 RID: 12578
		private static EmptyCollection s_theEmptyCollection = new EmptyCollection();
	}
}
