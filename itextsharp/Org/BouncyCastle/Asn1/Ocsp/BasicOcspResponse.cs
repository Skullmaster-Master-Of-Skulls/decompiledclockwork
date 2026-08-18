using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020001B5 RID: 437
	public class BasicOcspResponse : Asn1Encodable
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x0005EAB1 File Offset: 0x0005DAB1
		public static BasicOcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicOcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0005EAC0 File Offset: 0x0005DAC0
		public static BasicOcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is BasicOcspResponse)
			{
				return (BasicOcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BasicOcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0005EB12 File Offset: 0x0005DB12
		public BasicOcspResponse(ResponseData tbsResponseData, AlgorithmIdentifier signatureAlgorithm, DerBitString signature, Asn1Sequence certs)
		{
			this.tbsResponseData = tbsResponseData;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
			this.certs = certs;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0005EB38 File Offset: 0x0005DB38
		private BasicOcspResponse(Asn1Sequence seq)
		{
			this.tbsResponseData = ResponseData.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signature = (DerBitString)seq[2];
			if (seq.Count > 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[3], true);
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0005EBA2 File Offset: 0x0005DBA2
		[Obsolete("Use TbsResponseData property instead")]
		public ResponseData GetTbsResponseData()
		{
			return this.tbsResponseData;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0005EBAA File Offset: 0x0005DBAA
		public ResponseData TbsResponseData
		{
			get
			{
				return this.tbsResponseData;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0005EBB2 File Offset: 0x0005DBB2
		[Obsolete("Use SignatureAlgorithm property instead")]
		public AlgorithmIdentifier GetSignatureAlgorithm()
		{
			return this.signatureAlgorithm;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0005EBBA File Offset: 0x0005DBBA
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0005EBC2 File Offset: 0x0005DBC2
		[Obsolete("Use Signature property instead")]
		public DerBitString GetSignature()
		{
			return this.signature;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0005EBCA File Offset: 0x0005DBCA
		public DerBitString Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0005EBD2 File Offset: 0x0005DBD2
		[Obsolete("Use Certs property instead")]
		public Asn1Sequence GetCerts()
		{
			return this.certs;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0005EBDA File Offset: 0x0005DBDA
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0005EBE4 File Offset: 0x0005DBE4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsResponseData,
				this.signatureAlgorithm,
				this.signature
			});
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.certs)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C25 RID: 3109
		private readonly ResponseData tbsResponseData;

		// Token: 0x04000C26 RID: 3110
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04000C27 RID: 3111
		private readonly DerBitString signature;

		// Token: 0x04000C28 RID: 3112
		private readonly Asn1Sequence certs;
	}
}
