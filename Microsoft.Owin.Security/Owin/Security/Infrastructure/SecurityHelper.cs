using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000030 RID: 48
	public struct SecurityHelper
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000448F File Offset: 0x0000268F
		public SecurityHelper(IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._context = context;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000044A8 File Offset: 0x000026A8
		public void AddUserIdentity(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
			IPrincipal user = this._context.Request.User;
			if (user != null)
			{
				ClaimsPrincipal claimsPrincipal2 = user as ClaimsPrincipal;
				if (claimsPrincipal2 == null)
				{
					IIdentity identity2 = user.Identity;
					if (identity2.IsAuthenticated)
					{
						claimsPrincipal.AddIdentity((identity2 as ClaimsIdentity) ?? new ClaimsIdentity(identity2));
					}
				}
				else
				{
					foreach (ClaimsIdentity claimsIdentity in claimsPrincipal2.Identities)
					{
						if (claimsIdentity.IsAuthenticated)
						{
							claimsPrincipal.AddIdentity(claimsIdentity);
						}
					}
				}
			}
			this._context.Request.User = claimsPrincipal;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004570 File Offset: 0x00002770
		public AuthenticationResponseChallenge LookupChallenge(string authenticationType, AuthenticationMode authenticationMode)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseChallenge authenticationResponseChallenge = this._context.Authentication.AuthenticationResponseChallenge;
			if (authenticationResponseChallenge != null && authenticationResponseChallenge.AuthenticationTypes != null && authenticationResponseChallenge.AuthenticationTypes.Length != 0)
			{
				foreach (string a in authenticationResponseChallenge.AuthenticationTypes)
				{
					if (string.Equals(a, authenticationType, StringComparison.Ordinal))
					{
						return authenticationResponseChallenge;
					}
				}
				return null;
			}
			if (authenticationMode != AuthenticationMode.Active)
			{
				return null;
			}
			return authenticationResponseChallenge ?? new AuthenticationResponseChallenge(null, null);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004600 File Offset: 0x00002800
		public AuthenticationResponseGrant LookupSignIn(string authenticationType)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseGrant authenticationResponseGrant = this._context.Authentication.AuthenticationResponseGrant;
			if (authenticationResponseGrant == null)
			{
				return null;
			}
			foreach (ClaimsIdentity claimsIdentity in authenticationResponseGrant.Principal.Identities)
			{
				if (string.Equals(authenticationType, claimsIdentity.AuthenticationType, StringComparison.Ordinal))
				{
					return new AuthenticationResponseGrant(claimsIdentity, authenticationResponseGrant.Properties ?? new AuthenticationProperties());
				}
			}
			return null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000469C File Offset: 0x0000289C
		public AuthenticationResponseRevoke LookupSignOut(string authenticationType, AuthenticationMode authenticationMode)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseRevoke authenticationResponseRevoke = this._context.Authentication.AuthenticationResponseRevoke;
			if (authenticationResponseRevoke == null)
			{
				return null;
			}
			if (authenticationResponseRevoke.AuthenticationTypes != null && authenticationResponseRevoke.AuthenticationTypes.Length != 0)
			{
				for (int num = 0; num != authenticationResponseRevoke.AuthenticationTypes.Length; num++)
				{
					if (string.Equals(authenticationType, authenticationResponseRevoke.AuthenticationTypes[num], StringComparison.Ordinal))
					{
						return authenticationResponseRevoke;
					}
				}
				return null;
			}
			if (authenticationMode != AuthenticationMode.Active)
			{
				return null;
			}
			return authenticationResponseRevoke;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000470D File Offset: 0x0000290D
		public bool Equals(SecurityHelper other)
		{
			return object.Equals(this._context, other._context);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004721 File Offset: 0x00002921
		public override bool Equals(object obj)
		{
			return obj is SecurityHelper && this.Equals((SecurityHelper)obj);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004739 File Offset: 0x00002939
		public override int GetHashCode()
		{
			if (this._context == null)
			{
				return 0;
			}
			return this._context.GetHashCode();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004750 File Offset: 0x00002950
		public static bool operator ==(SecurityHelper left, SecurityHelper right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000475A File Offset: 0x0000295A
		public static bool operator !=(SecurityHelper left, SecurityHelper right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400004B RID: 75
		private readonly IOwinContext _context;
	}
}
