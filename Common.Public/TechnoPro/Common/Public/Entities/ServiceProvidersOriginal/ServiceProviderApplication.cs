using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001F9 RID: 505
	public class ServiceProviderApplication : ServiceProviderApplicationBase
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00016940 File Offset: 0x00014B40
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00016948 File Offset: 0x00014B48
		public ServiceProvider ServiceProvider { get; set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00016951 File Offset: 0x00014B51
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00016959 File Offset: 0x00014B59
		public int WhoEnteredPersonId { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00016962 File Offset: 0x00014B62
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0001696A File Offset: 0x00014B6A
		public IList<ServiceProviderApplicationCourse> Courses { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00016973 File Offset: 0x00014B73
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0001697B File Offset: 0x00014B7B
		public IList<ServiceProviderApplicationAvailability> Availabilities { get; set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00016984 File Offset: 0x00014B84
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0001698C File Offset: 0x00014B8C
		public string IsActiveComment { get; set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00016995 File Offset: 0x00014B95
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0001699D File Offset: 0x00014B9D
		public double RateOfPay { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000169A6 File Offset: 0x00014BA6
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x000169AE File Offset: 0x00014BAE
		public eServiceProviderRateOfPayType RateOfPayType { get; set; }
	}
}
