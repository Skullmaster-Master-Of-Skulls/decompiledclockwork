using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x0200065C RID: 1628
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06003245 RID: 12869 RVA: 0x000D60B0 File Offset: 0x000D50B0
		public RequestCachingSection()
		{
			this.properties.Add(this.disableAllCaching);
			this.properties.Add(this.defaultPolicyLevel);
			this.properties.Add(this.isPrivateCache);
			this.properties.Add(this.defaultHttpCachePolicy);
			this.properties.Add(this.defaultFtpCachePolicy);
			this.properties.Add(this.unspecifiedMaximumAge);
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000D61FD File Offset: 0x000D51FD
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[this.defaultHttpCachePolicy];
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000D6210 File Offset: 0x000D5210
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[this.defaultFtpCachePolicy];
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000D6223 File Offset: 0x000D5223
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x000D6236 File Offset: 0x000D5236
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = RequestCacheLevel.BypassCache)]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[this.defaultPolicyLevel];
			}
			set
			{
				base[this.defaultPolicyLevel] = value;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000D624A File Offset: 0x000D524A
		// (set) Token: 0x0600324B RID: 12875 RVA: 0x000D625D File Offset: 0x000D525D
		[ConfigurationProperty("disableAllCaching", DefaultValue = false)]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[this.disableAllCaching];
			}
			set
			{
				base[this.disableAllCaching] = value;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000D6271 File Offset: 0x000D5271
		// (set) Token: 0x0600324D RID: 12877 RVA: 0x000D6284 File Offset: 0x000D5284
		[ConfigurationProperty("isPrivateCache", DefaultValue = true)]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[this.isPrivateCache];
			}
			set
			{
				base[this.isPrivateCache] = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000D6298 File Offset: 0x000D5298
		// (set) Token: 0x0600324F RID: 12879 RVA: 0x000D62AB File Offset: 0x000D52AB
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[this.unspecifiedMaximumAge];
			}
			set
			{
				base[this.unspecifiedMaximumAge] = value;
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000D62C0 File Offset: 0x000D52C0
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			bool flag = this.DisableAllCaching;
			base.DeserializeElement(reader, serializeCollectionKey);
			if (flag)
			{
				this.DisableAllCaching = true;
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000D62E8 File Offset: 0x000D52E8
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[]
				{
					"requestCaching"
				}), inner);
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000D6344 File Offset: 0x000D5344
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F27 RID: 12071
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F28 RID: 12072
		private readonly ConfigurationProperty defaultHttpCachePolicy = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F29 RID: 12073
		private readonly ConfigurationProperty defaultFtpCachePolicy = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F2A RID: 12074
		private readonly ConfigurationProperty defaultPolicyLevel = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache, ConfigurationPropertyOptions.None);

		// Token: 0x04002F2B RID: 12075
		private readonly ConfigurationProperty disableAllCaching = new ConfigurationProperty("disableAllCaching", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002F2C RID: 12076
		private readonly ConfigurationProperty isPrivateCache = new ConfigurationProperty("isPrivateCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F2D RID: 12077
		private readonly ConfigurationProperty unspecifiedMaximumAge = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), TimeSpan.FromDays(1.0), ConfigurationPropertyOptions.None);
	}
}
