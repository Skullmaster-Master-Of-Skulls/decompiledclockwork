using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000941 RID: 2369
	[ComVisible(false)]
	internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
	{
		// Token: 0x06005581 RID: 21889 RVA: 0x00135D3C File Offset: 0x00134D3C
		internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._Collection = collection;
			this._Current = -1;
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x00135D60 File Offset: 0x00134D60
		object IEnumerator.Current
		{
			get
			{
				return this._Collection.Identities[this._Current];
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005583 RID: 21891 RVA: 0x00135D78 File Offset: 0x00134D78
		public IdentityReference Current
		{
			get
			{
				return ((IEnumerator)this).Current as IdentityReference;
			}
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x00135D85 File Offset: 0x00134D85
		public bool MoveNext()
		{
			this._Current++;
			return this._Current < this._Collection.Count;
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00135DA8 File Offset: 0x00134DA8
		public void Reset()
		{
			this._Current = -1;
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x00135DB1 File Offset: 0x00134DB1
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00135DB9 File Offset: 0x00134DB9
		protected void Dispose()
		{
		}

		// Token: 0x04002C63 RID: 11363
		private int _Current;

		// Token: 0x04002C64 RID: 11364
		private readonly IdentityReferenceCollection _Collection;
	}
}
