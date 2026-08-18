using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B6 RID: 182
	[ComVisible(false)]
	public interface IInternalConfigSettingsFactory
	{
		// Token: 0x0600073F RID: 1855
		void SetConfigurationSystem(IInternalConfigSystem internalConfigSystem, bool initComplete);

		// Token: 0x06000740 RID: 1856
		void CompleteInit();
	}
}
