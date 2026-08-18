using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018F RID: 399
	public class X509CertificateStoreTokenResolver : SecurityTokenResolver
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x0003CE84 File Offset: 0x0003B084
		public X509CertificateStoreTokenResolver() : this(System.Security.Cryptography.X509Certificates.StoreName.My, StoreLocation.LocalMachine)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003CE8E File Offset: 0x0003B08E
		public X509CertificateStoreTokenResolver(StoreName storeName, StoreLocation storeLocation) : this(Enum.GetName(typeof(StoreName), storeName), storeLocation)
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		public X509CertificateStoreTokenResolver(string storeName, StoreLocation storeLocation)
		{
			if (string.IsNullOrEmpty(storeName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("storeName");
			}
			this.storeName = storeName;
			this.storeLocation = storeLocation;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0003CED5 File Offset: 0x0003B0D5
		public string StoreName
		{
			get
			{
				return this.storeName;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0003CEDD File Offset: 0x0003B0DD
		public StoreLocation StoreLocation
		{
			get
			{
				return this.storeLocation;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003CEE8 File Offset: 0x0003B0E8
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			key = null;
			EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifierClause as EncryptedKeyIdentifierClause;
			if (encryptedKeyIdentifierClause != null)
			{
				SecurityKeyIdentifier encryptingKeyIdentifier = encryptedKeyIdentifierClause.EncryptingKeyIdentifier;
				if (encryptingKeyIdentifier != null && encryptingKeyIdentifier.Count > 0)
				{
					for (int i = 0; i < encryptingKeyIdentifier.Count; i++)
					{
						SecurityKey securityKey = null;
						if (base.TryResolveSecurityKey(encryptingKeyIdentifier[i], out securityKey))
						{
							byte[] encryptedKey = encryptedKeyIdentifierClause.GetEncryptedKey();
							string encryptionMethod = encryptedKeyIdentifierClause.EncryptionMethod;
							byte[] symmetricKey = securityKey.DecryptKey(encryptionMethod, encryptedKey);
							key = new InMemorySymmetricSecurityKey(symmetricKey, false);
							return true;
						}
					}
				}
			}
			else
			{
				SecurityToken securityToken = null;
				if (base.TryResolveToken(keyIdentifierClause, out securityToken) && securityToken.SecurityKeys.Count > 0)
				{
					key = securityToken.SecurityKeys[0];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003CFA8 File Offset: 0x0003B1A8
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			token = null;
			foreach (SecurityKeyIdentifierClause keyIdentifierClause in keyIdentifier)
			{
				if (base.TryResolveToken(keyIdentifierClause, out token))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003D010 File Offset: 0x0003B210
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			token = null;
			X509Store x509Store = null;
			X509Certificate2Collection x509Certificate2Collection = null;
			try
			{
				x509Store = new X509Store(this.storeName, this.storeLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				x509Certificate2Collection = x509Store.Certificates;
				foreach (X509Certificate2 certificate in x509Certificate2Collection)
				{
					X509ThumbprintKeyIdentifierClause x509ThumbprintKeyIdentifierClause = keyIdentifierClause as X509ThumbprintKeyIdentifierClause;
					if (x509ThumbprintKeyIdentifierClause != null && x509ThumbprintKeyIdentifierClause.Matches(certificate))
					{
						token = new X509SecurityToken(certificate);
						return true;
					}
					X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = keyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
					if (x509IssuerSerialKeyIdentifierClause != null && x509IssuerSerialKeyIdentifierClause.Matches(certificate))
					{
						token = new X509SecurityToken(certificate);
						return true;
					}
					X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause = keyIdentifierClause as X509SubjectKeyIdentifierClause;
					if (x509SubjectKeyIdentifierClause != null && x509SubjectKeyIdentifierClause.Matches(certificate))
					{
						token = new X509SecurityToken(certificate);
						return true;
					}
					X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = keyIdentifierClause as X509RawDataKeyIdentifierClause;
					if (x509RawDataKeyIdentifierClause != null && x509RawDataKeyIdentifierClause.Matches(certificate))
					{
						token = new X509SecurityToken(certificate);
						return true;
					}
				}
			}
			finally
			{
				if (x509Certificate2Collection != null)
				{
					for (int i = 0; i < x509Certificate2Collection.Count; i++)
					{
						x509Certificate2Collection[i].Reset();
					}
				}
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return false;
		}

		// Token: 0x04000CAB RID: 3243
		private string storeName;

		// Token: 0x04000CAC RID: 3244
		private StoreLocation storeLocation;
	}
}
