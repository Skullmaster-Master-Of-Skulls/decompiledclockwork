using System;
using System.Configuration;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Configuration
{
	// Token: 0x0200026F RID: 623
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartsPersonalizationAuthorization : ConfigurationElement
	{
		// Token: 0x060020AA RID: 8362 RVA: 0x0008E3C4 File Offset: 0x0008D3C4
		static WebPartsPersonalizationAuthorization()
		{
			WebPartsPersonalizationAuthorization._properties = new ConfigurationPropertyCollection();
			WebPartsPersonalizationAuthorization._properties.Add(WebPartsPersonalizationAuthorization._propRules);
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0008E3F6 File Offset: 0x0008D3F6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalizationAuthorization._properties;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0008E3FD File Offset: 0x0008D3FD
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[WebPartsPersonalizationAuthorization._propRules];
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0008E40F File Offset: 0x0008D40F
		internal bool IsUserAllowed(IPrincipal user, string verb)
		{
			return this.Rules.IsUserAllowed(user, verb);
		}

		// Token: 0x04001AB6 RID: 6838
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04001AB7 RID: 6839
		private static readonly ConfigurationProperty _propRules = new ConfigurationProperty(null, typeof(AuthorizationRuleCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
