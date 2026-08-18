using System;
using System.IO;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x0200014B RID: 331
	public class RequestedCertificate : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0004218C File Offset: 0x0004118C
		public static RequestedCertificate GetInstance(object obj)
		{
			if (obj == null || obj is RequestedCertificate)
			{
				return (RequestedCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RequestedCertificate(X509CertificateStructure.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject)
			{
				return new RequestedCertificate((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000421F2 File Offset: 0x000411F2
		public static RequestedCertificate GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return RequestedCertificate.GetInstance(obj.GetObject());
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00042210 File Offset: 0x00041210
		private RequestedCertificate(Asn1TaggedObject tagged)
		{
			switch (tagged.TagNo)
			{
			case 0:
				this.publicKeyCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			case 1:
				this.attributeCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			default:
				throw new ArgumentException("unknown tag number: " + tagged.TagNo);
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0004227A File Offset: 0x0004127A
		public RequestedCertificate(X509CertificateStructure certificate)
		{
			this.cert = certificate;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00042289 File Offset: 0x00041289
		public RequestedCertificate(RequestedCertificate.Choice type, byte[] certificateOctets) : this(new DerTaggedObject((int)type, new DerOctetString(certificateOctets)))
		{
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0004229D File Offset: 0x0004129D
		public RequestedCertificate.Choice Type
		{
			get
			{
				if (this.cert != null)
				{
					return RequestedCertificate.Choice.Certificate;
				}
				if (this.publicKeyCert != null)
				{
					return RequestedCertificate.Choice.PublicKeyCertificate;
				}
				return RequestedCertificate.Choice.AttributeCertificate;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000422B4 File Offset: 0x000412B4
		public byte[] GetCertificateBytes()
		{
			if (this.cert != null)
			{
				try
				{
					return this.cert.GetEncoded();
				}
				catch (IOException arg)
				{
					throw new InvalidOperationException("can't decode certificate: " + arg);
				}
			}
			if (this.publicKeyCert != null)
			{
				return this.publicKeyCert;
			}
			return this.attributeCert;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00042310 File Offset: 0x00041310
		public override Asn1Object ToAsn1Object()
		{
			if (this.publicKeyCert != null)
			{
				return new DerTaggedObject(0, new DerOctetString(this.publicKeyCert));
			}
			if (this.attributeCert != null)
			{
				return new DerTaggedObject(1, new DerOctetString(this.attributeCert));
			}
			return this.cert.ToAsn1Object();
		}

		// Token: 0x04000978 RID: 2424
		private readonly X509CertificateStructure cert;

		// Token: 0x04000979 RID: 2425
		private readonly byte[] publicKeyCert;

		// Token: 0x0400097A RID: 2426
		private readonly byte[] attributeCert;

		// Token: 0x0200014C RID: 332
		public enum Choice
		{
			// Token: 0x0400097C RID: 2428
			Certificate = -1,
			// Token: 0x0400097D RID: 2429
			PublicKeyCertificate,
			// Token: 0x0400097E RID: 2430
			AttributeCertificate
		}
	}
}
