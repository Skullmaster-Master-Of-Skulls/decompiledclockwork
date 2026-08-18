using System;

namespace TechnoPro.Common.Public.Entities.Caching
{
	// Token: 0x02000470 RID: 1136
	public class ServerCacheItem : BusinessBase<eServerCacheItemType>, IComparable
	{
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00026528 File Offset: 0x00024728
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x00026540 File Offset: 0x00024740
		public virtual eServerCacheItemType ServerCacheItemType
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0002654B File Offset: 0x0002474B
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x00026553 File Offset: 0x00024753
		public int SubItemId { get; set; }

		// Token: 0x0600226E RID: 8814 RVA: 0x0002655C File Offset: 0x0002475C
		public int CompareTo(object obj)
		{
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				ServerCacheItem serverCacheItem = obj as ServerCacheItem;
				bool flag2 = serverCacheItem == null;
				if (flag2)
				{
					throw new Exception("Can't compare - object is not of type ServerCacheItem");
				}
				bool flag3 = serverCacheItem.ServerCacheItemType == this.ServerCacheItemType && serverCacheItem.SubItemId == this.SubItemId;
				if (flag3)
				{
					result = 0;
				}
				else
				{
					result = serverCacheItem.ServerCacheItemType.CompareTo(this.ServerCacheItemType);
				}
			}
			return result;
		}
	}
}
