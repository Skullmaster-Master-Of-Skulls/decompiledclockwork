using System;
using System.ComponentModel;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004BC RID: 1212
	public sealed class RoleGroup
	{
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000C4398 File Offset: 0x000C2598
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000C43A0 File Offset: 0x000C25A0
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
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

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000C43A9 File Offset: 0x000C25A9
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000C43CA File Offset: 0x000C25CA
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

		// Token: 0x06003C81 RID: 15489 RVA: 0x000C43E8 File Offset: 0x000C25E8
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

		// Token: 0x06003C82 RID: 15490 RVA: 0x000C4434 File Offset: 0x000C2634
		public override string ToString()
		{
			StringArrayConverter stringArrayConverter = new StringArrayConverter();
			return stringArrayConverter.ConvertToString(this.Roles);
		}

		// Token: 0x04002387 RID: 9095
		private ITemplate _contentTemplate;

		// Token: 0x04002388 RID: 9096
		private string[] _roles;
	}
}
