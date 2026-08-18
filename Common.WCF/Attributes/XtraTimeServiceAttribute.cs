using System;

namespace TechnoPro.Common.WCF.Attributes
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class XtraTimeServiceAttribute : BindingServiceAttribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003E1A File Offset: 0x0000201A
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003E22 File Offset: 0x00002022
		public int TimeoutInMinutes { get; set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00003E2B File Offset: 0x0000202B
		public XtraTimeServiceAttribute()
		{
			this.TimeoutInMinutes = 30;
		}
	}
}
