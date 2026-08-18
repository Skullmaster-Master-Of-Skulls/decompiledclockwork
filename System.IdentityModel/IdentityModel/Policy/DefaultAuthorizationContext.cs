using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001B7 RID: 439
	internal class DefaultAuthorizationContext : AuthorizationContext
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x000416C2 File Offset: 0x0003F8C2
		public DefaultAuthorizationContext(DefaultEvaluationContext evaluationContext)
		{
			this.claimSets = evaluationContext.ClaimSets;
			this.expirationTime = evaluationContext.ExpirationTime;
			this.properties = evaluationContext.Properties;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000416EE File Offset: 0x0003F8EE
		public static DefaultAuthorizationContext Empty
		{
			get
			{
				if (LocalAppContextSwitches.EnableCachedEmptyDefaultAuthorizationContext)
				{
					if (DefaultAuthorizationContext.empty == null)
					{
						DefaultAuthorizationContext.empty = new DefaultAuthorizationContext(new DefaultEvaluationContext());
					}
					return DefaultAuthorizationContext.empty;
				}
				return new DefaultAuthorizationContext(new DefaultEvaluationContext());
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0004171D File Offset: 0x0003F91D
		public override string Id
		{
			get
			{
				if (this.id == null)
				{
					this.id = SecurityUniqueId.Create();
				}
				return this.id.Value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0004173D File Offset: 0x0003F93D
		public override ReadOnlyCollection<ClaimSet> ClaimSets
		{
			get
			{
				return this.claimSets;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00041745 File Offset: 0x0003F945
		public override DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0004174D File Offset: 0x0003F94D
		public override IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000CFD RID: 3325
		private static DefaultAuthorizationContext empty;

		// Token: 0x04000CFE RID: 3326
		private SecurityUniqueId id;

		// Token: 0x04000CFF RID: 3327
		private ReadOnlyCollection<ClaimSet> claimSets;

		// Token: 0x04000D00 RID: 3328
		private DateTime expirationTime;

		// Token: 0x04000D01 RID: 3329
		private IDictionary<string, object> properties;
	}
}
