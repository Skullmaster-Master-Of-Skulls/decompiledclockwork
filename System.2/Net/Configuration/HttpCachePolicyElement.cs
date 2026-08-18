using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000334 RID: 820
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06001D61 RID: 7521 RVA: 0x0008BCA0 File Offset: 0x00089EA0
		public HttpCachePolicyElement()
		{
			this.properties.Add(this.maximumAge);
			this.properties.Add(this.maximumStale);
			this.properties.Add(this.minimumFresh);
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0008BD92 File Offset: 0x00089F92
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0008BD9A File Offset: 0x00089F9A
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0008BDAD File Offset: 0x00089FAD
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[this.maximumAge];
			}
			set
			{
				base[this.maximumAge] = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0008BDC1 File Offset: 0x00089FC1
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x0008BDD4 File Offset: 0x00089FD4
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[this.maximumStale];
			}
			set
			{
				base[this.maximumStale] = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0008BDE8 File Offset: 0x00089FE8
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0008BDFB File Offset: 0x00089FFB
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[this.minimumFresh];
			}
			set
			{
				base[this.minimumFresh] = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0008BE0F File Offset: 0x0008A00F
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0008BE22 File Offset: 0x0008A022
		[ConfigurationProperty("policyLevel", IsRequired = true, DefaultValue = HttpRequestCacheLevel.Default)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[this.policyLevel];
			}
			set
			{
				base[this.policyLevel] = value;
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0008BE36 File Offset: 0x0008A036
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0008BE48 File Offset: 0x0008A048
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				HttpCachePolicyElement httpCachePolicyElement = (HttpCachePolicyElement)parentElement;
				this.wasReadFromConfig = httpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0008BE72 File Offset: 0x0008A072
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04001C46 RID: 7238
		private bool wasReadFromConfig;

		// Token: 0x04001C47 RID: 7239
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C48 RID: 7240
		private readonly ConfigurationProperty maximumAge = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C49 RID: 7241
		private readonly ConfigurationProperty maximumStale = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C4A RID: 7242
		private readonly ConfigurationProperty minimumFresh = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C4B RID: 7243
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
