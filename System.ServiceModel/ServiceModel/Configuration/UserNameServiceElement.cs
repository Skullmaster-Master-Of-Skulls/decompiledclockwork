using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.ServiceModel.Activation;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000699 RID: 1689
	public sealed class UserNameServiceElement : ConfigurationElement
	{
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x000F7F8C File Offset: 0x000F618C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("userNamePasswordValidationMode", typeof(UserNamePasswordValidationMode), UserNamePasswordValidationMode.Windows, null, new ServiceModelEnumValidator(typeof(UserNamePasswordValidationModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("includeWindowsGroups", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("membershipProviderName", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("customUserNamePasswordValidatorType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("cacheLogonTokens", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxCachedLogonTokens", typeof(int), 128, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("cachedLogonTokenLifetime", typeof(TimeSpan), TimeSpan.Parse("00:15:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00.0000001", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x000F8119 File Offset: 0x000F6319
		// (set) Token: 0x0600415E RID: 16734 RVA: 0x000F812B File Offset: 0x000F632B
		[ConfigurationProperty("userNamePasswordValidationMode", DefaultValue = UserNamePasswordValidationMode.Windows)]
		[ServiceModelEnumValidator(typeof(UserNamePasswordValidationModeHelper))]
		public UserNamePasswordValidationMode UserNamePasswordValidationMode
		{
			get
			{
				return (UserNamePasswordValidationMode)base["userNamePasswordValidationMode"];
			}
			set
			{
				base["userNamePasswordValidationMode"] = value;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x000F813E File Offset: 0x000F633E
		// (set) Token: 0x06004160 RID: 16736 RVA: 0x000F8150 File Offset: 0x000F6350
		[ConfigurationProperty("includeWindowsGroups", DefaultValue = true)]
		public bool IncludeWindowsGroups
		{
			get
			{
				return (bool)base["includeWindowsGroups"];
			}
			set
			{
				base["includeWindowsGroups"] = value;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x000F8163 File Offset: 0x000F6363
		// (set) Token: 0x06004162 RID: 16738 RVA: 0x000F8175 File Offset: 0x000F6375
		[ConfigurationProperty("membershipProviderName", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string MembershipProviderName
		{
			get
			{
				return (string)base["membershipProviderName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["membershipProviderName"] = value;
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000F8192 File Offset: 0x000F6392
		// (set) Token: 0x06004164 RID: 16740 RVA: 0x000F81A4 File Offset: 0x000F63A4
		[ConfigurationProperty("customUserNamePasswordValidatorType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string CustomUserNamePasswordValidatorType
		{
			get
			{
				return (string)base["customUserNamePasswordValidatorType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["customUserNamePasswordValidatorType"] = value;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000F81C1 File Offset: 0x000F63C1
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x000F81D3 File Offset: 0x000F63D3
		[ConfigurationProperty("cacheLogonTokens", DefaultValue = false)]
		public bool CacheLogonTokens
		{
			get
			{
				return (bool)base["cacheLogonTokens"];
			}
			set
			{
				base["cacheLogonTokens"] = value;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x000F81E6 File Offset: 0x000F63E6
		// (set) Token: 0x06004168 RID: 16744 RVA: 0x000F81F8 File Offset: 0x000F63F8
		[ConfigurationProperty("maxCachedLogonTokens", DefaultValue = 128)]
		[IntegerValidator(MinValue = 1)]
		public int MaxCachedLogonTokens
		{
			get
			{
				return (int)base["maxCachedLogonTokens"];
			}
			set
			{
				base["maxCachedLogonTokens"] = value;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x000F820B File Offset: 0x000F640B
		// (set) Token: 0x0600416A RID: 16746 RVA: 0x000F821D File Offset: 0x000F641D
		[ConfigurationProperty("cachedLogonTokenLifetime", DefaultValue = "00:15:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.0000001")]
		public TimeSpan CachedLogonTokenLifetime
		{
			get
			{
				return (TimeSpan)base["cachedLogonTokenLifetime"];
			}
			set
			{
				base["cachedLogonTokenLifetime"] = value;
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000F8230 File Offset: 0x000F6430
		public void Copy(UserNameServiceElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.UserNamePasswordValidationMode = from.UserNamePasswordValidationMode;
			this.IncludeWindowsGroups = from.IncludeWindowsGroups;
			this.MembershipProviderName = from.MembershipProviderName;
			this.CustomUserNamePasswordValidatorType = from.CustomUserNamePasswordValidatorType;
			this.CacheLogonTokens = from.CacheLogonTokens;
			this.MaxCachedLogonTokens = from.MaxCachedLogonTokens;
			this.CachedLogonTokenLifetime = from.CachedLogonTokenLifetime;
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000F82C8 File Offset: 0x000F64C8
		internal void ApplyConfiguration(UserNamePasswordServiceCredential userName)
		{
			if (userName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("userName");
			}
			userName.UserNamePasswordValidationMode = this.UserNamePasswordValidationMode;
			userName.IncludeWindowsGroups = this.IncludeWindowsGroups;
			userName.CacheLogonTokens = this.CacheLogonTokens;
			userName.MaxCachedLogonTokens = this.MaxCachedLogonTokens;
			userName.CachedLogonTokenLifetime = this.CachedLogonTokenLifetime;
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["membershipProviderName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				userName.MembershipProvider = SystemWebHelper.GetMembershipProvider(this.MembershipProviderName);
				if (userName.MembershipProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("InvalidMembershipProviderSpecifiedInConfig", new object[]
					{
						this.MembershipProviderName
					})));
				}
			}
			else if (userName.UserNamePasswordValidationMode == UserNamePasswordValidationMode.MembershipProvider)
			{
				userName.MembershipProvider = SystemWebHelper.GetMembershipProvider();
			}
			if (!string.IsNullOrEmpty(this.CustomUserNamePasswordValidatorType))
			{
				Type type = Type.GetType(this.CustomUserNamePasswordValidatorType, true);
				if (!typeof(UserNamePasswordValidator).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidUserNamePasswordValidatorType", new object[]
					{
						this.CustomUserNamePasswordValidatorType,
						typeof(UserNamePasswordValidator).ToString()
					})));
				}
				userName.CustomUserNamePasswordValidator = (UserNamePasswordValidator)Activator.CreateInstance(type);
			}
		}

		// Token: 0x04002CE7 RID: 11495
		private ConfigurationPropertyCollection properties;
	}
}
