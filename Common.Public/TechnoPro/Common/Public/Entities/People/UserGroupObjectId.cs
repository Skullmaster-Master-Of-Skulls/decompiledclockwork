using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000268 RID: 616
	public class UserGroupObjectId : BusinessBase<eUserGroupObjectType, int>
	{
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00018BB8 File Offset: 0x00016DB8
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00018BD0 File Offset: 0x00016DD0
		public virtual eUserGroupObjectType UserGroupObjectType
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00018BDC File Offset: 0x00016DDC
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public virtual int ObjectId
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.SecondId = value;
			}
		}
	}
}
