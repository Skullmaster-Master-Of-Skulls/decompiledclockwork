using System;
using System.Configuration;
using System.Security.Principal;

namespace System.Web.Configuration
{
	// Token: 0x020006A3 RID: 1699
	public sealed class AuthorizationSection : ConfigurationSection
	{
		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x00118EDC File Offset: 0x001170DC
		internal bool EveryoneAllowed
		{
			get
			{
				return this._EveryoneAllowed;
			}
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00118EE4 File Offset: 0x001170E4
		static AuthorizationSection()
		{
			AuthorizationSection._properties = new ConfigurationPropertyCollection();
			AuthorizationSection._properties.Add(AuthorizationSection._propRules);
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x00118F16 File Offset: 0x00117116
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationSection._properties;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x00118F1D File Offset: 0x0011711D
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[AuthorizationSection._propRules];
			}
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x00118F2F File Offset: 0x0011712F
		protected override void PostDeserialize()
		{
			if (this.Rules.Count > 0)
			{
				this._EveryoneAllowed = (this.Rules[0].Action == AuthorizationRuleAction.Allow && this.Rules[0].Everyone);
			}
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x00118F6D File Offset: 0x0011716D
		internal bool IsUserAllowed(IPrincipal user, string verb)
		{
			return this.Rules.IsUserAllowed(user, verb);
		}

		// Token: 0x04002B2F RID: 11055
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B30 RID: 11056
		private static readonly ConfigurationProperty _propRules = new ConfigurationProperty(null, typeof(AuthorizationRuleCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002B31 RID: 11057
		private bool _EveryoneAllowed;
	}
}
