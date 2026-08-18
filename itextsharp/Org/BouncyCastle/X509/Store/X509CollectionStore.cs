using System;
using System.Collections;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000FB RID: 251
	internal class X509CollectionStore : IX509Store
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x00032BFD File Offset: 0x00031BFD
		internal X509CollectionStore(ICollection collection)
		{
			this._local = new ArrayList(collection);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00032C14 File Offset: 0x00031C14
		public ICollection GetMatches(IX509Selector selector)
		{
			if (selector == null)
			{
				return new ArrayList(this._local);
			}
			IList list = new ArrayList();
			foreach (object obj in this._local)
			{
				if (selector.Match(obj))
				{
					list.Add(obj);
				}
			}
			return list;
		}

		// Token: 0x04000806 RID: 2054
		private ICollection _local;
	}
}
