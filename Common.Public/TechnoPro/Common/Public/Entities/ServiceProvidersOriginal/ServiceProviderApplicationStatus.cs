using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001FD RID: 509
	public class ServiceProviderApplicationStatus : BusinessBase<int>
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00016B08 File Offset: 0x00014D08
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00016B20 File Offset: 0x00014D20
		public new virtual int Id
		{
			get
			{
				return this.ServiceProviderApplicationStatusLookupId;
			}
			set
			{
				this.ServiceProviderApplicationStatusLookupId = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00016B2B File Offset: 0x00014D2B
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00016B33 File Offset: 0x00014D33
		public int ServiceProviderApplicationStatusLookupId { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00016B3C File Offset: 0x00014D3C
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00016B44 File Offset: 0x00014D44
		public string Title { get; set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00016B4D File Offset: 0x00014D4D
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00016B55 File Offset: 0x00014D55
		public bool IsActive { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00016B5E File Offset: 0x00014D5E
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x00016B66 File Offset: 0x00014D66
		public int OrderNum { get; set; }
	}
}
