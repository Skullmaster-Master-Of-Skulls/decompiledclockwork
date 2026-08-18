using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000635 RID: 1589
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroup
	{
		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x0013DE04 File Offset: 0x0013CE04
		// (set) Token: 0x06004E93 RID: 20115 RVA: 0x0013DE0C File Offset: 0x0013CE0C
		[TemplateContainer(typeof(LoginView))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x0013DE15 File Offset: 0x0013CE15
		// (set) Token: 0x06004E95 RID: 20117 RVA: 0x0013DE36 File Offset: 0x0013CE36
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Roles
		{
			get
			{
				if (this._roles == null)
				{
					return new string[0];
				}
				return (string[])this._roles.Clone();
			}
			set
			{
				if (value == null)
				{
					this._roles = value;
					return;
				}
				this._roles = (string[])value.Clone();
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0013DE54 File Offset: 0x0013CE54
		public bool ContainsUser(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (this._roles == null)
			{
				return false;
			}
			foreach (string role in this._roles)
			{
				if (user.IsInRole(role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0013DEA4 File Offset: 0x0013CEA4
		public override string ToString()
		{
			StringArrayConverter stringArrayConverter = new StringArrayConverter();
			return stringArrayConverter.ConvertToString(this.Roles);
		}

		// Token: 0x04002CA5 RID: 11429
		private ITemplate _contentTemplate;

		// Token: 0x04002CA6 RID: 11430
		private string[] _roles;
	}
}
