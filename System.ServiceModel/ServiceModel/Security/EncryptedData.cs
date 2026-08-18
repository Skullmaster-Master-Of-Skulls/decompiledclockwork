using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027E RID: 638
	internal class EncryptedData : EncryptedType
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000434E7 File Offset: 0x000416E7
		protected override XmlDictionaryString OpeningElementName
		{
			get
			{
				return EncryptedData.ElementName;
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000434EE File Offset: 0x000416EE
		private void EnsureDecryptionSet()
		{
			if (base.State == EncryptedType.EncryptionState.DecryptionSetup)
			{
				this.SetPlainText();
				return;
			}
			if (base.State != EncryptedType.EncryptionState.Decrypted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadEncryptionState")));
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00043523 File Offset: 0x00041723
		protected override void ForceEncryption()
		{
			CryptoHelper.GenerateIVAndEncrypt(this.algorithm, this.buffer, out this.iv, out this.cipherText);
			base.State = EncryptedType.EncryptionState.Encrypted;
			this.buffer = new ArraySegment<byte>(CryptoHelper.EmptyBuffer);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00043559 File Offset: 0x00041759
		public byte[] GetDecryptedBuffer()
		{
			this.EnsureDecryptionSet();
			return this.decryptedBuffer;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00043567 File Offset: 0x00041767
		protected override void ReadCipherData(XmlDictionaryReader reader)
		{
			this.cipherText = reader.ReadContentAsBase64();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00043575 File Offset: 0x00041775
		protected override void ReadCipherData(XmlDictionaryReader reader, long maxBufferSize)
		{
			this.cipherText = SecurityUtils.ReadContentAsBase64(reader, maxBufferSize);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00043584 File Offset: 0x00041784
		private void SetPlainText()
		{
			this.decryptedBuffer = CryptoHelper.ExtractIVAndDecrypt(this.algorithm, this.cipherText, 0, this.cipherText.Length);
			base.State = EncryptedType.EncryptionState.Decrypted;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000435B0 File Offset: 0x000417B0
		public void SetUpDecryption(SymmetricAlgorithm algorithm)
		{
			if (base.State != EncryptedType.EncryptionState.Read)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadEncryptionState")));
			}
			if (algorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
			}
			this.algorithm = algorithm;
			base.State = EncryptedType.EncryptionState.DecryptionSetup;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00043604 File Offset: 0x00041804
		public void SetUpEncryption(SymmetricAlgorithm algorithm, ArraySegment<byte> buffer)
		{
			if (base.State != EncryptedType.EncryptionState.New)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadEncryptionState")));
			}
			if (algorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
			}
			this.algorithm = algorithm;
			this.buffer = buffer;
			base.State = EncryptedType.EncryptionState.EncryptionSetup;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004365B File Offset: 0x0004185B
		protected override void WriteCipherData(XmlDictionaryWriter writer)
		{
			writer.WriteBase64(this.iv, 0, this.iv.Length);
			writer.WriteBase64(this.cipherText, 0, this.cipherText.Length);
		}

		// Token: 0x040019DB RID: 6619
		internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.EncryptedData;

		// Token: 0x040019DC RID: 6620
		internal static readonly string ElementType = "http://www.w3.org/2001/04/xmlenc#Element";

		// Token: 0x040019DD RID: 6621
		internal static readonly string ContentType = "http://www.w3.org/2001/04/xmlenc#Content";

		// Token: 0x040019DE RID: 6622
		private SymmetricAlgorithm algorithm;

		// Token: 0x040019DF RID: 6623
		private byte[] decryptedBuffer;

		// Token: 0x040019E0 RID: 6624
		private ArraySegment<byte> buffer;

		// Token: 0x040019E1 RID: 6625
		private byte[] iv;

		// Token: 0x040019E2 RID: 6626
		private byte[] cipherText;
	}
}
