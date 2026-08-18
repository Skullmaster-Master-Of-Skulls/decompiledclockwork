using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200066F RID: 1647
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x060032EE RID: 13038 RVA: 0x000D7995 File Offset: 0x000D6995
		public WebRequestModulesSection()
		{
			this.properties.Add(this.webRequestModules);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000D79D4 File Offset: 0x000D69D4
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[]
				{
					"webRequestModules"
				}), inner);
			}
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000D7A30 File Offset: 0x000D6A30
		protected override void InitializeDefault()
		{
			this.WebRequestModules.Add(new WebRequestModuleElement("https:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("http:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("file:", typeof(FileWebRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("ftp:", typeof(FtpWebRequestCreator)));
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000D7AB9 File Offset: 0x000D6AB9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000D7AC1 File Offset: 0x000D6AC1
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[this.webRequestModules];
			}
		}

		// Token: 0x04002F7C RID: 12156
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F7D RID: 12157
		private readonly ConfigurationProperty webRequestModules = new ConfigurationProperty(null, typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
