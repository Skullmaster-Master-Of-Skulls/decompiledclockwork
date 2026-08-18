using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001F9 RID: 505
	public class DataSourceGroupCollection : CollectionBase
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(DataSourceGroup value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DataSourceGroup value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, DataSourceGroup value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DataSourceGroup value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(DataSourceGroup[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DataSourceGroup value)
		{
			base.List.Remove(value);
		}

		// Token: 0x17000431 RID: 1073
		public DataSourceGroup this[int index]
		{
			get
			{
				return (DataSourceGroup)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
	}
}
