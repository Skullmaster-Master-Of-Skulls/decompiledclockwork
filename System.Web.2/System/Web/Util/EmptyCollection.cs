using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x020001F7 RID: 503
	internal class EmptyCollection : ICollection, IEnumerable, IEnumerator
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x000030B5 File Offset: 0x000012B5
		private EmptyCollection()
		{
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0004CE22 File Offset: 0x0004B022
		internal static EmptyCollection Instance
		{
			get
			{
				return EmptyCollection.s_theEmptyCollection;
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00004335 File Offset: 0x00002535
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00007722 File Offset: 0x00005922
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x000097B7 File Offset: 0x000079B7
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00004335 File Offset: 0x00002535
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00006164 File Offset: 0x00004364
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0000298D File Offset: 0x00000B8D
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00007722 File Offset: 0x00005922
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00006164 File Offset: 0x00004364
		void IEnumerator.Reset()
		{
		}

		// Token: 0x04001798 RID: 6040
		private static EmptyCollection s_theEmptyCollection = new EmptyCollection();
	}
}
