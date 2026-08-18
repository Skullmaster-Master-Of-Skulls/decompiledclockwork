using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x02000E7C RID: 3708
	public class WebResourceCacheProviderCollection : ProviderCollection
	{
		// Token: 0x17002C68 RID: 11368
		public WebResourceCacheProvider this[string name]
		{
			get
			{
				return (WebResourceCacheProvider)base[name];
			}
		}
	}
}
