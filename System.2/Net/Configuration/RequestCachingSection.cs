using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x0200033D RID: 829
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06001D9B RID: 7579 RVA: 0x0008C3C4 File Offset: 0x0008A5C4
		public RequestCachingSection()
		{
			this.properties.Add(this.disableAllCaching);
			this.properties.Add(this.defaultPolicyLevel);
			this.properties.Add(this.isPrivateCache);
			this.properties.Add(this.defaultHttpCachePolicy);
			this.properties.Add(this.defaultFtpCachePolicy);
			this.properties.Add(this.unspecifiedMaximumAge);
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0008C511 File Offset: 0x0008A711
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[this.defaultHttpCachePolicy];
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0008C524 File Offset: 0x0008A724
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[this.defaultFtpCachePolicy];
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0008C537 File Offset: 0x0008A737
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0008C54A File Offset: 0x0008A74A
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

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0008C55E File Offset: 0x0008A75E
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0008C571 File Offset: 0x0008A771
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0008C585 File Offset: 0x0008A785
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0008C598 File Offset: 0x0008A798
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

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0008C5AC File Offset: 0x0008A7AC
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0008C5BF File Offset: 0x0008A7BF
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

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0008C5D4 File Offset: 0x0008A7D4
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			bool flag = this.DisableAllCaching;
			base.DeserializeElement(reader, serializeCollectionKey);
			if (flag)
			{
				this.DisableAllCaching = true;
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0008C5FC File Offset: 0x0008A7FC
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

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0008C654 File Offset: 0x0008A854
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C5C RID: 7260
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C5D RID: 7261
		private readonly ConfigurationProperty defaultHttpCachePolicy = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5E RID: 7262
		private readonly ConfigurationProperty defaultFtpCachePolicy = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5F RID: 7263
		private readonly ConfigurationProperty defaultPolicyLevel = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache, ConfigurationPropertyOptions.None);

		// Token: 0x04001C60 RID: 7264
		private readonly ConfigurationProperty disableAllCaching = new ConfigurationProperty("disableAllCaching", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C61 RID: 7265
		private readonly ConfigurationProperty isPrivateCache = new ConfigurationProperty("isPrivateCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C62 RID: 7266
		private readonly ConfigurationProperty unspecifiedMaximumAge = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), TimeSpan.FromDays(1.0), ConfigurationPropertyOptions.None);
	}
}
