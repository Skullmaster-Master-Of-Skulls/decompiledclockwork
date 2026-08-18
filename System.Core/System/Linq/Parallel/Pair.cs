using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000200 RID: 512
	internal struct Pair<T, U>
	{
		// Token: 0x06001044 RID: 4164 RVA: 0x0003962A File Offset: 0x0003782A
		public Pair(T first, U second)
		{
			this.m_first = first;
			this.m_second = second;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003963A File Offset: 0x0003783A
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00039642 File Offset: 0x00037842
		public T First
		{
			get
			{
				return this.m_first;
			}
			set
			{
				this.m_first = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0003964B File Offset: 0x0003784B
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x00039653 File Offset: 0x00037853
		public U Second
		{
			get
			{
				return this.m_second;
			}
			set
			{
				this.m_second = value;
			}
		}

		// Token: 0x04000938 RID: 2360
		internal T m_first;

		// Token: 0x04000939 RID: 2361
		internal U m_second;
	}
}
