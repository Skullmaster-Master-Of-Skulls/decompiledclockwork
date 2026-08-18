using System;
using System.Collections;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004C8 RID: 1224
	public class ArrangedElementCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06005028 RID: 20520 RVA: 0x0014D604 File Offset: 0x0014B804
		internal ArrangedElementCollection()
		{
			this._innerList = new ArrayList(4);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0014D618 File Offset: 0x0014B818
		internal ArrangedElementCollection(ArrayList innerList)
		{
			this._innerList = innerList;
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0014D627 File Offset: 0x0014B827
		private ArrangedElementCollection(int size)
		{
			this._innerList = new ArrayList(size);
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0014D63B File Offset: 0x0014B83B
		internal ArrayList InnerList
		{
			get
			{
				return this._innerList;
			}
		}

		// Token: 0x17001384 RID: 4996
		internal virtual IArrangedElement this[int index]
		{
			get
			{
				return (IArrangedElement)this.InnerList[index];
			}
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x0014D658 File Offset: 0x0014B858
		public override bool Equals(object obj)
		{
			ArrangedElementCollection arrangedElementCollection = obj as ArrangedElementCollection;
			if (arrangedElementCollection == null || this.Count != arrangedElementCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.InnerList[i] != arrangedElementCollection.InnerList[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x0014D6AD File Offset: 0x0014B8AD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0014D6B8 File Offset: 0x0014B8B8
		internal void MoveElement(IArrangedElement element, int fromIndex, int toIndex)
		{
			int num = toIndex - fromIndex;
			if (num == -1 || num == 1)
			{
				this.InnerList[fromIndex] = this.InnerList[toIndex];
			}
			else
			{
				int sourceIndex;
				int destinationIndex;
				if (num > 0)
				{
					sourceIndex = fromIndex + 1;
					destinationIndex = fromIndex;
				}
				else
				{
					sourceIndex = toIndex;
					destinationIndex = toIndex + 1;
					num = -num;
				}
				ArrangedElementCollection.Copy(this, sourceIndex, this, destinationIndex, num);
			}
			this.InnerList[toIndex] = element;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0014D71C File Offset: 0x0014B91C
		private static void Copy(ArrangedElementCollection sourceList, int sourceIndex, ArrangedElementCollection destinationList, int destinationIndex, int length)
		{
			if (sourceIndex < destinationIndex)
			{
				sourceIndex += length;
				destinationIndex += length;
				while (length > 0)
				{
					destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
					length--;
				}
				return;
			}
			while (length > 0)
			{
				destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
				length--;
			}
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x0014D796 File Offset: 0x0014B996
		void IList.Clear()
		{
			this.InnerList.Clear();
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0011CD5C File Offset: 0x0011AF5C
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x0014D7A3 File Offset: 0x0014B9A3
		public virtual bool IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0014D7B0 File Offset: 0x0014B9B0
		void IList.RemoveAt(int index)
		{
			this.InnerList.RemoveAt(index);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0014D7BE File Offset: 0x0014B9BE
		void IList.Remove(object value)
		{
			this.InnerList.Remove(value);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0014D7CC File Offset: 0x0014B9CC
		int IList.Add(object value)
		{
			return this.InnerList.Add(value);
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0011CE4C File Offset: 0x0011B04C
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0000A547 File Offset: 0x00008747
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17001387 RID: 4999
		object IList.this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x0014D7DA File Offset: 0x0014B9DA
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x0014D7E7 File Offset: 0x0014B9E7
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0011D029 File Offset: 0x0011B229
		public void CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x0014D7F4 File Offset: 0x0014B9F4
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0014D801 File Offset: 0x0014BA01
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x0400347D RID: 13437
		internal static ArrangedElementCollection Empty = new ArrangedElementCollection(0);

		// Token: 0x0400347E RID: 13438
		private ArrayList _innerList;
	}
}
