using System;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;

namespace System.Web.Configuration
{
	// Token: 0x020006A2 RID: 1698
	[ConfigurationCollection(typeof(AuthorizationRule), AddItemName = "allow,deny", CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
	public sealed class AuthorizationRuleCollection : ConfigurationElementCollection
	{
		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x00118C14 File Offset: 0x00116E14
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRuleCollection._properties;
			}
		}

		// Token: 0x1700176A RID: 5994
		public AuthorizationRule this[int index]
		{
			get
			{
				return (AuthorizationRule)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00118C29 File Offset: 0x00116E29
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthorizationRule();
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x00118C30 File Offset: 0x00116E30
		protected override ConfigurationElement CreateNewElement(string elementName)
		{
			AuthorizationRule authorizationRule = new AuthorizationRule();
			string a = elementName.ToLower(CultureInfo.InvariantCulture);
			if (!(a == "allow"))
			{
				if (a == "deny")
				{
					authorizationRule.Action = AuthorizationRuleAction.Deny;
				}
			}
			else
			{
				authorizationRule.Action = AuthorizationRuleAction.Allow;
			}
			return authorizationRule;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00118C7C File Offset: 0x00116E7C
		protected override object GetElementKey(ConfigurationElement element)
		{
			AuthorizationRule authorizationRule = (AuthorizationRule)element;
			return authorizationRule._ActionString;
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x00028752 File Offset: 0x00026952
		protected override string ElementName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x00118C96 File Offset: 0x00116E96
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x00118C9C File Offset: 0x00116E9C
		protected override bool IsElementName(string elementname)
		{
			bool result = false;
			string a = elementname.ToLower(CultureInfo.InvariantCulture);
			if (a == "allow" || a == "deny")
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x00118CD4 File Offset: 0x00116ED4
		internal bool IsUserAllowed(IPrincipal user, string verb)
		{
			if (user == null)
			{
				return false;
			}
			if (!this._fCheckForCommonCasesDone)
			{
				this.DoCheckForCommonCases();
				this._fCheckForCommonCasesDone = true;
			}
			if (!user.Identity.IsAuthenticated && this._iAnonymousAllowed != 0)
			{
				return this._iAnonymousAllowed > 0;
			}
			if (this._iAllUsersAllowed != 0)
			{
				return this._iAllUsersAllowed > 0;
			}
			foreach (object obj in this)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				int num = authorizationRule.IsUserAllowed(user, verb);
				if (num != 0)
				{
					return num > 0;
				}
			}
			return false;
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00118D88 File Offset: 0x00116F88
		private void DoCheckForCommonCases()
		{
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in this)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				if (authorizationRule.Everyone)
				{
					if (!flag2 && authorizationRule.Action == AuthorizationRuleAction.Deny)
					{
						this._iAllUsersAllowed = -1;
					}
					if (!flag3 && authorizationRule.Action == AuthorizationRuleAction.Allow)
					{
						this._iAllUsersAllowed = 1;
					}
					break;
				}
				if (flag && authorizationRule.IncludesAnonymous)
				{
					if (!flag2 && authorizationRule.Action == AuthorizationRuleAction.Deny)
					{
						this._iAnonymousAllowed = -1;
					}
					if (!flag3 && authorizationRule.Action == AuthorizationRuleAction.Allow)
					{
						this._iAnonymousAllowed = 1;
					}
					flag = false;
				}
				if (!flag2 && authorizationRule.Action == AuthorizationRuleAction.Allow)
				{
					flag2 = true;
				}
				if (!flag3 && authorizationRule.Action == AuthorizationRuleAction.Deny)
				{
					flag3 = true;
				}
				if (!flag && flag2 && flag3)
				{
					break;
				}
			}
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00118E78 File Offset: 0x00117078
		public void Add(AuthorizationRule rule)
		{
			this.BaseAdd(-1, rule);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00118C1B File Offset: 0x00116E1B
		public AuthorizationRule Get(int index)
		{
			return (AuthorizationRule)base.BaseGet(index);
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00118E82 File Offset: 0x00117082
		public void Set(int index, AuthorizationRule rule)
		{
			this.BaseAdd(index, rule);
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00118E8C File Offset: 0x0011708C
		public int IndexOf(AuthorizationRule rule)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (object.Equals(this.Get(i), rule))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00118EBC File Offset: 0x001170BC
		public void Remove(AuthorizationRule rule)
		{
			int num = this.IndexOf(rule);
			if (num >= 0)
			{
				base.BaseRemoveAt(num);
			}
		}

		// Token: 0x04002B2B RID: 11051
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002B2C RID: 11052
		private int _iAllUsersAllowed;

		// Token: 0x04002B2D RID: 11053
		private int _iAnonymousAllowed;

		// Token: 0x04002B2E RID: 11054
		private bool _fCheckForCommonCasesDone;
	}
}
