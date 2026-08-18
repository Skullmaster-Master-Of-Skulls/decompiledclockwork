using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200076B RID: 1899
	public sealed class UrlMappingsSection : ConfigurationSection
	{
		// Token: 0x06005B87 RID: 23431 RVA: 0x0013D3F0 File Offset: 0x0013B5F0
		static UrlMappingsSection()
		{
			UrlMappingsSection._properties = new ConfigurationPropertyCollection();
			UrlMappingsSection._properties.Add(UrlMappingsSection._propMappings);
			UrlMappingsSection._properties.Add(UrlMappingsSection._propEnabled);
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06005B88 RID: 23432 RVA: 0x0013D45C File Offset: 0x0013B65C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingsSection._properties;
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06005B89 RID: 23433 RVA: 0x0013D463 File Offset: 0x0013B663
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public UrlMappingCollection UrlMappings
		{
			get
			{
				return (UrlMappingCollection)base[UrlMappingsSection._propMappings];
			}
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x06005B8A RID: 23434 RVA: 0x0013D475 File Offset: 0x0013B675
		// (set) Token: 0x06005B8B RID: 23435 RVA: 0x0013D487 File Offset: 0x0013B687
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool IsEnabled
		{
			get
			{
				return (bool)base[UrlMappingsSection._propEnabled];
			}
			set
			{
				base[UrlMappingsSection._propEnabled] = value;
			}
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0013D49C File Offset: 0x0013B69C
		internal string HttpResolveMapping(string path)
		{
			string result = null;
			string name = UrlPath.MakeVirtualPathAppRelative(path);
			UrlMapping urlMapping = this.UrlMappings[name];
			if (urlMapping != null)
			{
				result = urlMapping.MappedUrl;
			}
			return result;
		}

		// Token: 0x0400303D RID: 12349
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400303E RID: 12350
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x0400303F RID: 12351
		private static readonly ConfigurationProperty _propMappings = new ConfigurationProperty(null, typeof(UrlMappingCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
