using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf.crypto;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000323 RID: 803
	public class PdfPublicKeySecurityHandler
	{
		// Token: 0x06001D39 RID: 7481 RVA: 0x000AF77C File Offset: 0x000AE77C
		public PdfPublicKeySecurityHandler()
		{
			this.seed = IVGenerator.GetIV(20);
			this.recipients = new List<PdfPublicKeyRecipient>();
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000AF79C File Offset: 0x000AE79C
		public void AddRecipient(PdfPublicKeyRecipient recipient)
		{
			this.recipients.Add(recipient);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000AF7AA File Offset: 0x000AE7AA
		protected internal byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000AF7BC File Offset: 0x000AE7BC
		public int GetRecipientsSize()
		{
			return this.recipients.Count;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000AF7CC File Offset: 0x000AE7CC
		public byte[] GetEncodedRecipient(int index)
		{
			PdfPublicKeyRecipient pdfPublicKeyRecipient = this.recipients[index];
			byte[] array = pdfPublicKeyRecipient.Cms;
			if (array != null)
			{
				return array;
			}
			X509Certificate certificate = pdfPublicKeyRecipient.Certificate;
			int num = pdfPublicKeyRecipient.Permission;
			int num2 = 3;
			num |= ((num2 == 3) ? -3904 : -64);
			num &= -4;
			num++;
			byte[] array2 = new byte[24];
			byte b = (byte)num;
			byte b2 = (byte)(num >> 8);
			byte b3 = (byte)(num >> 16);
			byte b4 = (byte)(num >> 24);
			Array.Copy(this.seed, 0, array2, 0, 20);
			array2[20] = b4;
			array2[21] = b3;
			array2[22] = b2;
			array2[23] = b;
			Asn1Object obj = this.CreateDERForRecipient(array2, certificate);
			MemoryStream memoryStream = new MemoryStream();
			DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
			derOutputStream.WriteObject(obj);
			array = memoryStream.ToArray();
			pdfPublicKeyRecipient.Cms = array;
			return array;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000AF8A0 File Offset: 0x000AE8A0
		public PdfArray GetEncodedRecipients()
		{
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < this.recipients.Count; i++)
			{
				try
				{
					byte[] encodedRecipient = this.GetEncodedRecipient(i);
					pdfArray.Add(new PdfLiteral(PdfContentByte.EscapeString(encodedRecipient)));
				}
				catch
				{
					pdfArray = null;
				}
			}
			return pdfArray;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000AF900 File Offset: 0x000AE900
		private Asn1Object CreateDERForRecipient(byte[] inp, X509Certificate cert)
		{
			string identifier = "1.2.840.113549.3.2";
			byte[] array = new byte[100];
			DerObjectIdentifier derObjectIdentifier = new DerObjectIdentifier(identifier);
			byte[] iv = IVGenerator.GetIV(16);
			IBufferedCipher cipher = CipherUtilities.GetCipher(derObjectIdentifier);
			KeyParameter parameters = new KeyParameter(iv);
			byte[] iv2 = IVGenerator.GetIV(cipher.GetBlockSize());
			ParametersWithIV parameters2 = new ParametersWithIV(parameters, iv2);
			cipher.Init(true, parameters2);
			int num = cipher.DoFinal(inp, array, 0);
			byte[] array2 = new byte[num];
			Array.Copy(array, 0, array2, 0, num);
			DerOctetString encryptedContent = new DerOctetString(array2);
			KeyTransRecipientInfo info = this.ComputeRecipientInfo(cert, iv);
			DerSet recipientInfos = new DerSet(new RecipientInfo(info));
			DerSequence parameters3 = new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
			{
				new Asn1Encodable[]
				{
					new DerInteger(58)
				},
				new Asn1Encodable[]
				{
					new DerOctetString(iv2)
				}
			});
			AlgorithmIdentifier contentEncryptionAlgorithm = new AlgorithmIdentifier(derObjectIdentifier, parameters3);
			EncryptedContentInfo encryptedContentInfo = new EncryptedContentInfo(PkcsObjectIdentifiers.Data, contentEncryptionAlgorithm, encryptedContent);
			EnvelopedData content = new EnvelopedData(null, recipientInfos, encryptedContentInfo, null);
			Org.BouncyCastle.Asn1.Cms.ContentInfo contentInfo = new Org.BouncyCastle.Asn1.Cms.ContentInfo(PkcsObjectIdentifiers.EnvelopedData, content);
			return contentInfo.ToAsn1Object();
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000AFA2C File Offset: 0x000AEA2C
		private KeyTransRecipientInfo ComputeRecipientInfo(X509Certificate x509certificate, byte[] abyte0)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(x509certificate.GetTbsCertificate()));
			TbsCertificateStructure instance = TbsCertificateStructure.GetInstance(asn1InputStream.ReadObject());
			AlgorithmIdentifier algorithmID = instance.SubjectPublicKeyInfo.AlgorithmID;
			Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber id = new Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value);
			IBufferedCipher cipher = CipherUtilities.GetCipher(algorithmID.ObjectID);
			cipher.Init(true, x509certificate.GetPublicKey());
			byte[] array = new byte[10000];
			int num = cipher.DoFinal(abyte0, array, 0);
			byte[] array2 = new byte[num];
			Array.Copy(array, 0, array2, 0, num);
			DerOctetString encryptedKey = new DerOctetString(array2);
			RecipientIdentifier rid = new RecipientIdentifier(id);
			return new KeyTransRecipientInfo(rid, algorithmID, encryptedKey);
		}

		// Token: 0x04001428 RID: 5160
		private const int SEED_LENGTH = 20;

		// Token: 0x04001429 RID: 5161
		private List<PdfPublicKeyRecipient> recipients;

		// Token: 0x0400142A RID: 5162
		private byte[] seed;
	}
}
