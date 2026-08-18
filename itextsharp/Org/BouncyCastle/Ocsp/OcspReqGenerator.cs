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
	// Token: 0x02000237 RID: 567
	public class OcspReqGenerator
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x00081BA5 File Offset: 0x00080BA5
		public void AddRequest(CertificateID certId)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, null));
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00081BBA File Offset: 0x00080BBA
		public void AddRequest(CertificateID certId, X509Extensions singleRequestExtensions)
		{
			this.list.Add(new OcspReqGenerator.RequestObject(certId, singleRequestExtensions));
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00081BD0 File Offset: 0x00080BD0
		public void SetRequestorName(X509Name requestorName)
		{
			try
			{
				this.requestorName = new GeneralName(4, requestorName);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("cannot encode principal", innerException);
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00081C0C File Offset: 0x00080C0C
		public void SetRequestorName(GeneralName requestorName)
		{
			this.requestorName = requestorName;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00081C15 File Offset: 0x00080C15
		public void SetRequestExtensions(X509Extensions requestExtensions)
		{
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00081C20 File Offset: 0x00080C20
		private OcspReq GenerateRequest(DerObjectIdentifier signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.list)
			{
				OcspReqGenerator.RequestObject requestObject = (OcspReqGenerator.RequestObject)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						requestObject.ToRequest()
					});
				}
				catch (Exception e)
				{
					throw new OcspException("exception creating Request", e);
				}
			}
			TbsRequest tbsRequest = new TbsRequest(this.requestorName, new DerSequence(asn1EncodableVector), this.requestExtensions);
			ISigner signer = null;
			Signature optionalSignature = null;
			if (signingAlgorithm != null)
			{
				if (this.requestorName == null)
				{
					throw new OcspException("requestorName must be specified if request is signed.");
				}
				try
				{
					signer = SignerUtilities.GetSigner(signingAlgorithm.Id);
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
				DerBitString signatureValue = null;
				try
				{
					byte[] encoded = tbsRequest.GetEncoded();
					signer.BlockUpdate(encoded, 0, encoded.Length);
					signatureValue = new DerBitString(signer.GenerateSignature());
				}
				catch (Exception ex2)
				{
					throw new OcspException("exception processing TBSRequest: " + ex2, ex2);
				}
				AlgorithmIdentifier signatureAlgorithm = new AlgorithmIdentifier(signingAlgorithm, DerNull.Instance);
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
					optionalSignature = new Signature(signatureAlgorithm, signatureValue, new DerSequence(asn1EncodableVector2));
				}
				else
				{
					optionalSignature = new Signature(signatureAlgorithm, signatureValue);
				}
			}
			return new OcspReq(new OcspRequest(tbsRequest, optionalSignature));
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00081E50 File Offset: 0x00080E50
		public OcspReq Generate()
		{
			return this.GenerateRequest(null, null, null, null);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00081E5C File Offset: 0x00080E5C
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain)
		{
			return this.Generate(signingAlgorithm, privateKey, chain, null);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00081E68 File Offset: 0x00080E68
		public OcspReq Generate(string signingAlgorithm, AsymmetricKeyParameter privateKey, X509Certificate[] chain, SecureRandom random)
		{
			if (signingAlgorithm == null)
			{
				throw new ArgumentException("no signing algorithm specified");
			}
			OcspReq result;
			try
			{
				DerObjectIdentifier algorithmOid = OcspUtilities.GetAlgorithmOid(signingAlgorithm);
				result = this.GenerateRequest(algorithmOid, privateKey, chain, random);
			}
			catch (ArgumentException)
			{
				throw new ArgumentException("unknown signing algorithm specified: " + signingAlgorithm);
			}
			return result;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00081EBC File Offset: 0x00080EBC
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return OcspUtilities.AlgNames;
			}
		}

		// Token: 0x04000F41 RID: 3905
		private IList list = new ArrayList();

		// Token: 0x04000F42 RID: 3906
		private GeneralName requestorName;

		// Token: 0x04000F43 RID: 3907
		private X509Extensions requestExtensions;

		// Token: 0x02000238 RID: 568
		private class RequestObject
		{
			// Token: 0x0600162F RID: 5679 RVA: 0x00081ED6 File Offset: 0x00080ED6
			public RequestObject(CertificateID certId, X509Extensions extensions)
			{
				this.certId = certId;
				this.extensions = extensions;
			}

			// Token: 0x06001630 RID: 5680 RVA: 0x00081EEC File Offset: 0x00080EEC
			public Request ToRequest()
			{
				return new Request(this.certId.ToAsn1Object(), this.extensions);
			}

			// Token: 0x04000F44 RID: 3908
			internal CertificateID certId;

			// Token: 0x04000F45 RID: 3909
			internal X509Extensions extensions;
		}
	}
}
