using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x020003D0 RID: 976
	public class OcspReq : X509ExtensionBase
	{
		// Token: 0x060021EA RID: 8682 RVA: 0x000CDB41 File Offset: 0x000CCB41
		public OcspReq(OcspRequest req)
		{
			this.req = req;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000CDB50 File Offset: 0x000CCB50
		public OcspReq(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000CDB5E File Offset: 0x000CCB5E
		public OcspReq(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000CDB6C File Offset: 0x000CCB6C
		private OcspReq(Asn1InputStream aIn)
		{
			try
			{
				this.req = OcspRequest.GetInstance(aIn.ReadObject());
			}
			catch (ArgumentException ex)
			{
				throw new IOException("malformed request: " + ex.Message);
			}
			catch (InvalidCastException ex2)
			{
				throw new IOException("malformed request: " + ex2.Message);
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000CDBDC File Offset: 0x000CCBDC
		public byte[] GetTbsRequest()
		{
			byte[] encoded;
			try
			{
				encoded = this.req.TbsRequest.GetEncoded();
			}
			catch (IOException e)
			{
				throw new OcspException("problem encoding tbsRequest", e);
			}
			return encoded;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000CDC1C File Offset: 0x000CCC1C
		public int Version
		{
			get
			{
				return this.req.TbsRequest.Version.Value.IntValue + 1;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000CDC3A File Offset: 0x000CCC3A
		public GeneralName RequestorName
		{
			get
			{
				return GeneralName.GetInstance(this.req.TbsRequest.RequestorName);
			}
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000CDC54 File Offset: 0x000CCC54
		public Req[] GetRequestList()
		{
			Asn1Sequence requestList = this.req.TbsRequest.RequestList;
			Req[] array = new Req[requestList.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new Req(Request.GetInstance(requestList[num]));
			}
			return array;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000CDCA1 File Offset: 0x000CCCA1
		public X509Extensions RequestExtensions
		{
			get
			{
				return X509Extensions.GetInstance(this.req.TbsRequest.RequestExtensions);
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000CDCB8 File Offset: 0x000CCCB8
		protected override X509Extensions GetX509Extensions()
		{
			return this.RequestExtensions;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000CDCC0 File Offset: 0x000CCCC0
		public string SignatureAlgOid
		{
			get
			{
				if (!this.IsSigned)
				{
					return null;
				}
				return this.req.OptionalSignature.SignatureAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000CDCE6 File Offset: 0x000CCCE6
		public byte[] GetSignature()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			return this.req.OptionalSignature.SignatureValue.GetBytes();
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000CDD08 File Offset: 0x000CCD08
		private ArrayList GetCertList()
		{
			ArrayList arrayList = new ArrayList();
			Asn1Sequence certs = this.req.OptionalSignature.Certs;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					try
					{
						arrayList.Add(new X509CertificateParser().ReadCertificate(asn1Encodable.GetEncoded()));
					}
					catch (Exception e)
					{
						throw new OcspException("can't re-encode certificate!", e);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000CDDAC File Offset: 0x000CCDAC
		public X509Certificate[] GetCerts()
		{
			if (!this.IsSigned)
			{
				return null;
			}
			ArrayList certList = this.GetCertList();
			return (X509Certificate[])certList.ToArray(typeof(X509Certificate));
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000CDDE0 File Offset: 0x000CCDE0
		public IX509Store GetCertificates(string type)
		{
			if (!this.IsSigned)
			{
				return null;
			}
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

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000CDE34 File Offset: 0x000CCE34
		public bool IsSigned
		{
			get
			{
				return this.req.OptionalSignature != null;
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000CDE48 File Offset: 0x000CCE48
		public bool Verify(AsymmetricKeyParameter publicKey)
		{
			if (!this.IsSigned)
			{
				throw new OcspException("attempt to Verify signature on unsigned object");
			}
			bool result;
			try
			{
				ISigner signer = SignerUtilities.GetSigner(this.SignatureAlgOid);
				signer.Init(false, publicKey);
				byte[] encoded = this.req.TbsRequest.GetEncoded();
				signer.BlockUpdate(encoded, 0, encoded.Length);
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (Exception ex)
			{
				throw new OcspException("exception processing sig: " + ex, ex);
			}
			return result;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000CDECC File Offset: 0x000CCECC
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x04001752 RID: 5970
		private OcspRequest req;
	}
}
