using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000221 RID: 545
	internal class SingleObjectCollection : ICollection, IEnumerable
	{
		// Token: 0x06001A23 RID: 6691 RVA: 0x00051DC4 File Offset: 0x0004FFC4
		public SingleObjectCollection(object o)
		{
			this._object = o;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00051DD3 File Offset: 0x0004FFD3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SingleObjectCollection.SingleObjectEnumerator(this._object);
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000097B7 File Offset: 0x000079B7
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000097B7 File Offset: 0x000079B7
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00004335 File Offset: 0x00002535
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00051DE0 File Offset: 0x0004FFE0
		public void CopyTo(Array array, int index)
		{
			array.SetValue(this._object, index);
		}

		// Token: 0x04001818 RID: 6168
		private object _object;

		// Token: 0x0200094C RID: 2380
		private class SingleObjectEnumerator : IEnumerator
		{
			// Token: 0x0600699A RID: 27034 RVA: 0x00177991 File Offset: 0x00175B91
			public SingleObjectEnumerator(object o)
			{
				this._object = o;
			}

			// Token: 0x17001D26 RID: 7462
			// (get) Token: 0x0600699B RID: 27035 RVA: 0x001779A0 File Offset: 0x00175BA0
			public object Current
			{
				get
				{
					return this._object;
				}
			}

			// Token: 0x0600699C RID: 27036 RVA: 0x001779A8 File Offset: 0x00175BA8
			public bool MoveNext()
			{
				if (!this.done)
				{
					this.done = true;
					return true;
				}
				return false;
			}

			// Token: 0x0600699D RID: 27037 RVA: 0x001779BC File Offset: 0x00175BBC
			public void Reset()
			{
				this.done = false;
			}

			// Token: 0x040037D3 RID: 14291
			private object _object;

			// Token: 0x040037D4 RID: 14292
			private bool done;
		}
	}
}
