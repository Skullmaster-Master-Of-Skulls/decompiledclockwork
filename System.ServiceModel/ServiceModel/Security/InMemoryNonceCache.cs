using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A6 RID: 678
	internal sealed class InMemoryNonceCache : NonceCache
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0004CBE5 File Offset: 0x0004ADE5
		public InMemoryNonceCache(TimeSpan cachingTime, int maxCachedNonces)
		{
			base.CacheSize = maxCachedNonces;
			base.CachingTimeSpan = cachingTime;
			this.cacheImpl = new InMemoryNonceCache.NonceCacheImpl(cachingTime, maxCachedNonces);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004CC08 File Offset: 0x0004AE08
		public override bool CheckNonce(byte[] nonce)
		{
			return this.cacheImpl.CheckNonce(nonce);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004CC16 File Offset: 0x0004AE16
		public override bool TryAddNonce(byte[] nonce)
		{
			return this.cacheImpl.TryAddNonce(nonce);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0004CC24 File Offset: 0x0004AE24
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			stringWriter.WriteLine("NonceCache:");
			stringWriter.WriteLine("   Caching Timespan: {0}", base.CachingTimeSpan);
			stringWriter.WriteLine("   Capacity: {0}", base.CacheSize);
			return stringWriter.ToString();
		}

		// Token: 0x04001AC1 RID: 6849
		private InMemoryNonceCache.NonceCacheImpl cacheImpl;

		// Token: 0x02000B3B RID: 2875
		internal sealed class NonceCacheImpl : TimeBoundedCache
		{
			// Token: 0x060070B1 RID: 28849 RVA: 0x001A3CCE File Offset: 0x001A1ECE
			public NonceCacheImpl(TimeSpan cachingTimeSpan, int maxCachedNonces) : base(InMemoryNonceCache.NonceCacheImpl.lowWaterMark, maxCachedNonces, InMemoryNonceCache.NonceCacheImpl.comparer, PurgingMode.AccessBasedPurge, TimeSpan.FromTicks(cachingTimeSpan.Ticks >> 2), false)
			{
				this.cachingTimeSpan = cachingTimeSpan;
			}

			// Token: 0x060070B2 RID: 28850 RVA: 0x001A3CF8 File Offset: 0x001A1EF8
			public bool TryAddNonce(byte[] nonce)
			{
				if (nonce == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("nonce");
				}
				if (nonce.Length < InMemoryNonceCache.NonceCacheImpl.minimumNonceLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("NonceLengthTooShort"));
				}
				DateTime expirationTime = TimeoutHelper.Add(DateTime.UtcNow, this.cachingTimeSpan);
				return base.TryAddItem(nonce, InMemoryNonceCache.NonceCacheImpl.dummyItem, expirationTime, false);
			}

			// Token: 0x060070B3 RID: 28851 RVA: 0x001A3D56 File Offset: 0x001A1F56
			public bool CheckNonce(byte[] nonce)
			{
				if (nonce == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("nonce");
				}
				if (nonce.Length < InMemoryNonceCache.NonceCacheImpl.minimumNonceLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("NonceLengthTooShort"));
				}
				return base.GetItem(nonce) != null;
			}

			// Token: 0x0400400C RID: 16396
			private static InMemoryNonceCache.NonceCacheImpl.NonceKeyComparer comparer = new InMemoryNonceCache.NonceCacheImpl.NonceKeyComparer();

			// Token: 0x0400400D RID: 16397
			private static object dummyItem = new object();

			// Token: 0x0400400E RID: 16398
			private static int lowWaterMark = 50;

			// Token: 0x0400400F RID: 16399
			private static int minimumNonceLength = 4;

			// Token: 0x04004010 RID: 16400
			private TimeSpan cachingTimeSpan;

			// Token: 0x02000EDB RID: 3803
			internal sealed class NonceKeyComparer : IEqualityComparer, IEqualityComparer<byte[]>
			{
				// Token: 0x060084AE RID: 33966 RVA: 0x001EA0C5 File Offset: 0x001E82C5
				public int GetHashCode(object o)
				{
					return this.GetHashCode((byte[])o);
				}

				// Token: 0x060084AF RID: 33967 RVA: 0x001EA0D4 File Offset: 0x001E82D4
				public int GetHashCode(byte[] o)
				{
					return (int)o[0] | (int)o[1] << 8 | (int)o[2] << 16 | (int)o[3] << 24;
				}

				// Token: 0x060084B0 RID: 33968 RVA: 0x001EA0FA File Offset: 0x001E82FA
				public int Compare(object x, object y)
				{
					return this.Compare((byte[])x, (byte[])y);
				}

				// Token: 0x060084B1 RID: 33969 RVA: 0x001EA110 File Offset: 0x001E8310
				public int Compare(byte[] x, byte[] y)
				{
					if (x == y)
					{
						return 0;
					}
					if (x == null)
					{
						return -1;
					}
					if (y == null)
					{
						return 1;
					}
					int num = x.Length;
					int num2 = y.Length;
					if (num == num2)
					{
						for (int i = 0; i < num; i++)
						{
							int num3 = (int)(x[i] - y[i]);
							if (num3 != 0)
							{
								return num3;
							}
						}
						return 0;
					}
					if (num > num2)
					{
						return 1;
					}
					return -1;
				}

				// Token: 0x060084B2 RID: 33970 RVA: 0x001EA168 File Offset: 0x001E8368
				public bool Equals(object x, object y)
				{
					return this.Compare(x, y) == 0;
				}

				// Token: 0x060084B3 RID: 33971 RVA: 0x001EA175 File Offset: 0x001E8375
				public bool Equals(byte[] x, byte[] y)
				{
					return this.Compare(x, y) == 0;
				}
			}
		}
	}
}
