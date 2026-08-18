using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000754 RID: 1876
	public sealed class SiteMapSection : ConfigurationSection
	{
		// Token: 0x06005A72 RID: 23154 RVA: 0x0013B2D8 File Offset: 0x001394D8
		static SiteMapSection()
		{
			SiteMapSection._properties = new ConfigurationPropertyCollection();
			SiteMapSection._properties.Add(SiteMapSection._propDefaultProvider);
			SiteMapSection._properties.Add(SiteMapSection._propEnabled);
			SiteMapSection._properties.Add(SiteMapSection._propProviders);
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06005A74 RID: 23156 RVA: 0x0013B37C File Offset: 0x0013957C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SiteMapSection._properties;
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06005A75 RID: 23157 RVA: 0x0013B383 File Offset: 0x00139583
		// (set) Token: 0x06005A76 RID: 23158 RVA: 0x0013B395 File Offset: 0x00139595
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetXmlSiteMapProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[SiteMapSection._propDefaultProvider];
			}
			set
			{
				base[SiteMapSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x0013B3A3 File Offset: 0x001395A3
		// (set) Token: 0x06005A78 RID: 23160 RVA: 0x0013B3B5 File Offset: 0x001395B5
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[SiteMapSection._propEnabled];
			}
			set
			{
				base[SiteMapSection._propEnabled] = value;
			}
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x0013B3C8 File Offset: 0x001395C8
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[SiteMapSection._propProviders];
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0013B3DC File Offset: 0x001395DC
		internal SiteMapProviderCollection ProvidersInternal
		{
			get
			{
				if (this._siteMapProviders == null)
				{
					lock (this)
					{
						if (this._siteMapProviders == null)
						{
							SiteMapProviderCollection siteMapProviderCollection = new SiteMapProviderCollection();
							ProvidersHelper.InstantiateProviders(this.Providers, siteMapProviderCollection, typeof(SiteMapProvider));
							this._siteMapProviders = siteMapProviderCollection;
						}
					}
				}
				return this._siteMapProviders;
			}
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0013B44C File Offset: 0x0013964C
		internal void ValidateDefaultProvider()
		{
			if (!string.IsNullOrEmpty(this.DefaultProvider) && this.Providers[this.DefaultProvider] == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_provider_must_exist", new object[]
				{
					this.DefaultProvider
				}), base.ElementInformation.Properties[SiteMapSection._propDefaultProvider.Name].Source, base.ElementInformation.Properties[SiteMapSection._propDefaultProvider.Name].LineNumber);
			}
		}

		// Token: 0x04002FEE RID: 12270
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002FEF RID: 12271
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetXmlSiteMapProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002FF0 RID: 12272
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002FF1 RID: 12273
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002FF2 RID: 12274
		private SiteMapProviderCollection _siteMapProviders;
	}
}
