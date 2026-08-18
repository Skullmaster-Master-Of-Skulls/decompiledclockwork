using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x020012DD RID: 4829
	public class SchedulerProviderCollection : ProviderCollection
	{
		// Token: 0x1700417F RID: 16767
		public SchedulerProviderBase this[string name]
		{
			get
			{
				return (SchedulerProviderBase)base[name];
			}
		}

		// Token: 0x0600CAC1 RID: 51905 RVA: 0x002D4588 File Offset: 0x002D2788
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SchedulerProviderBase))
			{
				throw new ArgumentException("Invalid provider type", "provider");
			}
			base.Add(provider);
		}
	}
}
