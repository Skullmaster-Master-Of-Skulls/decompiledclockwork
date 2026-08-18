using System;
using System.Globalization;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x0200007C RID: 124
	public class Pkcs10CertificationRequestDelaySigned : Pkcs10CertificationRequest
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00015E81 File Offset: 0x00014E81
		protected Pkcs10CertificationRequestDelaySigned()
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00015E89 File Offset: 0x00014E89
		public Pkcs10CertificationRequestDelaySigned(byte[] encoded) : base(encoded)
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00015E92 File Offset: 0x00014E92
		public Pkcs10CertificationRequestDelaySigned(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015E9B File Offset: 0x00014E9B
		public Pkcs10CertificationRequestDelaySigned(Stream input) : base(input)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015EA4 File Offset: 0x00014EA4
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes, AsymmetricKeyParameter signingKey) : base(signatureAlgorithm, subject, publicKey, attributes, signingKey)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015EB4 File Offset: 0x00014EB4
		public Pkcs10CertificationRequestDelaySigned(string signatureAlgorithm, X509Name subject, AsymmetricKeyParameter publicKey, Asn1Set attributes)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("expected public key", "publicKey");
			}
			string key = signatureAlgorithm.ToUpper(CultureInfo.InvariantCulture);
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)Pkcs10CertificationRequest.algorithms[key];
			if (derObjectIdentifier == null)
			{
				throw new ArgumentException("Unknown signature type requested");
			}
			if (Pkcs10CertificationRequest.noParams.Contains(derObjectIdentifier))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier);
			}
			else if (Pkcs10CertificationRequest.exParams.ContainsKey(key))
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, (Asn1Encodable)Pkcs10CertificationRequest.exParams[key]);
			}
			else
			{
				this.sigAlgId = new AlgorithmIdentifier(derObjectIdentifier, null);
			}
			SubjectPublicKeyInfo pkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			this.reqInfo = new CertificationRequestInfo(subject, pkInfo, attributes);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015F9D File Offset: 0x00014F9D
		public byte[] GetDataToSign()
		{
			return this.reqInfo.GetDerEncoded();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00015FAA File Offset: 0x00014FAA
		public void SignRequest(byte[] signedData)
		{
			this.sigBits = new DerBitString(signedData);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00015FB8 File Offset: 0x00014FB8
		public void SignRequest(DerBitString signedData)
		{
			this.sigBits = signedData;
		}
	}
}
