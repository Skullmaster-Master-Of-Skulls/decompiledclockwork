using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200026E RID: 622
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartsPersonalization : ConfigurationElement
	{
		// Token: 0x060020A2 RID: 8354 RVA: 0x0008E158 File Offset: 0x0008D158
		static WebPartsPersonalization()
		{
			WebPartsPersonalization._properties = new ConfigurationPropertyCollection();
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propDefaultProvider);
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propProviders);
			WebPartsPersonalization._properties.Add(WebPartsPersonalization._propAuthorization);
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0008E1FF File Offset: 0x0008D1FF
		[ConfigurationProperty("authorization")]
		public WebPartsPersonalizationAuthorization Authorization
		{
			get
			{
				return (WebPartsPersonalizationAuthorization)base[WebPartsPersonalization._propAuthorization];
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0008E211 File Offset: 0x0008D211
		// (set) Token: 0x060020A6 RID: 8358 RVA: 0x0008E223 File Offset: 0x0008D223
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

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0008E231 File Offset: 0x0008D231
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalization._properties;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0008E238 File Offset: 0x0008D238
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[WebPartsPersonalization._propProviders];
			}
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0008E24C File Offset: 0x0008D24C
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

		// Token: 0x04001AB2 RID: 6834
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04001AB3 RID: 6835
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlPersonalizationProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04001AB4 RID: 6836
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001AB5 RID: 6837
		private static readonly ConfigurationProperty _propAuthorization = new ConfigurationProperty("authorization", typeof(WebPartsPersonalizationAuthorization), null, ConfigurationPropertyOptions.None);
	}
}
