using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000599 RID: 1433
	public class X509V2AttributeCertificate : X509ExtensionBase, IX509AttributeCertificate, IX509Extension
	{
		// Token: 0x06003114 RID: 12564 RVA: 0x0012FFDF File Offset: 0x0012EFDF
		public X509V2AttributeCertificate(Stream encIn) : this(new Asn1InputStream(encIn))
		{
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0012FFED File Offset: 0x0012EFED
		public X509V2AttributeCertificate(byte[] encoded) : this(new Asn1InputStream(encoded))
		{
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0012FFFB File Offset: 0x0012EFFB
		internal X509V2AttributeCertificate(Asn1InputStream ais) : this(AttributeCertificate.GetInstance(ais.ReadObject()))
		{
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00130010 File Offset: 0x0012F010
		internal X509V2AttributeCertificate(AttributeCertificate cert)
		{
			this.cert = cert;
			try
			{
				this.notAfter = cert.ACInfo.AttrCertValidityPeriod.NotAfterTime.ToDateTime();
				this.notBefore = cert.ACInfo.AttrCertValidityPeriod.NotBeforeTime.ToDateTime();
			}
			catch (Exception innerException)
			{
				throw new IOException("invalid data structure in certificate!", innerException);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00130080 File Offset: 0x0012F080
		public virtual int Version
		{
			get
			{
				return this.cert.ACInfo.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0013009E File Offset: 0x0012F09E
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.cert.ACInfo.SerialNumber.Value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x001300B5 File Offset: 0x0012F0B5
		public virtual AttributeCertificateHolder Holder
		{
			get
			{
				return new AttributeCertificateHolder((Asn1Sequence)this.cert.ACInfo.Holder.ToAsn1Object());
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x001300D6 File Offset: 0x0012F0D6
		public virtual AttributeCertificateIssuer Issuer
		{
			get
			{
				return new AttributeCertificateIssuer(this.cert.ACInfo.Issuer);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x001300ED File Offset: 0x0012F0ED
		public virtual DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x001300F5 File Offset: 0x0012F0F5
		public virtual DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00130100 File Offset: 0x0012F100
		public virtual bool[] GetIssuerUniqueID()
		{
			DerBitString issuerUniqueID = this.cert.ACInfo.IssuerUniqueID;
			if (issuerUniqueID != null)
			{
				byte[] bytes = issuerUniqueID.GetBytes();
				bool[] array = new bool[bytes.Length * 8 - issuerUniqueID.PadBits];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (((int)bytes[num / 8] & 128 >> num % 8) != 0);
				}
				return array;
			}
			return null;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x00130166 File Offset: 0x0012F166
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00130173 File Offset: 0x0012F173
		public virtual bool IsValid(DateTime date)
		{
			return date.CompareTo(this.NotBefore) >= 0 && date.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x0013019A File Offset: 0x0012F19A
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x001301A8 File Offset: 0x0012F1A8
		public virtual void CheckValidity(DateTime date)
		{
			if (date.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.NotAfter);
			}
			if (date.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.NotBefore);
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0013020B File Offset: 0x0012F20B
		public virtual byte[] GetSignature()
		{
			return this.cert.SignatureValue.GetBytes();
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00130220 File Offset: 0x0012F220
		public virtual void Verify(AsymmetricKeyParameter publicKey)
		{
			if (!this.cert.SignatureAlgorithm.Equals(this.cert.ACInfo.Signature))
			{
				throw new CertificateException("Signature algorithm in certificate info not same as outer certificate");
			}
			ISigner signer = SignerUtilities.GetSigner(this.cert.SignatureAlgorithm.ObjectID.Id);
			signer.Init(false, publicKey);
			try
			{
				byte[] encoded = this.cert.ACInfo.GetEncoded();
				signer.BlockUpdate(encoded, 0, encoded.Length);
			}
			catch (IOException exception)
			{
				throw new SignatureException("Exception encoding certificate info object", exception);
			}
			if (!signer.VerifySignature(this.GetSignature()))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x001302D4 File Offset: 0x0012F2D4
		public virtual byte[] GetEncoded()
		{
			return this.cert.GetEncoded();
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x001302E1 File Offset: 0x0012F2E1
		protected override X509Extensions GetX509Extensions()
		{
			return this.cert.ACInfo.Extensions;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x001302F4 File Offset: 0x0012F2F4
		public virtual X509Attribute[] GetAttributes()
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			X509Attribute[] array = new X509Attribute[attributes.Count];
			for (int num = 0; num != attributes.Count; num++)
			{
				array[num] = new X509Attribute(attributes[num]);
			}
			return array;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00130340 File Offset: 0x0012F340
		public virtual X509Attribute[] GetAttributes(string oid)
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			ArrayList arrayList = new ArrayList();
			for (int num = 0; num != attributes.Count; num++)
			{
				X509Attribute x509Attribute = new X509Attribute(attributes[num]);
				if (x509Attribute.Oid.Equals(oid))
				{
					arrayList.Add(x509Attribute);
				}
			}
			if (arrayList.Count < 1)
			{
				return null;
			}
			return (X509Attribute[])arrayList.ToArray(typeof(X509Attribute));
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x001303B8 File Offset: 0x0012F3B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509V2AttributeCertificate x509V2AttributeCertificate = obj as X509V2AttributeCertificate;
			return x509V2AttributeCertificate != null && this.cert.Equals(x509V2AttributeCertificate.cert);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x001303E8 File Offset: 0x0012F3E8
		public override int GetHashCode()
		{
			return this.cert.GetHashCode();
		}

		// Token: 0x040021C6 RID: 8646
		private readonly AttributeCertificate cert;

		// Token: 0x040021C7 RID: 8647
		private readonly DateTime notBefore;

		// Token: 0x040021C8 RID: 8648
		private readonly DateTime notAfter;
	}
}
