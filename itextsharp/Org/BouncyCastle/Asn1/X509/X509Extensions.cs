using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000481 RID: 1153
	public class X509Extensions : Asn1Encodable
	{
		// Token: 0x06002715 RID: 10005 RVA: 0x000EC937 File Offset: 0x000EB937
		public static X509Extensions GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Extensions.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000EC948 File Offset: 0x000EB948
		public static X509Extensions GetInstance(object obj)
		{
			if (obj == null || obj is X509Extensions)
			{
				return (X509Extensions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new X509Extensions((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return X509Extensions.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000EC9B4 File Offset: 0x000EB9B4
		private X509Extensions(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Sequence instance = Asn1Sequence.GetInstance(asn1Encodable.ToAsn1Object());
				if (instance.Count < 2 || instance.Count > 3)
				{
					throw new ArgumentException("Bad sequence size: " + instance.Count);
				}
				DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(instance[0].ToAsn1Object());
				bool critical = instance.Count == 3 && DerBoolean.GetInstance(instance[1].ToAsn1Object()).IsTrue;
				Asn1OctetString instance3 = Asn1OctetString.GetInstance(instance[instance.Count - 1].ToAsn1Object());
				this.extensions.Add(instance2, new X509Extension(critical, instance3));
				this.ordering.Add(instance2);
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000ECAD4 File Offset: 0x000EBAD4
		public X509Extensions(Hashtable extensions) : this(null, extensions)
		{
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000ECAE0 File Offset: 0x000EBAE0
		public X509Extensions(ArrayList ordering, Hashtable extensions)
		{
			ICollection c = (ordering == null) ? extensions.Keys : ordering;
			this.ordering.AddRange(c);
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)extensions[key]);
			}
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000ECB80 File Offset: 0x000EBB80
		public X509Extensions(ArrayList oids, ArrayList values)
		{
			this.ordering.AddRange(oids);
			int num = 0;
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)values[num++]);
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000ECC18 File Offset: 0x000EBC18
		[Obsolete("Use ExtensionOids IEnumerable property")]
		public IEnumerator Oids()
		{
			return this.ExtensionOids.GetEnumerator();
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000ECC25 File Offset: 0x000EBC25
		public IEnumerable ExtensionOids
		{
			get
			{
				return new EnumerableProxy(this.ordering);
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000ECC32 File Offset: 0x000EBC32
		public X509Extension GetExtension(DerObjectIdentifier oid)
		{
			return (X509Extension)this.extensions[oid];
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000ECC48 File Offset: 0x000EBC48
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				X509Extension x509Extension = (X509Extension)this.extensions[derObjectIdentifier];
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derObjectIdentifier
				});
				if (x509Extension.IsCritical)
				{
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						DerBoolean.True
					});
				}
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					x509Extension.Value
				});
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(asn1EncodableVector2)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000ECD38 File Offset: 0x000EBD38
		public bool Equivalent(X509Extensions other)
		{
			if (this.extensions.Count != other.extensions.Count)
			{
				return false;
			}
			foreach (object obj in this.extensions.Keys)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				if (!this.extensions[key].Equals(other.extensions[key]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001ADB RID: 6875
		public static readonly DerObjectIdentifier SubjectDirectoryAttributes = new DerObjectIdentifier("2.5.29.9");

		// Token: 0x04001ADC RID: 6876
		public static readonly DerObjectIdentifier SubjectKeyIdentifier = new DerObjectIdentifier("2.5.29.14");

		// Token: 0x04001ADD RID: 6877
		public static readonly DerObjectIdentifier KeyUsage = new DerObjectIdentifier("2.5.29.15");

		// Token: 0x04001ADE RID: 6878
		public static readonly DerObjectIdentifier PrivateKeyUsagePeriod = new DerObjectIdentifier("2.5.29.16");

		// Token: 0x04001ADF RID: 6879
		public static readonly DerObjectIdentifier SubjectAlternativeName = new DerObjectIdentifier("2.5.29.17");

		// Token: 0x04001AE0 RID: 6880
		public static readonly DerObjectIdentifier IssuerAlternativeName = new DerObjectIdentifier("2.5.29.18");

		// Token: 0x04001AE1 RID: 6881
		public static readonly DerObjectIdentifier BasicConstraints = new DerObjectIdentifier("2.5.29.19");

		// Token: 0x04001AE2 RID: 6882
		public static readonly DerObjectIdentifier CrlNumber = new DerObjectIdentifier("2.5.29.20");

		// Token: 0x04001AE3 RID: 6883
		public static readonly DerObjectIdentifier ReasonCode = new DerObjectIdentifier("2.5.29.21");

		// Token: 0x04001AE4 RID: 6884
		public static readonly DerObjectIdentifier InstructionCode = new DerObjectIdentifier("2.5.29.23");

		// Token: 0x04001AE5 RID: 6885
		public static readonly DerObjectIdentifier InvalidityDate = new DerObjectIdentifier("2.5.29.24");

		// Token: 0x04001AE6 RID: 6886
		public static readonly DerObjectIdentifier DeltaCrlIndicator = new DerObjectIdentifier("2.5.29.27");

		// Token: 0x04001AE7 RID: 6887
		public static readonly DerObjectIdentifier IssuingDistributionPoint = new DerObjectIdentifier("2.5.29.28");

		// Token: 0x04001AE8 RID: 6888
		public static readonly DerObjectIdentifier CertificateIssuer = new DerObjectIdentifier("2.5.29.29");

		// Token: 0x04001AE9 RID: 6889
		public static readonly DerObjectIdentifier NameConstraints = new DerObjectIdentifier("2.5.29.30");

		// Token: 0x04001AEA RID: 6890
		public static readonly DerObjectIdentifier CrlDistributionPoints = new DerObjectIdentifier("2.5.29.31");

		// Token: 0x04001AEB RID: 6891
		public static readonly DerObjectIdentifier CertificatePolicies = new DerObjectIdentifier("2.5.29.32");

		// Token: 0x04001AEC RID: 6892
		public static readonly DerObjectIdentifier PolicyMappings = new DerObjectIdentifier("2.5.29.33");

		// Token: 0x04001AED RID: 6893
		public static readonly DerObjectIdentifier AuthorityKeyIdentifier = new DerObjectIdentifier("2.5.29.35");

		// Token: 0x04001AEE RID: 6894
		public static readonly DerObjectIdentifier PolicyConstraints = new DerObjectIdentifier("2.5.29.36");

		// Token: 0x04001AEF RID: 6895
		public static readonly DerObjectIdentifier ExtendedKeyUsage = new DerObjectIdentifier("2.5.29.37");

		// Token: 0x04001AF0 RID: 6896
		public static readonly DerObjectIdentifier FreshestCrl = new DerObjectIdentifier("2.5.29.46");

		// Token: 0x04001AF1 RID: 6897
		public static readonly DerObjectIdentifier InhibitAnyPolicy = new DerObjectIdentifier("2.5.29.54");

		// Token: 0x04001AF2 RID: 6898
		public static readonly DerObjectIdentifier AuthorityInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.1");

		// Token: 0x04001AF3 RID: 6899
		public static readonly DerObjectIdentifier SubjectInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.11");

		// Token: 0x04001AF4 RID: 6900
		public static readonly DerObjectIdentifier LogoType = new DerObjectIdentifier("1.3.6.1.5.5.7.1.12");

		// Token: 0x04001AF5 RID: 6901
		public static readonly DerObjectIdentifier BiometricInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.1.2");

		// Token: 0x04001AF6 RID: 6902
		public static readonly DerObjectIdentifier QCStatements = new DerObjectIdentifier("1.3.6.1.5.5.7.1.3");

		// Token: 0x04001AF7 RID: 6903
		public static readonly DerObjectIdentifier AuditIdentity = new DerObjectIdentifier("1.3.6.1.5.5.7.1.4");

		// Token: 0x04001AF8 RID: 6904
		public static readonly DerObjectIdentifier NoRevAvail = new DerObjectIdentifier("2.5.29.56");

		// Token: 0x04001AF9 RID: 6905
		public static readonly DerObjectIdentifier TargetInformation = new DerObjectIdentifier("2.5.29.55");

		// Token: 0x04001AFA RID: 6906
		private readonly Hashtable extensions = new Hashtable();

		// Token: 0x04001AFB RID: 6907
		private readonly ArrayList ordering = new ArrayList();
	}
}
