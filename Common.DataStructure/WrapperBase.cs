using System;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x0200000A RID: 10
	public class WrapperBase<T>
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002050 File Offset: 0x00000250
		public WrapperBase()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000265A File Offset: 0x0000085A
		public WrapperBase(T item)
		{
			this.item = item;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002669 File Offset: 0x00000869
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002671 File Offset: 0x00000871
		public virtual void SetItem(T newItem)
		{
			this.item = newItem;
		}

		// Token: 0x04000009 RID: 9
		private T item;
	}
}
