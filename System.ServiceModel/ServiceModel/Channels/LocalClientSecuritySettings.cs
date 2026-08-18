using System;
using System.Runtime;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000995 RID: 2453
	[__DynamicallyInvokable]
	public sealed class LocalClientSecuritySettings
	{
		// Token: 0x06005F9E RID: 24478 RVA: 0x00164BD0 File Offset: 0x00162DD0
		private LocalClientSecuritySettings(LocalClientSecuritySettings other)
		{
			this.detectReplays = other.detectReplays;
			this.replayCacheSize = other.replayCacheSize;
			this.replayWindow = other.replayWindow;
			this.maxClockSkew = other.maxClockSkew;
			this.cacheCookies = other.cacheCookies;
			this.maxCookieCachingTime = other.maxCookieCachingTime;
			this.sessionKeyRenewalInterval = other.sessionKeyRenewalInterval;
			this.sessionKeyRolloverInterval = other.sessionKeyRolloverInterval;
			this.reconnectTransportOnFailure = other.reconnectTransportOnFailure;
			this.timestampValidityDuration = other.timestampValidityDuration;
			this.identityVerifier = other.identityVerifier;
			this.cookieRenewalThresholdPercentage = other.cookieRenewalThresholdPercentage;
			this.nonceCache = other.nonceCache;
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x00164C7F File Offset: 0x00162E7F
		// (set) Token: 0x06005FA0 RID: 24480 RVA: 0x00164C87 File Offset: 0x00162E87
		public bool DetectReplays
		{
			get
			{
				return this.detectReplays;
			}
			set
			{
				this.detectReplays = value;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06005FA1 RID: 24481 RVA: 0x00164C90 File Offset: 0x00162E90
		// (set) Token: 0x06005FA2 RID: 24482 RVA: 0x00164C98 File Offset: 0x00162E98
		public int ReplayCacheSize
		{
			get
			{
				return this.replayCacheSize;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.replayCacheSize = value;
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06005FA3 RID: 24483 RVA: 0x00164CCA File Offset: 0x00162ECA
		// (set) Token: 0x06005FA4 RID: 24484 RVA: 0x00164CD4 File Offset: 0x00162ED4
		[__DynamicallyInvokable]
		public TimeSpan ReplayWindow
		{
			[__DynamicallyInvokable]
			get
			{
				return this.replayWindow;
			}
			[__DynamicallyInvokable]
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
				this.replayWindow = value;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x00164D47 File Offset: 0x00162F47
		// (set) Token: 0x06005FA6 RID: 24486 RVA: 0x00164D50 File Offset: 0x00162F50
		[__DynamicallyInvokable]
		public TimeSpan MaxClockSkew
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxClockSkew;
			}
			[__DynamicallyInvokable]
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
				this.maxClockSkew = value;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x00164DC3 File Offset: 0x00162FC3
		// (set) Token: 0x06005FA8 RID: 24488 RVA: 0x00164DCB File Offset: 0x00162FCB
		public NonceCache NonceCache
		{
			get
			{
				return this.nonceCache;
			}
			set
			{
				this.nonceCache = value;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x00164DD4 File Offset: 0x00162FD4
		// (set) Token: 0x06005FAA RID: 24490 RVA: 0x00164DDC File Offset: 0x00162FDC
		[__DynamicallyInvokable]
		public TimeSpan TimestampValidityDuration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.timestampValidityDuration;
			}
			[__DynamicallyInvokable]
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
				this.timestampValidityDuration = value;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06005FAB RID: 24491 RVA: 0x00164E4F File Offset: 0x0016304F
		// (set) Token: 0x06005FAC RID: 24492 RVA: 0x00164E57 File Offset: 0x00163057
		public bool CacheCookies
		{
			get
			{
				return this.cacheCookies;
			}
			set
			{
				this.cacheCookies = value;
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x00164E60 File Offset: 0x00163060
		// (set) Token: 0x06005FAE RID: 24494 RVA: 0x00164E68 File Offset: 0x00163068
		public TimeSpan MaxCookieCachingTime
		{
			get
			{
				return this.maxCookieCachingTime;
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
				this.maxCookieCachingTime = value;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06005FAF RID: 24495 RVA: 0x00164EDB File Offset: 0x001630DB
		// (set) Token: 0x06005FB0 RID: 24496 RVA: 0x00164EE4 File Offset: 0x001630E4
		public int CookieRenewalThresholdPercentage
		{
			get
			{
				return this.cookieRenewalThresholdPercentage;
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						100
					})));
				}
				this.cookieRenewalThresholdPercentage = value;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06005FB1 RID: 24497 RVA: 0x00164F3F File Offset: 0x0016313F
		// (set) Token: 0x06005FB2 RID: 24498 RVA: 0x00164F48 File Offset: 0x00163148
		public TimeSpan SessionKeyRenewalInterval
		{
			get
			{
				return this.sessionKeyRenewalInterval;
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
				this.sessionKeyRenewalInterval = value;
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x00164FBB File Offset: 0x001631BB
		// (set) Token: 0x06005FB4 RID: 24500 RVA: 0x00164FC4 File Offset: 0x001631C4
		public TimeSpan SessionKeyRolloverInterval
		{
			get
			{
				return this.sessionKeyRolloverInterval;
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
				this.sessionKeyRolloverInterval = value;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x00165037 File Offset: 0x00163237
		// (set) Token: 0x06005FB6 RID: 24502 RVA: 0x0016503F File Offset: 0x0016323F
		public bool ReconnectTransportOnFailure
		{
			get
			{
				return this.reconnectTransportOnFailure;
			}
			set
			{
				this.reconnectTransportOnFailure = value;
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x00165048 File Offset: 0x00163248
		// (set) Token: 0x06005FB8 RID: 24504 RVA: 0x00165050 File Offset: 0x00163250
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.identityVerifier;
			}
			set
			{
				this.identityVerifier = value;
			}
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x0016505C File Offset: 0x0016325C
		[__DynamicallyInvokable]
		public LocalClientSecuritySettings()
		{
			this.DetectReplays = true;
			this.ReplayCacheSize = 900000;
			this.ReplayWindow = SecurityProtocolFactory.defaultReplayWindow;
			this.MaxClockSkew = SecurityProtocolFactory.defaultMaxClockSkew;
			this.TimestampValidityDuration = SecurityProtocolFactory.defaultTimestampValidityDuration;
			this.CacheCookies = true;
			this.MaxCookieCachingTime = IssuanceTokenProviderBase<IssuanceTokenProviderState>.DefaultClientMaxTokenCachingTime;
			this.SessionKeyRenewalInterval = SecuritySessionClientSettings.defaultKeyRenewalInterval;
			this.SessionKeyRolloverInterval = SecuritySessionClientSettings.defaultKeyRolloverInterval;
			this.ReconnectTransportOnFailure = true;
			this.CookieRenewalThresholdPercentage = 60;
			this.IdentityVerifier = IdentityVerifier.CreateDefault();
			this.nonceCache = null;
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x001650EB File Offset: 0x001632EB
		[__DynamicallyInvokable]
		public LocalClientSecuritySettings Clone()
		{
			return new LocalClientSecuritySettings(this);
		}

		// Token: 0x0400383F RID: 14399
		private bool detectReplays;

		// Token: 0x04003840 RID: 14400
		private int replayCacheSize;

		// Token: 0x04003841 RID: 14401
		private TimeSpan replayWindow;

		// Token: 0x04003842 RID: 14402
		private TimeSpan maxClockSkew;

		// Token: 0x04003843 RID: 14403
		private bool cacheCookies;

		// Token: 0x04003844 RID: 14404
		private TimeSpan maxCookieCachingTime;

		// Token: 0x04003845 RID: 14405
		private TimeSpan sessionKeyRenewalInterval;

		// Token: 0x04003846 RID: 14406
		private TimeSpan sessionKeyRolloverInterval;

		// Token: 0x04003847 RID: 14407
		private bool reconnectTransportOnFailure;

		// Token: 0x04003848 RID: 14408
		private TimeSpan timestampValidityDuration;

		// Token: 0x04003849 RID: 14409
		private IdentityVerifier identityVerifier;

		// Token: 0x0400384A RID: 14410
		private int cookieRenewalThresholdPercentage;

		// Token: 0x0400384B RID: 14411
		private NonceCache nonceCache;
	}
}
