using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E1 RID: 225
	public sealed class ScriptingAuthenticationServiceSection : ConfigurationSection
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				ScriptingAuthenticationServiceSection._propEnabled,
				ScriptingAuthenticationServiceSection._propRequireSSL
			};
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002AB0A File Offset: 0x00028D0A
		internal static ScriptingAuthenticationServiceSection GetConfigurationSection()
		{
			return (ScriptingAuthenticationServiceSection)WebConfigurationManager.GetWebApplicationSection("system.web.extensions/scripting/webServices/authenticationService");
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002AB1B File Offset: 0x00028D1B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ScriptingAuthenticationServiceSection._properties;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002AB22 File Offset: 0x00028D22
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0002AB34 File Offset: 0x00028D34
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ScriptingAuthenticationServiceSection._propEnabled];
			}
			set
			{
				base[ScriptingAuthenticationServiceSection._propEnabled] = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002AB47 File Offset: 0x00028D47
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0002AB59 File Offset: 0x00028D59
		[ConfigurationProperty("requireSSL", DefaultValue = false)]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[ScriptingAuthenticationServiceSection._propRequireSSL];
			}
			set
			{
				base[ScriptingAuthenticationServiceSection._propRequireSSL] = value;
			}
		}

		// Token: 0x0400037A RID: 890
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x0400037B RID: 891
		private static readonly ConfigurationProperty _propRequireSSL = new ConfigurationProperty("requireSSL", typeof(bool), false);

		// Token: 0x0400037C RID: 892
		private static ConfigurationPropertyCollection _properties = ScriptingAuthenticationServiceSection.BuildProperties();
	}
}
