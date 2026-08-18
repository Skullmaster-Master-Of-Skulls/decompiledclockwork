using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200034C RID: 844
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x0008DE43 File Offset: 0x0008C043
		public WebRequestModulesSection()
		{
			this.properties.Add(this.webRequestModules);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0008DE80 File Offset: 0x0008C080
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

		// Token: 0x06001E52 RID: 7762 RVA: 0x0008DED8 File Offset: 0x0008C0D8
		protected override void InitializeDefault()
		{
			this.WebRequestModules.Add(new WebRequestModuleElement("https:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("http:", typeof(HttpRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("file:", typeof(FileWebRequestCreator)));
			this.WebRequestModules.Add(new WebRequestModuleElement("ftp:", typeof(FtpWebRequestCreator)));
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0008DF61 File Offset: 0x0008C161
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0008DF69 File Offset: 0x0008C169
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[this.webRequestModules];
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CC1 RID: 7361
		private readonly ConfigurationProperty webRequestModules = new ConfigurationProperty(null, typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
