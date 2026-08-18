using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Diagnostics;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000126 RID: 294
	public class KerberosRequestorSecurityToken : SecurityToken
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00021D54 File Offset: 0x0001FF54
		public KerberosRequestorSecurityToken(string servicePrincipalName) : this(servicePrincipalName, TokenImpersonationLevel.Impersonation, null, SecurityUniqueId.Create().Value, null)
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00021D6A File Offset: 0x0001FF6A
		public KerberosRequestorSecurityToken(string servicePrincipalName, TokenImpersonationLevel tokenImpersonationLevel, NetworkCredential networkCredential, string id) : this(servicePrincipalName, tokenImpersonationLevel, networkCredential, id, null, null)
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00021D79 File Offset: 0x0001FF79
		internal KerberosRequestorSecurityToken(string servicePrincipalName, TokenImpersonationLevel tokenImpersonationLevel, NetworkCredential networkCredential, string id, ChannelBinding channelBinding) : this(servicePrincipalName, tokenImpersonationLevel, networkCredential, id, null, channelBinding)
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00021D8C File Offset: 0x0001FF8C
		internal KerberosRequestorSecurityToken(string servicePrincipalName, TokenImpersonationLevel tokenImpersonationLevel, NetworkCredential networkCredential, string id, SafeFreeCredentials credentialsHandle, ChannelBinding channelBinding)
		{
			if (servicePrincipalName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("servicePrincipalName");
			}
			if (tokenImpersonationLevel != TokenImpersonationLevel.Identification && tokenImpersonationLevel != TokenImpersonationLevel.Impersonation)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenImpersonationLevel", SR.GetString("ImpersonationLevelNotSupported", new object[]
				{
					tokenImpersonationLevel
				})));
			}
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			this.servicePrincipalName = servicePrincipalName;
			if (networkCredential != null && networkCredential != CredentialCache.DefaultNetworkCredentials && string.IsNullOrEmpty(networkCredential.UserName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ProvidedNetworkCredentialsForKerberosHasInvalidUserName"));
			}
			this.id = id;
			try
			{
				this.Initialize(tokenImpersonationLevel, networkCredential, credentialsHandle, channelBinding);
			}
			catch (Win32Exception innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("UnableToCreateKerberosCredentials"), innerException));
			}
			catch (SecurityTokenException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("UnableToCreateKerberosCredentials"), innerException2));
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00021E9C File Offset: 0x0002009C
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00021EA4 File Offset: 0x000200A4
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.securityKeys == null)
				{
					this.securityKeys = new List<SecurityKey>(1)
					{
						this.symmetricSecurityKey
					}.AsReadOnly();
				}
				return this.securityKeys;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00021EDE File Offset: 0x000200DE
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00021EE6 File Offset: 0x000200E6
		public override DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00021EEE File Offset: 0x000200EE
		public string ServicePrincipalName
		{
			get
			{
				return this.servicePrincipalName;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00021EF6 File Offset: 0x000200F6
		public SymmetricSecurityKey SecurityKey
		{
			get
			{
				return this.symmetricSecurityKey;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00021EFE File Offset: 0x000200FE
		public byte[] GetRequest()
		{
			return SecurityUtils.CloneBuffer(this.apreq);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00021F0C File Offset: 0x0002010C
		private void Initialize(TokenImpersonationLevel tokenImpersonationLevel, NetworkCredential networkCredential, SafeFreeCredentials credentialsHandle, ChannelBinding channelBinding)
		{
			bool flag = false;
			SafeDeleteContext safeDeleteContext = null;
			try
			{
				if (credentialsHandle == null)
				{
					if (networkCredential == null || networkCredential == CredentialCache.DefaultNetworkCredentials)
					{
						credentialsHandle = SspiWrapper.AcquireDefaultCredential("Kerberos", CredentialUse.Outbound, new string[0]);
					}
					else
					{
						AuthIdentityEx authIdentityEx = new AuthIdentityEx(networkCredential.UserName, networkCredential.Password, networkCredential.Domain, new string[0]);
						credentialsHandle = SspiWrapper.AcquireCredentialsHandle("Kerberos", CredentialUse.Outbound, ref authIdentityEx);
					}
					flag = true;
				}
				SspiContextFlags sspiContextFlags = SspiContextFlags.ReplayDetect | SspiContextFlags.SequenceDetect | SspiContextFlags.Confidentiality | SspiContextFlags.AllocateMemory;
				if (tokenImpersonationLevel == TokenImpersonationLevel.Identification)
				{
					sspiContextFlags |= SspiContextFlags.InitIdentify;
				}
				SspiContextFlags sspiContextFlags2 = SspiContextFlags.Zero;
				SecurityBuffer inputBuffer = null;
				if (channelBinding != null)
				{
					inputBuffer = new SecurityBuffer(channelBinding);
				}
				SecurityBuffer securityBuffer = new SecurityBuffer(0, BufferType.Token);
				int num = SspiWrapper.InitializeSecurityContext(credentialsHandle, ref safeDeleteContext, this.servicePrincipalName, sspiContextFlags, Endianness.Native, inputBuffer, securityBuffer, ref sspiContextFlags2);
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					SecurityTraceRecordHelper.TraceChannelBindingInformation(null, false, channelBinding);
				}
				if (num != 0)
				{
					if (num == 590610)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("KerberosMultilegsNotSupported"), new Win32Exception(num)));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("FailInitializeSecurityContext"), new Win32Exception(num)));
				}
				else
				{
					this.apreq = securityBuffer.token;
					LifeSpan lifeSpan = (LifeSpan)SspiWrapper.QueryContextAttributes(safeDeleteContext, ContextAttribute.Lifespan);
					this.effectiveTime = lifeSpan.EffectiveTimeUtc;
					this.expirationTime = lifeSpan.ExpiryTimeUtc;
					SecuritySessionKeyClass securitySessionKeyClass = (SecuritySessionKeyClass)SspiWrapper.QueryContextAttributes(safeDeleteContext, ContextAttribute.SessionKey);
					this.symmetricSecurityKey = new InMemorySymmetricSecurityKey(securitySessionKeyClass.SessionKey);
				}
			}
			finally
			{
				if (safeDeleteContext != null)
				{
					safeDeleteContext.Close();
				}
				if (flag && credentialsHandle != null)
				{
					credentialsHandle.Close();
				}
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000220A0 File Offset: 0x000202A0
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(KerberosTicketHashKeyIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000220C8 File Offset: 0x000202C8
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(KerberosTicketHashKeyIdentifierClause))
			{
				return new KerberosTicketHashKeyIdentifierClause(CryptoHelper.ComputeHash(this.apreq), false, null, 0) as T;
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00022114 File Offset: 0x00020314
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			KerberosTicketHashKeyIdentifierClause kerberosTicketHashKeyIdentifierClause = keyIdentifierClause as KerberosTicketHashKeyIdentifierClause;
			if (kerberosTicketHashKeyIdentifierClause != null)
			{
				return kerberosTicketHashKeyIdentifierClause.Matches(CryptoHelper.ComputeHash(this.apreq));
			}
			return base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x04000AFB RID: 2811
		private string id;

		// Token: 0x04000AFC RID: 2812
		private byte[] apreq;

		// Token: 0x04000AFD RID: 2813
		private readonly string servicePrincipalName;

		// Token: 0x04000AFE RID: 2814
		private SymmetricSecurityKey symmetricSecurityKey;

		// Token: 0x04000AFF RID: 2815
		private ReadOnlyCollection<SecurityKey> securityKeys;

		// Token: 0x04000B00 RID: 2816
		private DateTime effectiveTime;

		// Token: 0x04000B01 RID: 2817
		private DateTime expirationTime;
	}
}
