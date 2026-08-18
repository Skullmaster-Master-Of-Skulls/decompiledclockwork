using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020000A5 RID: 165
	internal class FipsAwareEncryptedXml : EncryptedXml
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001F34D File Offset: 0x0001D54D
		public FipsAwareEncryptedXml(XmlDocument doc) : base(doc)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001F358 File Offset: 0x0001D558
		public override SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			bool flag = FipsAwareEncryptedXml.IsAesDetected(encryptedData, symmetricAlgorithmUri);
			if (flag)
			{
				EncryptedKey encryptedKey = null;
				foreach (object obj in encryptedData.KeyInfo)
				{
					KeyInfoEncryptedKey keyInfoEncryptedKey = obj as KeyInfoEncryptedKey;
					if (keyInfoEncryptedKey != null)
					{
						encryptedKey = keyInfoEncryptedKey.EncryptedKey;
						break;
					}
				}
				if (encryptedKey != null)
				{
					byte[] array = this.DecryptEncryptedKey(encryptedKey);
					if (array != null)
					{
						return new AesCryptoServiceProvider
						{
							Key = array
						};
					}
				}
			}
			return base.GetDecryptionKey(encryptedData, symmetricAlgorithmUri);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		private static bool IsAesDetected(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData != null && encryptedData.KeyInfo != null && (symmetricAlgorithmUri != null || encryptedData.EncryptionMethod != null))
			{
				if (symmetricAlgorithmUri == null)
				{
					symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
				}
				return string.Equals(symmetricAlgorithmUri, "http://www.w3.org/2001/04/xmlenc#aes256-cbc", StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}
	}
}
