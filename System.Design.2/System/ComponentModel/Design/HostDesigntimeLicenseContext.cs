using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001CE RID: 462
	internal class HostDesigntimeLicenseContext : DesigntimeLicenseContext
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		public HostDesigntimeLicenseContext(IServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0005F103 File Offset: 0x0005D303
		public override object GetService(Type serviceClass)
		{
			return this.provider.GetService(serviceClass);
		}

		// Token: 0x040009B2 RID: 2482
		private IServiceProvider provider;
	}
}
