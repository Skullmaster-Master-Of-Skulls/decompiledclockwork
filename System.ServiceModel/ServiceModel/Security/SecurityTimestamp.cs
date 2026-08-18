using System;
using System.Globalization;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B2 RID: 690
	internal sealed class SecurityTimestamp
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x00051603 File Offset: 0x0004F803
		public SecurityTimestamp(DateTime creationTimeUtc, DateTime expiryTimeUtc, string id) : this(creationTimeUtc, expiryTimeUtc, id, null, null)
		{
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00051610 File Offset: 0x0004F810
		internal SecurityTimestamp(DateTime creationTimeUtc, DateTime expiryTimeUtc, string id, string digestAlgorithm, byte[] digest)
		{
			if (creationTimeUtc > expiryTimeUtc)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ArgumentOutOfRangeException("recordedExpiryTime", SR.GetString("CreationTimeUtcIsAfterExpiryTime")));
			}
			this.creationTimeUtc = creationTimeUtc;
			this.expiryTimeUtc = expiryTimeUtc;
			this.id = id;
			this.digestAlgorithm = digestAlgorithm;
			this.digest = digest;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00051670 File Offset: 0x0004F870
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.creationTimeUtc;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00051678 File Offset: 0x0004F878
		public DateTime ExpiryTimeUtc
		{
			get
			{
				return this.expiryTimeUtc;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00051680 File Offset: 0x0004F880
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00051688 File Offset: 0x0004F888
		public string DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00051690 File Offset: 0x0004F890
		internal byte[] GetDigest()
		{
			return this.digest;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00051698 File Offset: 0x0004F898
		internal char[] GetCreationTimeChars()
		{
			if (this.computedCreationTimeUtc == null)
			{
				this.computedCreationTimeUtc = SecurityTimestamp.ToChars(ref this.creationTimeUtc);
			}
			return this.computedCreationTimeUtc;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000516B9 File Offset: 0x0004F8B9
		internal char[] GetExpiryTimeChars()
		{
			if (this.computedExpiryTimeUtc == null)
			{
				this.computedExpiryTimeUtc = SecurityTimestamp.ToChars(ref this.expiryTimeUtc);
			}
			return this.computedExpiryTimeUtc;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000516DC File Offset: 0x0004F8DC
		private static char[] ToChars(ref DateTime utcTime)
		{
			char[] array = new char["yyyy-MM-ddTHH:mm:ss.fffZ".Length];
			int num = 0;
			SecurityTimestamp.ToChars(utcTime.Year, array, ref num, 4);
			array[num++] = '-';
			SecurityTimestamp.ToChars(utcTime.Month, array, ref num, 2);
			array[num++] = '-';
			SecurityTimestamp.ToChars(utcTime.Day, array, ref num, 2);
			array[num++] = 'T';
			SecurityTimestamp.ToChars(utcTime.Hour, array, ref num, 2);
			array[num++] = ':';
			SecurityTimestamp.ToChars(utcTime.Minute, array, ref num, 2);
			array[num++] = ':';
			SecurityTimestamp.ToChars(utcTime.Second, array, ref num, 2);
			array[num++] = '.';
			SecurityTimestamp.ToChars(utcTime.Millisecond, array, ref num, 3);
			array[num++] = 'Z';
			return array;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000517A4 File Offset: 0x0004F9A4
		private static void ToChars(int n, char[] buffer, ref int offset, int count)
		{
			for (int i = offset + count - 1; i >= offset; i--)
			{
				buffer[i] = (char)(48 + n % 10);
				n /= 10;
			}
			offset += count;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000517DA File Offset: 0x0004F9DA
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SecurityTimestamp: Id={0}, CreationTimeUtc={1}, ExpirationTimeUtc={2}", new object[]
			{
				this.Id,
				XmlConvert.ToString(this.CreationTimeUtc, XmlDateTimeSerializationMode.RoundtripKind),
				XmlConvert.ToString(this.ExpiryTimeUtc, XmlDateTimeSerializationMode.RoundtripKind)
			});
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00051818 File Offset: 0x0004FA18
		internal void ValidateRangeAndFreshness(TimeSpan timeToLive, TimeSpan allowedClockSkew)
		{
			if (this.CreationTimeUtc >= this.ExpiryTimeUtc)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimeStampHasCreationAheadOfExpiry", new object[]
				{
					this.CreationTimeUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					this.ExpiryTimeUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture)
				})));
			}
			this.ValidateFreshness(timeToLive, allowedClockSkew);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00051898 File Offset: 0x0004FA98
		internal void ValidateFreshness(TimeSpan timeToLive, TimeSpan allowedClockSkew)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (this.ExpiryTimeUtc <= TimeoutHelper.Subtract(utcNow, allowedClockSkew))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimeStampHasExpiryTimeInPast", new object[]
				{
					this.ExpiryTimeUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					utcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					allowedClockSkew
				})));
			}
			if (this.CreationTimeUtc >= TimeoutHelper.Add(utcNow, allowedClockSkew))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimeStampHasCreationTimeInFuture", new object[]
				{
					this.CreationTimeUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					utcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					allowedClockSkew
				})));
			}
			if (this.CreationTimeUtc <= TimeoutHelper.Subtract(utcNow, TimeoutHelper.Add(timeToLive, allowedClockSkew)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimeStampWasCreatedTooLongAgo", new object[]
				{
					this.CreationTimeUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					utcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.CurrentCulture),
					timeToLive,
					allowedClockSkew
				})));
			}
		}

		// Token: 0x04001B5C RID: 7004
		private const string DefaultFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

		// Token: 0x04001B5D RID: 7005
		internal static readonly TimeSpan defaultTimeToLive = SecurityProtocolFactory.defaultTimestampValidityDuration;

		// Token: 0x04001B5E RID: 7006
		private char[] computedCreationTimeUtc;

		// Token: 0x04001B5F RID: 7007
		private char[] computedExpiryTimeUtc;

		// Token: 0x04001B60 RID: 7008
		private DateTime creationTimeUtc;

		// Token: 0x04001B61 RID: 7009
		private DateTime expiryTimeUtc;

		// Token: 0x04001B62 RID: 7010
		private readonly string id;

		// Token: 0x04001B63 RID: 7011
		private readonly string digestAlgorithm;

		// Token: 0x04001B64 RID: 7012
		private readonly byte[] digest;
	}
}
