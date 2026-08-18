using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000651 RID: 1617
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x000D5B68 File Offset: 0x000D4B68
		public FtpCachePolicyElement()
		{
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000D5BB8 File Offset: 0x000D4BB8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000D5BC0 File Offset: 0x000D4BC0
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x000D5BD3 File Offset: 0x000D4BD3
		[ConfigurationProperty("policyLevel", DefaultValue = RequestCacheLevel.Default)]
		public RequestCacheLevel PolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[this.policyLevel];
			}
			set
			{
				base[this.policyLevel] = value;
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000D5BE7 File Offset: 0x000D4BE7
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000D5BF8 File Offset: 0x000D4BF8
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				FtpCachePolicyElement ftpCachePolicyElement = (FtpCachePolicyElement)parentElement;
				this.wasReadFromConfig = ftpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000D5C22 File Offset: 0x000D4C22
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04002F0B RID: 12043
		private bool wasReadFromConfig;

		// Token: 0x04002F0C RID: 12044
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F0D RID: 12045
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
