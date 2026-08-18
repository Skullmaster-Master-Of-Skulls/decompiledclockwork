using System;

namespace System.Configuration
{
	// Token: 0x020006FD RID: 1789
	public interface ISettingsProviderService
	{
		// Token: 0x06003732 RID: 14130
		SettingsProvider GetSettingsProvider(SettingsProperty property);
	}
}
