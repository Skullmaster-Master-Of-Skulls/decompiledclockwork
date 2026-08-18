using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001B9 RID: 441
	public abstract class EvaluationContext
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E55 RID: 3669
		public abstract ReadOnlyCollection<ClaimSet> ClaimSets { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E56 RID: 3670
		public abstract IDictionary<string, object> Properties { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E57 RID: 3671
		public abstract int Generation { get; }

		// Token: 0x06000E58 RID: 3672
		public abstract void AddClaimSet(IAuthorizationPolicy policy, ClaimSet claimSet);

		// Token: 0x06000E59 RID: 3673
		public abstract void RecordExpirationTime(DateTime expirationTime);
	}
}
