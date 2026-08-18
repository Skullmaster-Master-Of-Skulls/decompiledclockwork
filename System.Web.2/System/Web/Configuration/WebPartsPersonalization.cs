using System;
using System.Collections.Specialized;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200077A RID: 1914
	public sealed class WebPartsPersonalization : ConfigurationElement
	{
		// Token: 0x06005C20 RID: 23584 RVA: 0x0013F048 File Offset: 0x0013D248
		static WebPartsPersonalization()
		{
			WebPartsPersonalization._properties = new ConfigurationPropertyCollection();
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propDefaultProvider);
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propProviders);
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propAuthorization);
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x0013F0E7 File Offset: 0x0013D2E7
		[ConfigurationProperty("authorization")]
		public WebPartsPersonalizationAuthorization Authorization
		{
			get
			{
				return (WebPartsPersonalizationAuthorization)base[WebPartsPersonalization._propAuthorization];
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06005C23 RID: 23587 RVA: 0x0013F0F9 File Offset: 0x0013D2F9
		// (set) Token: 0x06005C24 RID: 23588 RVA: 0x0013F10B File Offset: 0x0013D30B
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlPersonalizationProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[WebPartsPersonalization._propDefaultProvider];
			}
			set
			{
				base[WebPartsPersonalization._propDefaultProvider] = value;
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x0013F119 File Offset: 0x0013D319
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalization._properties;
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06005C26 RID: 23590 RVA: 0x0013F120 File Offset: 0x0013D320
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[WebPartsPersonalization._propProviders];
			}
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x0013F134 File Offset: 0x0013D334
		internal void ValidateAuthorization()
		{
			foreach (object obj in this.Authorization.Rules)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				StringCollection verbs = authorizationRule.Verbs;
				if (verbs.Count == 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("WebPartsSection_NoVerbs"), authorizationRule.ElementInformation.Properties["verbs"].Source, authorizationRule.ElementInformation.Properties["verbs"].LineNumber);
				}
				foreach (string text in verbs)
				{
					if (text != "enterSharedScope" && text != "modifyState")
					{
						throw new ConfigurationErrorsException(SR.GetString("WebPartsSection_InvalidVerb", new object[]
						{
							text
						}), authorizationRule.ElementInformation.Properties["verbs"].Source, authorizationRule.ElementInformation.Properties["verbs"].LineNumber);
					}
				}
			}
		}

		// Token: 0x04003075 RID: 12405
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003076 RID: 12406
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlPersonalizationProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04003077 RID: 12407
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04003078 RID: 12408
		private static readonly ConfigurationProperty _propAuthorization = new ConfigurationProperty("authorization", typeof(WebPartsPersonalizationAuthorization), null, ConfigurationPropertyOptions.None);
	}
}
