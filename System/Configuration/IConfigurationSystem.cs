using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	// Token: 0x020006FA RID: 1786
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		// Token: 0x06003727 RID: 14119
		object GetConfig(string configKey);

		// Token: 0x06003728 RID: 14120
		void Init();
	}
}
