using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000FC RID: 252
	public class X509CertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00032C88 File Offset: 0x00031C88
		public X509CertStoreSelector()
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00032C98 File Offset: 0x00031C98
		public X509CertStoreSelector(X509CertStoreSelector o)
		{
			this.authorityKeyIdentifier = o.AuthorityKeyIdentifier;
			this.basicConstraints = o.BasicConstraints;
			this.certificate = o.Certificate;
			this.certificateValid = o.CertificateValid;
			this.extendedKeyUsage = o.ExtendedKeyUsage;
			this.issuer = o.Issuer;
			this.keyUsage = o.KeyUsage;
			this.policy = o.Policy;
			this.privateKeyValid = o.PrivateKeyValid;
			this.serialNumber = o.SerialNumber;
			this.subject = o.Subject;
			this.subjectKeyIdentifier = o.SubjectKeyIdentifier;
			this.subjectPublicKey = o.SubjectPublicKey;
			this.subjectPublicKeyAlgID = o.SubjectPublicKeyAlgID;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00032D5A File Offset: 0x00031D5A
		public virtual object Clone()
		{
			return new X509CertStoreSelector(this);
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00032D62 File Offset: 0x00031D62
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00032D6F File Offset: 0x00031D6F
		public byte[] AuthorityKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.authorityKeyIdentifier);
			}
			set
			{
				this.authorityKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00032D7D File Offset: 0x00031D7D
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00032D85 File Offset: 0x00031D85
		public int BasicConstraints
		{
			get
			{
				return this.basicConstraints;
			}
			set
			{
				if (value < -2)
				{
					throw new ArgumentException("value can't be less than -2", "value");
				}
				this.basicConstraints = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00032DA3 File Offset: 0x00031DA3
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00032DAB File Offset: 0x00031DAB
		public X509Certificate Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.certificate = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00032DB4 File Offset: 0x00031DB4
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00032DBC File Offset: 0x00031DBC
		public DateTimeObject CertificateValid
		{
			get
			{
				return this.certificateValid;
			}
			set
			{
				this.certificateValid = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00032DC5 File Offset: 0x00031DC5
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00032DD2 File Offset: 0x00031DD2
		public ISet ExtendedKeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.extendedKeyUsage);
			}
			set
			{
				this.extendedKeyUsage = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00032DE0 File Offset: 0x00031DE0
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00032DE8 File Offset: 0x00031DE8
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00032DF1 File Offset: 0x00031DF1
		[Obsolete("Avoid working with X509Name objects in string form")]
		public string IssuerAsString
		{
			get
			{
				if (this.issuer == null)
				{
					return null;
				}
				return this.issuer.ToString();
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00032E08 File Offset: 0x00031E08
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00032E15 File Offset: 0x00031E15
		public bool[] KeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopyBoolArray(this.keyUsage);
			}
			set
			{
				this.keyUsage = X509CertStoreSelector.CopyBoolArray(value);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00032E23 File Offset: 0x00031E23
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00032E30 File Offset: 0x00031E30
		public ISet Policy
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.policy);
			}
			set
			{
				this.policy = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00032E3E File Offset: 0x00031E3E
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00032E46 File Offset: 0x00031E46
		public DateTimeObject PrivateKeyValid
		{
			get
			{
				return this.privateKeyValid;
			}
			set
			{
				this.privateKeyValid = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00032E4F File Offset: 0x00031E4F
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00032E57 File Offset: 0x00031E57
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00032E60 File Offset: 0x00031E60
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00032E68 File Offset: 0x00031E68
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00032E71 File Offset: 0x00031E71
		public string SubjectAsString
		{
			get
			{
				if (this.subject == null)
				{
					return null;
				}
				return this.subject.ToString();
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00032E88 File Offset: 0x00031E88
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00032E95 File Offset: 0x00031E95
		public byte[] SubjectKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.subjectKeyIdentifier);
			}
			set
			{
				this.subjectKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00032EA3 File Offset: 0x00031EA3
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00032EAB File Offset: 0x00031EAB
		public SubjectPublicKeyInfo SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
			set
			{
				this.subjectPublicKey = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00032EB4 File Offset: 0x00031EB4
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00032EBC File Offset: 0x00031EBC
		public DerObjectIdentifier SubjectPublicKeyAlgID
		{
			get
			{
				return this.subjectPublicKeyAlgID;
			}
			set
			{
				this.subjectPublicKeyAlgID = value;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00032EC8 File Offset: 0x00031EC8
		public virtual bool Match(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			if (x509Certificate == null)
			{
				return false;
			}
			if (!X509CertStoreSelector.MatchExtension(this.authorityKeyIdentifier, x509Certificate, X509Extensions.AuthorityKeyIdentifier))
			{
				return false;
			}
			if (this.basicConstraints != -1)
			{
				int num = x509Certificate.GetBasicConstraints();
				if (this.basicConstraints == -2)
				{
					if (num != -1)
					{
						return false;
					}
				}
				else if (num < this.basicConstraints)
				{
					return false;
				}
			}
			if (this.certificate != null && !this.certificate.Equals(x509Certificate))
			{
				return false;
			}
			if (this.certificateValid != null && !x509Certificate.IsValid(this.certificateValid.Value))
			{
				return false;
			}
			if (this.extendedKeyUsage != null)
			{
				IList list = x509Certificate.GetExtendedKeyUsage();
				if (list != null)
				{
					foreach (object obj2 in this.extendedKeyUsage)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj2;
						if (!list.Contains(derObjectIdentifier.Id))
						{
							return false;
						}
					}
				}
			}
			if (this.issuer != null && !this.issuer.Equivalent(x509Certificate.IssuerDN, true))
			{
				return false;
			}
			if (this.keyUsage != null)
			{
				bool[] array = x509Certificate.GetKeyUsage();
				if (array != null)
				{
					for (int i = 0; i < 9; i++)
					{
						if (this.keyUsage[i] && !array[i])
						{
							return false;
						}
					}
				}
			}
			if (this.policy != null)
			{
				Asn1OctetString extensionValue = x509Certificate.GetExtensionValue(X509Extensions.CertificatePolicies);
				if (extensionValue == null)
				{
					return false;
				}
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				if (this.policy.Count < 1 && instance.Count < 1)
				{
					return false;
				}
				bool flag = false;
				foreach (object obj3 in instance)
				{
					PolicyInformation policyInformation = (PolicyInformation)obj3;
					if (this.policy.Contains(policyInformation.PolicyIdentifier))
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
			if (this.privateKeyValid != null)
			{
				Asn1OctetString extensionValue2 = x509Certificate.GetExtensionValue(X509Extensions.PrivateKeyUsagePeriod);
				if (extensionValue2 == null)
				{
					return false;
				}
				PrivateKeyUsagePeriod instance2 = PrivateKeyUsagePeriod.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				DateTime value = this.privateKeyValid.Value;
				DateTime value2 = instance2.NotAfter.ToDateTime();
				DateTime value3 = instance2.NotBefore.ToDateTime();
				if (value.CompareTo(value2) > 0 || value.CompareTo(value3) < 0)
				{
					return false;
				}
			}
			return (this.serialNumber == null || this.serialNumber.Equals(x509Certificate.SerialNumber)) && (this.subject == null || this.subject.Equivalent(x509Certificate.SubjectDN, true)) && X509CertStoreSelector.MatchExtension(this.subjectKeyIdentifier, x509Certificate, X509Extensions.SubjectKeyIdentifier) && (this.subjectPublicKey == null || this.subjectPublicKey.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate))) && (this.subjectPublicKeyAlgID == null || this.subjectPublicKeyAlgID.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate).AlgorithmID));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000331D0 File Offset: 0x000321D0
		internal static bool IssuersMatch(X509Name a, X509Name b)
		{
			if (a != null)
			{
				return a.Equivalent(b, true);
			}
			return b == null;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000331E2 File Offset: 0x000321E2
		private static bool[] CopyBoolArray(bool[] b)
		{
			if (b != null)
			{
				return (bool[])b.Clone();
			}
			return null;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000331F4 File Offset: 0x000321F4
		private static ISet CopySet(ISet s)
		{
			if (s != null)
			{
				return new HashSet(s);
			}
			return null;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00033201 File Offset: 0x00032201
		private static SubjectPublicKeyInfo GetSubjectPublicKey(X509Certificate c)
		{
			return SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(c.GetPublicKey());
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00033210 File Offset: 0x00032210
		private static bool MatchExtension(byte[] b, X509Certificate c, DerObjectIdentifier oid)
		{
			if (b == null)
			{
				return true;
			}
			Asn1OctetString extensionValue = c.GetExtensionValue(oid);
			return extensionValue != null && Arrays.AreEqual(b, extensionValue.GetOctets());
		}

		// Token: 0x04000807 RID: 2055
		private byte[] authorityKeyIdentifier;

		// Token: 0x04000808 RID: 2056
		private int basicConstraints = -1;

		// Token: 0x04000809 RID: 2057
		private X509Certificate certificate;

		// Token: 0x0400080A RID: 2058
		private DateTimeObject certificateValid;

		// Token: 0x0400080B RID: 2059
		private ISet extendedKeyUsage;

		// Token: 0x0400080C RID: 2060
		private X509Name issuer;

		// Token: 0x0400080D RID: 2061
		private bool[] keyUsage;

		// Token: 0x0400080E RID: 2062
		private ISet policy;

		// Token: 0x0400080F RID: 2063
		private DateTimeObject privateKeyValid;

		// Token: 0x04000810 RID: 2064
		private BigInteger serialNumber;

		// Token: 0x04000811 RID: 2065
		private X509Name subject;

		// Token: 0x04000812 RID: 2066
		private byte[] subjectKeyIdentifier;

		// Token: 0x04000813 RID: 2067
		private SubjectPublicKeyInfo subjectPublicKey;

		// Token: 0x04000814 RID: 2068
		private DerObjectIdentifier subjectPublicKeyAlgID;
	}
}
