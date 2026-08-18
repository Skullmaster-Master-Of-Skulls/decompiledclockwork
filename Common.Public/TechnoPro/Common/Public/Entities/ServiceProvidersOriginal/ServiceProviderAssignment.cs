using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001FE RID: 510
	public class ServiceProviderAssignment : ServiceRequest
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00016B6F File Offset: 0x00014D6F
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00016B77 File Offset: 0x00014D77
		public StudentCommonInfo StudentCommonInfo { get; set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00016B80 File Offset: 0x00014D80
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00016B88 File Offset: 0x00014D88
		public ServiceProviderBase AssignedServiceProvider { get; set; }
	}
}
