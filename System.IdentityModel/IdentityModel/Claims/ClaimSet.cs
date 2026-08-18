using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DA RID: 474
	[DataContract(Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity")]
	public abstract class ClaimSet : IEnumerable<Claim>, IEnumerable
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00044894 File Offset: 0x00042A94
		public static ClaimSet System
		{
			get
			{
				if (ClaimSet.system == null)
				{
					ClaimSet.system = new DefaultClaimSet(new List<Claim>(2)
					{
						Claim.System,
						new Claim(ClaimTypes.System, "System", Rights.PossessProperty)
					});
				}
				return ClaimSet.system;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000448E4 File Offset: 0x00042AE4
		public static ClaimSet Windows
		{
			get
			{
				if (ClaimSet.windows == null)
				{
					List<Claim> list = new List<Claim>(2);
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.NTAuthoritySid, null);
					list.Add(new Claim(ClaimTypes.Sid, securityIdentifier, Rights.Identity));
					list.Add(Claim.CreateWindowsSidClaim(securityIdentifier));
					ClaimSet.windows = new DefaultClaimSet(list);
				}
				return ClaimSet.windows;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00044939 File Offset: 0x00042B39
		internal static ClaimSet Anonymous
		{
			get
			{
				if (ClaimSet.anonymous == null)
				{
					ClaimSet.anonymous = new DefaultClaimSet(new Claim[0]);
				}
				return ClaimSet.anonymous;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00044957 File Offset: 0x00042B57
		internal static bool SupportedRight(string right)
		{
			return right == null || Rights.Identity.Equals(right) || Rights.PossessProperty.Equals(right);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00044978 File Offset: 0x00042B78
		public virtual bool ContainsClaim(Claim claim, IEqualityComparer<Claim> comparer)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			if (comparer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("comparer");
			}
			IEnumerable<Claim> enumerable = this.FindClaims(null, null);
			if (enumerable != null)
			{
				foreach (Claim y in enumerable)
				{
					if (comparer.Equals(claim, y))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000449FC File Offset: 0x00042BFC
		public virtual bool ContainsClaim(Claim claim)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			IEnumerable<Claim> enumerable = this.FindClaims(claim.ClaimType, claim.Right);
			if (enumerable != null)
			{
				foreach (Claim obj in enumerable)
				{
					if (claim.Equals(obj))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x1700040B RID: 1035
		public abstract Claim this[int index]
		{
			get;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F99 RID: 3993
		public abstract int Count { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F9A RID: 3994
		public abstract ClaimSet Issuer { get; }

		// Token: 0x06000F9B RID: 3995
		public abstract IEnumerable<Claim> FindClaims(string claimType, string right);

		// Token: 0x06000F9C RID: 3996
		public abstract IEnumerator<Claim> GetEnumerator();

		// Token: 0x06000F9D RID: 3997 RVA: 0x00044A78 File Offset: 0x00042C78
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000DA2 RID: 3490
		private static ClaimSet system;

		// Token: 0x04000DA3 RID: 3491
		private static ClaimSet windows;

		// Token: 0x04000DA4 RID: 3492
		private static ClaimSet anonymous;
	}
}
