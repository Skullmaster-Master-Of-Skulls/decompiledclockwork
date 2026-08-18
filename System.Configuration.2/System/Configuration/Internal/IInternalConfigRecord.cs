using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B4 RID: 180
	[ComVisible(false)]
	public interface IInternalConfigRecord
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600072C RID: 1836
		string ConfigPath { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600072D RID: 1837
		string StreamName { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600072E RID: 1838
		bool HasInitErrors { get; }

		// Token: 0x0600072F RID: 1839
		void ThrowIfInitErrors();

		// Token: 0x06000730 RID: 1840
		object GetSection(string configKey);

		// Token: 0x06000731 RID: 1841
		object GetLkgSection(string configKey);

		// Token: 0x06000732 RID: 1842
		void RefreshSection(string configKey);

		// Token: 0x06000733 RID: 1843
		void Remove();
	}
}
