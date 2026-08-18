using System;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x0200018E RID: 398
	internal class ColumnMasterKeyMetadataSignatureVerificationCache
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000A9708 File Offset: 0x000A8B08
		internal static ColumnMasterKeyMetadataSignatureVerificationCache Instance
		{
			get
			{
				return ColumnMasterKeyMetadataSignatureVerificationCache._signatureVerificationCache;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000A971C File Offset: 0x000A8B1C
		private ColumnMasterKeyMetadataSignatureVerificationCache()
		{
			this._cache = new MemoryCache("ColumnMasterKeyMetadataSignatureVerificationCache", null);
			this._inTrim = 0;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000A9748 File Offset: 0x000A8B48
		internal bool? GetSignatureVerificationResult(string keyStoreName, string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			this.ValidateStringArgumentNotNullOrEmpty(masterKeyPath, "masterKeyPath", "GetSignatureVerificationResult");
			this.ValidateStringArgumentNotNullOrEmpty(keyStoreName, "keyStoreName", "GetSignatureVerificationResult");
			this.ValidateSignatureNotNullOrEmpty(signature, "GetSignatureVerificationResult");
			string cacheLookupKey = this.GetCacheLookupKey(masterKeyPath, allowEnclaveComputations, signature, keyStoreName);
			return this._cache.Get(cacheLookupKey, null) as bool?;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000A97A8 File Offset: 0x000A8BA8
		internal void AddSignatureVerificationResult(string keyStoreName, string masterKeyPath, bool allowEnclaveComputations, byte[] signature, bool result)
		{
			this.ValidateStringArgumentNotNullOrEmpty(masterKeyPath, "masterKeyPath", "AddSignatureVerificationResult");
			this.ValidateStringArgumentNotNullOrEmpty(keyStoreName, "keyStoreName", "AddSignatureVerificationResult");
			this.ValidateSignatureNotNullOrEmpty(signature, "AddSignatureVerificationResult");
			string cacheLookupKey = this.GetCacheLookupKey(masterKeyPath, allowEnclaveComputations, signature, keyStoreName);
			this.TrimCacheIfNeeded();
			this._cache.Set(cacheLookupKey, result, DateTimeOffset.UtcNow.AddDays(10.0), null);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000A9820 File Offset: 0x000A8C20
		private void ValidateSignatureNotNullOrEmpty(byte[] signature, string methodName)
		{
			if (signature != null && signature.Length != 0)
			{
				return;
			}
			if (signature == null)
			{
				throw SQL.NullArgumentInternal("signature", "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
			}
			throw SQL.EmptyArgumentInternal("signature", "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000A985C File Offset: 0x000A8C5C
		private void ValidateStringArgumentNotNullOrEmpty(string stringArgValue, string stringArgName, string methodName)
		{
			if (!string.IsNullOrWhiteSpace(stringArgValue))
			{
				return;
			}
			if (stringArgValue == null)
			{
				throw SQL.NullArgumentInternal(stringArgName, "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
			}
			throw SQL.EmptyArgumentInternal(stringArgName, "ColumnMasterKeyMetadataSignatureVerificationCache", methodName);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000A9890 File Offset: 0x000A8C90
		private void TrimCacheIfNeeded()
		{
			long count = this._cache.GetCount(null);
			if (count > 2300L && Interlocked.CompareExchange(ref this._inTrim, 1, 0) == 0)
			{
				try
				{
					this._cache.Trim((int)((double)(count - 2000L) / (double)count * 100.0));
				}
				finally
				{
					Interlocked.CompareExchange(ref this._inTrim, 0, 1);
				}
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000A9914 File Offset: 0x000A8D14
		private string GetCacheLookupKey(string masterKeyPath, bool allowEnclaveComputations, byte[] signature, string keyStoreName)
		{
			StringBuilder stringBuilder = new StringBuilder(keyStoreName, keyStoreName.Length + masterKeyPath.Length + SqlSecurityUtility.GetBase64LengthFromByteLength(signature.Length) + 3 + 10);
			stringBuilder.Append(":");
			stringBuilder.Append(masterKeyPath);
			stringBuilder.Append(":");
			stringBuilder.Append(allowEnclaveComputations);
			stringBuilder.Append(":");
			stringBuilder.Append(Convert.ToBase64String(signature));
			stringBuilder.Append(":");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E5F RID: 3679
		private const int _cacheSize = 2000;

		// Token: 0x04000E60 RID: 3680
		private const int _cacheTrimThreshold = 300;

		// Token: 0x04000E61 RID: 3681
		private const string _className = "ColumnMasterKeyMetadataSignatureVerificationCache";

		// Token: 0x04000E62 RID: 3682
		private const string _getSignatureVerificationResultMethodName = "GetSignatureVerificationResult";

		// Token: 0x04000E63 RID: 3683
		private const string _addSignatureVerificationResultMethodName = "AddSignatureVerificationResult";

		// Token: 0x04000E64 RID: 3684
		private const string _masterkeypathArgumentName = "masterKeyPath";

		// Token: 0x04000E65 RID: 3685
		private const string _keyStoreNameArgumentName = "keyStoreName";

		// Token: 0x04000E66 RID: 3686
		private const string _signatureName = "signature";

		// Token: 0x04000E67 RID: 3687
		private const string _cacheLookupKeySeparator = ":";

		// Token: 0x04000E68 RID: 3688
		private static readonly ColumnMasterKeyMetadataSignatureVerificationCache _signatureVerificationCache = new ColumnMasterKeyMetadataSignatureVerificationCache();

		// Token: 0x04000E69 RID: 3689
		private readonly MemoryCache _cache;

		// Token: 0x04000E6A RID: 3690
		private int _inTrim;
	}
}
