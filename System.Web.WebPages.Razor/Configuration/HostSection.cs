using System;
using System.Configuration;

namespace System.Web.WebPages.Razor.Configuration
{
	// Token: 0x02000006 RID: 6
	public class HostSection : ConfigurationSection
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022A8 File Offset: 0x000004A8
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022C9 File Offset: 0x000004C9
		[ConfigurationProperty("factoryType", IsRequired = true, DefaultValue = null)]
		public string FactoryType
		{
			get
			{
				if (!this._factoryTypeSet)
				{
					return (string)base[HostSection._typeProperty];
				}
				return this._factoryType;
			}
			set
			{
				this._factoryType = value;
				this._factoryTypeSet = true;
			}
		}

		// Token: 0x04000007 RID: 7
		public static readonly string SectionName = RazorWebSectionGroup.GroupName + "/host";

		// Token: 0x04000008 RID: 8
		private static readonly ConfigurationProperty _typeProperty = new ConfigurationProperty("factoryType", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000009 RID: 9
		private bool _factoryTypeSet;

		// Token: 0x0400000A RID: 10
		private string _factoryType;
	}
}
