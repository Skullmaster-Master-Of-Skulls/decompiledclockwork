using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x0200085D RID: 2141
	public class AssemblyProviderCollection : ProviderCollection
	{
		// Token: 0x170019C4 RID: 6596
		public AssemblyProviderBase this[string name]
		{
			get
			{
				return (AssemblyProviderBase)base[name];
			}
		}
	}
}
