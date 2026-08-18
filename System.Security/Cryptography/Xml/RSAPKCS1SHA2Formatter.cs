using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000CD RID: 205
	internal class RSAPKCS1SHA2Formatter : AsymmetricSignatureFormatter
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00019881 File Offset: 0x00018881
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this._key = (RSA)key;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001988F File Offset: 0x0001888F
		public override void SetHashAlgorithm(string strName)
		{
			this._hashAlgorithm = strName;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00019898 File Offset: 0x00018898
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = this._key as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				using (RSACryptoServiceProvider rsacryptoServiceProvider2 = RSAPKCS1SHA2Formatter.UpgradeCspIfNeeded(rsacryptoServiceProvider))
				{
					RSACryptoServiceProvider rsacryptoServiceProvider3 = rsacryptoServiceProvider2 ?? rsacryptoServiceProvider;
					return rsacryptoServiceProvider3.SignHash(rgbHash, this._hashAlgorithm);
				}
			}
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = new RSAPKCS1SignatureFormatter(this._key);
			asymmetricSignatureFormatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureFormatter.CreateSignature(rgbHash);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00019910 File Offset: 0x00018910
		private static bool ShouldUpgrade(CspKeyContainerInfo keyContainerInfo)
		{
			int providerType = keyContainerInfo.ProviderType;
			switch (providerType)
			{
			case 1:
			case 2:
				break;
			default:
				if (providerType != 12)
				{
					return providerType == 24 && false;
				}
				break;
			}
			string providerName = keyContainerInfo.ProviderName;
			StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
			return providerName.Equals("Microsoft Base Cryptographic Provider v1.0", comparisonType) || providerName.Equals("Microsoft RSA Schannel Cryptographic Provider", comparisonType) || providerName.Equals("Microsoft RSA Signature Cryptographic Provider", comparisonType) || providerName.Equals("Microsoft Enhanced Cryptographic Provider v1.0", comparisonType) || providerName.Equals("Microsoft Strong Cryptographic Provider", comparisonType) || providerName.Equals("Microsoft Enhanced RSA and AES Cryptographic Provider", comparisonType) || providerName.Equals("Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)", comparisonType);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000199B0 File Offset: 0x000189B0
		private static RSACryptoServiceProvider UpgradeCspIfNeeded(RSACryptoServiceProvider rsaCsp)
		{
			CspKeyContainerInfo cspKeyContainerInfo = rsaCsp.CspKeyContainerInfo;
			if (!RSAPKCS1SHA2Formatter.ShouldUpgrade(cspKeyContainerInfo))
			{
				return null;
			}
			CspParameters cspParameters = new CspParameters(24);
			cspParameters.KeyContainerName = cspKeyContainerInfo.KeyContainerName;
			cspParameters.Flags = CspProviderFlags.UseExistingKey;
			if (cspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			cspParameters.KeyNumber = (int)cspKeyContainerInfo.KeyNumber;
			RSACryptoServiceProvider result;
			try
			{
				result = new RSACryptoServiceProvider(cspParameters);
			}
			catch (CryptographicException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040005D9 RID: 1497
		private RSA _key;

		// Token: 0x040005DA RID: 1498
		private string _hashAlgorithm;
	}
}
