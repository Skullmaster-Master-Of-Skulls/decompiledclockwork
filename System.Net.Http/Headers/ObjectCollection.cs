using System;
using System.Collections.ObjectModel;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003C RID: 60
	internal class ObjectCollection<T> : Collection<T> where T : class
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000D123 File Offset: 0x0000B323
		public ObjectCollection() : this(ObjectCollection<T>.defaultValidator)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D130 File Offset: 0x0000B330
		public ObjectCollection(Action<T> validator)
		{
			this.validator = validator;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D13F File Offset: 0x0000B33F
		protected override void InsertItem(int index, T item)
		{
			if (this.validator != null)
			{
				this.validator(item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D15D File Offset: 0x0000B35D
		protected override void SetItem(int index, T item)
		{
			if (this.validator != null)
			{
				this.validator(item);
			}
			base.SetItem(index, item);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D17B File Offset: 0x0000B37B
		private static void CheckNotNull(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
		}

		// Token: 0x04000167 RID: 359
		private static readonly Action<T> defaultValidator = new Action<T>(ObjectCollection<T>.CheckNotNull);

		// Token: 0x04000168 RID: 360
		private Action<T> validator;
	}
}
