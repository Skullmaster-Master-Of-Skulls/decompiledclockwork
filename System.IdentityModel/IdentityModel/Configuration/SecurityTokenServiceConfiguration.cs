using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CF RID: 463
	public class SecurityTokenServiceConfiguration : IdentityConfiguration
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x000439C8 File Offset: 0x00041BC8
		public SecurityTokenServiceConfiguration() : this(null, null)
		{
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000439D2 File Offset: 0x00041BD2
		public SecurityTokenServiceConfiguration(bool loadConfig) : this(null, null, loadConfig)
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000439DD File Offset: 0x00041BDD
		public SecurityTokenServiceConfiguration(string issuerName) : this(issuerName, null)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000439E7 File Offset: 0x00041BE7
		public SecurityTokenServiceConfiguration(string issuerName, bool loadConfig) : this(issuerName, null, loadConfig)
		{
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000439F4 File Offset: 0x00041BF4
		public SecurityTokenServiceConfiguration(string issuerName, SigningCredentials signingCredentials)
		{
			this._defaultTokenLifetime = TimeSpan.FromHours(1.0);
			this._maximumTokenLifetime = TimeSpan.FromDays(1.0);
			this._defaultTokenType = "urn:oasis:names:tc:SAML:1.0:assertion";
			this._defaultSymmetricKeySizeInBits = 256;
			this._defaultMaxSymmetricKeySizeInBits = 1024;
			this._wsTrust13RequestSerializer = new WSTrust13RequestSerializer();
			this._wsTrust13ResponseSerializer = new WSTrust13ResponseSerializer();
			this._wsTrustFeb2005RequestSerializer = new WSTrustFeb2005RequestSerializer();
			this._wsTrustFeb2005ResponseSerializer = new WSTrustFeb2005ResponseSerializer();
			base..ctor();
			this._tokenIssuerName = issuerName;
			this._signingCredentials = signingCredentials;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00043A8C File Offset: 0x00041C8C
		public SecurityTokenServiceConfiguration(string issuerName, SigningCredentials signingCredentials, bool loadConfig)
		{
			this._defaultTokenLifetime = TimeSpan.FromHours(1.0);
			this._maximumTokenLifetime = TimeSpan.FromDays(1.0);
			this._defaultTokenType = "urn:oasis:names:tc:SAML:1.0:assertion";
			this._defaultSymmetricKeySizeInBits = 256;
			this._defaultMaxSymmetricKeySizeInBits = 1024;
			this._wsTrust13RequestSerializer = new WSTrust13RequestSerializer();
			this._wsTrust13ResponseSerializer = new WSTrust13ResponseSerializer();
			this._wsTrustFeb2005RequestSerializer = new WSTrustFeb2005RequestSerializer();
			this._wsTrustFeb2005ResponseSerializer = new WSTrustFeb2005ResponseSerializer();
			base..ctor(loadConfig);
			this._tokenIssuerName = issuerName;
			this._signingCredentials = signingCredentials;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00043B24 File Offset: 0x00041D24
		public SecurityTokenServiceConfiguration(string issuerName, SigningCredentials signingCredentials, string serviceName)
		{
			this._defaultTokenLifetime = TimeSpan.FromHours(1.0);
			this._maximumTokenLifetime = TimeSpan.FromDays(1.0);
			this._defaultTokenType = "urn:oasis:names:tc:SAML:1.0:assertion";
			this._defaultSymmetricKeySizeInBits = 256;
			this._defaultMaxSymmetricKeySizeInBits = 1024;
			this._wsTrust13RequestSerializer = new WSTrust13RequestSerializer();
			this._wsTrust13ResponseSerializer = new WSTrust13ResponseSerializer();
			this._wsTrustFeb2005RequestSerializer = new WSTrustFeb2005RequestSerializer();
			this._wsTrustFeb2005ResponseSerializer = new WSTrustFeb2005ResponseSerializer();
			base..ctor(serviceName);
			this._tokenIssuerName = issuerName;
			this._signingCredentials = signingCredentials;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00043BBB File Offset: 0x00041DBB
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x00043BC4 File Offset: 0x00041DC4
		public Type SecurityTokenService
		{
			get
			{
				return this._securityTokenServiceType;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (!typeof(SecurityTokenService).IsAssignableFrom(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID2069"));
				}
				this._securityTokenServiceType = value;
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00043C20 File Offset: 0x00041E20
		public virtual SecurityTokenService CreateSecurityTokenService()
		{
			Type securityTokenService = this.SecurityTokenService;
			if (securityTokenService == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2073"));
			}
			if (!typeof(SecurityTokenService).IsAssignableFrom(securityTokenService))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2074", new object[]
				{
					securityTokenService,
					typeof(SecurityTokenService)
				}));
			}
			return Activator.CreateInstance(securityTokenService, new object[]
			{
				this
			}) as SecurityTokenService;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00043C9B File Offset: 0x00041E9B
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x00043CA3 File Offset: 0x00041EA3
		public int DefaultSymmetricKeySizeInBits
		{
			get
			{
				return this._defaultSymmetricKeySizeInBits;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", SR.GetString("ID0002"));
				}
				this._defaultSymmetricKeySizeInBits = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00043CC5 File Offset: 0x00041EC5
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x00043CCD File Offset: 0x00041ECD
		public int DefaultMaxSymmetricKeySizeInBits
		{
			get
			{
				return this._defaultMaxSymmetricKeySizeInBits;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", SR.GetString("ID0002"));
				}
				this._defaultMaxSymmetricKeySizeInBits = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00043CEF File Offset: 0x00041EEF
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x00043CF7 File Offset: 0x00041EF7
		public TimeSpan DefaultTokenLifetime
		{
			get
			{
				return this._defaultTokenLifetime;
			}
			set
			{
				this._defaultTokenLifetime = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00043D00 File Offset: 0x00041F00
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00043D08 File Offset: 0x00041F08
		public string DefaultTokenType
		{
			get
			{
				return this._defaultTokenType;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("value");
				}
				if (base.SecurityTokenHandlers[value] == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID2015", new object[]
					{
						value
					}));
				}
				this._defaultTokenType = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00043D61 File Offset: 0x00041F61
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x00043D69 File Offset: 0x00041F69
		public bool DisableWsdl
		{
			get
			{
				return this._disableWsdl;
			}
			set
			{
				this._disableWsdl = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00043D72 File Offset: 0x00041F72
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x00043D7A File Offset: 0x00041F7A
		public TimeSpan MaximumTokenLifetime
		{
			get
			{
				return this._maximumTokenLifetime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ThrowHelperArgumentOutOfRange("value", SR.GetString("ID0016"));
				}
				this._maximumTokenLifetime = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00043DA5 File Offset: 0x00041FA5
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00043DAD File Offset: 0x00041FAD
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this._signingCredentials;
			}
			set
			{
				this._signingCredentials = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00043DB6 File Offset: 0x00041FB6
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00043DBE File Offset: 0x00041FBE
		public string TokenIssuerName
		{
			get
			{
				return this._tokenIssuerName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._tokenIssuerName = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00043DDF File Offset: 0x00041FDF
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00043DE7 File Offset: 0x00041FE7
		public WSTrust13RequestSerializer WSTrust13RequestSerializer
		{
			get
			{
				return this._wsTrust13RequestSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._wsTrust13RequestSerializer = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00043E03 File Offset: 0x00042003
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00043E0B File Offset: 0x0004200B
		public WSTrust13ResponseSerializer WSTrust13ResponseSerializer
		{
			get
			{
				return this._wsTrust13ResponseSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._wsTrust13ResponseSerializer = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00043E27 File Offset: 0x00042027
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00043E2F File Offset: 0x0004202F
		public WSTrustFeb2005RequestSerializer WSTrustFeb2005RequestSerializer
		{
			get
			{
				return this._wsTrustFeb2005RequestSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._wsTrustFeb2005RequestSerializer = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00043E4B File Offset: 0x0004204B
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x00043E53 File Offset: 0x00042053
		public WSTrustFeb2005ResponseSerializer WSTrustFeb2005ResponseSerializer
		{
			get
			{
				return this._wsTrustFeb2005ResponseSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._wsTrustFeb2005ResponseSerializer = value;
			}
		}

		// Token: 0x04000D83 RID: 3459
		private string _tokenIssuerName;

		// Token: 0x04000D84 RID: 3460
		private SigningCredentials _signingCredentials;

		// Token: 0x04000D85 RID: 3461
		private TimeSpan _defaultTokenLifetime;

		// Token: 0x04000D86 RID: 3462
		private TimeSpan _maximumTokenLifetime;

		// Token: 0x04000D87 RID: 3463
		private string _defaultTokenType;

		// Token: 0x04000D88 RID: 3464
		internal const int DefaultKeySizeInBitsConstant = 256;

		// Token: 0x04000D89 RID: 3465
		private int _defaultSymmetricKeySizeInBits;

		// Token: 0x04000D8A RID: 3466
		private int _defaultMaxSymmetricKeySizeInBits;

		// Token: 0x04000D8B RID: 3467
		private bool _disableWsdl;

		// Token: 0x04000D8C RID: 3468
		private Type _securityTokenServiceType;

		// Token: 0x04000D8D RID: 3469
		private WSTrust13RequestSerializer _wsTrust13RequestSerializer;

		// Token: 0x04000D8E RID: 3470
		private WSTrust13ResponseSerializer _wsTrust13ResponseSerializer;

		// Token: 0x04000D8F RID: 3471
		private WSTrustFeb2005RequestSerializer _wsTrustFeb2005RequestSerializer;

		// Token: 0x04000D90 RID: 3472
		private WSTrustFeb2005ResponseSerializer _wsTrustFeb2005ResponseSerializer;
	}
}
