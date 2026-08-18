using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020001DA RID: 474
	public class PkixParameters
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0006AA8C File Offset: 0x00069A8C
		public PkixParameters(ISet trustAnchors)
		{
			this.SetTrustAnchors(trustAnchors);
			this.initialPolicies = new HashSet();
			this.certPathCheckers = new ArrayList();
			this.stores = new ArrayList();
			this.additionalStores = new ArrayList();
			this.trustedACIssuers = new HashSet();
			this.necessaryACAttributes = new HashSet();
			this.prohibitedACAttributes = new HashSet();
			this.attrCertCheckers = new HashSet();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0006AB0C File Offset: 0x00069B0C
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0006AB14 File Offset: 0x00069B14
		public virtual bool IsRevocationEnabled
		{
			get
			{
				return this.revocationEnabled;
			}
			set
			{
				this.revocationEnabled = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0006AB1D File Offset: 0x00069B1D
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x0006AB25 File Offset: 0x00069B25
		public virtual bool IsExplicitPolicyRequired
		{
			get
			{
				return this.explicitPolicyRequired;
			}
			set
			{
				this.explicitPolicyRequired = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0006AB2E File Offset: 0x00069B2E
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x0006AB36 File Offset: 0x00069B36
		public virtual bool IsAnyPolicyInhibited
		{
			get
			{
				return this.anyPolicyInhibited;
			}
			set
			{
				this.anyPolicyInhibited = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0006AB3F File Offset: 0x00069B3F
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x0006AB47 File Offset: 0x00069B47
		public virtual bool IsPolicyMappingInhibited
		{
			get
			{
				return this.policyMappingInhibited;
			}
			set
			{
				this.policyMappingInhibited = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0006AB50 File Offset: 0x00069B50
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x0006AB58 File Offset: 0x00069B58
		public virtual bool IsPolicyQualifiersRejected
		{
			get
			{
				return this.policyQualifiersRejected;
			}
			set
			{
				this.policyQualifiersRejected = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0006AB61 File Offset: 0x00069B61
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x0006AB69 File Offset: 0x00069B69
		public virtual DateTimeObject Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0006AB72 File Offset: 0x00069B72
		public virtual ISet GetTrustAnchors()
		{
			return new HashSet(this.trustAnchors);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0006AB80 File Offset: 0x00069B80
		public virtual void SetTrustAnchors(ISet tas)
		{
			if (tas == null)
			{
				throw new ArgumentNullException("value");
			}
			if (tas.IsEmpty)
			{
				throw new ArgumentException("non-empty set required", "value");
			}
			this.trustAnchors = new HashSet();
			foreach (object obj in tas)
			{
				TrustAnchor trustAnchor = (TrustAnchor)obj;
				if (trustAnchor != null)
				{
					this.trustAnchors.Add(trustAnchor);
				}
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0006AC10 File Offset: 0x00069C10
		public virtual X509CertStoreSelector GetTargetCertConstraints()
		{
			if (this.certSelector == null)
			{
				return null;
			}
			return (X509CertStoreSelector)this.certSelector.Clone();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006AC2C File Offset: 0x00069C2C
		public virtual void SetTargetCertConstraints(IX509Selector selector)
		{
			if (selector == null)
			{
				this.certSelector = null;
				return;
			}
			this.certSelector = (IX509Selector)selector.Clone();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0006AC4C File Offset: 0x00069C4C
		public virtual ISet GetInitialPolicies()
		{
			ISet s = this.initialPolicies;
			if (this.initialPolicies == null)
			{
				s = new HashSet();
			}
			return new HashSet(s);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0006AC74 File Offset: 0x00069C74
		public virtual void SetInitialPolicies(ISet initialPolicies)
		{
			this.initialPolicies = new HashSet();
			if (initialPolicies != null)
			{
				foreach (object obj in initialPolicies)
				{
					string text = (string)obj;
					if (text != null)
					{
						this.initialPolicies.Add(text);
					}
				}
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0006ACE0 File Offset: 0x00069CE0
		public virtual void SetCertPathCheckers(IList checkers)
		{
			this.certPathCheckers = new ArrayList();
			if (checkers != null)
			{
				foreach (object obj in checkers)
				{
					PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
					this.certPathCheckers.Add(pkixCertPathChecker.Clone());
				}
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006AD50 File Offset: 0x00069D50
		public virtual IList GetCertPathCheckers()
		{
			IList list = new ArrayList();
			foreach (object obj in this.certPathCheckers)
			{
				PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
				list.Add(pkixCertPathChecker.Clone());
			}
			return list;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0006ADB8 File Offset: 0x00069DB8
		public virtual void AddCertPathChecker(PkixCertPathChecker checker)
		{
			if (checker != null)
			{
				this.certPathCheckers.Add(checker.Clone());
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0006ADD0 File Offset: 0x00069DD0
		public virtual object Clone()
		{
			PkixParameters pkixParameters = new PkixParameters(this.GetTrustAnchors());
			pkixParameters.SetParams(this);
			return pkixParameters;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0006ADF4 File Offset: 0x00069DF4
		protected virtual void SetParams(PkixParameters parameters)
		{
			this.Date = parameters.Date;
			this.SetCertPathCheckers(parameters.GetCertPathCheckers());
			this.IsAnyPolicyInhibited = parameters.IsAnyPolicyInhibited;
			this.IsExplicitPolicyRequired = parameters.IsExplicitPolicyRequired;
			this.IsPolicyMappingInhibited = parameters.IsPolicyMappingInhibited;
			this.IsRevocationEnabled = parameters.IsRevocationEnabled;
			this.SetInitialPolicies(parameters.GetInitialPolicies());
			this.IsPolicyQualifiersRejected = parameters.IsPolicyQualifiersRejected;
			this.SetTargetCertConstraints(parameters.GetTargetCertConstraints());
			this.SetTrustAnchors(parameters.GetTrustAnchors());
			this.validityModel = parameters.validityModel;
			this.useDeltas = parameters.useDeltas;
			this.additionalLocationsEnabled = parameters.additionalLocationsEnabled;
			this.selector = ((parameters.selector == null) ? null : ((IX509Selector)parameters.selector.Clone()));
			this.stores = new ArrayList(parameters.stores);
			this.additionalStores = new ArrayList(parameters.additionalStores);
			this.trustedACIssuers = new HashSet(parameters.trustedACIssuers);
			this.prohibitedACAttributes = new HashSet(parameters.prohibitedACAttributes);
			this.necessaryACAttributes = new HashSet(parameters.necessaryACAttributes);
			this.attrCertCheckers = new HashSet(parameters.attrCertCheckers);
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0006AF24 File Offset: 0x00069F24
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0006AF2C File Offset: 0x00069F2C
		public virtual bool IsUseDeltasEnabled
		{
			get
			{
				return this.useDeltas;
			}
			set
			{
				this.useDeltas = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0006AF35 File Offset: 0x00069F35
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0006AF3D File Offset: 0x00069F3D
		public virtual int ValidityModel
		{
			get
			{
				return this.validityModel;
			}
			set
			{
				this.validityModel = value;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0006AF48 File Offset: 0x00069F48
		public virtual void SetStores(IList stores)
		{
			if (stores == null)
			{
				this.stores = new ArrayList();
				return;
			}
			foreach (object obj in stores)
			{
				if (!(obj is IX509Store))
				{
					throw new InvalidCastException("All elements of list must be of type " + typeof(IX509Store).FullName);
				}
			}
			this.stores = new ArrayList(stores);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0006AFD4 File Offset: 0x00069FD4
		public virtual void AddStore(IX509Store store)
		{
			if (store != null)
			{
				this.stores.Add(store);
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0006AFE6 File Offset: 0x00069FE6
		public virtual void AddAdditionalStore(IX509Store store)
		{
			if (store != null)
			{
				this.additionalStores.Add(store);
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0006AFF8 File Offset: 0x00069FF8
		public virtual IList GetAdditionalStores()
		{
			return new ArrayList(this.additionalStores);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0006B005 File Offset: 0x0006A005
		public virtual IList GetStores()
		{
			return new ArrayList(this.stores);
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0006B012 File Offset: 0x0006A012
		public virtual bool IsAdditionalLocationsEnabled
		{
			get
			{
				return this.additionalLocationsEnabled;
			}
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0006B01A File Offset: 0x0006A01A
		public virtual void SetAdditionalLocationsEnabled(bool enabled)
		{
			this.additionalLocationsEnabled = enabled;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0006B023 File Offset: 0x0006A023
		public virtual IX509Selector GetTargetConstraints()
		{
			if (this.selector != null)
			{
				return (IX509Selector)this.selector.Clone();
			}
			return null;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0006B03F File Offset: 0x0006A03F
		public virtual void SetTargetConstraints(IX509Selector selector)
		{
			if (selector != null)
			{
				this.selector = (IX509Selector)selector.Clone();
				return;
			}
			this.selector = null;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0006B05D File Offset: 0x0006A05D
		public virtual ISet GetTrustedACIssuers()
		{
			return new HashSet(this.trustedACIssuers);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0006B06C File Offset: 0x0006A06C
		public virtual void SetTrustedACIssuers(ISet trustedACIssuers)
		{
			if (trustedACIssuers == null)
			{
				this.trustedACIssuers = new HashSet();
				return;
			}
			foreach (object obj in trustedACIssuers)
			{
				if (!(obj is TrustAnchor))
				{
					throw new InvalidCastException("All elements of set must be of type " + typeof(TrustAnchor).Name + ".");
				}
			}
			this.trustedACIssuers = new HashSet(trustedACIssuers);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0006B0FC File Offset: 0x0006A0FC
		public virtual ISet GetNecessaryACAttributes()
		{
			return new HashSet(this.necessaryACAttributes);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0006B10C File Offset: 0x0006A10C
		public virtual void SetNecessaryACAttributes(ISet necessaryACAttributes)
		{
			if (necessaryACAttributes == null)
			{
				this.necessaryACAttributes = new HashSet();
				return;
			}
			foreach (object obj in necessaryACAttributes)
			{
				if (!(obj is string))
				{
					throw new InvalidCastException("All elements of set must be of type string.");
				}
			}
			this.necessaryACAttributes = new HashSet(necessaryACAttributes);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0006B184 File Offset: 0x0006A184
		public virtual ISet GetProhibitedACAttributes()
		{
			return new HashSet(this.prohibitedACAttributes);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0006B194 File Offset: 0x0006A194
		public virtual void SetProhibitedACAttributes(ISet prohibitedACAttributes)
		{
			if (prohibitedACAttributes == null)
			{
				this.prohibitedACAttributes = new HashSet();
				return;
			}
			foreach (object obj in prohibitedACAttributes)
			{
				if (!(obj is string))
				{
					throw new InvalidCastException("All elements of set must be of type string.");
				}
			}
			this.prohibitedACAttributes = new HashSet(prohibitedACAttributes);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0006B20C File Offset: 0x0006A20C
		public virtual ISet GetAttrCertCheckers()
		{
			return new HashSet(this.attrCertCheckers);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0006B21C File Offset: 0x0006A21C
		public virtual void SetAttrCertCheckers(ISet attrCertCheckers)
		{
			if (attrCertCheckers == null)
			{
				this.attrCertCheckers = new HashSet();
				return;
			}
			foreach (object obj in attrCertCheckers)
			{
				if (!(obj is PkixAttrCertChecker))
				{
					throw new InvalidCastException("All elements of set must be of type " + typeof(PkixAttrCertChecker).FullName + ".");
				}
			}
			this.attrCertCheckers = new HashSet(attrCertCheckers);
		}

		// Token: 0x04000D30 RID: 3376
		public const int PkixValidityModel = 0;

		// Token: 0x04000D31 RID: 3377
		public const int ChainValidityModel = 1;

		// Token: 0x04000D32 RID: 3378
		private ISet trustAnchors;

		// Token: 0x04000D33 RID: 3379
		private DateTimeObject date;

		// Token: 0x04000D34 RID: 3380
		private IList certPathCheckers;

		// Token: 0x04000D35 RID: 3381
		private bool revocationEnabled = true;

		// Token: 0x04000D36 RID: 3382
		private ISet initialPolicies;

		// Token: 0x04000D37 RID: 3383
		private bool explicitPolicyRequired;

		// Token: 0x04000D38 RID: 3384
		private bool anyPolicyInhibited;

		// Token: 0x04000D39 RID: 3385
		private bool policyMappingInhibited;

		// Token: 0x04000D3A RID: 3386
		private bool policyQualifiersRejected = true;

		// Token: 0x04000D3B RID: 3387
		private IX509Selector certSelector;

		// Token: 0x04000D3C RID: 3388
		private IList stores;

		// Token: 0x04000D3D RID: 3389
		private IX509Selector selector;

		// Token: 0x04000D3E RID: 3390
		private bool additionalLocationsEnabled;

		// Token: 0x04000D3F RID: 3391
		private IList additionalStores;

		// Token: 0x04000D40 RID: 3392
		private ISet trustedACIssuers;

		// Token: 0x04000D41 RID: 3393
		private ISet necessaryACAttributes;

		// Token: 0x04000D42 RID: 3394
		private ISet prohibitedACAttributes;

		// Token: 0x04000D43 RID: 3395
		private ISet attrCertCheckers;

		// Token: 0x04000D44 RID: 3396
		private int validityModel;

		// Token: 0x04000D45 RID: 3397
		private bool useDeltas;
	}
}
