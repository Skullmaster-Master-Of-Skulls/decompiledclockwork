using System;

namespace System.ComponentModel.Design
{
	// Token: 0x02000566 RID: 1382
	internal class HostDesigntimeLicenseContext : DesigntimeLicenseContext
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x00114116 File Offset: 0x00113116
		public HostDesigntimeLicenseContext(IServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x00114125 File Offset: 0x00113125
		public override object GetService(Type serviceClass)
		{
			return this.provider.GetService(serviceClass);
		}

		// Token: 0x040020BA RID: 8378
		private IServiceProvider provider;
	}
}
