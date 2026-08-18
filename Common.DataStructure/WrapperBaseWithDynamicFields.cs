using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x0200000B RID: 11
	public class WrapperBaseWithDynamicFields<T> : DynamicClassFields
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000267A File Offset: 0x0000087A
		public WrapperBaseWithDynamicFields()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002682 File Offset: 0x00000882
		public WrapperBaseWithDynamicFields(T item, Dictionary<string, object> args) : base(args)
		{
			this.item = item;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002692 File Offset: 0x00000892
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000269A File Offset: 0x0000089A
		public virtual void SetItem(T newItem)
		{
			this.item = newItem;
		}

		// Token: 0x0400000A RID: 10
		private T item;
	}
}
