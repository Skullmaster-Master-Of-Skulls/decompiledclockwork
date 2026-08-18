using System;
using System.Collections;
using System.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x02000565 RID: 1381
	internal sealed class ExtenderProviderService : IExtenderProviderService, IExtenderListService
	{
		// Token: 0x060030CF RID: 12495 RVA: 0x00114045 File Offset: 0x00113045
		internal ExtenderProviderService()
		{
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x00114050 File Offset: 0x00113050
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

		// Token: 0x060030D1 RID: 12497 RVA: 0x0011408C File Offset: 0x0011308C
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

		// Token: 0x060030D2 RID: 12498 RVA: 0x001140F2 File Offset: 0x001130F2
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

		// Token: 0x040020B9 RID: 8377
		private ArrayList _providers;
	}
}
