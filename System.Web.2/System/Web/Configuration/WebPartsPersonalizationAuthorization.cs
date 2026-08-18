using System;
using System.Configuration;
using System.Security.Principal;

namespace System.Web.Configuration
{
	// Token: 0x0200077B RID: 1915
	public sealed class WebPartsPersonalizationAuthorization : ConfigurationElement
	{
		// Token: 0x06005C28 RID: 23592 RVA: 0x0013F2A8 File Offset: 0x0013D4A8
		static WebPartsPersonalizationAuthorization()
		{
			WebPartsPersonalizationAuthorization._properties = new ConfigurationPropertyCollection();
			WebPartsPersonalizationAuthorization._properties.Add(WebPartsPersonalizationAuthorization._propRules);
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x0013F2DA File Offset: 0x0013D4DA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalizationAuthorization._properties;
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x0013F2E1 File Offset: 0x0013D4E1
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[WebPartsPersonalizationAuthorization._propRules];
			}
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x0013F2F3 File Offset: 0x0013D4F3
		internal bool IsUserAllowed(IPrincipal user, string verb)
		{
			return this.Rules.IsUserAllowed(user, verb);
		}

		// Token: 0x04003079 RID: 12409
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400307A RID: 12410
		private static readonly ConfigurationProperty _propRules = new ConfigurationProperty(null, typeof(AuthorizationRuleCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
