using System;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003CB RID: 971
	public class DataSyncExternalCourseAltContact : BusinessBase<string>
	{
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000218AC File Offset: 0x0001FAAC
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x000218B4 File Offset: 0x0001FAB4
		public virtual string ExternalId { get; set; }

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000218BD File Offset: 0x0001FABD
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x000218C5 File Offset: 0x0001FAC5
		public string Name { get; set; }

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000218CE File Offset: 0x0001FACE
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000218D6 File Offset: 0x0001FAD6
		public string Email { get; set; }

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000218DF File Offset: 0x0001FADF
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000218E7 File Offset: 0x0001FAE7
		public string Username { get; set; }

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000218F0 File Offset: 0x0001FAF0
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x000218F8 File Offset: 0x0001FAF8
		public string EmployeeId { get; set; }

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00021901 File Offset: 0x0001FB01
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00021909 File Offset: 0x0001FB09
		public string Phone { get; set; }
	}
}
