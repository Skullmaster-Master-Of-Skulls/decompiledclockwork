using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000182 RID: 386
	public class BasicOcspResp : X509ExtensionBase
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x000572B2 File Offset: 0x000562B2
		public BasicOcspResp(BasicOcspResponse resp)
		{
			this.resp = resp;
			this.data = resp.TbsResponseData;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000572D0 File Offset: 0x000562D0
		public byte[] GetTbsResponseData()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.data.GetDerEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsResponseData", e);
			}
			return derEncoded;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0005730C File Offset: 0x0005630C
		public int Version
		{
			get
			{
				return this.data.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00057325 File Offset: 0x00056325
		public RespID ResponderId
		{
			get
			{
				return new RespID(this.data.ResponderID);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00057337 File Offset: 0x00056337
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0005734C File Offset: 0x0005634C
		public SingleResp[] Responses
		{
			get
			{
				Asn1Sequence responses = this.data.Responses;
				SingleResp[] array = new SingleResp[responses.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
				}
				return array;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00057394 File Offset: 0x00056394
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000573A1 File Offset: 0x000563A1
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000573A9 File Offset: 0x000563A9
		public string SignatureAlgName
		{
			get
			{
				return OcspUtilities.GetAlgorithmName(this.resp.SignatureAlgorithm.ObjectID);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x000573C0 File Offset: 0x000563C0
		public string SignatureAlgOid
		{
			get
			{
				return this.resp.SignatureAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000573D7 File Offset: 0x000563D7
		[Obsolete("RespData class is no longer required as all functionality is available on this class")]
		public RespData GetResponseData()
		{
			return new RespData(this.data);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000573E4 File Offset: 0x000563E4
		public byte[] GetSignature()
		{
			return this.resp.Signature.GetBytes();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000573F8 File Offset: 0x000563F8
		private ArrayList GetCertList()
		{
			ArrayList arrayList = new ArrayList();
			Asn1Sequence certs = this.resp.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						arrayList.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (IOException e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
					catch (CertificateException e2)
					{
						throw new OcspException("can't re-encode certificate!", e2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000574B0 File Offset: 0x000564B0
		public X509Certificate[] GetCerts()
		{
			ArrayList certList = this.GetCertList();
			return (X509Certificate[])certList.ToArray(typeof(X509Certificate));
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000574DC File Offset: 0x000564DC
		public IX509Store GetCertificates(string type)
		{
			IX509Store result;
			try
			{
				result = X509StoreFactory.Create("Certificate/" + type, new X509CollectionStoreParameters(this.GetCertList()));
			}
			catch (Exception e)
			{
				throw new OcspException("can't setup the CertStore", e);
			}
			return result;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00057528 File Offset: 0x00056528
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgName);
				signer.Init(false, publicKey);
				byte[] derEncoded = this.data.GetDerEncoded();
				signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00057594 File Offset: 0x00056594
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000575A4 File Offset: 0x000565A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BasicOcspResp basicOcspResp = obj as BasicOcspResp;
			return basicOcspResp != null && this.resp.Equals(basicOcspResp.resp);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000575D4 File Offset: 0x000565D4
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x04000B0B RID: 2827
		private readonly BasicOcspResponse resp;

		// Token: 0x04000B0C RID: 2828
		private readonly ResponseData data;
	}
}
