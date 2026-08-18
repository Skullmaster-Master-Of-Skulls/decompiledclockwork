using System;
using System.Collections;

namespace System.Net.Security
{
	// Token: 0x0200053D RID: 1341
	internal static class SslSessionsCache
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x000AA5A4 File Offset: 0x000A95A4
		internal static SafeFreeCredentials TryCachedCredential(byte[] thumbPrint, SchProtocols allowedProtocols)
		{
			if (SslSessionsCache.s_CachedCreds.Count == 0)
			{
				return null;
			}
			object key = new SslSessionsCache.SslCredKey(thumbPrint, allowedProtocols);
			SafeCredentialReference safeCredentialReference = SslSessionsCache.s_CachedCreds[key] as SafeCredentialReference;
			if (safeCredentialReference == null || safeCredentialReference.IsClosed || safeCredentialReference._Target.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference._Target;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000AA600 File Offset: 0x000A9600
		internal static void CacheCredential(SafeFreeCredentials creds, byte[] thumbPrint, SchProtocols allowedProtocols)
		{
			if (creds.IsInvalid)
			{
				return;
			}
			object key = new SslSessionsCache.SslCredKey(thumbPrint, allowedProtocols);
			SafeCredentialReference safeCredentialReference = SslSessionsCache.s_CachedCreds[key] as SafeCredentialReference;
			if (safeCredentialReference == null || safeCredentialReference.IsClosed || safeCredentialReference._Target.IsInvalid)
			{
				lock (SslSessionsCache.s_CachedCreds)
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

		// Token: 0x040027D6 RID: 10198
		private const int c_CheckExpiredModulo = 32;

		// Token: 0x040027D7 RID: 10199
		private static Hashtable s_CachedCreds = new Hashtable(32);

		// Token: 0x0200053E RID: 1342
		private struct SslCredKey
		{
			// Token: 0x06002902 RID: 10498 RVA: 0x000AA768 File Offset: 0x000A9768
			internal SslCredKey(byte[] thumbPrint, SchProtocols allowedProtocols)
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
				this._AllowedProtocols = allowedProtocols;
				this._HashCode ^= (int)this._AllowedProtocols;
			}

			// Token: 0x06002903 RID: 10499 RVA: 0x000AA827 File Offset: 0x000A9827
			public override int GetHashCode()
			{
				return this._HashCode;
			}

			// Token: 0x06002904 RID: 10500 RVA: 0x000AA82F File Offset: 0x000A982F
			public static bool operator ==(SslSessionsCache.SslCredKey sslCredKey1, SslSessionsCache.SslCredKey sslCredKey2)
			{
				return sslCredKey1 == sslCredKey2 || (sslCredKey1 != null && sslCredKey2 != null && sslCredKey1.Equals(sslCredKey2));
			}

			// Token: 0x06002905 RID: 10501 RVA: 0x000AA866 File Offset: 0x000A9866
			public static bool operator !=(SslSessionsCache.SslCredKey sslCredKey1, SslSessionsCache.SslCredKey sslCredKey2)
			{
				return sslCredKey1 != sslCredKey2 && (sslCredKey1 == null || sslCredKey2 == null || !sslCredKey1.Equals(sslCredKey2));
			}

			// Token: 0x06002906 RID: 10502 RVA: 0x000AA8A0 File Offset: 0x000A98A0
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
				for (int i = 0; i < this._CertThumbPrint.Length; i++)
				{
					if (this._CertThumbPrint[i] != sslCredKey._CertThumbPrint[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040027D8 RID: 10200
			private static readonly byte[] s_EmptyArray = new byte[0];

			// Token: 0x040027D9 RID: 10201
			private byte[] _CertThumbPrint;

			// Token: 0x040027DA RID: 10202
			private SchProtocols _AllowedProtocols;

			// Token: 0x040027DB RID: 10203
			private int _HashCode;
		}
	}
}
