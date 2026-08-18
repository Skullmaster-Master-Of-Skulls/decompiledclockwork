using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x020001E7 RID: 487
	public class BasicOcspRespGenerator
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x0006D59C File Offset: 0x0006C59C
		public BasicOcspRespGenerator(RespID responderID)
		{
			this.responderID = responderID;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0006D5B6 File Offset: 0x0006C5B6
		public BasicOcspRespGenerator(AsymmetricKeyParameter publicKey)
		{
			this.responderID = new RespID(publicKey);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0006D5D5 File Offset: 0x0006C5D5
		public void AddResponse(CertificateID certID, CertificateStatus certStatus)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, null));
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0006D5F0 File Offset: 0x0006C5F0
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, singleExtensions));
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0006D60B File Offset: 0x0006C60B
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, DateTime.UtcNow, nextUpdate, singleExtensions));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0006D628 File Offset: 0x0006C628
		public void AddResponse(CertificateID certID, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions singleExtensions)
		{
			this.list.Add(new BasicOcspRespGenerator.ResponseObject(certID, certStatus, thisUpdate, nextUpdate, singleExtensions));
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006D642 File Offset: 0x0006C642
		public void SetResponseExtensions(X509Extensions responseExtensions)
		{
			this.responseExtensions = responseExtensions;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0006D64C File Offset: 0x0006C64C
		private BasicOcspResp GenerateResponse(string signatureName, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime producedAt, SecureRandom random)
		{
			DerObjectIdentifier algorithmOid;
			try
			{
				algorithmOid = OcspUtilities.GetAlgorithmOid(signatureName);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("unknown signing algorithm specified", innerException);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.list)
			{
				BasicOcspRespGenerator.ResponseObject responseObject = (BasicOcspRespGenerator.ResponseObject)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						responseObject.ToResponse()
					});
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			ResponseData responseData = new ResponseData(this.responderID.ToAsn1Object(), new DerGeneralizedTime(producedAt), new DerSequence(asn1EncodableVector), this.responseExtensions);
			ISigner signer = null;
			try
			{
				signer = SignerUtilities.GetSigner(signatureName);
				if (random != null)
				{
					signer.Init(true, new ParametersWithRandom(privateKey, random));
				}
				else
				{
					signer.Init(true, privateKey);
				}
			}
			catch (Exception ex)
			{
				throw new OcspException("exception creating signature: " + ex, ex);
			}
			DerBitString signature = null;
			try
			{
				byte[] derEncoded = responseData.GetDerEncoded();
				signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
				signature = new DerBitString(signer.GenerateSignature());
			}
			catch (Exception ex2)
			{
				throw new OcspException("exception processing TBSRequest: " + ex2, ex2);
			}
			AlgorithmIdentifier sigAlgID = OcspUtilities.GetSigAlgID(algorithmOid);
			DerSequence certs = null;
			if (chain != null && chain.Length > 0)
			{
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
				try
				{
					for (int num = 0; num != chain.Length; num++)
					{
						asn1EncodableVector2.Add(new Asn1Encodable[]
						{
							X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(chain[num].GetEncoded()))
						});
					}
				}
				catch (IOException e2)
				{
					throw new OcspException("error processing certs", e2);
				}
				catch (CertificateEncodingException e3)
				{
					throw new OcspException("error encoding certs", e3);
				}
				certs = new DerSequence(asn1EncodableVector2);
			}
			return new BasicOcspResp(new BasicOcspResponse(responseData, sigAlgID, signature, certs));
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0006D878 File Offset: 0x0006C878
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime thisUpdate)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, thisUpdate, null);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006D886 File Offset: 0x0006C886
		public BasicOcspResp Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, DateTime producedAt, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			return this.GenerateResponse(signingAlgorithm, privateKey, chain, producedAt, random);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0006D8A3 File Offset: 0x0006C8A3
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x04000D60 RID: 3424
		private readonly IList list = new ArrayList();

		// Token: 0x04000D61 RID: 3425
		private X509Extensions responseExtensions;

		// Token: 0x04000D62 RID: 3426
		private RespID responderID;

		// Token: 0x020001E8 RID: 488
		private class ResponseObject
		{
			// Token: 0x0600131C RID: 4892 RVA: 0x0006D8AA File Offset: 0x0006C8AA
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), null, extensions)
			{
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x0006D8BD File Offset: 0x0006C8BD
			public ResponseObject(CertificateID certId, CertificateStatus certStatus, DateTime thisUpdate, DateTime nextUpdate, X509Extensions extensions) : this(certId, certStatus, new DerGeneralizedTime(thisUpdate), new DerGeneralizedTime(nextUpdate), extensions)
			{
			}

			// Token: 0x0600131E RID: 4894 RVA: 0x0006D8D8 File Offset: 0x0006C8D8
			private ResponseObject(CertificateID certId, CertificateStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions extensions)
			{
				this.certId = certId;
				if (certStatus == null)
				{
					this.certStatus = new CertStatus();
				}
				else if (certStatus is UnknownStatus)
				{
					this.certStatus = new CertStatus(2, DerNull.Instance);
				}
				else
				{
					RevokedStatus revokedStatus = (RevokedStatus)certStatus;
					CrlReason revocationReason = revokedStatus.HasRevocationReason ? new CrlReason(revokedStatus.RevocationReason) : null;
					this.certStatus = new CertStatus(new RevokedInfo(new DerGeneralizedTime(revokedStatus.RevocationTime), revocationReason));
				}
				this.thisUpdate = thisUpdate;
				this.nextUpdate = nextUpdate;
				this.extensions = extensions;
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x0006D96E File Offset: 0x0006C96E
			public SingleResponse ToResponse()
			{
				return new SingleResponse(this.certId.ToAsn1Object(), this.certStatus, this.thisUpdate, this.nextUpdate, this.extensions);
			}

			// Token: 0x04000D63 RID: 3427
			internal CertificateID certId;

			// Token: 0x04000D64 RID: 3428
			internal CertStatus certStatus;

			// Token: 0x04000D65 RID: 3429
			internal DerGeneralizedTime thisUpdate;

			// Token: 0x04000D66 RID: 3430
			internal DerGeneralizedTime nextUpdate;

			// Token: 0x04000D67 RID: 3431
			internal X509Extensions extensions;
		}
	}
}
