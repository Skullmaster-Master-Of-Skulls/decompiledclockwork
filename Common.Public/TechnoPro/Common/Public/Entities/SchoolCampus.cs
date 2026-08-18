using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000DA RID: 218
	public class SchoolCampus : BusinessBase<int>, ICloneable<SchoolCampus>, ICloneable
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public SchoolCampus()
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		public SchoolCampus(SchoolCampus sc)
		{
			bool flag = sc == null;
			if (!flag)
			{
				this.CampusId = sc.CampusId;
				this.CampusName = sc.CampusName;
				this.Description = sc.Description;
				this.Id = sc.Id;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000E240 File Offset: 0x0000C440
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000E258 File Offset: 0x0000C458
		public int CampusId
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000E263 File Offset: 0x0000C463
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000E26B File Offset: 0x0000C46B
		public string CampusName { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000E274 File Offset: 0x0000C474
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000E27C File Offset: 0x0000C47C
		public string Description { get; set; }

		// Token: 0x06000537 RID: 1335 RVA: 0x0000E288 File Offset: 0x0000C488
		public SchoolCampus Clone()
		{
			return new SchoolCampus(this);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
