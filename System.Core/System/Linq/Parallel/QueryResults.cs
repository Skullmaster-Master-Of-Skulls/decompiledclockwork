using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001C6 RID: 454
	internal abstract class QueryResults<T> : IList<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000F06 RID: 3846
		internal abstract void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient);

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000357A3 File Offset: 0x000339A3
		internal virtual bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000357A6 File Offset: 0x000339A6
		internal virtual T GetElement(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000357AD File Offset: 0x000339AD
		internal virtual int ElementsCount
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000357B4 File Offset: 0x000339B4
		int IList<!0>.IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000357BB File Offset: 0x000339BB
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000357C2 File Offset: 0x000339C2
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700029F RID: 671
		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000357D9 File Offset: 0x000339D9
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000357E0 File Offset: 0x000339E0
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000357E7 File Offset: 0x000339E7
		bool ICollection<!0>.Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000357EE File Offset: 0x000339EE
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x000357F5 File Offset: 0x000339F5
		public int Count
		{
			get
			{
				return this.ElementsCount;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000357FD File Offset: 0x000339FD
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00035800 File Offset: 0x00033A00
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00035807 File Offset: 0x00033A07
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			int num;
			for (int index = 0; index < this.Count; index = num + 1)
			{
				yield return this[index];
				num = index;
			}
			yield break;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00035816 File Offset: 0x00033A16
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
	}
}
