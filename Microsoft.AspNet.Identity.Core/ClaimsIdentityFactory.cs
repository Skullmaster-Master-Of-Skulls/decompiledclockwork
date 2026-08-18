using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000032 RID: 50
	public class ClaimsIdentityFactory<TUser, TKey> : IClaimsIdentityFactory<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000604D File Offset: 0x0000424D
		public ClaimsIdentityFactory()
		{
			this.RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
			this.UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.SecurityStampClaimType = "AspNet.Identity.SecurityStamp";
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006081 File Offset: 0x00004281
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006089 File Offset: 0x00004289
		public string RoleClaimType { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006092 File Offset: 0x00004292
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000609A File Offset: 0x0000429A
		public string UserNameClaimType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000060A3 File Offset: 0x000042A3
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000060AB File Offset: 0x000042AB
		public string UserIdClaimType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000060BC File Offset: 0x000042BC
		public string SecurityStampClaimType { get; set; }

		// Token: 0x060000DF RID: 223 RVA: 0x000064E0 File Offset: 0x000046E0
		public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TKey> manager, TUser user, string authenticationType)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			ClaimsIdentity id = new ClaimsIdentity(authenticationType, this.UserNameClaimType, this.RoleClaimType);
			id.AddClaim(new Claim(this.UserIdClaimType, this.ConvertIdToString(user.Id), "http://www.w3.org/2001/XMLSchema#string"));
			id.AddClaim(new Claim(this.UserNameClaimType, user.UserName, "http://www.w3.org/2001/XMLSchema#string"));
			id.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
			if (manager.SupportsUserSecurityStamp)
			{
				id.AddClaim(new Claim(this.SecurityStampClaimType, await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>()));
			}
			if (manager.SupportsUserRole)
			{
				IList<string> roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture<IList<string>>();
				foreach (string value in roles)
				{
					id.AddClaim(new Claim(this.RoleClaimType, value, "http://www.w3.org/2001/XMLSchema#string"));
				}
			}
			if (manager.SupportsUserClaim)
			{
				id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture<IList<Claim>>());
			}
			return id;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000653E File Offset: 0x0000473E
		public virtual string ConvertIdToString(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key.ToString();
		}

		// Token: 0x04000021 RID: 33
		internal const string IdentityProviderClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

		// Token: 0x04000022 RID: 34
		internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";
	}
}
