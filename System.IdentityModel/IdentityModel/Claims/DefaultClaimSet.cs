using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DC RID: 476
	[DataContract(Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity")]
	public class DefaultClaimSet : ClaimSet
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x00044B59 File Offset: 0x00042D59
		public DefaultClaimSet(params Claim[] claims)
		{
			this.Initialize(this, claims);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00044B59 File Offset: 0x00042D59
		public DefaultClaimSet(IList<Claim> claims)
		{
			this.Initialize(this, claims);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00044B69 File Offset: 0x00042D69
		public DefaultClaimSet(ClaimSet issuer, params Claim[] claims)
		{
			this.Initialize(issuer, claims);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00044B69 File Offset: 0x00042D69
		public DefaultClaimSet(ClaimSet issuer, IList<Claim> claims)
		{
			this.Initialize(issuer, claims);
		}

		// Token: 0x1700042D RID: 1069
		public override Claim this[int index]
		{
			get
			{
				return this.claims[index];
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00044B87 File Offset: 0x00042D87
		public override int Count
		{
			get
			{
				return this.claims.Count;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00044B94 File Offset: 0x00042D94
		public override ClaimSet Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00044B9C File Offset: 0x00042D9C
		public override bool ContainsClaim(Claim claim)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			for (int i = 0; i < this.claims.Count; i++)
			{
				if (claim.Equals(this.claims[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00044BE9 File Offset: 0x00042DE9
		public override IEnumerable<Claim> FindClaims(string claimType, string right)
		{
			bool anyClaimType = claimType == null;
			bool anyRight = right == null;
			int num;
			for (int i = 0; i < this.claims.Count; i = num)
			{
				Claim claim = this.claims[i];
				if (claim != null && (anyClaimType || claimType == claim.ClaimType) && (anyRight || right == claim.Right))
				{
					yield return claim;
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00044C07 File Offset: 0x00042E07
		public override IEnumerator<Claim> GetEnumerator()
		{
			return this.claims.GetEnumerator();
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00044C14 File Offset: 0x00042E14
		protected void Initialize(ClaimSet issuer, IList<Claim> claims)
		{
			if (issuer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuer");
			}
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claims");
			}
			this.issuer = issuer;
			this.claims = claims;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00044C4A File Offset: 0x00042E4A
		public override string ToString()
		{
			return SecurityUtils.ClaimSetToString(this);
		}

		// Token: 0x04000DC5 RID: 3525
		[DataMember(Name = "Issuer")]
		private ClaimSet issuer;

		// Token: 0x04000DC6 RID: 3526
		[DataMember(Name = "Claims")]
		private IList<Claim> claims;
	}
}
