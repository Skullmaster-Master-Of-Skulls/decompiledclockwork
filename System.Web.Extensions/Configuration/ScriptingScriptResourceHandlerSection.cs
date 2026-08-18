using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E5 RID: 229
	public sealed class ScriptingScriptResourceHandlerSection : ConfigurationSection
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002AF0C File Offset: 0x0002910C
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				ScriptingScriptResourceHandlerSection._propEnableCaching,
				ScriptingScriptResourceHandlerSection._propEnableCompression
			};
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002AF36 File Offset: 0x00029136
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ScriptingScriptResourceHandlerSection._properties;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002AF3D File Offset: 0x0002913D
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0002AF4F File Offset: 0x0002914F
		[ConfigurationProperty("enableCaching", DefaultValue = true)]
		public bool EnableCaching
		{
			get
			{
				return (bool)base[ScriptingScriptResourceHandlerSection._propEnableCaching];
			}
			set
			{
				base[ScriptingScriptResourceHandlerSection._propEnableCaching] = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002AF62 File Offset: 0x00029162
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002AF74 File Offset: 0x00029174
		[ConfigurationProperty("enableCompression", DefaultValue = true)]
		public bool EnableCompression
		{
			get
			{
				return (bool)base[ScriptingScriptResourceHandlerSection._propEnableCompression];
			}
			set
			{
				base[ScriptingScriptResourceHandlerSection._propEnableCompression] = value;
			}
		}

		// Token: 0x04000387 RID: 903
		private static readonly ConfigurationProperty _propEnableCaching = new ConfigurationProperty("enableCaching", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04000388 RID: 904
		private static readonly ConfigurationProperty _propEnableCompression = new ConfigurationProperty("enableCompression", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04000389 RID: 905
		private static ConfigurationPropertyCollection _properties = ScriptingScriptResourceHandlerSection.BuildProperties();

		// Token: 0x02000179 RID: 377
		internal static class ApplicationSettings
		{
			// Token: 0x0600107A RID: 4218 RVA: 0x00038A0C File Offset: 0x00036C0C
			private static void EnsureSectionLoaded()
			{
				if (!ScriptingScriptResourceHandlerSection.ApplicationSettings.s_sectionLoaded)
				{
					ScriptingScriptResourceHandlerSection scriptingScriptResourceHandlerSection = (ScriptingScriptResourceHandlerSection)WebConfigurationManager.GetWebApplicationSection("system.web.extensions/scripting/scriptResourceHandler");
					if (scriptingScriptResourceHandlerSection != null)
					{
						ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCaching = scriptingScriptResourceHandlerSection.EnableCaching;
						ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCompression = scriptingScriptResourceHandlerSection.EnableCompression;
					}
					else
					{
						ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCaching = (bool)ScriptingScriptResourceHandlerSection._propEnableCaching.DefaultValue;
						ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCompression = (bool)ScriptingScriptResourceHandlerSection._propEnableCompression.DefaultValue;
					}
					ScriptingScriptResourceHandlerSection.ApplicationSettings.s_sectionLoaded = true;
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x0600107B RID: 4219 RVA: 0x00038A7D File Offset: 0x00036C7D
			internal static bool EnableCaching
			{
				get
				{
					ScriptingScriptResourceHandlerSection.ApplicationSettings.EnsureSectionLoaded();
					return ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCaching;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x0600107C RID: 4220 RVA: 0x00038A89 File Offset: 0x00036C89
			internal static bool EnableCompression
			{
				get
				{
					ScriptingScriptResourceHandlerSection.ApplicationSettings.EnsureSectionLoaded();
					return ScriptingScriptResourceHandlerSection.ApplicationSettings.s_enableCompression;
				}
			}

			// Token: 0x04000519 RID: 1305
			private static volatile bool s_sectionLoaded;

			// Token: 0x0400051A RID: 1306
			private static bool s_enableCaching;

			// Token: 0x0400051B RID: 1307
			private static bool s_enableCompression;
		}
	}
}
