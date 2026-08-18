using System;
using System.Web.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x0200085E RID: 2142
	internal class AssemblyProviderFactory
	{
		// Token: 0x06004ED4 RID: 20180 RVA: 0x000F7204 File Offset: 0x000F5404
		public static AssemblyProviderBase GetProvider(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty.", "name");
			}
			AssemblyProviderFactory.LoadProviders();
			AssemblyProviderBase result;
			lock (AssemblyProviderFactory.locker)
			{
				AssemblyProviderBase assemblyProviderBase = AssemblyProviderFactory.assemblyProviders[name];
				if (assemblyProviderBase == null)
				{
					throw new ArgumentException("Provider '" + name + "' has not been declared in web.config.");
				}
				result = assemblyProviderBase;
			}
			return result;
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x000F7284 File Offset: 0x000F5484
		private static void LoadProviders()
		{
			lock (AssemblyProviderFactory.locker)
			{
				AssemblyProviderFactory.assemblyProviders = new AssemblyProviderCollection();
				ScriptManagerConfigurationSection scriptManagerConfigurationSection = (ScriptManagerConfigurationSection)WebConfigurationManager.GetSection("telerik.web.ui/radScriptManager");
				if (scriptManagerConfigurationSection != null && scriptManagerConfigurationSection.WhiteList.AssemblyProviders.Count > 0)
				{
					ProvidersHelper.InstantiateProviders(scriptManagerConfigurationSection.WhiteList.AssemblyProviders, AssemblyProviderFactory.assemblyProviders, typeof(AssemblyProviderBase));
				}
				else
				{
					XmlAssemblyProvider provider = new XmlAssemblyProvider();
					AssemblyProviderFactory.assemblyProviders.Add(provider);
				}
			}
		}

		// Token: 0x0400139A RID: 5018
		private static AssemblyProviderCollection assemblyProviders;

		// Token: 0x0400139B RID: 5019
		private static readonly object locker = new object();
	}
}
