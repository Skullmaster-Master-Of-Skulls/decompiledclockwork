using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Data
{
	// Token: 0x020003C6 RID: 966
	public class StaffDropListAssignment : BusinessBase<int>
	{
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000214A8 File Offset: 0x0001F6A8
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int DataId
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

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000214C0 File Offset: 0x0001F6C0
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public BasicPerson Student { get; set; }
	}
}
