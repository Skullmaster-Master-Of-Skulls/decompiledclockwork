using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001B8 RID: 440
	internal class DefaultEvaluationContext : EvaluationContext
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00041755 File Offset: 0x0003F955
		public DefaultEvaluationContext()
		{
			this.properties = new Dictionary<string, object>();
			this.generation = 0;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0004177A File Offset: 0x0003F97A
		public override int Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00041782 File Offset: 0x0003F982
		public override ReadOnlyCollection<ClaimSet> ClaimSets
		{
			get
			{
				if (this.claimSets == null)
				{
					return EmptyReadOnlyCollection<ClaimSet>.Instance;
				}
				if (this.readOnlyClaimSets == null)
				{
					this.readOnlyClaimSets = new ReadOnlyCollection<ClaimSet>(this.claimSets);
				}
				return this.readOnlyClaimSets;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x000417B1 File Offset: 0x0003F9B1
		public override IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000417B9 File Offset: 0x0003F9B9
		public DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000417C4 File Offset: 0x0003F9C4
		public override void AddClaimSet(IAuthorizationPolicy policy, ClaimSet claimSet)
		{
			if (claimSet == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimSet");
			}
			if (this.claimSets == null)
			{
				this.claimSets = new List<ClaimSet>();
			}
			this.claimSets.Add(claimSet);
			this.generation++;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00041811 File Offset: 0x0003FA11
		public override void RecordExpirationTime(DateTime expirationTime)
		{
			if (this.expirationTime > expirationTime)
			{
				this.expirationTime = expirationTime;
			}
		}

		// Token: 0x04000D02 RID: 3330
		private List<ClaimSet> claimSets;

		// Token: 0x04000D03 RID: 3331
		private Dictionary<string, object> properties;

		// Token: 0x04000D04 RID: 3332
		private DateTime expirationTime = SecurityUtils.MaxUtcDateTime;

		// Token: 0x04000D05 RID: 3333
		private int generation;

		// Token: 0x04000D06 RID: 3334
		private ReadOnlyCollection<ClaimSet> readOnlyClaimSets;
	}
}
