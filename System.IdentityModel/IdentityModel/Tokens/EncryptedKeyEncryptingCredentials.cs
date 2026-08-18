using System;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011A RID: 282
	public class EncryptedKeyEncryptingCredentials : EncryptingCredentials
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x0002093B File Offset: 0x0001EB3B
		public EncryptedKeyEncryptingCredentials(X509Certificate2 certificate) : this(new X509EncryptingCredentials(certificate), 256, "http://www.w3.org/2001/04/xmlenc#aes256-cbc")
		{
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00020953 File Offset: 0x0001EB53
		public EncryptedKeyEncryptingCredentials(X509Certificate2 certificate, string keyWrappingAlgorithm, int keySizeInBits, string encryptionAlgorithm) : this(new X509EncryptingCredentials(certificate, keyWrappingAlgorithm), keySizeInBits, encryptionAlgorithm)
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00020968 File Offset: 0x0001EB68
		public EncryptedKeyEncryptingCredentials(EncryptingCredentials wrappingCredentials, int keySizeInBits, string encryptionAlgorithm)
		{
			if (wrappingCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappingCredentials");
			}
			if (encryptionAlgorithm == "http://www.w3.org/2001/04/xmlenc#des-cbc" || encryptionAlgorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc" || encryptionAlgorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes")
			{
				this._keyBytes = CryptoHelper.KeyGenerator.GenerateDESKey(keySizeInBits);
			}
			else
			{
				this._keyBytes = CryptoHelper.KeyGenerator.GenerateSymmetricKey(keySizeInBits);
			}
			base.SecurityKey = new InMemorySymmetricSecurityKey(this._keyBytes);
			this._wrappingCredentials = wrappingCredentials;
			byte[] encryptedKey = this._wrappingCredentials.SecurityKey.EncryptKey(this._wrappingCredentials.Algorithm, this._keyBytes);
			base.SecurityKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				new EncryptedKeyIdentifierClause(encryptedKey, this._wrappingCredentials.Algorithm, this._wrappingCredentials.SecurityKeyIdentifier)
			});
			base.Algorithm = encryptionAlgorithm;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00020A40 File Offset: 0x0001EC40
		public EncryptingCredentials WrappingCredentials
		{
			get
			{
				return this._wrappingCredentials;
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private EncryptingCredentials _wrappingCredentials;

		// Token: 0x04000AD7 RID: 2775
		private byte[] _keyBytes;
	}
}
