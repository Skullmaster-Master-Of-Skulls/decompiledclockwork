using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02000868 RID: 2152
	internal class ScriptManagerConfigurationSettings
	{
		// Token: 0x06004F14 RID: 20244 RVA: 0x000F7F5C File Offset: 0x000F615C
		private ScriptManagerConfigurationSettings()
		{
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x000F7F64 File Offset: 0x000F6164
		public static ScriptManagerConfigurationSettings GetConfiguration()
		{
			return ScriptManagerConfigurationSettings.configuration;
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x000F7F6C File Offset: 0x000F616C
		public virtual bool EnableEmbeddedjQuery
		{
			get
			{
				bool result = true;
				string value = ConfigurationManager.AppSettings["Telerik.ScriptManager.EnableEmbeddedjQuery"];
				if (!string.IsNullOrEmpty(value))
				{
					bool.TryParse(value, out result);
				}
				return result;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x000F7FA0 File Offset: 0x000F61A0
		public virtual bool EnableHandlerEncryption
		{
			get
			{
				bool result = false;
				string value = ConfigurationManager.AppSettings["Telerik.ScriptManager.EnableHandlerEncryption"];
				if (!string.IsNullOrEmpty(value))
				{
					bool.TryParse(value, out result);
				}
				return result;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x000F7FD1 File Offset: 0x000F61D1
		public virtual string ScriptFolder
		{
			get
			{
				return ConfigurationManager.AppSettings["Telerik.Web.UI.ScriptFolder"];
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x000F7FE2 File Offset: 0x000F61E2
		public virtual string ScriptsFolders
		{
			get
			{
				return ConfigurationManager.AppSettings["Telerik.Web.UI.ScriptsFolder"];
			}
		}

		// Token: 0x040013BD RID: 5053
		private static readonly ScriptManagerConfigurationSettings configuration = new ScriptManagerConfigurationSettings();
	}
}
