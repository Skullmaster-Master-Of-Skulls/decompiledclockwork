using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000BE RID: 190
	internal sealed class InternalConfigSettingsFactory : IInternalConfigSettingsFactory
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x000115BE File Offset: 0x0000F7BE
		private InternalConfigSettingsFactory()
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00020397 File Offset: 0x0001E597
		void IInternalConfigSettingsFactory.SetConfigurationSystem(IInternalConfigSystem configSystem, bool initComplete)
		{
			ConfigurationManager.SetConfigurationSystem(configSystem, initComplete);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000203A0 File Offset: 0x0001E5A0
		void IInternalConfigSettingsFactory.CompleteInit()
		{
			ConfigurationManager.CompleteConfigInit();
		}
	}
}
