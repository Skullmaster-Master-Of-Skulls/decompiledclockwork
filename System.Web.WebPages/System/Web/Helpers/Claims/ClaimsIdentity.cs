using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace System.Web.Helpers.Claims
{
	// Token: 0x02000028 RID: 40
	internal abstract class ClaimsIdentity
	{
		// Token: 0x0600011D RID: 285
		public abstract IEnumerable<Claim> GetClaims();

		// Token: 0x0600011E RID: 286 RVA: 0x000048D0 File Offset: 0x00002AD0
		internal static ClaimsIdentity TryConvert<TClaimsIdentity, TClaim>(IIdentity identity) where TClaimsIdentity : class, IIdentity
		{
			TClaimsIdentity tclaimsIdentity = identity as TClaimsIdentity;
			if (tclaimsIdentity == null)
			{
				return null;
			}
			return new ClaimsIdentity.ClaimsIdentityImpl<TClaimsIdentity, TClaim>(tclaimsIdentity);
		}

		// Token: 0x02000029 RID: 41
		private sealed class ClaimsIdentityImpl<TClaimsIdentity, TClaim> : ClaimsIdentity where TClaimsIdentity : class, IIdentity
		{
			// Token: 0x06000120 RID: 288 RVA: 0x00004901 File Offset: 0x00002B01
			public ClaimsIdentityImpl(TClaimsIdentity claimsIdentity)
			{
				this._claimsIdentity = claimsIdentity;
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00004910 File Offset: 0x00002B10
			private static Func<TClaimsIdentity, IEnumerable<TClaim>> CreateClaimsGetter()
			{
				PropertyInfo property = typeof(TClaimsIdentity).GetProperty("Claims", BindingFlags.Instance | BindingFlags.Public);
				MethodInfo getMethod = property.GetGetMethod();
				return (Func<TClaimsIdentity, IEnumerable<TClaim>>)Delegate.CreateDelegate(typeof(Func<TClaimsIdentity, IEnumerable<TClaim>>), getMethod);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00004950 File Offset: 0x00002B50
			public override IEnumerable<Claim> GetClaims()
			{
				return ClaimsIdentity.ClaimsIdentityImpl<TClaimsIdentity, TClaim>._claimsGetter(this._claimsIdentity).Select(new Func<TClaim, Claim>(Claim.Create<TClaim>));
			}

			// Token: 0x0400005D RID: 93
			private static readonly Func<TClaimsIdentity, IEnumerable<TClaim>> _claimsGetter = ClaimsIdentity.ClaimsIdentityImpl<TClaimsIdentity, TClaim>.CreateClaimsGetter();

			// Token: 0x0400005E RID: 94
			private readonly TClaimsIdentity _claimsIdentity;
		}
	}
}
