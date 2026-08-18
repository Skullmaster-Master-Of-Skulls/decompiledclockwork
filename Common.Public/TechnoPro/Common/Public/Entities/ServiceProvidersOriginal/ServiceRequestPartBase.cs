using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000206 RID: 518
	public class ServiceRequestPartBase : BusinessBase<int>
	{
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00017014 File Offset: 0x00015214
		// (set) Token: 0x06000FAB RID: 4011 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderRequestId
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0001702C File Offset: 0x0001522C
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00017034 File Offset: 0x00015234
		public string PartsDescription { get; set; }
	}
}
