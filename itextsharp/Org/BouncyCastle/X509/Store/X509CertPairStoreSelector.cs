using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200053B RID: 1339
	public class X509CertPairStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06002E1A RID: 11802 RVA: 0x0011CFD6 File Offset: 0x0011BFD6
		private static X509CertStoreSelector CloneSelector(X509CertStoreSelector s)
		{
			if (s != null)
			{
				return (X509CertStoreSelector)s.Clone();
			}
			return null;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0011CFE8 File Offset: 0x0011BFE8
		public X509CertPairStoreSelector()
		{
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x0011CFF0 File Offset: 0x0011BFF0
		private X509CertPairStoreSelector(X509CertPairStoreSelector o)
		{
			this.certPair = o.CertPair;
			this.forwardSelector = o.ForwardSelector;
			this.reverseSelector = o.ReverseSelector;
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x0011D01C File Offset: 0x0011C01C
		// (set) Token: 0x06002E1E RID: 11806 RVA: 0x0011D024 File Offset: 0x0011C024
		public X509CertificatePair CertPair
		{
			get
			{
				return this.certPair;
			}
			set
			{
				this.certPair = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x0011D02D File Offset: 0x0011C02D
		// (set) Token: 0x06002E20 RID: 11808 RVA: 0x0011D03A File Offset: 0x0011C03A
		public X509CertStoreSelector ForwardSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.forwardSelector);
			}
			set
			{
				this.forwardSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0011D048 File Offset: 0x0011C048
		// (set) Token: 0x06002E22 RID: 11810 RVA: 0x0011D055 File Offset: 0x0011C055
		public X509CertStoreSelector ReverseSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.reverseSelector);
			}
			set
			{
				this.reverseSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x0011D064 File Offset: 0x0011C064
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && (this.certPair == null || this.certPair.Equals(x509CertificatePair)) && (this.forwardSelector == null || this.forwardSelector.Match(x509CertificatePair.Forward)) && (this.reverseSelector == null || this.reverseSelector.Match(x509CertificatePair.Reverse));
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x0011D0DE File Offset: 0x0011C0DE
		public object Clone()
		{
			return new X509CertPairStoreSelector(this);
		}

		// Token: 0x04001FF3 RID: 8179
		private X509CertificatePair certPair;

		// Token: 0x04001FF4 RID: 8180
		private X509CertStoreSelector forwardSelector;

		// Token: 0x04001FF5 RID: 8181
		private X509CertStoreSelector reverseSelector;
	}
}
