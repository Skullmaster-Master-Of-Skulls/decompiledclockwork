using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x0200000C RID: 12
	public class WrapperBase<T>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002221 File Offset: 0x00000421
		public WrapperBase()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000222B File Offset: 0x0000042B
		public WrapperBase(T item)
		{
			this.item = item;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000223C File Offset: 0x0000043C
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04000057 RID: 87
		private T item;
	}
}
