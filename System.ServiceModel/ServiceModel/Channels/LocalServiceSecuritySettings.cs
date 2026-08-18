using System;
using System.Runtime;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000996 RID: 2454
	public sealed class LocalServiceSecuritySettings
	{
		// Token: 0x06005FBB RID: 24507 RVA: 0x001650F4 File Offset: 0x001632F4
		private LocalServiceSecuritySettings(LocalServiceSecuritySettings other)
		{
			this.detectReplays = other.detectReplays;
			this.replayCacheSize = other.replayCacheSize;
			this.replayWindow = other.replayWindow;
			this.maxClockSkew = other.maxClockSkew;
			this.issuedCookieLifetime = other.issuedCookieLifetime;
			this.maxStatefulNegotiations = other.maxStatefulNegotiations;
			this.negotiationTimeout = other.negotiationTimeout;
			this.maxPendingSessions = other.maxPendingSessions;
			this.inactivityTimeout = other.inactivityTimeout;
			this.sessionKeyRenewalInterval = other.sessionKeyRenewalInterval;
			this.sessionKeyRolloverInterval = other.sessionKeyRolloverInterval;
			this.reconnectTransportOnFailure = other.reconnectTransportOnFailure;
			this.timestampValidityDuration = other.timestampValidityDuration;
			this.maxCachedCookies = other.maxCachedCookies;
			this.nonceCache = other.nonceCache;
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x001651BB File Offset: 0x001633BB
		// (set) Token: 0x06005FBD RID: 24509 RVA: 0x001651C3 File Offset: 0x001633C3
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

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x001651CC File Offset: 0x001633CC
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x001651D4 File Offset: 0x001633D4
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

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x00165206 File Offset: 0x00163406
		// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x00165210 File Offset: 0x00163410
		public TimeSpan ReplayWindow
		{
			get
			{
				return this.replayWindow;
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
				this.replayWindow = value;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x00165283 File Offset: 0x00163483
		// (set) Token: 0x06005FC3 RID: 24515 RVA: 0x0016528C File Offset: 0x0016348C
		public TimeSpan MaxClockSkew
		{
			get
			{
				return this.maxClockSkew;
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
				this.maxClockSkew = value;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x001652FF File Offset: 0x001634FF
		// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x00165307 File Offset: 0x00163507
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

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x00165310 File Offset: 0x00163510
		// (set) Token: 0x06005FC7 RID: 24519 RVA: 0x00165318 File Offset: 0x00163518
		public TimeSpan IssuedCookieLifetime
		{
			get
			{
				return this.issuedCookieLifetime;
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
				this.issuedCookieLifetime = value;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x0016538B File Offset: 0x0016358B
		// (set) Token: 0x06005FC9 RID: 24521 RVA: 0x00165393 File Offset: 0x00163593
		public int MaxStatefulNegotiations
		{
			get
			{
				return this.maxStatefulNegotiations;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxStatefulNegotiations = value;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06005FCA RID: 24522 RVA: 0x001653C5 File Offset: 0x001635C5
		// (set) Token: 0x06005FCB RID: 24523 RVA: 0x001653D0 File Offset: 0x001635D0
		public TimeSpan NegotiationTimeout
		{
			get
			{
				return this.negotiationTimeout;
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
				this.negotiationTimeout = value;
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x00165443 File Offset: 0x00163643
		// (set) Token: 0x06005FCD RID: 24525 RVA: 0x0016544B File Offset: 0x0016364B
		public int MaxPendingSessions
		{
			get
			{
				return this.maxPendingSessions;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxPendingSessions = value;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x0016547D File Offset: 0x0016367D
		// (set) Token: 0x06005FCF RID: 24527 RVA: 0x00165488 File Offset: 0x00163688
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
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
				this.inactivityTimeout = value;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06005FD0 RID: 24528 RVA: 0x001654FB File Offset: 0x001636FB
		// (set) Token: 0x06005FD1 RID: 24529 RVA: 0x00165504 File Offset: 0x00163704
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

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x00165577 File Offset: 0x00163777
		// (set) Token: 0x06005FD3 RID: 24531 RVA: 0x00165580 File Offset: 0x00163780
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

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x001655F3 File Offset: 0x001637F3
		// (set) Token: 0x06005FD5 RID: 24533 RVA: 0x001655FB File Offset: 0x001637FB
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

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x00165604 File Offset: 0x00163804
		// (set) Token: 0x06005FD7 RID: 24535 RVA: 0x0016560C File Offset: 0x0016380C
		public TimeSpan TimestampValidityDuration
		{
			get
			{
				return this.timestampValidityDuration;
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
				this.timestampValidityDuration = value;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06005FD8 RID: 24536 RVA: 0x0016567F File Offset: 0x0016387F
		// (set) Token: 0x06005FD9 RID: 24537 RVA: 0x00165687 File Offset: 0x00163887
		public int MaxCachedCookies
		{
			get
			{
				return this.maxCachedCookies;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxCachedCookies = value;
			}
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x001656BC File Offset: 0x001638BC
		public LocalServiceSecuritySettings()
		{
			this.DetectReplays = true;
			this.ReplayCacheSize = 900000;
			this.ReplayWindow = SecurityProtocolFactory.defaultReplayWindow;
			this.MaxClockSkew = SecurityProtocolFactory.defaultMaxClockSkew;
			this.IssuedCookieLifetime = NegotiationTokenAuthenticator<NegotiationTokenAuthenticatorState>.defaultServerIssuedTokenLifetime;
			this.MaxStatefulNegotiations = 128;
			this.NegotiationTimeout = NegotiationTokenAuthenticator<NegotiationTokenAuthenticatorState>.defaultServerMaxNegotiationLifetime;
			this.maxPendingSessions = 128;
			this.inactivityTimeout = SecuritySessionServerSettings.defaultInactivityTimeout;
			this.sessionKeyRenewalInterval = SecuritySessionServerSettings.defaultKeyRenewalInterval;
			this.sessionKeyRolloverInterval = SecuritySessionServerSettings.defaultKeyRolloverInterval;
			this.reconnectTransportOnFailure = true;
			this.TimestampValidityDuration = SecurityProtocolFactory.defaultTimestampValidityDuration;
			this.maxCachedCookies = 1000;
			this.nonceCache = null;
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x00165768 File Offset: 0x00163968
		public LocalServiceSecuritySettings Clone()
		{
			return new LocalServiceSecuritySettings(this);
		}

		// Token: 0x0400384C RID: 14412
		private bool detectReplays;

		// Token: 0x0400384D RID: 14413
		private int replayCacheSize;

		// Token: 0x0400384E RID: 14414
		private TimeSpan replayWindow;

		// Token: 0x0400384F RID: 14415
		private TimeSpan maxClockSkew;

		// Token: 0x04003850 RID: 14416
		private TimeSpan issuedCookieLifetime;

		// Token: 0x04003851 RID: 14417
		private int maxStatefulNegotiations;

		// Token: 0x04003852 RID: 14418
		private TimeSpan negotiationTimeout;

		// Token: 0x04003853 RID: 14419
		private int maxCachedCookies;

		// Token: 0x04003854 RID: 14420
		private int maxPendingSessions;

		// Token: 0x04003855 RID: 14421
		private TimeSpan inactivityTimeout;

		// Token: 0x04003856 RID: 14422
		private TimeSpan sessionKeyRenewalInterval;

		// Token: 0x04003857 RID: 14423
		private TimeSpan sessionKeyRolloverInterval;

		// Token: 0x04003858 RID: 14424
		private bool reconnectTransportOnFailure;

		// Token: 0x04003859 RID: 14425
		private TimeSpan timestampValidityDuration;

		// Token: 0x0400385A RID: 14426
		private NonceCache nonceCache;
	}
}
