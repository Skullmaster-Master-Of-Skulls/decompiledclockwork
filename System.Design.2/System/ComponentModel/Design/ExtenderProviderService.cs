using System;
using System.Collections;
using System.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001CD RID: 461
	internal sealed class ExtenderProviderService : IExtenderProviderService, IExtenderListService
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x0000362F File Offset: 0x0000182F
		internal ExtenderProviderService()
		{
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0005F030 File Offset: 0x0005D230
		IExtenderProvider[] IExtenderListService.GetExtenderProviders()
		{
			if (this._providers != null)
			{
				IExtenderProvider[] array = new IExtenderProvider[this._providers.Count];
				this._providers.CopyTo(array, 0);
				return array;
			}
			return new IExtenderProvider[0];
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0005F06C File Offset: 0x0005D26C
		void IExtenderProviderService.AddExtenderProvider(IExtenderProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (this._providers == null)
			{
				this._providers = new ArrayList(4);
			}
			if (this._providers.Contains(provider))
			{
				throw new ArgumentException(SR.GetString("ExtenderProviderServiceDuplicateProvider", new object[]
				{
					provider
				}));
			}
			this._providers.Add(provider);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
		void IExtenderProviderService.RemoveExtenderProvider(IExtenderProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (this._providers != null)
			{
				this._providers.Remove(provider);
			}
		}

		// Token: 0x040009B1 RID: 2481
		private ArrayList _providers;
	}
}
