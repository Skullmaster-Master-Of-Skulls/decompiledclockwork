using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003E9 RID: 1001
	public class DataSyncDataItemBase
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0002232E File Offset: 0x0002052E
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x00022336 File Offset: 0x00020536
		public object DataValue { get; set; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0002233F File Offset: 0x0002053F
		public virtual bool HasValue
		{
			get
			{
				return this.DataValue != null;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0002234C File Offset: 0x0002054C
		public virtual bool Equals(DataSyncDataItemBase item)
		{
			bool flag = !this.CheckEqualsShallow(item);
			return !flag && item.DataValue == this.DataValue;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00022380 File Offset: 0x00020580
		public T ConvertTo<T>() where T : DataSyncDataItemBase
		{
			T t = Activator.CreateInstance<T>();
			t.DataValue = this.DataValue;
			return t;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000223AC File Offset: 0x000205AC
		public bool CheckEqualsShallow(DataSyncDataItemBase item)
		{
			bool flag = item == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = item.DataValue == null && this.DataValue == null;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = item.DataValue == null || this.DataValue == null;
					result = !flag3;
				}
			}
			return result;
		}
	}
}
