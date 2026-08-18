using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E4 RID: 228
	public sealed class ScriptingRoleServiceSection : ConfigurationSection
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x0002AE84 File Offset: 0x00029084
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				ScriptingRoleServiceSection._propEnabled
			};
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002AEA3 File Offset: 0x000290A3
		internal static ScriptingRoleServiceSection GetConfigurationSection()
		{
			return (ScriptingRoleServiceSection)WebConfigurationManager.GetWebApplicationSection("system.web.extensions/scripting/webServices/roleService");
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002AEB4 File Offset: 0x000290B4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ScriptingRoleServiceSection._properties;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002AEBB File Offset: 0x000290BB
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0002AECD File Offset: 0x000290CD
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ScriptingRoleServiceSection._propEnabled];
			}
			set
			{
				base[ScriptingRoleServiceSection._propEnabled] = value;
			}
		}

		// Token: 0x04000385 RID: 901
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04000386 RID: 902
		private static ConfigurationPropertyCollection _properties = ScriptingRoleServiceSection.BuildProperties();
	}
}
