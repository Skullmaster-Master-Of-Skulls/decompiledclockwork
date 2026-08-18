using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x0200073B RID: 1851
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class AspNetHostingPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003873 RID: 14451 RVA: 0x000EDFAD File Offset: 0x000ECFAD
		public AspNetHostingPermissionAttribute(SecurityAction action) : base(action)
		{
			this._level = AspNetHostingPermissionLevel.None;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000EDFBE File Offset: 0x000ECFBE
		// (set) Token: 0x06003875 RID: 14453 RVA: 0x000EDFC6 File Offset: 0x000ECFC6
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(value, "Level");
				this._level = value;
			}
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000EDFDA File Offset: 0x000ECFDA
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new AspNetHostingPermission(PermissionState.Unrestricted);
			}
			return new AspNetHostingPermission(this._level);
		}

		// Token: 0x04003258 RID: 12888
		private AspNetHostingPermissionLevel _level;
	}
}
