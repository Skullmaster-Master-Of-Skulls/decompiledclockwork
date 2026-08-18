using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Activation;
using System.Web.Security;

namespace System.ServiceModel.Security
{
	// Token: 0x0200033D RID: 829
	public sealed class UserNamePasswordServiceCredential
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x0006FDCF File Offset: 0x0006DFCF
		internal UserNamePasswordServiceCredential()
		{
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
		internal UserNamePasswordServiceCredential(UserNamePasswordServiceCredential other)
		{
			this.includeWindowsGroups = other.includeWindowsGroups;
			this.membershipProvider = other.membershipProvider;
			this.validationMode = other.validationMode;
			this.validator = other.validator;
			this.cacheLogonTokens = other.cacheLogonTokens;
			this.maxCachedLogonTokens = other.maxCachedLogonTokens;
			this.cachedLogonTokenLifetime = other.cachedLogonTokenLifetime;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0006FE84 File Offset: 0x0006E084
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0006FE8C File Offset: 0x0006E08C
		public UserNamePasswordValidationMode UserNamePasswordValidationMode
		{
			get
			{
				return this.validationMode;
			}
			set
			{
				UserNamePasswordValidationModeHelper.Validate(value);
				this.ThrowIfImmutable();
				this.validationMode = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0006FEA1 File Offset: 0x0006E0A1
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		public UserNamePasswordValidator CustomUserNamePasswordValidator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.ThrowIfImmutable();
				this.validator = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0006FEB8 File Offset: 0x0006E0B8
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0006FEC5 File Offset: 0x0006E0C5
		public MembershipProvider MembershipProvider
		{
			get
			{
				return (MembershipProvider)this.membershipProvider;
			}
			set
			{
				this.ThrowIfImmutable();
				this.membershipProvider = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0006FED4 File Offset: 0x0006E0D4
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x0006FEDC File Offset: 0x0006E0DC
		public bool IncludeWindowsGroups
		{
			get
			{
				return this.includeWindowsGroups;
			}
			set
			{
				this.ThrowIfImmutable();
				this.includeWindowsGroups = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0006FEEB File Offset: 0x0006E0EB
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0006FEF3 File Offset: 0x0006E0F3
		public bool CacheLogonTokens
		{
			get
			{
				return this.cacheLogonTokens;
			}
			set
			{
				this.ThrowIfImmutable();
				this.cacheLogonTokens = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0006FF02 File Offset: 0x0006E102
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0006FF0A File Offset: 0x0006E10A
		public int MaxCachedLogonTokens
		{
			get
			{
				return this.maxCachedLogonTokens;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeGreaterThanZero")));
				}
				this.ThrowIfImmutable();
				this.maxCachedLogonTokens = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0006FF3C File Offset: 0x0006E13C
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x0006FF44 File Offset: 0x0006E144
		public TimeSpan CachedLogonTokenLifetime
		{
			get
			{
				return this.cachedLogonTokenLifetime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.ThrowIfImmutable();
				this.cachedLogonTokenLifetime = value;
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		internal UserNamePasswordValidator GetUserNamePasswordValidator()
		{
			if (this.validationMode == UserNamePasswordValidationMode.MembershipProvider)
			{
				return this.GetMembershipProviderValidator();
			}
			if (this.validationMode != UserNamePasswordValidationMode.Custom)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			if (this.validator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingCustomUserNamePasswordValidator")));
			}
			return this.validator;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00070018 File Offset: 0x0006E218
		[MethodImpl(MethodImplOptions.NoInlining)]
		private UserNamePasswordValidator GetMembershipProviderValidator()
		{
			MembershipProvider membershipProvider;
			if (this.membershipProvider != null)
			{
				membershipProvider = (MembershipProvider)this.membershipProvider;
			}
			else
			{
				membershipProvider = SystemWebHelper.GetMembershipProvider();
			}
			if (membershipProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingMembershipProvider")));
			}
			return UserNamePasswordValidator.CreateMembershipProviderValidator(membershipProvider);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00070064 File Offset: 0x0006E264
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0007006D File Offset: 0x0006E26D
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E5C RID: 7772
		internal const UserNamePasswordValidationMode DefaultUserNamePasswordValidationMode = UserNamePasswordValidationMode.Windows;

		// Token: 0x04001E5D RID: 7773
		internal const bool DefaultCacheLogonTokens = false;

		// Token: 0x04001E5E RID: 7774
		internal const int DefaultMaxCachedLogonTokens = 128;

		// Token: 0x04001E5F RID: 7775
		internal const string DefaultCachedLogonTokenLifetimeString = "00:15:00";

		// Token: 0x04001E60 RID: 7776
		internal static readonly TimeSpan DefaultCachedLogonTokenLifetime = TimeSpan.Parse("00:15:00", CultureInfo.InvariantCulture);

		// Token: 0x04001E61 RID: 7777
		private UserNamePasswordValidationMode validationMode;

		// Token: 0x04001E62 RID: 7778
		private UserNamePasswordValidator validator;

		// Token: 0x04001E63 RID: 7779
		private object membershipProvider;

		// Token: 0x04001E64 RID: 7780
		private bool includeWindowsGroups = true;

		// Token: 0x04001E65 RID: 7781
		private bool cacheLogonTokens;

		// Token: 0x04001E66 RID: 7782
		private int maxCachedLogonTokens = 128;

		// Token: 0x04001E67 RID: 7783
		private TimeSpan cachedLogonTokenLifetime = UserNamePasswordServiceCredential.DefaultCachedLogonTokenLifetime;

		// Token: 0x04001E68 RID: 7784
		private bool isReadOnly;
	}
}
