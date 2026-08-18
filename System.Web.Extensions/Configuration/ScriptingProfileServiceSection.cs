using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace System.Web.Configuration
{
	// Token: 0x020000E3 RID: 227
	public sealed class ScriptingProfileServiceSection : ConfigurationSection
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				ScriptingProfileServiceSection._propEnabled,
				ScriptingProfileServiceSection._propEnableForReading,
				ScriptingProfileServiceSection._propEnableForWriting
			};
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002AD29 File Offset: 0x00028F29
		internal static ScriptingProfileServiceSection GetConfigurationSection()
		{
			return (ScriptingProfileServiceSection)WebConfigurationManager.GetWebApplicationSection("system.web.extensions/scripting/webServices/profileService");
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002AD3A File Offset: 0x00028F3A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ScriptingProfileServiceSection._properties;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002AD41 File Offset: 0x00028F41
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0002AD53 File Offset: 0x00028F53
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ScriptingProfileServiceSection._propEnabled];
			}
			set
			{
				base[ScriptingProfileServiceSection._propEnabled] = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002AD68 File Offset: 0x00028F68
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0002AD96 File Offset: 0x00028F96
		[ConfigurationProperty("readAccessProperties", DefaultValue = null)]
		public string[] ReadAccessProperties
		{
			get
			{
				string[] array = (string[])base[ScriptingProfileServiceSection._propEnableForReading];
				if (array != null)
				{
					return (string[])array.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					value = (string[])value.Clone();
				}
				base[ScriptingProfileServiceSection._propEnableForReading] = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0002ADE2 File Offset: 0x00028FE2
		[ConfigurationProperty("writeAccessProperties", DefaultValue = null)]
		public string[] WriteAccessProperties
		{
			get
			{
				string[] array = (string[])base[ScriptingProfileServiceSection._propEnableForWriting];
				if (array != null)
				{
					return (string[])array.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					value = (string[])value.Clone();
				}
				base[ScriptingProfileServiceSection._propEnableForWriting] = value;
			}
		}

		// Token: 0x04000381 RID: 897
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04000382 RID: 898
		private static readonly ConfigurationProperty _propEnableForReading = new ConfigurationProperty("readAccessProperties", typeof(string[]), new string[0], new StringArrayConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000383 RID: 899
		private static readonly ConfigurationProperty _propEnableForWriting = new ConfigurationProperty("writeAccessProperties", typeof(string[]), new string[0], new StringArrayConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000384 RID: 900
		private static ConfigurationPropertyCollection _properties = ScriptingProfileServiceSection.BuildProperties();
	}
}
