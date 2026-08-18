using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x02000898 RID: 2200
	public class SpreadsheetProviderCollection : ProviderCollection
	{
		// Token: 0x17001AD0 RID: 6864
		public SpreadsheetProviderBase this[string name]
		{
			get
			{
				return (SpreadsheetProviderBase)base[name];
			}
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x000FF32C File Offset: 0x000FD52C
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SpreadsheetProviderBase))
			{
				throw new ArgumentException("Invalid provider type", "provider");
			}
			base.Add(provider);
		}
	}
}
