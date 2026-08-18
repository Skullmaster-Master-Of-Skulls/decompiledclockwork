using System;
using System.Collections;

namespace System.Security
{
	// Token: 0x02000677 RID: 1655
	internal class PermissionSetEnumerator : IEnumerator
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000CC47B File Offset: 0x000CB47B
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000CC488 File Offset: 0x000CB488
		public bool MoveNext()
		{
			return this.enm.MoveNext();
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000CC495 File Offset: 0x000CB495
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000CC4A2 File Offset: 0x000CB4A2
		internal PermissionSetEnumerator(PermissionSet permSet)
		{
			this.enm = new PermissionSetEnumeratorInternal(permSet);
		}

		// Token: 0x04001EDE RID: 7902
		private PermissionSetEnumeratorInternal enm;
	}
}
