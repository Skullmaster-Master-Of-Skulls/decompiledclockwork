using System;
using System.Configuration;
using System.Web.Configuration;

namespace System.Web.WebPages.Razor.Configuration
{
	// Token: 0x02000007 RID: 7
	public class RazorPagesSection : ConfigurationSection
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002312 File Offset: 0x00000512
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002333 File Offset: 0x00000533
		[ConfigurationProperty("pageBaseType", IsRequired = true)]
		public string PageBaseType
		{
			get
			{
				if (!this._pageBaseTypeSet)
				{
					return (string)base[RazorPagesSection._pageBaseTypeProperty];
				}
				return this._pageBaseType;
			}
			set
			{
				this._pageBaseType = value;
				this._pageBaseTypeSet = true;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002343 File Offset: 0x00000543
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002364 File Offset: 0x00000564
		[ConfigurationProperty("namespaces", IsRequired = true)]
		public NamespaceCollection Namespaces
		{
			get
			{
				if (!this._namespacesSet)
				{
					return (NamespaceCollection)base[RazorPagesSection._namespacesProperty];
				}
				return this._namespaces;
			}
			set
			{
				this._namespaces = value;
				this._namespacesSet = true;
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly string SectionName = RazorWebSectionGroup.GroupName + "/pages";

		// Token: 0x0400000C RID: 12
		private static readonly ConfigurationProperty _pageBaseTypeProperty = new ConfigurationProperty("pageBaseType", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400000D RID: 13
		private static readonly ConfigurationProperty _namespacesProperty = new ConfigurationProperty("namespaces", typeof(NamespaceCollection), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400000E RID: 14
		private bool _pageBaseTypeSet;

		// Token: 0x0400000F RID: 15
		private bool _namespacesSet;

		// Token: 0x04000010 RID: 16
		private string _pageBaseType;

		// Token: 0x04000011 RID: 17
		private NamespaceCollection _namespaces;
	}
}
