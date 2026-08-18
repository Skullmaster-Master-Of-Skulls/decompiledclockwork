using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000650 RID: 1616
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x000D598C File Offset: 0x000D498C
		public HttpCachePolicyElement()
		{
			this.properties.Add(this.maximumAge);
			this.properties.Add(this.maximumStale);
			this.properties.Add(this.minimumFresh);
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000D5A7E File Offset: 0x000D4A7E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000D5A86 File Offset: 0x000D4A86
		// (set) Token: 0x0600320E RID: 12814 RVA: 0x000D5A99 File Offset: 0x000D4A99
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

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000D5AAD File Offset: 0x000D4AAD
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x000D5AC0 File Offset: 0x000D4AC0
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

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000D5AD4 File Offset: 0x000D4AD4
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x000D5AE7 File Offset: 0x000D4AE7
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

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000D5AFB File Offset: 0x000D4AFB
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000D5B0E File Offset: 0x000D4B0E
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

		// Token: 0x06003215 RID: 12821 RVA: 0x000D5B22 File Offset: 0x000D4B22
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000D5B34 File Offset: 0x000D4B34
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				HttpCachePolicyElement httpCachePolicyElement = (HttpCachePolicyElement)parentElement;
				this.wasReadFromConfig = httpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000D5B5E File Offset: 0x000D4B5E
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04002F05 RID: 12037
		private bool wasReadFromConfig;

		// Token: 0x04002F06 RID: 12038
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F07 RID: 12039
		private readonly ConfigurationProperty maximumAge = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue, ConfigurationPropertyOptions.None);

		// Token: 0x04002F08 RID: 12040
		private readonly ConfigurationProperty maximumStale = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04002F09 RID: 12041
		private readonly ConfigurationProperty minimumFresh = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04002F0A RID: 12042
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
