using System;
using System.Runtime;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A5 RID: 677
	public abstract class NonceCache
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0004CB27 File Offset: 0x0004AD27
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0004CB30 File Offset: 0x0004AD30
		public TimeSpan CachingTimeSpan
		{
			get
			{
				return this.cachingTime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.cachingTime = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0004CBA3 File Offset: 0x0004ADA3
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0004CBAB File Offset: 0x0004ADAB
		public int CacheSize
		{
			get
			{
				return this.maxCachedNonces;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxCachedNonces = value;
			}
		}

		// Token: 0x0600147C RID: 5244
		public abstract bool TryAddNonce(byte[] nonce);

		// Token: 0x0600147D RID: 5245
		public abstract bool CheckNonce(byte[] nonce);

		// Token: 0x04001ABF RID: 6847
		private TimeSpan cachingTime;

		// Token: 0x04001AC0 RID: 6848
		private int maxCachedNonces;
	}
}
