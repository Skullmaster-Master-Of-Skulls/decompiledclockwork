using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A8 RID: 424
	public class SecurityTokenRequirement
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003F82F File Offset: 0x0003DA2F
		public SecurityTokenRequirement()
		{
			this.properties = new Dictionary<string, object>();
			this.Initialize();
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0003F848 File Offset: 0x0003DA48
		public static string TokenTypeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/TokenType";
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003F84F File Offset: 0x0003DA4F
		public static string KeyUsageProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeyUsage";
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0003F856 File Offset: 0x0003DA56
		public static string KeyTypeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeyType";
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003F85D File Offset: 0x0003DA5D
		public static string KeySizeProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeySize";
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0003F864 File Offset: 0x0003DA64
		public static string RequireCryptographicTokenProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/RequireCryptographicToken";
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003F86B File Offset: 0x0003DA6B
		public static string PeerAuthenticationMode
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/PeerAuthenticationMode";
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0003F872 File Offset: 0x0003DA72
		public static string IsOptionalTokenProperty
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/IsOptionalTokenProperty";
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003F87C File Offset: 0x0003DA7C
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0003F89B File Offset: 0x0003DA9B
		public string TokenType
		{
			get
			{
				string result;
				if (!this.TryGetProperty<string>(SecurityTokenRequirement.TokenTypeProperty, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.properties[SecurityTokenRequirement.TokenTypeProperty] = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0003F8CF File Offset: 0x0003DACF
		internal bool IsOptionalToken
		{
			get
			{
				bool flag;
				return this.TryGetProperty<bool>(SecurityTokenRequirement.IsOptionalTokenProperty, out flag) && flag;
			}
			set
			{
				this.properties[SecurityTokenRequirement.IsOptionalTokenProperty] = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0003F8E8 File Offset: 0x0003DAE8
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0003F907 File Offset: 0x0003DB07
		public bool RequireCryptographicToken
		{
			get
			{
				bool flag;
				return this.TryGetProperty<bool>(SecurityTokenRequirement.RequireCryptographicTokenProperty, out flag) && flag;
			}
			set
			{
				this.properties[SecurityTokenRequirement.RequireCryptographicTokenProperty] = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003F920 File Offset: 0x0003DB20
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0003F93F File Offset: 0x0003DB3F
		public SecurityKeyUsage KeyUsage
		{
			get
			{
				SecurityKeyUsage result;
				if (!this.TryGetProperty<SecurityKeyUsage>(SecurityTokenRequirement.KeyUsageProperty, out result))
				{
					return SecurityKeyUsage.Signature;
				}
				return result;
			}
			set
			{
				SecurityKeyUsageHelper.Validate(value);
				this.properties[SecurityTokenRequirement.KeyUsageProperty] = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003F960 File Offset: 0x0003DB60
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0003F97F File Offset: 0x0003DB7F
		public SecurityKeyType KeyType
		{
			get
			{
				SecurityKeyType result;
				if (!this.TryGetProperty<SecurityKeyType>(SecurityTokenRequirement.KeyTypeProperty, out result))
				{
					return SecurityKeyType.SymmetricKey;
				}
				return result;
			}
			set
			{
				SecurityKeyTypeHelper.Validate(value);
				this.properties[SecurityTokenRequirement.KeyTypeProperty] = value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003F9A0 File Offset: 0x0003DBA0
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0003F9BF File Offset: 0x0003DBBF
		public int KeySize
		{
			get
			{
				int result;
				if (!this.TryGetProperty<int>(SecurityTokenRequirement.KeySizeProperty, out result))
				{
					return 0;
				}
				return result;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeNonNegative")));
				}
				this.Properties[SecurityTokenRequirement.KeySizeProperty] = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0003F9FA File Offset: 0x0003DBFA
		public IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003FA02 File Offset: 0x0003DC02
		private void Initialize()
		{
			this.KeyType = SecurityKeyType.SymmetricKey;
			this.KeyUsage = SecurityKeyUsage.Signature;
			this.RequireCryptographicToken = false;
			this.KeySize = 0;
			this.IsOptionalToken = false;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003FA28 File Offset: 0x0003DC28
		public TValue GetProperty<TValue>(string propertyName)
		{
			TValue result;
			if (!this.TryGetProperty<TValue>(propertyName, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SecurityTokenRequirementDoesNotContainProperty", new object[]
				{
					propertyName
				})));
			}
			return result;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003FA68 File Offset: 0x0003DC68
		public bool TryGetProperty<TValue>(string propertyName, out TValue result)
		{
			object obj;
			if (!this.Properties.TryGetValue(propertyName, out obj))
			{
				result = default(TValue);
				return false;
			}
			if (obj != null && !typeof(TValue).IsAssignableFrom(obj.GetType()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SecurityTokenRequirementHasInvalidTypeForProperty", new object[]
				{
					propertyName,
					obj.GetType(),
					typeof(TValue)
				})));
			}
			result = (TValue)((object)obj);
			return true;
		}

		// Token: 0x04000CDA RID: 3290
		private const string Namespace = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement";

		// Token: 0x04000CDB RID: 3291
		private const string tokenTypeProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/TokenType";

		// Token: 0x04000CDC RID: 3292
		private const string keyUsageProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeyUsage";

		// Token: 0x04000CDD RID: 3293
		private const string keyTypeProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeyType";

		// Token: 0x04000CDE RID: 3294
		private const string keySizeProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/KeySize";

		// Token: 0x04000CDF RID: 3295
		private const string requireCryptographicTokenProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/RequireCryptographicToken";

		// Token: 0x04000CE0 RID: 3296
		private const string peerAuthenticationMode = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/PeerAuthenticationMode";

		// Token: 0x04000CE1 RID: 3297
		private const string isOptionalTokenProperty = "http://schemas.microsoft.com/ws/2006/05/identitymodel/securitytokenrequirement/IsOptionalTokenProperty";

		// Token: 0x04000CE2 RID: 3298
		private const bool defaultRequireCryptographicToken = false;

		// Token: 0x04000CE3 RID: 3299
		private const SecurityKeyUsage defaultKeyUsage = SecurityKeyUsage.Signature;

		// Token: 0x04000CE4 RID: 3300
		private const SecurityKeyType defaultKeyType = SecurityKeyType.SymmetricKey;

		// Token: 0x04000CE5 RID: 3301
		private const int defaultKeySize = 0;

		// Token: 0x04000CE6 RID: 3302
		private const bool defaultIsOptionalToken = false;

		// Token: 0x04000CE7 RID: 3303
		private Dictionary<string, object> properties;
	}
}
