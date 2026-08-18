using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000594 RID: 1428
	public class MediaPublisher : BusinessBase<int>
	{
		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00033094 File Offset: 0x00031294
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0000E258 File Offset: 0x0000C458
		public int PublisherId
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

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000330AC File Offset: 0x000312AC
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000330B4 File Offset: 0x000312B4
		public string Name { get; set; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000330BD File Offset: 0x000312BD
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000330C5 File Offset: 0x000312C5
		public string Phone { get; set; }

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000330CE File Offset: 0x000312CE
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000330D6 File Offset: 0x000312D6
		public string Address { get; set; }

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000330DF File Offset: 0x000312DF
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x000330E7 File Offset: 0x000312E7
		public string Fax { get; set; }

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000330F0 File Offset: 0x000312F0
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x000330F8 File Offset: 0x000312F8
		public string Email { get; set; }

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x00033101 File Offset: 0x00031301
		// (set) Token: 0x06002E6C RID: 11884 RVA: 0x00033109 File Offset: 0x00031309
		public string Website { get; set; }

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x00033112 File Offset: 0x00031312
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x0003311A File Offset: 0x0003131A
		public string Description { get; set; }

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x00033123 File Offset: 0x00031323
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x0003312B File Offset: 0x0003132B
		public string Notes { get; set; }
	}
}
