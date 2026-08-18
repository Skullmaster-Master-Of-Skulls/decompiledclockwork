using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x02000E70 RID: 3696
	public class TimeZoneProviderCollection : ProviderCollection
	{
		// Token: 0x17002C53 RID: 11347
		public TimeZoneProviderBase this[string name]
		{
			get
			{
				return (TimeZoneProviderBase)base[name];
			}
		}

		// Token: 0x06008C32 RID: 35890 RVA: 0x001FD16F File Offset: 0x001FB36F
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is TimeZoneProviderBase))
			{
				throw new ArgumentException("Invalid provider type", "provider");
			}
			base.Add(provider);
		}
	}
}
