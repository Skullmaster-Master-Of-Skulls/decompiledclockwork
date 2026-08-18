using System;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Caching;

namespace System.Web.Configuration
{
	// Token: 0x02000721 RID: 1825
	public sealed class OutputCacheSection : ConfigurationSection
	{
		// Token: 0x060057DE RID: 22494 RVA: 0x00133838 File Offset: 0x00131A38
		static OutputCacheSection()
		{
			OutputCacheSection._properties = new ConfigurationPropertyCollection();
			OutputCacheSection._properties.Add(OutputCacheSection._propEnableOutputCache);
			OutputCacheSection._properties.Add(OutputCacheSection._propEnableFragmentCache);
			OutputCacheSection._properties.Add(OutputCacheSection._propSendCacheControlHeader);
			OutputCacheSection._properties.Add(OutputCacheSection._propOmitVaryStar);
			OutputCacheSection._properties.Add(OutputCacheSection._propEnableKernelCacheForVaryByStar);
			OutputCacheSection._properties.Add(OutputCacheSection._propDefaultProviderName);
			OutputCacheSection._properties.Add(OutputCacheSection._propProviders);
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x060057E0 RID: 22496 RVA: 0x00133998 File Offset: 0x00131B98
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSection._properties;
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x0013399F File Offset: 0x00131B9F
		// (set) Token: 0x060057E2 RID: 22498 RVA: 0x001339CC File Offset: 0x00131BCC
		[ConfigurationProperty("enableOutputCache", DefaultValue = true)]
		public bool EnableOutputCache
		{
			get
			{
				if (!this.enableOutputCacheCached)
				{
					this.enableOutputCache = (bool)base[OutputCacheSection._propEnableOutputCache];
					this.enableOutputCacheCached = true;
				}
				return this.enableOutputCache;
			}
			set
			{
				base[OutputCacheSection._propEnableOutputCache] = value;
				this.enableOutputCache = value;
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x001339E6 File Offset: 0x00131BE6
		// (set) Token: 0x060057E4 RID: 22500 RVA: 0x001339F8 File Offset: 0x00131BF8
		[ConfigurationProperty("enableFragmentCache", DefaultValue = true)]
		public bool EnableFragmentCache
		{
			get
			{
				return (bool)base[OutputCacheSection._propEnableFragmentCache];
			}
			set
			{
				base[OutputCacheSection._propEnableFragmentCache] = value;
			}
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x00133A0B File Offset: 0x00131C0B
		// (set) Token: 0x060057E6 RID: 22502 RVA: 0x00133A38 File Offset: 0x00131C38
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = true)]
		public bool SendCacheControlHeader
		{
			get
			{
				if (!this.sendCacheControlHeaderCached)
				{
					this.sendCacheControlHeaderCache = (bool)base[OutputCacheSection._propSendCacheControlHeader];
					this.sendCacheControlHeaderCached = true;
				}
				return this.sendCacheControlHeaderCache;
			}
			set
			{
				base[OutputCacheSection._propSendCacheControlHeader] = value;
				this.sendCacheControlHeaderCache = value;
			}
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x060057E7 RID: 22503 RVA: 0x00133A52 File Offset: 0x00131C52
		// (set) Token: 0x060057E8 RID: 22504 RVA: 0x00133A7F File Offset: 0x00131C7F
		[ConfigurationProperty("omitVaryStar", DefaultValue = false)]
		public bool OmitVaryStar
		{
			get
			{
				if (!this.omitVaryStarCached)
				{
					this.omitVaryStar = (bool)base[OutputCacheSection._propOmitVaryStar];
					this.omitVaryStarCached = true;
				}
				return this.omitVaryStar;
			}
			set
			{
				base[OutputCacheSection._propOmitVaryStar] = value;
				this.omitVaryStar = value;
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x00133A99 File Offset: 0x00131C99
		// (set) Token: 0x060057EA RID: 22506 RVA: 0x00133AC6 File Offset: 0x00131CC6
		[ConfigurationProperty("enableKernelCacheForVaryByStar", DefaultValue = false)]
		public bool EnableKernelCacheForVaryByStar
		{
			get
			{
				if (!this.enableKernelCacheForVaryByStarCached)
				{
					this.enableKernelCacheForVaryByStar = (bool)base[OutputCacheSection._propEnableKernelCacheForVaryByStar];
					this.enableKernelCacheForVaryByStarCached = true;
				}
				return this.enableKernelCacheForVaryByStar;
			}
			set
			{
				base[OutputCacheSection._propEnableKernelCacheForVaryByStar] = value;
				this.enableKernelCacheForVaryByStar = value;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x00133AE0 File Offset: 0x00131CE0
		// (set) Token: 0x060057EC RID: 22508 RVA: 0x00133AF2 File Offset: 0x00131CF2
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetInternalProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProviderName
		{
			get
			{
				return (string)base[OutputCacheSection._propDefaultProviderName];
			}
			set
			{
				base[OutputCacheSection._propDefaultProviderName] = value;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x00133B00 File Offset: 0x00131D00
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[OutputCacheSection._propProviders];
			}
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x00133B14 File Offset: 0x00131D14
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal OutputCacheProviderCollection CreateProviderCollection()
		{
			ProviderSettingsCollection providers = this.Providers;
			if (providers == null || providers.Count == 0)
			{
				return null;
			}
			OutputCacheProviderCollection outputCacheProviderCollection = new OutputCacheProviderCollection();
			ProvidersHelper.InstantiateProviders(providers, outputCacheProviderCollection, typeof(OutputCacheProvider));
			outputCacheProviderCollection.SetReadOnly();
			return outputCacheProviderCollection;
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x00133B54 File Offset: 0x00131D54
		internal OutputCacheProvider GetDefaultProvider(OutputCacheProviderCollection providers)
		{
			string defaultProviderName = this.DefaultProviderName;
			if (defaultProviderName == "AspNetInternalProvider")
			{
				return null;
			}
			OutputCacheProvider outputCacheProvider = (providers == null) ? null : providers[defaultProviderName];
			if (outputCacheProvider == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Def_provider_not_found"), base.ElementInformation.Properties["defaultProvider"].Source, base.ElementInformation.Properties["defaultProvider"].LineNumber);
			}
			return outputCacheProvider;
		}

		// Token: 0x04002EA6 RID: 11942
		internal const bool DefaultOmitVaryStar = false;

		// Token: 0x04002EA7 RID: 11943
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002EA8 RID: 11944
		private static readonly ConfigurationProperty _propEnableOutputCache = new ConfigurationProperty("enableOutputCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EA9 RID: 11945
		private static readonly ConfigurationProperty _propEnableFragmentCache = new ConfigurationProperty("enableFragmentCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAA RID: 11946
		private static readonly ConfigurationProperty _propSendCacheControlHeader = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAB RID: 11947
		private static readonly ConfigurationProperty _propOmitVaryStar = new ConfigurationProperty("omitVaryStar", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAC RID: 11948
		private static readonly ConfigurationProperty _propEnableKernelCacheForVaryByStar = new ConfigurationProperty("enableKernelCacheForVaryByStar", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAD RID: 11949
		private static readonly ConfigurationProperty _propDefaultProviderName = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetInternalProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAE RID: 11950
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002EAF RID: 11951
		private bool sendCacheControlHeaderCached;

		// Token: 0x04002EB0 RID: 11952
		private bool sendCacheControlHeaderCache;

		// Token: 0x04002EB1 RID: 11953
		private bool omitVaryStarCached;

		// Token: 0x04002EB2 RID: 11954
		private bool omitVaryStar;

		// Token: 0x04002EB3 RID: 11955
		private bool enableKernelCacheForVaryByStarCached;

		// Token: 0x04002EB4 RID: 11956
		private bool enableKernelCacheForVaryByStar;

		// Token: 0x04002EB5 RID: 11957
		private bool enableOutputCacheCached;

		// Token: 0x04002EB6 RID: 11958
		private bool enableOutputCache;
	}
}
