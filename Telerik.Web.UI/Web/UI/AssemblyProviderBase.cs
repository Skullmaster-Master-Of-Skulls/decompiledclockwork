using System;
using System.Collections.ObjectModel;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x0200085C RID: 2140
	public class AssemblyProviderBase : ProviderBase
	{
		// Token: 0x06004ED0 RID: 20176 RVA: 0x000F71DE File Offset: 0x000F53DE
		public virtual Collection<AssemblyReference> GetAssembliesList()
		{
			throw new NotImplementedException();
		}
	}
}
