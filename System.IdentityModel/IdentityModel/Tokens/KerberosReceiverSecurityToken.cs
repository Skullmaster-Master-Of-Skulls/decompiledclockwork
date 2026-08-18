using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000125 RID: 293
	public class KerberosReceiverSecurityToken : WindowsSecurityToken
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x000218AC File Offset: 0x0001FAAC
		public KerberosReceiverSecurityToken(byte[] request) : this(request, SecurityUniqueId.Create().Value)
		{
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000218BF File Offset: 0x0001FABF
		public KerberosReceiverSecurityToken(byte[] request, string id) : this(request, id, true, null)
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000218CB File Offset: 0x0001FACB
		public KerberosReceiverSecurityToken(byte[] request, string id, string valueTypeUri) : this(request, id, true, valueTypeUri)
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000218D7 File Offset: 0x0001FAD7
		internal KerberosReceiverSecurityToken(byte[] request, string id, bool doAuthenticate, string valueTypeUri) : this(request, id, doAuthenticate, valueTypeUri, null, null)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000218E8 File Offset: 0x0001FAE8
		internal KerberosReceiverSecurityToken(byte[] request, string id, bool doAuthenticate, string valueTypeUri, ChannelBinding channelBinding, ExtendedProtectionPolicy extendedProtectionPolicy)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("request"));
			}
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("id"));
			}
			this.id = id;
			this.request = request;
			this.valueTypeUri = valueTypeUri;
			this.channelBinding = channelBinding;
			this.extendedProtectionPolicy = extendedProtectionPolicy;
			if (doAuthenticate)
			{
				this.Initialize(null, channelBinding, extendedProtectionPolicy);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00021960 File Offset: 0x0001FB60
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.securityKeys == null)
				{
					this.securityKeys = new List<SecurityKey>(1)
					{
						this.SecurityKey
					}.AsReadOnly();
				}
				return this.securityKeys;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0002199A File Offset: 0x0001FB9A
		public SymmetricSecurityKey SecurityKey
		{
			get
			{
				if (!this.isAuthenticated)
				{
					this.Initialize(null, this.channelBinding, this.extendedProtectionPolicy);
				}
				return this.symmetricSecurityKey;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000219BD File Offset: 0x0001FBBD
		public override DateTime ValidFrom
		{
			get
			{
				if (!this.isAuthenticated)
				{
					this.Initialize(null, this.channelBinding, this.extendedProtectionPolicy);
				}
				return base.ValidFrom;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000219E0 File Offset: 0x0001FBE0
		public override DateTime ValidTo
		{
			get
			{
				if (!this.isAuthenticated)
				{
					this.Initialize(null, this.channelBinding, this.extendedProtectionPolicy);
				}
				return base.ValidTo;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00021A03 File Offset: 0x0001FC03
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				base.ThrowIfDisposed();
				if (!this.isAuthenticated)
				{
					this.Initialize(null, this.channelBinding, this.extendedProtectionPolicy);
				}
				return base.WindowsIdentity;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00021A2C File Offset: 0x0001FC2C
		public string ValueTypeUri
		{
			get
			{
				return this.valueTypeUri;
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00021A34 File Offset: 0x0001FC34
		public byte[] GetRequest()
		{
			return SecurityUtils.CloneBuffer(this.request);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00021A44 File Offset: 0x0001FC44
		internal void Initialize(SafeFreeCredentials credentialsHandle, ChannelBinding channelBinding, ExtendedProtectionPolicy extendedProtectionPolicy)
		{
			if (this.isAuthenticated)
			{
				return;
			}
			bool flag = false;
			SafeDeleteContext safeDeleteContext = null;
			SafeCloseHandle safeCloseHandle = null;
			byte[] data = this.request;
			try
			{
				if (credentialsHandle == null)
				{
					credentialsHandle = SspiWrapper.AcquireDefaultCredential("Kerberos", CredentialUse.Inbound, new string[0]);
					flag = true;
				}
				SspiContextFlags sspiContextFlags = SspiContextFlags.ReplayDetect | SspiContextFlags.SequenceDetect | SspiContextFlags.Confidentiality | SspiContextFlags.AllocateMemory;
				ExtendedProtectionPolicyHelper extendedProtectionPolicyHelper = new ExtendedProtectionPolicyHelper(channelBinding, extendedProtectionPolicy);
				if (extendedProtectionPolicyHelper.PolicyEnforcement == PolicyEnforcement.Always && extendedProtectionPolicyHelper.ChannelBinding == null && extendedProtectionPolicyHelper.ProtectionScenario != ProtectionScenario.TrustedProxy)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SecurityChannelBindingMissing")));
				}
				if (extendedProtectionPolicyHelper.PolicyEnforcement == PolicyEnforcement.WhenSupported)
				{
					sspiContextFlags |= SspiContextFlags.ChannelBindingAllowMissingBindings;
				}
				if (extendedProtectionPolicyHelper.ProtectionScenario == ProtectionScenario.TrustedProxy)
				{
					sspiContextFlags |= SspiContextFlags.ChannelBindingProxyBindings;
				}
				SspiContextFlags sspiContextFlags2 = SspiContextFlags.Zero;
				SecurityBuffer outputBuffer = new SecurityBuffer(0, BufferType.Token);
				List<SecurityBuffer> list = new List<SecurityBuffer>(2);
				list.Add(new SecurityBuffer(data, BufferType.Token));
				if (extendedProtectionPolicyHelper.ShouldAddChannelBindingToASC())
				{
					list.Add(new SecurityBuffer(extendedProtectionPolicyHelper.ChannelBinding));
				}
				SecurityBuffer[] inputBuffers = null;
				if (list.Count > 0)
				{
					inputBuffers = list.ToArray();
				}
				int num = SspiWrapper.AcceptSecurityContext(credentialsHandle, ref safeDeleteContext, sspiContextFlags, Endianness.Native, inputBuffers, outputBuffer, ref sspiContextFlags2);
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					SecurityTraceRecordHelper.TraceChannelBindingInformation(extendedProtectionPolicyHelper, true, channelBinding);
				}
				if (num != 0)
				{
					if (num == 590610)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("KerberosMultilegsNotSupported"), new Win32Exception(num)));
					}
					if (num == -2146893056)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("KerberosApReqInvalidOrOutOfMemory"), new Win32Exception(num)));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("FailAcceptSecurityContext"), new Win32Exception(num)));
				}
				else
				{
					LifeSpan lifeSpan = (LifeSpan)SspiWrapper.QueryContextAttributes(safeDeleteContext, ContextAttribute.Lifespan);
					DateTime effectiveTimeUtc = lifeSpan.EffectiveTimeUtc;
					DateTime expiryTimeUtc = lifeSpan.ExpiryTimeUtc;
					SecuritySessionKeyClass securitySessionKeyClass = (SecuritySessionKeyClass)SspiWrapper.QueryContextAttributes(safeDeleteContext, ContextAttribute.SessionKey);
					this.symmetricSecurityKey = new InMemorySymmetricSecurityKey(securitySessionKeyClass.SessionKey);
					num = SspiWrapper.QuerySecurityContextToken(safeDeleteContext, out safeCloseHandle);
					if (num != 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
					}
					WindowsIdentity windowsIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), "Kerberos");
					base.Initialize(this.id, "Kerberos", effectiveTimeUtc, expiryTimeUtc, windowsIdentity, false);
					this.isAuthenticated = true;
				}
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Close();
				}
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

		// Token: 0x06000819 RID: 2073 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(KerberosTicketHashKeyIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00021CD8 File Offset: 0x0001FED8
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(KerberosTicketHashKeyIdentifierClause))
			{
				return new KerberosTicketHashKeyIdentifierClause(CryptoHelper.ComputeHash(this.request), false, null, 0) as T;
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00021D24 File Offset: 0x0001FF24
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			KerberosTicketHashKeyIdentifierClause kerberosTicketHashKeyIdentifierClause = keyIdentifierClause as KerberosTicketHashKeyIdentifierClause;
			if (kerberosTicketHashKeyIdentifierClause != null)
			{
				return kerberosTicketHashKeyIdentifierClause.Matches(CryptoHelper.ComputeHash(this.request));
			}
			return base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x04000AF3 RID: 2803
		private string id;

		// Token: 0x04000AF4 RID: 2804
		private byte[] request;

		// Token: 0x04000AF5 RID: 2805
		private SymmetricSecurityKey symmetricSecurityKey;

		// Token: 0x04000AF6 RID: 2806
		private ReadOnlyCollection<SecurityKey> securityKeys;

		// Token: 0x04000AF7 RID: 2807
		private bool isAuthenticated;

		// Token: 0x04000AF8 RID: 2808
		private string valueTypeUri;

		// Token: 0x04000AF9 RID: 2809
		private ChannelBinding channelBinding;

		// Token: 0x04000AFA RID: 2810
		private ExtendedProtectionPolicy extendedProtectionPolicy;
	}
}
