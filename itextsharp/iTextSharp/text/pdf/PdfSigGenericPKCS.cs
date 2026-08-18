using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000499 RID: 1177
	public abstract class PdfSigGenericPKCS : PdfSignature
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x000F0BA7 File Offset: 0x000EFBA7
		public PdfSigGenericPKCS(PdfName filter, PdfName subFilter) : base(filter, subFilter)
		{
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000F0BB4 File Offset: 0x000EFBB4
		public void SetSignInfo(ICipherParameters privKey, X509Certificate[] certChain, object[] crlList)
		{
			this.pkcs = new PdfPKCS7(privKey, certChain, crlList, this.hashAlgorithm, PdfName.ADBE_PKCS7_SHA1.Equals(base.Get(PdfName.SUBFILTER)));
			this.pkcs.SetExternalDigest(this.externalDigest, this.externalRSAdata, this.digestEncryptionAlgorithm);
			if (PdfName.ADBE_X509_RSA_SHA1.Equals(base.Get(PdfName.SUBFILTER)))
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int i = 0; i < certChain.Length; i++)
				{
					byte[] encoded = certChain[i].GetEncoded();
					memoryStream.Write(encoded, 0, encoded.Length);
				}
				memoryStream.Close();
				base.Cert = memoryStream.ToArray();
				base.Contents = this.pkcs.GetEncodedPKCS1();
			}
			else
			{
				base.Contents = this.pkcs.GetEncodedPKCS7();
			}
			this.name = PdfPKCS7.GetSubjectFields(this.pkcs.SigningCertificate).GetField("CN");
			if (this.name != null)
			{
				base.Put(PdfName.NAME, new PdfString(this.name, "UnicodeBig"));
			}
			this.pkcs = new PdfPKCS7(privKey, certChain, crlList, this.hashAlgorithm, PdfName.ADBE_PKCS7_SHA1.Equals(base.Get(PdfName.SUBFILTER)));
			this.pkcs.SetExternalDigest(this.externalDigest, this.externalRSAdata, this.digestEncryptionAlgorithm);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000F0D05 File Offset: 0x000EFD05
		public void SetExternalDigest(byte[] digest, byte[] RSAdata, string digestEncryptionAlgorithm)
		{
			this.externalDigest = digest;
			this.externalRSAdata = RSAdata;
			this.digestEncryptionAlgorithm = digestEncryptionAlgorithm;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x000F0D1C File Offset: 0x000EFD1C
		public new string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x000F0D24 File Offset: 0x000EFD24
		public PdfPKCS7 Signer
		{
			get
			{
				return this.pkcs;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000F0D2C File Offset: 0x000EFD2C
		public byte[] SignerContents
		{
			get
			{
				if (PdfName.ADBE_X509_RSA_SHA1.Equals(base.Get(PdfName.SUBFILTER)))
				{
					return this.pkcs.GetEncodedPKCS1();
				}
				return this.pkcs.GetEncodedPKCS7();
			}
		}

		// Token: 0x04001B7F RID: 7039
		protected string hashAlgorithm;

		// Token: 0x04001B80 RID: 7040
		protected PdfPKCS7 pkcs;

		// Token: 0x04001B81 RID: 7041
		protected string name;

		// Token: 0x04001B82 RID: 7042
		private byte[] externalDigest;

		// Token: 0x04001B83 RID: 7043
		private byte[] externalRSAdata;

		// Token: 0x04001B84 RID: 7044
		private string digestEncryptionAlgorithm;

		// Token: 0x0200049A RID: 1178
		public class VeriSign : PdfSigGenericPKCS
		{
			// Token: 0x06002809 RID: 10249 RVA: 0x000F0D5C File Offset: 0x000EFD5C
			public VeriSign() : base(PdfName.VERISIGN_PPKVS, PdfName.ADBE_PKCS7_DETACHED)
			{
				this.hashAlgorithm = "MD5";
				base.Put(PdfName.R, new PdfNumber(65537));
			}
		}

		// Token: 0x0200049B RID: 1179
		public class PPKLite : PdfSigGenericPKCS
		{
			// Token: 0x0600280A RID: 10250 RVA: 0x000F0D8E File Offset: 0x000EFD8E
			public PPKLite() : base(PdfName.ADOBE_PPKLITE, PdfName.ADBE_X509_RSA_SHA1)
			{
				this.hashAlgorithm = "SHA1";
				base.Put(PdfName.R, new PdfNumber(65541));
			}
		}

		// Token: 0x0200049C RID: 1180
		public class PPKMS : PdfSigGenericPKCS
		{
			// Token: 0x0600280B RID: 10251 RVA: 0x000F0DC0 File Offset: 0x000EFDC0
			public PPKMS() : base(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1)
			{
				this.hashAlgorithm = "SHA1";
			}
		}
	}
}
