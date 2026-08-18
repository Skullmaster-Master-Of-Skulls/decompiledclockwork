using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020003C5 RID: 965
	public class X509CrlStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x000CA639 File Offset: 0x000C9639
		public X509CrlStoreSelector()
		{
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000CA644 File Offset: 0x000C9644
		public X509CrlStoreSelector(X509CrlStoreSelector o)
		{
			this.certificateChecking = o.CertificateChecking;
			this.dateAndTime = o.DateAndTime;
			this.issuers = o.Issuers;
			this.maxCrlNumber = o.MaxCrlNumber;
			this.minCrlNumber = o.MinCrlNumber;
			this.deltaCrlIndicatorEnabled = o.DeltaCrlIndicatorEnabled;
			this.completeCrlEnabled = o.CompleteCrlEnabled;
			this.maxBaseCrlNumber = o.MaxBaseCrlNumber;
			this.attrCertChecking = o.AttrCertChecking;
			this.issuingDistributionPointEnabled = o.IssuingDistributionPointEnabled;
			this.issuingDistributionPoint = o.IssuingDistributionPoint;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000CA6DB File Offset: 0x000C96DB
		public virtual object Clone()
		{
			return new X509CrlStoreSelector(this);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000CA6E3 File Offset: 0x000C96E3
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x000CA6EB File Offset: 0x000C96EB
		public X509Certificate CertificateChecking
		{
			get
			{
				return this.certificateChecking;
			}
			set
			{
				this.certificateChecking = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000CA6F4 File Offset: 0x000C96F4
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x000CA6FC File Offset: 0x000C96FC
		public DateTimeObject DateAndTime
		{
			get
			{
				return this.dateAndTime;
			}
			set
			{
				this.dateAndTime = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x000CA705 File Offset: 0x000C9705
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x000CA712 File Offset: 0x000C9712
		public ICollection Issuers
		{
			get
			{
				return new ArrayList(this.issuers);
			}
			set
			{
				this.issuers = new ArrayList(value);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000CA720 File Offset: 0x000C9720
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x000CA728 File Offset: 0x000C9728
		public BigInteger MaxCrlNumber
		{
			get
			{
				return this.maxCrlNumber;
			}
			set
			{
				this.maxCrlNumber = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000CA731 File Offset: 0x000C9731
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x000CA739 File Offset: 0x000C9739
		public BigInteger MinCrlNumber
		{
			get
			{
				return this.minCrlNumber;
			}
			set
			{
				this.minCrlNumber = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000CA742 File Offset: 0x000C9742
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x000CA74A File Offset: 0x000C974A
		public IX509AttributeCertificate AttrCertChecking
		{
			get
			{
				return this.attrCertChecking;
			}
			set
			{
				this.attrCertChecking = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x000CA753 File Offset: 0x000C9753
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x000CA75B File Offset: 0x000C975B
		public bool CompleteCrlEnabled
		{
			get
			{
				return this.completeCrlEnabled;
			}
			set
			{
				this.completeCrlEnabled = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000CA764 File Offset: 0x000C9764
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x000CA76C File Offset: 0x000C976C
		public bool DeltaCrlIndicatorEnabled
		{
			get
			{
				return this.deltaCrlIndicatorEnabled;
			}
			set
			{
				this.deltaCrlIndicatorEnabled = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x000CA775 File Offset: 0x000C9775
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x000CA782 File Offset: 0x000C9782
		public byte[] IssuingDistributionPoint
		{
			get
			{
				return Arrays.Clone(this.issuingDistributionPoint);
			}
			set
			{
				this.issuingDistributionPoint = Arrays.Clone(value);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x000CA790 File Offset: 0x000C9790
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x000CA798 File Offset: 0x000C9798
		public bool IssuingDistributionPointEnabled
		{
			get
			{
				return this.issuingDistributionPointEnabled;
			}
			set
			{
				this.issuingDistributionPointEnabled = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000CA7A1 File Offset: 0x000C97A1
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x000CA7A9 File Offset: 0x000C97A9
		public BigInteger MaxBaseCrlNumber
		{
			get
			{
				return this.maxBaseCrlNumber;
			}
			set
			{
				this.maxBaseCrlNumber = value;
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000CA7B4 File Offset: 0x000C97B4
		public virtual bool Match(object obj)
		{
			X509Crl x509Crl = obj as X509Crl;
			if (x509Crl == null)
			{
				return false;
			}
			if (this.dateAndTime != null)
			{
				DateTime value = this.dateAndTime.Value;
				DateTime thisUpdate = x509Crl.ThisUpdate;
				DateTimeObject nextUpdate = x509Crl.NextUpdate;
				if (value.CompareTo(thisUpdate) < 0 || nextUpdate == null || value.CompareTo(nextUpdate.Value) >= 0)
				{
					return false;
				}
			}
			if (this.issuers != null)
			{
				X509Name issuerDN = x509Crl.IssuerDN;
				bool flag = false;
				foreach (object obj2 in this.issuers)
				{
					X509Name x509Name = (X509Name)obj2;
					if (x509Name.Equivalent(issuerDN, true))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.maxCrlNumber != null || this.minCrlNumber != null)
			{
				Asn1OctetString extensionValue = x509Crl.GetExtensionValue(X509Extensions.CrlNumber);
				if (extensionValue == null)
				{
					return false;
				}
				BigInteger positiveValue = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).PositiveValue;
				if (this.maxCrlNumber != null && positiveValue.CompareTo(this.maxCrlNumber) > 0)
				{
					return false;
				}
				if (this.minCrlNumber != null && positiveValue.CompareTo(this.minCrlNumber) < 0)
				{
					return false;
				}
			}
			DerInteger derInteger = null;
			try
			{
				Asn1OctetString extensionValue2 = x509Crl.GetExtensionValue(X509Extensions.DeltaCrlIndicator);
				if (extensionValue2 != null)
				{
					derInteger = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				}
			}
			catch (Exception)
			{
				return false;
			}
			if (derInteger == null)
			{
				if (this.DeltaCrlIndicatorEnabled)
				{
					return false;
				}
			}
			else
			{
				if (this.CompleteCrlEnabled)
				{
					return false;
				}
				if (this.maxBaseCrlNumber != null && derInteger.PositiveValue.CompareTo(this.maxBaseCrlNumber) > 0)
				{
					return false;
				}
			}
			if (this.issuingDistributionPointEnabled)
			{
				Asn1OctetString extensionValue3 = x509Crl.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				if (this.issuingDistributionPoint == null)
				{
					if (extensionValue3 != null)
					{
						return false;
					}
				}
				else if (!Arrays.AreEqual(extensionValue3.GetOctets(), this.issuingDistributionPoint))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400171F RID: 5919
		private X509Certificate certificateChecking;

		// Token: 0x04001720 RID: 5920
		private DateTimeObject dateAndTime;

		// Token: 0x04001721 RID: 5921
		private ICollection issuers;

		// Token: 0x04001722 RID: 5922
		private BigInteger maxCrlNumber;

		// Token: 0x04001723 RID: 5923
		private BigInteger minCrlNumber;

		// Token: 0x04001724 RID: 5924
		private IX509AttributeCertificate attrCertChecking;

		// Token: 0x04001725 RID: 5925
		private bool completeCrlEnabled;

		// Token: 0x04001726 RID: 5926
		private bool deltaCrlIndicatorEnabled;

		// Token: 0x04001727 RID: 5927
		private byte[] issuingDistributionPoint;

		// Token: 0x04001728 RID: 5928
		private bool issuingDistributionPointEnabled;

		// Token: 0x04001729 RID: 5929
		private BigInteger maxBaseCrlNumber;
	}
}
