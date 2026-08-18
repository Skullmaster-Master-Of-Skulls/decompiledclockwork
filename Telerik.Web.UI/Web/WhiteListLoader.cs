using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x020001CB RID: 459
	internal class WhiteListLoader : IAssemblyWhiteListLoader
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0003D18C File Offset: 0x0003B38C
		private ScriptManagerConfigurationSection WhiteListConfig
		{
			get
			{
				return WebConfigurationManager.GetSection("telerik.web.ui/radScriptManager") as ScriptManagerConfigurationSection;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0003D19D File Offset: 0x0003B39D
		public bool WhiteListEnabled
		{
			get
			{
				return this.WhiteListConfig != null && this.WhiteListConfig.EnableAssemblyWhiteList;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003D1B4 File Offset: 0x0003B3B4
		public virtual ICollection<AssemblyReference> LoadWhiteList()
		{
			if (this.WhiteListConfig != null)
			{
				ScriptManagerConfigurationSection whiteListConfig = this.WhiteListConfig;
				string defaultAssemblyProvider = whiteListConfig.WhiteList.DefaultAssemblyProvider;
				AssemblyProviderBase provider = AssemblyProviderFactory.GetProvider(defaultAssemblyProvider);
				return new AssemblyWhiteListCollection(provider.GetAssembliesList());
			}
			return new AssemblyWhiteListCollection(new List<AssemblyReference>());
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003D21C File Offset: 0x0003B41C
		public virtual void VerifyEntry(ScriptEntry entry)
		{
			ICollection<AssemblyReference> source = this.LoadWhiteList();
			string assemblyName = entry.Assembly;
			if (!source.Any((AssemblyReference p) => p.Assembly.FullName == assemblyName))
			{
				throw new WhiteListArgumentException(assemblyName);
			}
		}
	}
}
