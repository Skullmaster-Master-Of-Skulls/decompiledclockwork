using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.Security.Principal;

namespace System.IdentityModel.Policy
{
	// Token: 0x020001BD RID: 445
	internal class UnconditionalPolicy : IAuthorizationPolicy, IAuthorizationComponent, IDisposable
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00041828 File Offset: 0x0003FA28
		public UnconditionalPolicy(ClaimSet issuance) : this(issuance, SecurityUtils.MaxUtcDateTime)
		{
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00041836 File Offset: 0x0003FA36
		public UnconditionalPolicy(ClaimSet issuance, DateTime expirationTime)
		{
			if (issuance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuance");
			}
			this.Initialize(ClaimSet.System, issuance, null, expirationTime);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0004185F File Offset: 0x0003FA5F
		public UnconditionalPolicy(ReadOnlyCollection<ClaimSet> issuances, DateTime expirationTime)
		{
			if (issuances == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuances");
			}
			this.Initialize(ClaimSet.System, null, issuances, expirationTime);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00041888 File Offset: 0x0003FA88
		internal UnconditionalPolicy(IIdentity primaryIdentity, ClaimSet issuance) : this(issuance)
		{
			this.primaryIdentity = primaryIdentity;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00041898 File Offset: 0x0003FA98
		internal UnconditionalPolicy(IIdentity primaryIdentity, ClaimSet issuance, DateTime expirationTime) : this(issuance, expirationTime)
		{
			this.primaryIdentity = primaryIdentity;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000418A9 File Offset: 0x0003FAA9
		internal UnconditionalPolicy(IIdentity primaryIdentity, ReadOnlyCollection<ClaimSet> issuances, DateTime expirationTime) : this(issuances, expirationTime)
		{
			this.primaryIdentity = primaryIdentity;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000418BC File Offset: 0x0003FABC
		private UnconditionalPolicy(UnconditionalPolicy from)
		{
			this.disposable = from.disposable;
			this.primaryIdentity = (from.disposable ? SecurityUtils.CloneIdentityIfNecessary(from.primaryIdentity) : from.primaryIdentity);
			if (from.issuance != null)
			{
				this.issuance = (from.disposable ? SecurityUtils.CloneClaimSetIfNecessary(from.issuance) : from.issuance);
			}
			else
			{
				this.issuances = (from.disposable ? SecurityUtils.CloneClaimSetsIfNecessary(from.issuances) : from.issuances);
			}
			this.issuer = from.issuer;
			this.expirationTime = from.expirationTime;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00041960 File Offset: 0x0003FB60
		private void Initialize(ClaimSet issuer, ClaimSet issuance, ReadOnlyCollection<ClaimSet> issuances, DateTime expirationTime)
		{
			this.issuer = issuer;
			this.issuance = issuance;
			this.issuances = issuances;
			this.expirationTime = expirationTime;
			if (issuance != null)
			{
				this.disposable = (issuance is WindowsClaimSet);
				return;
			}
			for (int i = 0; i < issuances.Count; i++)
			{
				if (issuances[i] is WindowsClaimSet)
				{
					this.disposable = true;
					return;
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000419C4 File Offset: 0x0003FBC4
		public string Id
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000419E4 File Offset: 0x0003FBE4
		public ClaimSet Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000419EC File Offset: 0x0003FBEC
		internal IIdentity PrimaryIdentity
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.primaryIdentity == null)
				{
					IIdentity identity = null;
					if (this.issuance != null)
					{
						if (this.issuance is IIdentityInfo)
						{
							identity = ((IIdentityInfo)this.issuance).Identity;
						}
					}
					else
					{
						for (int i = 0; i < this.issuances.Count; i++)
						{
							ClaimSet claimSet = this.issuances[i];
							if (claimSet is IIdentityInfo)
							{
								identity = ((IIdentityInfo)claimSet).Identity;
								if (identity != null && identity != SecurityUtils.AnonymousIdentity)
								{
									break;
								}
							}
						}
					}
					this.primaryIdentity = (identity ?? SecurityUtils.AnonymousIdentity);
				}
				return this.primaryIdentity;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00041A8C File Offset: 0x0003FC8C
		internal ReadOnlyCollection<ClaimSet> Issuances
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.issuances == null)
				{
					this.issuances = new List<ClaimSet>(1)
					{
						this.issuance
					}.AsReadOnly();
				}
				return this.issuances;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00041ACC File Offset: 0x0003FCCC
		public DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00041AD4 File Offset: 0x0003FCD4
		internal bool IsDisposable
		{
			get
			{
				return this.disposable;
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00041ADC File Offset: 0x0003FCDC
		internal UnconditionalPolicy Clone()
		{
			this.ThrowIfDisposed();
			if (!this.disposable)
			{
				return this;
			}
			return new UnconditionalPolicy(this);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00041AF4 File Offset: 0x0003FCF4
		public virtual void Dispose()
		{
			if (this.disposable && !this.disposed)
			{
				this.disposed = true;
				SecurityUtils.DisposeIfNecessary(this.primaryIdentity as WindowsIdentity);
				SecurityUtils.DisposeClaimSetIfNecessary(this.issuance);
				SecurityUtils.DisposeClaimSetsIfNecessary(this.issuances);
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00041B33 File Offset: 0x0003FD33
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00041B58 File Offset: 0x0003FD58
		public virtual bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			this.ThrowIfDisposed();
			if (this.issuance != null)
			{
				evaluationContext.AddClaimSet(this, this.issuance);
			}
			else
			{
				for (int i = 0; i < this.issuances.Count; i++)
				{
					if (this.issuances[i] != null)
					{
						evaluationContext.AddClaimSet(this, this.issuances[i]);
					}
				}
			}
			if (this.PrimaryIdentity != null && this.PrimaryIdentity != SecurityUtils.AnonymousIdentity)
			{
				object obj;
				IList<IIdentity> list;
				if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
				{
					list = new List<IIdentity>(1);
					evaluationContext.Properties.Add("Identities", list);
				}
				else
				{
					list = (obj as IList<IIdentity>);
				}
				if (list != null)
				{
					list.Add(this.PrimaryIdentity);
				}
			}
			evaluationContext.RecordExpirationTime(this.expirationTime);
			return true;
		}

		// Token: 0x04000D07 RID: 3335
		private SecurityUniqueId id;

		// Token: 0x04000D08 RID: 3336
		private ClaimSet issuer;

		// Token: 0x04000D09 RID: 3337
		private ClaimSet issuance;

		// Token: 0x04000D0A RID: 3338
		private ReadOnlyCollection<ClaimSet> issuances;

		// Token: 0x04000D0B RID: 3339
		private DateTime expirationTime;

		// Token: 0x04000D0C RID: 3340
		private IIdentity primaryIdentity;

		// Token: 0x04000D0D RID: 3341
		private bool disposable;

		// Token: 0x04000D0E RID: 3342
		private bool disposed;
	}
}
