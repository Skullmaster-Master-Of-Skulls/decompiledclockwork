using System;
using System.Collections;

namespace System.Net.Security
{
	// Token: 0x02000352 RID: 850
	internal static class SslSessionsCache
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x0008FCAC File Offset: 0x0008DEAC
		internal static SafeFreeCredentials TryCachedCredential(byte[] thumbPrint, SchProtocols allowedProtocols, EncryptionPolicy encryptionPolicy)
		{
			if (SslSessionsCache.s_CachedCreds.Count == 0)
			{
				return null;
			}
			object key = new SslSessionsCache.SslCredKey(thumbPrint, allowedProtocols, encryptionPolicy);
			SafeCredentialReference safeCredentialReference = SslSessionsCache.s_CachedCreds[key] as SafeCredentialReference;
			if (safeCredentialReference == null || safeCredentialReference.IsClosed || safeCredentialReference._Target.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference._Target;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0008FD08 File Offset: 0x0008DF08
		internal static void CacheCredential(SafeFreeCredentials creds, byte[] thumbPrint, SchProtocols allowedProtocols, EncryptionPolicy encryptionPolicy)
		{
			if (creds.IsInvalid)
			{
				return;
			}
			object key = new SslSessionsCache.SslCredKey(thumbPrint, allowedProtocols, encryptionPolicy);
			SafeCredentialReference safeCredentialReference = SslSessionsCache.s_CachedCreds[key] as SafeCredentialReference;
			if (safeCredentialReference == null || safeCredentialReference.IsClosed || safeCredentialReference._Target.IsInvalid)
			{
				Hashtable obj = SslSessionsCache.s_CachedCreds;
				lock (obj)
				{
					safeCredentialReference = (SslSessionsCache.s_CachedCreds[key] as SafeCredentialReference);
					if (safeCredentialReference == null || safeCredentialReference.IsClosed)
					{
						safeCredentialReference = SafeCredentialReference.CreateReference(creds);
						if (safeCredentialReference != null)
						{
							SslSessionsCache.s_CachedCreds[key] = safeCredentialReference;
							if (SslSessionsCache.s_CachedCreds.Count % 32 == 0)
							{
								DictionaryEntry[] array = new DictionaryEntry[SslSessionsCache.s_CachedCreds.Count];
								SslSessionsCache.s_CachedCreds.CopyTo(array, 0);
								for (int i = 0; i < array.Length; i++)
								{
									safeCredentialReference = (array[i].Value as SafeCredentialReference);
									if (safeCredentialReference != null)
									{
										creds = safeCredentialReference._Target;
										safeCredentialReference.Close();
										if (!creds.IsClosed && !creds.IsInvalid && (safeCredentialReference = SafeCredentialReference.CreateReference(creds)) != null)
										{
											SslSessionsCache.s_CachedCreds[array[i].Key] = safeCredentialReference;
										}
										else
										{
											SslSessionsCache.s_CachedCreds.Remove(array[i].Key);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001CEC RID: 7404
		private const int c_CheckExpiredModulo = 32;

		// Token: 0x04001CED RID: 7405
		private static Hashtable s_CachedCreds = new Hashtable(32);

		// Token: 0x020007CD RID: 1997
		private struct SslCredKey
		{
			// Token: 0x060043A8 RID: 17320 RVA: 0x0011D424 File Offset: 0x0011B624
			internal SslCredKey(byte[] thumbPrint, SchProtocols allowedProtocols, EncryptionPolicy encryptionPolicy)
			{
				this._CertThumbPrint = ((thumbPrint == null) ? SslSessionsCache.SslCredKey.s_EmptyArray : thumbPrint);
				this._HashCode = 0;
				if (thumbPrint != null)
				{
					this._HashCode ^= (int)this._CertThumbPrint[0];
					if (1 < this._CertThumbPrint.Length)
					{
						this._HashCode ^= (int)this._CertThumbPrint[1] << 8;
					}
					if (2 < this._CertThumbPrint.Length)
					{
						this._HashCode ^= (int)this._CertThumbPrint[2] << 16;
					}
					if (3 < this._CertThumbPrint.Length)
					{
						this._HashCode ^= (int)this._CertThumbPrint[3] << 24;
					}
				}
				this._HashCode ^= (int)allowedProtocols;
				this._HashCode ^= (int)encryptionPolicy;
				this._AllowedProtocols = allowedProtocols;
				this._EncryptionPolicy = encryptionPolicy;
			}

			// Token: 0x060043A9 RID: 17321 RVA: 0x0011D4F3 File Offset: 0x0011B6F3
			public override int GetHashCode()
			{
				return this._HashCode;
			}

			// Token: 0x060043AA RID: 17322 RVA: 0x0011D4FB File Offset: 0x0011B6FB
			public static bool operator ==(SslSessionsCache.SslCredKey sslCredKey1, SslSessionsCache.SslCredKey sslCredKey2)
			{
				return sslCredKey1 == sslCredKey2 || (sslCredKey1 != null && sslCredKey2 != null && sslCredKey1.Equals(sslCredKey2));
			}

			// Token: 0x060043AB RID: 17323 RVA: 0x0011D532 File Offset: 0x0011B732
			public static bool operator !=(SslSessionsCache.SslCredKey sslCredKey1, SslSessionsCache.SslCredKey sslCredKey2)
			{
				return sslCredKey1 != sslCredKey2 && (sslCredKey1 == null || sslCredKey2 == null || !sslCredKey1.Equals(sslCredKey2));
			}

			// Token: 0x060043AC RID: 17324 RVA: 0x0011D56C File Offset: 0x0011B76C
			public override bool Equals(object y)
			{
				SslSessionsCache.SslCredKey sslCredKey = (SslSessionsCache.SslCredKey)y;
				if (this._CertThumbPrint.Length != sslCredKey._CertThumbPrint.Length)
				{
					return false;
				}
				if (this._HashCode != sslCredKey._HashCode)
				{
					return false;
				}
				if (this._EncryptionPolicy != sslCredKey._EncryptionPolicy)
				{
					return false;
				}
				if (this._AllowedProtocols != sslCredKey._AllowedProtocols)
				{
					return false;
				}
				for (int i = 0; i < this._CertThumbPrint.Length; i++)
				{
					if (this._CertThumbPrint[i] != sslCredKey._CertThumbPrint[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04003484 RID: 13444
			private static readonly byte[] s_EmptyArray = new byte[0];

			// Token: 0x04003485 RID: 13445
			private byte[] _CertThumbPrint;

			// Token: 0x04003486 RID: 13446
			private SchProtocols _AllowedProtocols;

			// Token: 0x04003487 RID: 13447
			private EncryptionPolicy _EncryptionPolicy;

			// Token: 0x04003488 RID: 13448
			private int _HashCode;
		}
	}
}
