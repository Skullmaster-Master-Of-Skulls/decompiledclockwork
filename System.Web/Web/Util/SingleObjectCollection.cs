using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000783 RID: 1923
	internal class SingleObjectCollection : ICollection, IEnumerable
	{
		// Token: 0x06005CC6 RID: 23750 RVA: 0x00173F80 File Offset: 0x00172F80
		public SingleObjectCollection(object o)
		{
			this._object = o;
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x00173F8F File Offset: 0x00172F8F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SingleObjectCollection.SingleObjectEnumerator(this._object);
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x00173F9C File Offset: 0x00172F9C
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00173F9F File Offset: 0x00172F9F
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x00173FA2 File Offset: 0x00172FA2
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00173FA5 File Offset: 0x00172FA5
		public void CopyTo(Array array, int index)
		{
			array.SetValue(this._object, index);
		}

		// Token: 0x04003192 RID: 12690
		private object _object;

		// Token: 0x02000784 RID: 1924
		private class SingleObjectEnumerator : IEnumerator
		{
			// Token: 0x06005CCC RID: 23756 RVA: 0x00173FB4 File Offset: 0x00172FB4
			public SingleObjectEnumerator(object o)
			{
				this._object = o;
			}

			// Token: 0x170017D2 RID: 6098
			// (get) Token: 0x06005CCD RID: 23757 RVA: 0x00173FC3 File Offset: 0x00172FC3
			public object Current
			{
				get
				{
					return this._object;
				}
			}

			// Token: 0x06005CCE RID: 23758 RVA: 0x00173FCB File Offset: 0x00172FCB
			public bool MoveNext()
			{
				if (!this.done)
				{
					this.done = true;
					return true;
				}
				return false;
			}

			// Token: 0x06005CCF RID: 23759 RVA: 0x00173FDF File Offset: 0x00172FDF
			public void Reset()
			{
				this.done = false;
			}

			// Token: 0x04003193 RID: 12691
			private object _object;

			// Token: 0x04003194 RID: 12692
			private bool done;
		}
	}
}
