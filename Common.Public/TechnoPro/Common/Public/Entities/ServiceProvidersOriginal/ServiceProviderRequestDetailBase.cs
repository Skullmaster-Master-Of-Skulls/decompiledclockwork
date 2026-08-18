using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000201 RID: 513
	public class ServiceProviderRequestDetailBase : BusinessBase<int>
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00016C58 File Offset: 0x00014E58
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderRequestDetailId
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

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00016C70 File Offset: 0x00014E70
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x00016C78 File Offset: 0x00014E78
		public BasicPerson CounsellorWhoEntered { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00016C81 File Offset: 0x00014E81
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x00016C89 File Offset: 0x00014E89
		public string Rationale { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00016C92 File Offset: 0x00014E92
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x00016C9A File Offset: 0x00014E9A
		public string SpecialRequest { get; set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00016CA3 File Offset: 0x00014EA3
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00016CAB File Offset: 0x00014EAB
		public string Plan { get; set; }
	}
}
