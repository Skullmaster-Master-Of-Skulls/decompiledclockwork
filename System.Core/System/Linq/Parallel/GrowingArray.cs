using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001FB RID: 507
	internal class GrowingArray<T>
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x00038EF4 File Offset: 0x000370F4
		internal GrowingArray()
		{
			this.m_array = new T[1024];
			this.m_count = 0;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00038F13 File Offset: 0x00037113
		internal T[] InternalArray
		{
			get
			{
				return this.m_array;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00038F1B File Offset: 0x0003711B
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00038F24 File Offset: 0x00037124
		internal void Add(T element)
		{
			if (this.m_count >= this.m_array.Length)
			{
				this.GrowArray(2 * this.m_array.Length);
			}
			T[] array = this.m_array;
			int count = this.m_count;
			this.m_count = count + 1;
			array[count] = element;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00038F70 File Offset: 0x00037170
		private void GrowArray(int newSize)
		{
			T[] array = new T[newSize];
			this.m_array.CopyTo(array, 0);
			this.m_array = array;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00038F98 File Offset: 0x00037198
		internal void CopyFrom(T[] otherArray, int otherCount)
		{
			if (this.m_count + otherCount > this.m_array.Length)
			{
				this.GrowArray(this.m_count + otherCount);
			}
			Array.Copy(otherArray, 0, this.m_array, this.m_count, otherCount);
			this.m_count += otherCount;
		}

		// Token: 0x04000928 RID: 2344
		private T[] m_array;

		// Token: 0x04000929 RID: 2345
		private int m_count;

		// Token: 0x0400092A RID: 2346
		private const int DEFAULT_ARRAY_SIZE = 1024;
	}
}
