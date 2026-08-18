using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	// Token: 0x020006FE RID: 1790
	public abstract class SettingsProvider : ProviderBase
	{
		// Token: 0x06003733 RID: 14131
		public abstract SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection);

		// Token: 0x06003734 RID: 14132
		public abstract void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection);

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003735 RID: 14133
		// (set) Token: 0x06003736 RID: 14134
		public abstract string ApplicationName { get; set; }
	}
}
