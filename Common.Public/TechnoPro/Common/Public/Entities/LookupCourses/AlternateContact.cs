using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002EB RID: 747
	public class AlternateContact : BusinessBase<int>
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public AlternateContact()
		{
			this.Name = "";
			this.Email = "";
			this.Phone = "";
			this.Username = "";
			this.PermissionLevel = 0;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0001BD18 File Offset: 0x00019F18
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AlternateContactId
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

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0001BD30 File Offset: 0x00019F30
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0001BD38 File Offset: 0x00019F38
		public string Name { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0001BD41 File Offset: 0x00019F41
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x0001BD49 File Offset: 0x00019F49
		public string Email { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0001BD52 File Offset: 0x00019F52
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x0001BD5A File Offset: 0x00019F5A
		public string Phone { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0001BD63 File Offset: 0x00019F63
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x0001BD6B File Offset: 0x00019F6B
		public string Username { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0001BD74 File Offset: 0x00019F74
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public string EmployeeId { get; set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0001BD85 File Offset: 0x00019F85
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x0001BD8D File Offset: 0x00019F8D
		public int PermissionLevel { get; set; }
	}
}
