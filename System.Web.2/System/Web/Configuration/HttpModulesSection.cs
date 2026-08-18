using System;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	// Token: 0x020006FE RID: 1790
	public sealed class HttpModulesSection : ConfigurationSection
	{
		// Token: 0x0600566B RID: 22123 RVA: 0x0012E9BA File Offset: 0x0012CBBA
		static HttpModulesSection()
		{
			HttpModulesSection._properties = new ConfigurationPropertyCollection();
			HttpModulesSection._properties.Add(HttpModulesSection._propHttpModules);
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x0600566D RID: 22125 RVA: 0x0012E9EC File Offset: 0x0012CBEC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModulesSection._properties;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x0012E9F3 File Offset: 0x0012CBF3
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public HttpModuleActionCollection Modules
		{
			get
			{
				return (HttpModuleActionCollection)base[HttpModulesSection._propHttpModules];
			}
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x0012EA08 File Offset: 0x0012CC08
		internal HttpModuleCollection CreateModules()
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (object obj in this.Modules)
			{
				HttpModuleAction httpModuleAction = (HttpModuleAction)obj;
				httpModuleCollection.AddModule(httpModuleAction.Entry.ModuleName, httpModuleAction.Entry.Create());
			}
			httpModuleCollection.AddModule("DefaultAuthentication", DefaultAuthenticationModule.CreateDefaultAuthenticationModuleWithAssert());
			return httpModuleCollection;
		}

		// Token: 0x04002DDC RID: 11740
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002DDD RID: 11741
		private static readonly ConfigurationProperty _propHttpModules = new ConfigurationProperty(null, typeof(HttpModuleActionCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
