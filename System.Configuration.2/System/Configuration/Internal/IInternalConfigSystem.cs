using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B7 RID: 183
	[ComVisible(false)]
	public interface IInternalConfigSystem
	{
		// Token: 0x06000741 RID: 1857
		object GetSection(string configKey);

		// Token: 0x06000742 RID: 1858
		void RefreshConfig(string sectionName);

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000743 RID: 1859
		bool SupportsUserConfig { get; }
	}
}
