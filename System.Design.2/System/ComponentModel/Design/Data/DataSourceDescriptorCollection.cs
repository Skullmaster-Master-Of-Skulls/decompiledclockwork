using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001F7 RID: 503
	public class DataSourceDescriptorCollection : CollectionBase
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(DataSourceDescriptor value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DataSourceDescriptor value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, DataSourceDescriptor value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DataSourceDescriptor value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(DataSourceDescriptor[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DataSourceDescriptor value)
		{
			base.List.Remove(value);
		}

		// Token: 0x1700042C RID: 1068
		public DataSourceDescriptor this[int index]
		{
			get
			{
				return (DataSourceDescriptor)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
	}
}
