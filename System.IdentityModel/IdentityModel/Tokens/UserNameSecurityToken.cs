using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000187 RID: 391
	public class UserNameSecurityToken : SecurityToken
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x0003B903 File Offset: 0x00039B03
		public UserNameSecurityToken(string userName, string password) : this(userName, password, SecurityUniqueId.Create().Value)
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0003B918 File Offset: 0x00039B18
		public UserNameSecurityToken(string userName, string password, string id)
		{
			if (userName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("userName");
			}
			if (userName == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("UserNameCannotBeEmpty"));
			}
			this.userName = userName;
			this.password = password;
			this.id = id;
			this.effectiveTime = DateTime.UtcNow;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0003B980 File Offset: 0x00039B80
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0003B988 File Offset: 0x00039B88
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return EmptyReadOnlyCollection<SecurityKey>.Instance;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0003B98F File Offset: 0x00039B8F
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00023CAC File Offset: 0x00021EAC
		public override DateTime ValidTo
		{
			get
			{
				return SecurityUtils.MaxUtcDateTime;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0003B997 File Offset: 0x00039B97
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003B99F File Offset: 0x00039B9F
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x04000C98 RID: 3224
		private string id;

		// Token: 0x04000C99 RID: 3225
		private string password;

		// Token: 0x04000C9A RID: 3226
		private string userName;

		// Token: 0x04000C9B RID: 3227
		private DateTime effectiveTime;
	}
}
