using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000398 RID: 920
	public class SecurityContextSecurityToken : SecurityToken, TimeBoundedCache.IExpirableItem, IDisposable
	{
		// Token: 0x060021FF RID: 8703 RVA: 0x0007CBEC File Offset: 0x0007ADEC
		public SecurityContextSecurityToken(UniqueId contextId, byte[] key, DateTime validFrom, DateTime validTo) : this(contextId, SecurityUtils.GenerateId(), key, validFrom, validTo)
		{
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0007CBFE File Offset: 0x0007ADFE
		public SecurityContextSecurityToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo) : this(contextId, id, key, validFrom, validTo, null)
		{
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0007CC10 File Offset: 0x0007AE10
		public SecurityContextSecurityToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			this.id = id;
			this.Initialize(contextId, key, validFrom, validTo, authorizationPolicies, false, null, validFrom, validTo);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0007CC40 File Offset: 0x0007AE40
		public SecurityContextSecurityToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			this.id = id;
			this.Initialize(contextId, key, validFrom, validTo, authorizationPolicies, false, keyGeneration, keyEffectiveTime, keyExpirationTime);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0007CC6F File Offset: 0x0007AE6F
		internal SecurityContextSecurityToken(SecurityContextSecurityToken sourceToken, string id) : this(sourceToken, id, sourceToken.key, sourceToken.keyGeneration, sourceToken.keyEffectiveTime, sourceToken.keyExpirationTime, sourceToken.AuthorizationPolicies)
		{
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0007CC98 File Offset: 0x0007AE98
		internal SecurityContextSecurityToken(SecurityContextSecurityToken sourceToken, string id, byte[] key, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			this.id = id;
			this.Initialize(sourceToken.contextId, key, sourceToken.ValidFrom, sourceToken.ValidTo, authorizationPolicies, sourceToken.isCookieMode, keyGeneration, keyEffectiveTime, keyExpirationTime);
			this.cookieBlob = sourceToken.cookieBlob;
			this.bootstrapMessageProperty = ((sourceToken.bootstrapMessageProperty == null) ? null : ((SecurityMessageProperty)sourceToken.BootstrapMessageProperty.CreateCopy()));
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0007CD08 File Offset: 0x0007AF08
		internal SecurityContextSecurityToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, bool isCookieMode, byte[] cookieBlob) : this(contextId, id, key, validFrom, validTo, authorizationPolicies, isCookieMode, cookieBlob, null, validFrom, validTo)
		{
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0007CD30 File Offset: 0x0007AF30
		internal SecurityContextSecurityToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, bool isCookieMode, byte[] cookieBlob, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime)
		{
			this.id = id;
			this.Initialize(contextId, key, validFrom, validTo, authorizationPolicies, isCookieMode, keyGeneration, keyEffectiveTime, keyExpirationTime);
			this.cookieBlob = cookieBlob;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0007CD68 File Offset: 0x0007AF68
		private SecurityContextSecurityToken(SecurityContextSecurityToken from)
		{
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = SecurityUtils.CloneAuthorizationPoliciesIfNecessary(from.authorizationPolicies);
			this.id = from.id;
			this.Initialize(from.contextId, from.key, from.tokenEffectiveTime, from.tokenExpirationTime, readOnlyCollection, from.isCookieMode, from.keyGeneration, from.keyEffectiveTime, from.keyExpirationTime);
			this.cookieBlob = from.cookieBlob;
			this.bootstrapMessageProperty = ((from.bootstrapMessageProperty == null) ? null : ((SecurityMessageProperty)from.BootstrapMessageProperty.CreateCopy()));
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x0007CDF7 File Offset: 0x0007AFF7
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x0007CDFF File Offset: 0x0007AFFF
		public SecurityMessageProperty BootstrapMessageProperty
		{
			get
			{
				return this.bootstrapMessageProperty;
			}
			set
			{
				this.bootstrapMessageProperty = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0007CE08 File Offset: 0x0007B008
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0007CE10 File Offset: 0x0007B010
		public UniqueId ContextId
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0007CE18 File Offset: 0x0007B018
		public UniqueId KeyGeneration
		{
			get
			{
				return this.keyGeneration;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x0007CE20 File Offset: 0x0007B020
		public DateTime KeyEffectiveTime
		{
			get
			{
				return this.keyEffectiveTime;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0007CE28 File Offset: 0x0007B028
		public DateTime KeyExpirationTime
		{
			get
			{
				return this.keyExpirationTime;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0007CE30 File Offset: 0x0007B030
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x0007CE3E File Offset: 0x0007B03E
		public ReadOnlyCollection<IAuthorizationPolicy> AuthorizationPolicies
		{
			get
			{
				this.ThrowIfDisposed();
				return this.authorizationPolicies;
			}
			internal set
			{
				this.authorizationPolicies = value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0007CE47 File Offset: 0x0007B047
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.securityKeys;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0007CE4F File Offset: 0x0007B04F
		public override DateTime ValidFrom
		{
			get
			{
				return this.tokenEffectiveTime;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0007CE57 File Offset: 0x0007B057
		public override DateTime ValidTo
		{
			get
			{
				return this.tokenExpirationTime;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0007CE5F File Offset: 0x0007B05F
		internal byte[] CookieBlob
		{
			get
			{
				return this.cookieBlob;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0007CE67 File Offset: 0x0007B067
		public bool IsCookieMode
		{
			get
			{
				return this.isCookieMode;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0007CE6F File Offset: 0x0007B06F
		DateTime TimeBoundedCache.IExpirableItem.ExpirationTime
		{
			get
			{
				return this.ValidTo;
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0007CE77 File Offset: 0x0007B077
		internal string GetBase64KeyString()
		{
			if (this.keyString == null)
			{
				this.keyString = Convert.ToBase64String(this.key);
			}
			return this.keyString;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0007CE98 File Offset: 0x0007B098
		internal byte[] GetKeyBytes()
		{
			byte[] array = new byte[this.key.Length];
			Buffer.BlockCopy(this.key, 0, array, 0, this.key.Length);
			return array;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0007CECA File Offset: 0x0007B0CA
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "SecurityContextSecurityToken(Identifier='{0}', KeyGeneration='{1}')", new object[]
			{
				this.contextId,
				this.keyGeneration
			});
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0007CEF4 File Offset: 0x0007B0F4
		private void Initialize(UniqueId contextId, byte[] key, DateTime validFrom, DateTime validTo, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, bool isCookieMode, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			if (key == null || key.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			DateTime dateTime = validFrom.ToUniversalTime();
			DateTime t = validTo.ToUniversalTime();
			if (dateTime > t)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("validFrom", SR.GetString("EffectiveGreaterThanExpiration"));
			}
			this.tokenEffectiveTime = dateTime;
			this.tokenExpirationTime = t;
			this.keyEffectiveTime = keyEffectiveTime.ToUniversalTime();
			this.keyExpirationTime = keyExpirationTime.ToUniversalTime();
			if (this.keyEffectiveTime > this.keyExpirationTime)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("keyEffectiveTime", SR.GetString("EffectiveGreaterThanExpiration"));
			}
			if (this.keyEffectiveTime < dateTime || this.keyExpirationTime > t)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("KeyLifetimeNotWithinTokenLifetime"));
			}
			this.key = new byte[key.Length];
			Buffer.BlockCopy(key, 0, this.key, 0, key.Length);
			this.contextId = contextId;
			this.keyGeneration = keyGeneration;
			this.authorizationPolicies = (authorizationPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
			this.securityKeys = new List<SecurityKey>(1)
			{
				new InMemorySymmetricSecurityKey(this.key, false)
			}.AsReadOnly();
			this.isCookieMode = isCookieMode;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0007D056 File Offset: 0x0007B256
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(SecurityContextKeyIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0007D07B File Offset: 0x0007B27B
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(SecurityContextKeyIdentifierClause))
			{
				return new SecurityContextKeyIdentifierClause(this.contextId, this.keyGeneration) as T;
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0007D0BC File Offset: 0x0007B2BC
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = keyIdentifierClause as SecurityContextKeyIdentifierClause;
			if (securityContextKeyIdentifierClause != null)
			{
				return securityContextKeyIdentifierClause.Matches(this.contextId, this.keyGeneration);
			}
			return base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0007D0F0 File Offset: 0x0007B2F0
		public static SecurityContextSecurityToken CreateCookieSecurityContextToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, SecurityStateEncoder securityStateEncoder)
		{
			return SecurityContextSecurityToken.CreateCookieSecurityContextToken(contextId, id, key, validFrom, validTo, null, validFrom, validTo, authorizationPolicies, securityStateEncoder);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0007D110 File Offset: 0x0007B310
		public static SecurityContextSecurityToken CreateCookieSecurityContextToken(UniqueId contextId, string id, byte[] key, DateTime validFrom, DateTime validTo, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, SecurityStateEncoder securityStateEncoder)
		{
			SecurityContextCookieSerializer securityContextCookieSerializer = new SecurityContextCookieSerializer(securityStateEncoder, null);
			byte[] array = securityContextCookieSerializer.CreateCookieFromSecurityContext(contextId, id, key, validFrom, validTo, keyGeneration, keyEffectiveTime, keyExpirationTime, authorizationPolicies);
			return new SecurityContextSecurityToken(contextId, id, key, validFrom, validTo, authorizationPolicies, true, array, keyGeneration, keyEffectiveTime, keyExpirationTime);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0007D152 File Offset: 0x0007B352
		internal SecurityContextSecurityToken Clone()
		{
			this.ThrowIfDisposed();
			return new SecurityContextSecurityToken(this);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007D160 File Offset: 0x0007B360
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				SecurityUtils.DisposeAuthorizationPoliciesIfNecessary(this.authorizationPolicies);
				if (this.bootstrapMessageProperty != null)
				{
					this.bootstrapMessageProperty.Dispose();
				}
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007D18F File Offset: 0x0007B38F
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04001F8D RID: 8077
		private byte[] cookieBlob;

		// Token: 0x04001F8E RID: 8078
		private UniqueId contextId;

		// Token: 0x04001F8F RID: 8079
		private UniqueId keyGeneration;

		// Token: 0x04001F90 RID: 8080
		private DateTime keyEffectiveTime;

		// Token: 0x04001F91 RID: 8081
		private DateTime keyExpirationTime;

		// Token: 0x04001F92 RID: 8082
		private DateTime tokenEffectiveTime;

		// Token: 0x04001F93 RID: 8083
		private DateTime tokenExpirationTime;

		// Token: 0x04001F94 RID: 8084
		private bool isCookieMode;

		// Token: 0x04001F95 RID: 8085
		private byte[] key;

		// Token: 0x04001F96 RID: 8086
		private string keyString;

		// Token: 0x04001F97 RID: 8087
		private ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;

		// Token: 0x04001F98 RID: 8088
		private ReadOnlyCollection<SecurityKey> securityKeys;

		// Token: 0x04001F99 RID: 8089
		private string id;

		// Token: 0x04001F9A RID: 8090
		private SecurityMessageProperty bootstrapMessageProperty;

		// Token: 0x04001F9B RID: 8091
		private bool disposed;
	}
}
