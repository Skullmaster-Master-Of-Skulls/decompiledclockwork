using System;
using System.Configuration;
using System.Web.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x020008AD RID: 2221
	internal static class SpreadsheetProviderFactory
	{
		// Token: 0x06005271 RID: 21105 RVA: 0x0010061C File Offset: 0x000FE81C
		public static SpreadsheetProviderBase GetProvider(RadSpreadsheet owner, string name)
		{
			if (name == "Integrated")
			{
				return new SpreadsheetEmptyProvider();
			}
			return SpreadsheetProviderFactory.GetProvider(name);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00100638 File Offset: 0x000FE838
		public static SpreadsheetProviderBase GetProvider(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Provider name cannot be empty string");
			}
			SpreadsheetProviderFactory.LoadProviders();
			SpreadsheetProviderBase result;
			lock (SpreadsheetProviderFactory.locker)
			{
				SpreadsheetProviderBase spreadsheetProviderBase = SpreadsheetProviderFactory.spreadsheetProviders[name];
				if (spreadsheetProviderBase == null)
				{
					throw new ArgumentException("Provider '" + name + "' has not been declared in web.config.");
				}
				result = spreadsheetProviderBase;
			}
			return result;
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x001006B4 File Offset: 0x000FE8B4
		public static void LoadProviders()
		{
			if (SpreadsheetProviderFactory.spreadsheetProviders == null)
			{
				lock (SpreadsheetProviderFactory.locker)
				{
					if (SpreadsheetProviderFactory.spreadsheetProviders == null)
					{
						SpreadsheetProviderFactory.spreadsheetProviders = new SpreadsheetProviderCollection();
						RadSpreadsheetConfigurationSection radSpreadsheetConfigurationSection = (RadSpreadsheetConfigurationSection)ConfigurationManager.GetSection("telerik.web.ui/radSpreadsheet");
						if (radSpreadsheetConfigurationSection != null)
						{
							ProvidersHelper.InstantiateProviders(radSpreadsheetConfigurationSection.Providers, SpreadsheetProviderFactory.spreadsheetProviders, typeof(SpreadsheetProviderBase));
						}
					}
				}
			}
		}

		// Token: 0x04001444 RID: 5188
		private static SpreadsheetProviderCollection spreadsheetProviders;

		// Token: 0x04001445 RID: 5189
		private static readonly object locker = new object();
	}
}
