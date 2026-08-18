using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006A0 RID: 1696
	public sealed class AuthorizationRule : ConfigurationElement
	{
		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x0011814B File Offset: 0x0011634B
		internal bool Everyone
		{
			get
			{
				return this._Everyone;
			}
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x00118154 File Offset: 0x00116354
		static AuthorizationRule()
		{
			AuthorizationRule._properties = new ConfigurationPropertyCollection();
			AuthorizationRule._properties.Add(AuthorizationRule._propVerbs);
			AuthorizationRule._properties.Add(AuthorizationRule._propUsers);
			AuthorizationRule._properties.Add(AuthorizationRule._propRoles);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00118208 File Offset: 0x00116408
		internal AuthorizationRule()
		{
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x00118253 File Offset: 0x00116453
		public AuthorizationRule(AuthorizationRuleAction action) : this()
		{
			this.Action = action;
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x00118262 File Offset: 0x00116462
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRule._properties;
			}
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0011826C File Offset: 0x0011646C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			AuthorizationRule authorizationRule = parentElement as AuthorizationRule;
			AuthorizationRule authorizationRule2 = sourceElement as AuthorizationRule;
			if (authorizationRule != null)
			{
				authorizationRule.UpdateUsersRolesVerbs();
			}
			if (authorizationRule2 != null)
			{
				authorizationRule2.UpdateUsersRolesVerbs();
			}
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x001182A4 File Offset: 0x001164A4
		protected override void Reset(ConfigurationElement parentElement)
		{
			AuthorizationRule authorizationRule = parentElement as AuthorizationRule;
			if (authorizationRule != null)
			{
				authorizationRule.UpdateUsersRolesVerbs();
			}
			base.Reset(parentElement);
			this.EvaluateData();
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x001182CE File Offset: 0x001164CE
		internal void AddRole(string role)
		{
			if (!string.IsNullOrEmpty(role))
			{
				role = role.ToLower(CultureInfo.InvariantCulture);
			}
			this.Roles.Add(role);
			this.RolesExpanded.Add(this.ExpandName(role));
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x00118305 File Offset: 0x00116505
		internal void AddUser(string user)
		{
			if (!string.IsNullOrEmpty(user))
			{
				user = user.ToLower(CultureInfo.InvariantCulture);
			}
			this.Users.Add(user);
			this.UsersExpanded.Add(this.ExpandName(user));
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x0011833C File Offset: 0x0011653C
		private void UpdateUsersRolesVerbs()
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = (CommaDelimitedStringCollection)this.Roles;
			CommaDelimitedStringCollection commaDelimitedStringCollection2 = (CommaDelimitedStringCollection)this.Users;
			CommaDelimitedStringCollection commaDelimitedStringCollection3 = (CommaDelimitedStringCollection)this.Verbs;
			if (commaDelimitedStringCollection.IsModified)
			{
				this._RolesExpanded = null;
				base[AuthorizationRule._propRoles] = commaDelimitedStringCollection;
			}
			if (commaDelimitedStringCollection2.IsModified)
			{
				this._UsersExpanded = null;
				base[AuthorizationRule._propUsers] = commaDelimitedStringCollection2;
			}
			if (commaDelimitedStringCollection3.IsModified)
			{
				base[AuthorizationRule._propVerbs] = commaDelimitedStringCollection3;
			}
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x001183B8 File Offset: 0x001165B8
		protected override bool IsModified()
		{
			this.UpdateUsersRolesVerbs();
			return this._ActionModified || base.IsModified() || ((CommaDelimitedStringCollection)this.Users).IsModified || ((CommaDelimitedStringCollection)this.Roles).IsModified || ((CommaDelimitedStringCollection)this.Verbs).IsModified;
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00118411 File Offset: 0x00116611
		protected override void ResetModified()
		{
			this._ActionModified = false;
			base.ResetModified();
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00118420 File Offset: 0x00116620
		public override bool Equals(object obj)
		{
			AuthorizationRule authorizationRule = obj as AuthorizationRule;
			bool result = false;
			if (authorizationRule != null)
			{
				this.UpdateUsersRolesVerbs();
				result = (authorizationRule.Verbs.ToString() == this.Verbs.ToString() && authorizationRule.Roles.ToString() == this.Roles.ToString() && authorizationRule.Users.ToString() == this.Users.ToString() && authorizationRule.Action == this.Action);
			}
			return result;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x001184AC File Offset: 0x001166AC
		public override int GetHashCode()
		{
			string text = this.Verbs.ToString();
			string text2 = this.Roles.ToString();
			string text3 = this.Users.ToString();
			if (text == null)
			{
				text = string.Empty;
			}
			if (text2 == null)
			{
				text2 = string.Empty;
			}
			if (text3 == null)
			{
				text3 = string.Empty;
			}
			return HashCodeCombiner.CombineHashCodes(text.GetHashCode(), text2.GetHashCode(), text3.GetHashCode(), (int)this.Action);
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00118517 File Offset: 0x00116717
		protected override void SetReadOnly()
		{
			((CommaDelimitedStringCollection)this.Users).SetReadOnly();
			((CommaDelimitedStringCollection)this.Roles).SetReadOnly();
			((CommaDelimitedStringCollection)this.Verbs).SetReadOnly();
			base.SetReadOnly();
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0011854F File Offset: 0x0011674F
		// (set) Token: 0x06005181 RID: 20865 RVA: 0x00118558 File Offset: 0x00116758
		public AuthorizationRuleAction Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._ElementName = value.ToString().ToLower(CultureInfo.InvariantCulture);
				this._Action = value;
				this._ActionString = this._Action.ToString();
				this._ActionModified = true;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x001185A8 File Offset: 0x001167A8
		[ConfigurationProperty("verbs")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Verbs
		{
			get
			{
				if (this._Verbs == null)
				{
					CommaDelimitedStringCollection commaDelimitedStringCollection = (CommaDelimitedStringCollection)base[AuthorizationRule._propVerbs];
					if (commaDelimitedStringCollection == null)
					{
						this._Verbs = new CommaDelimitedStringCollection();
					}
					else
					{
						this._Verbs = commaDelimitedStringCollection.Clone();
					}
				}
				return this._Verbs;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x001185F0 File Offset: 0x001167F0
		[ConfigurationProperty("users")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Users
		{
			get
			{
				if (this._Users == null)
				{
					CommaDelimitedStringCollection commaDelimitedStringCollection = (CommaDelimitedStringCollection)base[AuthorizationRule._propUsers];
					if (commaDelimitedStringCollection == null)
					{
						this._Users = new CommaDelimitedStringCollection();
					}
					else
					{
						this._Users = commaDelimitedStringCollection.Clone();
					}
					this._UsersExpanded = null;
				}
				return this._Users;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x00118640 File Offset: 0x00116840
		[ConfigurationProperty("roles")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Roles
		{
			get
			{
				if (this._Roles == null)
				{
					CommaDelimitedStringCollection commaDelimitedStringCollection = (CommaDelimitedStringCollection)base[AuthorizationRule._propRoles];
					if (commaDelimitedStringCollection == null)
					{
						this._Roles = new CommaDelimitedStringCollection();
					}
					else
					{
						this._Roles = commaDelimitedStringCollection.Clone();
					}
					this._RolesExpanded = null;
				}
				return this._Roles;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x0011868F File Offset: 0x0011688F
		internal StringCollection UsersExpanded
		{
			get
			{
				if (this._UsersExpanded == null)
				{
					this._UsersExpanded = this.CreateExpandedCollection(this.Users);
				}
				return this._UsersExpanded;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x001186B1 File Offset: 0x001168B1
		internal StringCollection RolesExpanded
		{
			get
			{
				if (this._RolesExpanded == null)
				{
					this._RolesExpanded = this.CreateExpandedCollection(this.Roles);
				}
				return this._RolesExpanded;
			}
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x001186D4 File Offset: 0x001168D4
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			bool flag = false;
			this.UpdateUsersRolesVerbs();
			if (base.SerializeElement(null, false))
			{
				if (writer != null)
				{
					writer.WriteStartElement(this._ElementName);
					flag |= base.SerializeElement(writer, false);
					writer.WriteEndElement();
				}
				else
				{
					flag |= base.SerializeElement(writer, false);
				}
			}
			return flag;
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x00118724 File Offset: 0x00116924
		private string ExpandName(string name)
		{
			string result = name;
			if (StringUtil.StringStartsWith(name, ".\\"))
			{
				if (this.machineName == null)
				{
					this.machineName = HttpServerUtility.GetMachineNameInternal().ToLower(CultureInfo.InvariantCulture);
				}
				result = this.machineName + name.Substring(1);
			}
			return result;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00118774 File Offset: 0x00116974
		private StringCollection CreateExpandedCollection(StringCollection collection)
		{
			StringCollection stringCollection = new StringCollection();
			foreach (string name in collection)
			{
				string value = this.ExpandName(name);
				stringCollection.Add(value);
			}
			return stringCollection;
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x001187D8 File Offset: 0x001169D8
		private void EvaluateData()
		{
			if (!this.DataReady)
			{
				if (this.Users.Count > 0)
				{
					foreach (string text in this.Users)
					{
						if (text.Length > 1)
						{
							int num = text.IndexOfAny(new char[]
							{
								'*',
								'?'
							});
							if (num >= 0)
							{
								throw new ConfigurationErrorsException(SR.GetString("Auth_rule_names_cant_contain_char", new object[]
								{
									text[num].ToString(CultureInfo.InvariantCulture)
								}));
							}
						}
						if (text.Equals("*"))
						{
							this._AllUsersSpecified = true;
						}
						if (text.Equals("?"))
						{
							this._AnonUserSpecified = true;
						}
					}
				}
				if (this.Roles.Count > 0)
				{
					foreach (string text2 in this.Roles)
					{
						if (text2.Length > 0)
						{
							int num2 = text2.IndexOfAny(new char[]
							{
								'*',
								'?'
							});
							if (num2 >= 0)
							{
								throw new ConfigurationErrorsException(SR.GetString("Auth_rule_names_cant_contain_char", new object[]
								{
									text2[num2].ToString(CultureInfo.InvariantCulture)
								}));
							}
						}
					}
				}
				this._Everyone = (this._AllUsersSpecified && this.Verbs.Count == 0);
				this._RolesExpanded = this.CreateExpandedCollection(this.Roles);
				this._UsersExpanded = this.CreateExpandedCollection(this.Users);
				if (this.Roles.Count == 0 && this.Users.Count == 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Auth_rule_must_specify_users_andor_roles"));
				}
				this.DataReady = true;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x001189E4 File Offset: 0x00116BE4
		internal bool IncludesAnonymous
		{
			get
			{
				this.EvaluateData();
				return this._AnonUserSpecified && this.Verbs.Count == 0;
			}
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00118A04 File Offset: 0x00116C04
		protected override void PreSerialize(XmlWriter writer)
		{
			this.EvaluateData();
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00118A04 File Offset: 0x00116C04
		protected override void PostDeserialize()
		{
			this.EvaluateData();
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00118A0C File Offset: 0x00116C0C
		internal int IsUserAllowed(IPrincipal user, string verb)
		{
			this.EvaluateData();
			int result = (this.Action == AuthorizationRuleAction.Allow) ? 1 : -1;
			if (this.Everyone)
			{
				return result;
			}
			if (!this.FindVerb(verb))
			{
				return 0;
			}
			if (this._AllUsersSpecified)
			{
				return result;
			}
			if (this._AnonUserSpecified && !user.Identity.IsAuthenticated)
			{
				return result;
			}
			StringCollection stringCollection;
			StringCollection stringCollection2;
			if (user.Identity is WindowsIdentity)
			{
				stringCollection = this.UsersExpanded;
				stringCollection2 = this.RolesExpanded;
			}
			else
			{
				stringCollection = this.Users;
				stringCollection2 = this.Roles;
			}
			if (stringCollection.Count > 0 && this.FindUser(stringCollection, user.Identity.Name))
			{
				return result;
			}
			if (stringCollection2.Count > 0 && this.IsTheUserInAnyRole(stringCollection2, user))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x00118AC4 File Offset: 0x00116CC4
		private bool FindVerb(string verb)
		{
			if (this.Verbs.Count < 1)
			{
				return true;
			}
			foreach (string a in this.Verbs)
			{
				if (string.Equals(a, verb, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00118B34 File Offset: 0x00116D34
		private bool FindUser(StringCollection users, string principal)
		{
			foreach (string a in users)
			{
				if (string.Equals(a, principal, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00118B90 File Offset: 0x00116D90
		private bool IsTheUserInAnyRole(StringCollection roles, IPrincipal principal)
		{
			if (!HttpRuntime.DisableProcessRequestInApplicationTrust && HttpRuntime.NamedPermissionSet != null && HttpRuntime.ProcessRequestInApplicationTrust)
			{
				HttpRuntime.NamedPermissionSet.PermitOnly();
			}
			foreach (string role in roles)
			{
				if (principal.IsInRole(role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002B12 RID: 11026
		private static readonly TypeConverter s_PropConverter = new CommaDelimitedStringCollectionConverter();

		// Token: 0x04002B13 RID: 11027
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B14 RID: 11028
		private static readonly ConfigurationProperty _propVerbs = new ConfigurationProperty("verbs", typeof(CommaDelimitedStringCollection), null, AuthorizationRule.s_PropConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B15 RID: 11029
		private static readonly ConfigurationProperty _propUsers = new ConfigurationProperty("users", typeof(CommaDelimitedStringCollection), null, AuthorizationRule.s_PropConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B16 RID: 11030
		private static readonly ConfigurationProperty _propRoles = new ConfigurationProperty("roles", typeof(CommaDelimitedStringCollection), null, AuthorizationRule.s_PropConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B17 RID: 11031
		private AuthorizationRuleAction _Action = AuthorizationRuleAction.Allow;

		// Token: 0x04002B18 RID: 11032
		internal string _ActionString = AuthorizationRuleAction.Allow.ToString();

		// Token: 0x04002B19 RID: 11033
		private string _ElementName = "allow";

		// Token: 0x04002B1A RID: 11034
		private CommaDelimitedStringCollection _Roles;

		// Token: 0x04002B1B RID: 11035
		private CommaDelimitedStringCollection _Verbs;

		// Token: 0x04002B1C RID: 11036
		private CommaDelimitedStringCollection _Users;

		// Token: 0x04002B1D RID: 11037
		private StringCollection _RolesExpanded;

		// Token: 0x04002B1E RID: 11038
		private StringCollection _UsersExpanded;

		// Token: 0x04002B1F RID: 11039
		private char[] _delimiters = new char[]
		{
			','
		};

		// Token: 0x04002B20 RID: 11040
		private string machineName;

		// Token: 0x04002B21 RID: 11041
		private const string _strAnonUserTag = "?";

		// Token: 0x04002B22 RID: 11042
		private const string _strAllUsersTag = "*";

		// Token: 0x04002B23 RID: 11043
		private bool _AllUsersSpecified;

		// Token: 0x04002B24 RID: 11044
		private bool _AnonUserSpecified;

		// Token: 0x04002B25 RID: 11045
		private bool DataReady;

		// Token: 0x04002B26 RID: 11046
		private bool _Everyone;

		// Token: 0x04002B27 RID: 11047
		private bool _ActionModified;
	}
}
