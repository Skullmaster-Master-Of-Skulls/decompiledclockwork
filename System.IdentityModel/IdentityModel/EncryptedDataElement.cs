using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000038 RID: 56
	internal class EncryptedDataElement : EncryptedTypeElement
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000896D File Offset: 0x00006B6D
		public static bool CanReadFrom(XmlReader reader)
		{
			return reader != null && reader.IsStartElement("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008984 File Offset: 0x00006B84
		public EncryptedDataElement() : this(null)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000898D File Offset: 0x00006B8D
		public EncryptedDataElement(SecurityTokenSerializer tokenSerializer) : base(tokenSerializer)
		{
			base.KeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				new EmptySecurityKeyIdentifierClause()
			});
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000089B0 File Offset: 0x00006BB0
		public byte[] Decrypt(SymmetricAlgorithm algorithm)
		{
			if (algorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
			}
			if (base.CipherData == null || base.CipherData.CipherValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID6000")));
			}
			byte[] cipherValue = base.CipherData.CipherValue;
			return EncryptedDataElement.ExtractIVAndDecrypt(algorithm, cipherValue, 0, cipherValue.Length);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008A18 File Offset: 0x00006C18
		public void Encrypt(SymmetricAlgorithm algorithm, byte[] buffer, int offset, int length)
		{
			byte[] iv;
			byte[] cipherText;
			EncryptedDataElement.GenerateIVAndEncrypt(algorithm, buffer, offset, length, out iv, out cipherText);
			base.CipherData.SetCipherValueFragments(iv, cipherText);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008A40 File Offset: 0x00006C40
		private static byte[] ExtractIVAndDecrypt(SymmetricAlgorithm algorithm, byte[] cipherText, int offset, int count)
		{
			byte[] array = new byte[algorithm.BlockSize / 8];
			if (cipherText.Length - offset < array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID6019", new object[]
				{
					cipherText.Length - offset,
					array.Length
				})));
			}
			Buffer.BlockCopy(cipherText, offset, array, 0, array.Length);
			algorithm.Padding = PaddingMode.ISO10126;
			algorithm.Mode = CipherMode.CBC;
			ICryptoTransform cryptoTransform = null;
			byte[] result = null;
			try
			{
				cryptoTransform = algorithm.CreateDecryptor(algorithm.Key, array);
				result = cryptoTransform.TransformFinalBlock(cipherText, offset + array.Length, count - array.Length);
			}
			finally
			{
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008AF8 File Offset: 0x00006CF8
		private static void GenerateIVAndEncrypt(SymmetricAlgorithm algorithm, byte[] plainText, int offset, int length, out byte[] iv, out byte[] cipherText)
		{
			RandomNumberGenerator randomNumberGenerator = CryptoHelper.RandomNumberGenerator;
			int num = algorithm.BlockSize / 8;
			iv = new byte[num];
			randomNumberGenerator.GetBytes(iv);
			algorithm.Padding = PaddingMode.PKCS7;
			algorithm.Mode = CipherMode.CBC;
			ICryptoTransform cryptoTransform = algorithm.CreateEncryptor(algorithm.Key, iv);
			cipherText = cryptoTransform.TransformFinalBlock(plainText, offset, length);
			cryptoTransform.Dispose();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000024C1 File Offset: 0x000006C1
		public override void ReadExtensions(XmlDictionaryReader reader)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008B58 File Offset: 0x00006D58
		public override void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!reader.IsStartElement("EncryptedData", "http://www.w3.org/2001/04/xmlenc#"))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4193"));
			}
			base.ReadXml(reader);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008BAC File Offset: 0x00006DAC
		public virtual void WriteXml(XmlWriter writer, SecurityTokenSerializer securityTokenSerializer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (securityTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenSerializer");
			}
			if (base.KeyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID6001")));
			}
			writer.WriteStartElement("xenc", "EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(base.Id))
			{
				writer.WriteAttributeString("Id", null, base.Id);
			}
			if (!string.IsNullOrEmpty(base.Type))
			{
				writer.WriteAttributeString("Type", null, base.Type);
			}
			if (base.EncryptionMethod != null)
			{
				base.EncryptionMethod.WriteXml(writer);
			}
			if (base.KeyIdentifier != null)
			{
				securityTokenSerializer.WriteKeyIdentifier(XmlDictionaryWriter.CreateDictionaryWriter(writer), base.KeyIdentifier);
			}
			base.CipherData.WriteXml(writer);
			writer.WriteEndElement();
		}
	}
}
