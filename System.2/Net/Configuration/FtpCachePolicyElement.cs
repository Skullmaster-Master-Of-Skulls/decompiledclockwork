using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000335 RID: 821
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06001D6E RID: 7534 RVA: 0x0008BE7C File Offset: 0x0008A07C
		public FtpCachePolicyElement()
		{
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0008BECC File Offset: 0x0008A0CC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0008BED4 File Offset: 0x0008A0D4
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0008BEE7 File Offset: 0x0008A0E7
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

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008BEFB File Offset: 0x0008A0FB
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0008BF0C File Offset: 0x0008A10C
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				FtpCachePolicyElement ftpCachePolicyElement = (FtpCachePolicyElement)parentElement;
				this.wasReadFromConfig = ftpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0008BF36 File Offset: 0x0008A136
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04001C4C RID: 7244
		private bool wasReadFromConfig;

		// Token: 0x04001C4D RID: 7245
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C4E RID: 7246
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
