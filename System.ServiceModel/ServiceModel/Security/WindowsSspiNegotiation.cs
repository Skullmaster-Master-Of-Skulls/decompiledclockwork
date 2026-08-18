using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Tokens;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030F RID: 783
	internal sealed class WindowsSspiNegotiation : ISspiNegotiation, IDisposable
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x000655A0 File Offset: 0x000637A0
		internal WindowsSspiNegotiation(string package, SafeFreeCredentials credentialsHandle, TokenImpersonationLevel impersonationLevel, string servicePrincipalName, bool doMutualAuth, bool interactiveLogonEnabled, bool ntlmEnabled) : this(false, package, credentialsHandle, impersonationLevel, servicePrincipalName, doMutualAuth, interactiveLogonEnabled, ntlmEnabled)
		{
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000655C0 File Offset: 0x000637C0
		internal WindowsSspiNegotiation(string package, SafeFreeCredentials credentialsHandle, string defaultServiceBinding) : this(true, package, credentialsHandle, TokenImpersonationLevel.Delegation, defaultServiceBinding, false, false, true)
		{
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000655DC File Offset: 0x000637DC
		private WindowsSspiNegotiation(bool isServer, string package, SafeFreeCredentials credentialsHandle, TokenImpersonationLevel impersonationLevel, string servicePrincipalName, bool doMutualAuth, bool interactiveLogonEnabled, bool ntlmEnabled)
		{
			this.tokenSize = SspiWrapper.GetVerifyPackageInfo(package).MaxToken;
			this.isServer = isServer;
			this.servicePrincipalName = servicePrincipalName;
			this.securityContext = null;
			if (isServer)
			{
				this.impersonationLevel = TokenImpersonationLevel.Delegation;
				this.doMutualAuth = false;
			}
			else
			{
				this.impersonationLevel = impersonationLevel;
				this.doMutualAuth = doMutualAuth;
				this.interactiveNegoLogonEnabled = interactiveLogonEnabled;
				this.clientPackageName = package;
				this.allowNtlm = ntlmEnabled;
			}
			this.credentialsHandle = credentialsHandle;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00065670 File Offset: 0x00063870
		public DateTime ExpirationTimeUtc
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.LifeSpan == null)
				{
					return SecurityUtils.MaxUtcDateTime;
				}
				return this.LifeSpan.ExpiryTimeUtc;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00065691 File Offset: 0x00063891
		public bool IsCompleted
		{
			get
			{
				this.ThrowIfDisposed();
				return this.isCompleted;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0006569F File Offset: 0x0006389F
		public bool IsDelegationFlag
		{
			get
			{
				this.ThrowIfDisposed();
				return (this.contextFlags & SspiContextFlags.Delegate) > SspiContextFlags.Zero;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000656B2 File Offset: 0x000638B2
		public bool IsIdentifyFlag
		{
			get
			{
				this.ThrowIfDisposed();
				return (this.contextFlags & (this.isServer ? SspiContextFlags.AcceptIdentify : SspiContextFlags.InitIdentify)) > SspiContextFlags.Zero;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000656D8 File Offset: 0x000638D8
		public bool IsMutualAuthFlag
		{
			get
			{
				this.ThrowIfDisposed();
				return (this.contextFlags & SspiContextFlags.MutualAuth) > SspiContextFlags.Zero;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000656EB File Offset: 0x000638EB
		public bool IsValidContext
		{
			get
			{
				return this.securityContext != null && !this.securityContext.IsInvalid;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00065705 File Offset: 0x00063905
		public string KeyEncryptionAlgorithm
		{
			get
			{
				return "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0006570C File Offset: 0x0006390C
		public LifeSpan LifeSpan
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.lifespan == null)
				{
					LifeSpan result = (LifeSpan)SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.Lifespan);
					if (this.IsCompleted)
					{
						this.lifespan = result;
					}
					return result;
				}
				return this.lifespan;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00065750 File Offset: 0x00063950
		public string ProtocolName
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.protocolName == null)
				{
					NegotiationInfoClass negotiationInfoClass = SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.NegotiationInfo) as NegotiationInfoClass;
					if (this.IsCompleted)
					{
						this.protocolName = negotiationInfoClass.AuthenticationPackage;
					}
					return negotiationInfoClass.AuthenticationPackage;
				}
				return this.protocolName;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006579F File Offset: 0x0006399F
		public string ServicePrincipalName
		{
			get
			{
				this.ThrowIfDisposed();
				return this.servicePrincipalName;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000657B0 File Offset: 0x000639B0
		private SecSizes SecuritySizes
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.sizes == null)
				{
					SecSizes result = (SecSizes)SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.Sizes);
					if (this.IsCompleted)
					{
						this.sizes = result;
					}
					return result;
				}
				return this.sizes;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000657F4 File Offset: 0x000639F4
		public string GetRemoteIdentityName()
		{
			if (!this.isServer)
			{
				return this.servicePrincipalName;
			}
			if (this.IsValidContext)
			{
				using (SafeCloseHandle contextToken = this.GetContextToken())
				{
					using (WindowsIdentity windowsIdentity = new WindowsIdentity(contextToken.DangerousGetHandle(), this.ProtocolName))
					{
						return windowsIdentity.Name;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00065874 File Offset: 0x00063A74
		public byte[] Decrypt(byte[] encryptedContent)
		{
			if (encryptedContent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encryptedContent");
			}
			this.ThrowIfDisposed();
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(encryptedContent, 0, encryptedContent.Length, BufferType.Stream),
				new SecurityBuffer(0, BufferType.Data)
			};
			int num = SspiWrapper.DecryptMessage(this.securityContext, array, 0U, true);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == BufferType.Data)
				{
					return array[i].token;
				}
			}
			this.OnBadData();
			return null;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00065906 File Offset: 0x00063B06
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00065918 File Offset: 0x00063B18
		public byte[] Encrypt(byte[] input)
		{
			if (input == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("input");
			}
			this.ThrowIfDisposed();
			SecurityBuffer[] array = new SecurityBuffer[3];
			byte[] array2 = DiagnosticUtility.Utility.AllocateByteArray(this.SecuritySizes.SecurityTrailer);
			array[0] = new SecurityBuffer(array2, 0, array2.Length, BufferType.Token);
			byte[] array3 = DiagnosticUtility.Utility.AllocateByteArray(input.Length);
			Buffer.BlockCopy(input, 0, array3, 0, input.Length);
			array[1] = new SecurityBuffer(array3, 0, array3.Length, BufferType.Data);
			byte[] array4 = DiagnosticUtility.Utility.AllocateByteArray(this.SecuritySizes.BlockSize);
			array[2] = new SecurityBuffer(array4, 0, array4.Length, BufferType.Padding);
			int num = SspiWrapper.EncryptMessage(this.securityContext, array, 0U);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == BufferType.Token)
				{
					num2 = array[i].size;
				}
				else if (array[i].type == BufferType.Padding)
				{
					num3 = array[i].size;
				}
			}
			byte[] array5 = DiagnosticUtility.Utility.AllocateByteArray(checked(num2 + array3.Length + num3));
			Buffer.BlockCopy(array2, 0, array5, 0, num2);
			Buffer.BlockCopy(array3, 0, array5, num2, array3.Length);
			Buffer.BlockCopy(array4, 0, array5, num2 + array3.Length, num3);
			return array5;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00065A68 File Offset: 0x00063C68
		public byte[] GetOutgoingBlob(byte[] incomingBlob, ChannelBinding channelbinding, ExtendedProtectionPolicy protectionPolicy)
		{
			this.ThrowIfDisposed();
			SspiContextFlags sspiContextFlags = SspiContextFlags.ReplayDetect | SspiContextFlags.SequenceDetect | SspiContextFlags.Confidentiality;
			if (this.doMutualAuth)
			{
				sspiContextFlags |= SspiContextFlags.MutualAuth;
			}
			if (this.impersonationLevel == TokenImpersonationLevel.Delegation)
			{
				sspiContextFlags |= SspiContextFlags.Delegate;
			}
			else if (!this.isServer && this.impersonationLevel == TokenImpersonationLevel.Identification)
			{
				sspiContextFlags |= SspiContextFlags.InitIdentify;
			}
			else if (!this.isServer && this.impersonationLevel == TokenImpersonationLevel.Anonymous)
			{
				sspiContextFlags |= SspiContextFlags.InitAnonymous;
			}
			ExtendedProtectionPolicyHelper extendedProtectionPolicyHelper = new ExtendedProtectionPolicyHelper(channelbinding, protectionPolicy);
			if (this.isServer)
			{
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
			}
			List<SecurityBuffer> list = new List<SecurityBuffer>(2);
			if (incomingBlob != null)
			{
				list.Add(new SecurityBuffer(incomingBlob, BufferType.Token));
			}
			if (this.isServer)
			{
				if (extendedProtectionPolicyHelper.ShouldAddChannelBindingToASC())
				{
					list.Add(new SecurityBuffer(extendedProtectionPolicyHelper.ChannelBinding));
				}
			}
			else if (extendedProtectionPolicyHelper.ChannelBinding != null)
			{
				list.Add(new SecurityBuffer(extendedProtectionPolicyHelper.ChannelBinding));
			}
			SecurityBuffer[] inputBuffers = null;
			if (list.Count > 0)
			{
				inputBuffers = list.ToArray();
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(this.tokenSize, BufferType.Token);
			int num;
			if (!this.isServer)
			{
				num = SspiWrapper.InitializeSecurityContext(this.credentialsHandle, ref this.securityContext, this.servicePrincipalName, sspiContextFlags, Endianness.Network, inputBuffers, securityBuffer, ref this.contextFlags);
			}
			else
			{
				bool flag = this.securityContext == null;
				SspiContextFlags sspiContextFlags2 = this.contextFlags;
				num = SspiWrapper.AcceptSecurityContext(this.credentialsHandle, ref this.securityContext, sspiContextFlags, Endianness.Network, inputBuffers, securityBuffer, ref this.contextFlags);
				if (num == -2146893048 && !flag)
				{
					this.contextFlags = sspiContextFlags2;
					this.CloseContext();
					num = SspiWrapper.AcceptSecurityContext(this.credentialsHandle, ref this.securityContext, sspiContextFlags, Endianness.Network, inputBuffers, securityBuffer, ref this.contextFlags);
				}
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				SecurityTraceRecordHelper.TraceChannelBindingInformation(extendedProtectionPolicyHelper, this.isServer, channelbinding);
			}
			if ((num & -2147483648) == 0)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					if (this.isServer)
					{
						SecurityTraceRecordHelper.TraceServiceOutgoingSpnego(this);
					}
					else
					{
						SecurityTraceRecordHelper.TraceClientOutgoingSpnego(this);
					}
				}
				if (num == 0)
				{
					this.isCompleted = true;
					if (this.isServer && (this.contextFlags & SspiContextFlags.AcceptAnonymous) == SspiContextFlags.Zero && string.Compare(this.ProtocolName, "Kerberos", StringComparison.OrdinalIgnoreCase) != 0 && extendedProtectionPolicyHelper.ShouldCheckServiceBinding)
					{
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							string serviceBindingNameSentByClient;
							SspiWrapper.QuerySpecifiedTarget(this.securityContext, out serviceBindingNameSentByClient);
							SecurityTraceRecordHelper.TraceServiceNameBindingOnServer(serviceBindingNameSentByClient, this.servicePrincipalName, extendedProtectionPolicyHelper.ServiceNameCollection);
						}
						extendedProtectionPolicyHelper.CheckServiceBinding(this.securityContext, this.servicePrincipalName);
					}
				}
				return securityBuffer.token;
			}
			if (!this.isServer && this.interactiveNegoLogonEnabled && SecurityUtils.IsOSGreaterThanOrEqualToWin7() && SspiWrapper.IsSspiPromptingNeeded((uint)num) && SspiWrapper.IsNegotiateExPackagePresent())
			{
				if (this.MaxPromptAttempts >= 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num, SR.GetString("InvalidClientCredentials")));
				}
				IntPtr zero = IntPtr.Zero;
				uint num2 = SspiWrapper.SspiPromptForCredential(this.servicePrincipalName, this.clientPackageName, out zero, ref this.saveClientCredentialsOnSspiUi);
				if (num2 == 0U)
				{
					IntPtr intPtr = IntPtr.Zero;
					if (!this.allowNtlm)
					{
						uint num3 = UnsafeNativeMethods.SspiExcludePackage(zero, "NTLM", out intPtr);
					}
					else
					{
						intPtr = zero;
					}
					this.credentialsHandle = SspiWrapper.AcquireCredentialsHandle(this.clientPackageName, CredentialUse.Outbound, ref intPtr);
					if (IntPtr.Zero != intPtr)
					{
						UnsafeNativeMethods.SspiFreeAuthIdentity(intPtr);
					}
					this.CloseContext();
					this.MaxPromptAttempts++;
					return this.GetOutgoingBlob(null, channelbinding, protectionPolicy);
				}
				if (IntPtr.Zero != zero)
				{
					UnsafeNativeMethods.SspiFreeAuthIdentity(zero);
				}
				this.CloseContext();
				this.isCompleted = true;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception((int)num2, SR.GetString("SspiErrorOrInvalidClientCredentials")));
			}
			else
			{
				this.CloseContext();
				this.isCompleted = true;
				if (!this.isServer && (num == -2146893053 || num == -2146893022))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num, SR.GetString("IncorrectSpnOrUpnSpecified", new object[]
					{
						this.servicePrincipalName
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num, SR.GetString("InvalidSspiNegotiation")));
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00065E8F File Offset: 0x0006408F
		public void ImpersonateContext()
		{
			this.ThrowIfDisposed();
			if (!this.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
			}
			SspiWrapper.ImpersonateSecurityContext(this.securityContext);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00065EC0 File Offset: 0x000640C0
		internal void CloseContext()
		{
			this.ThrowIfDisposed();
			try
			{
				if (this.securityContext != null)
				{
					this.securityContext.Close();
				}
			}
			finally
			{
				this.securityContext = null;
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00065F00 File Offset: 0x00064100
		private void Dispose(bool disposing)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.CloseContext();
					}
					this.protocolName = null;
					this.servicePrincipalName = null;
					this.sizes = null;
					this.disposed = true;
				}
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00065F68 File Offset: 0x00064168
		internal SafeCloseHandle GetContextToken()
		{
			if (!this.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
			}
			SafeCloseHandle safeCloseHandle;
			SecurityStatus securityStatus = (SecurityStatus)SspiWrapper.QuerySecurityContextToken(this.securityContext, out safeCloseHandle);
			if (securityStatus != SecurityStatus.OK)
			{
				Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception((int)securityStatus));
			}
			return safeCloseHandle;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00065FBB File Offset: 0x000641BB
		private void OnBadData()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadData")));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00065FD8 File Offset: 0x000641D8
		private void ThrowIfDisposed()
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (this.disposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(null));
				}
			}
		}

		// Token: 0x04001D51 RID: 7505
		private const int DefaultMaxPromptAttempts = 1;

		// Token: 0x04001D52 RID: 7506
		private SspiContextFlags contextFlags;

		// Token: 0x04001D53 RID: 7507
		private SafeFreeCredentials credentialsHandle;

		// Token: 0x04001D54 RID: 7508
		private bool disposed;

		// Token: 0x04001D55 RID: 7509
		private bool doMutualAuth;

		// Token: 0x04001D56 RID: 7510
		private TokenImpersonationLevel impersonationLevel;

		// Token: 0x04001D57 RID: 7511
		private bool isCompleted;

		// Token: 0x04001D58 RID: 7512
		private bool isServer;

		// Token: 0x04001D59 RID: 7513
		private LifeSpan lifespan;

		// Token: 0x04001D5A RID: 7514
		private string protocolName;

		// Token: 0x04001D5B RID: 7515
		private SafeDeleteContext securityContext;

		// Token: 0x04001D5C RID: 7516
		private string servicePrincipalName;

		// Token: 0x04001D5D RID: 7517
		private SecSizes sizes;

		// Token: 0x04001D5E RID: 7518
		private object syncObject = new object();

		// Token: 0x04001D5F RID: 7519
		private int tokenSize;

		// Token: 0x04001D60 RID: 7520
		private bool interactiveNegoLogonEnabled = true;

		// Token: 0x04001D61 RID: 7521
		private string clientPackageName;

		// Token: 0x04001D62 RID: 7522
		private bool saveClientCredentialsOnSspiUi = true;

		// Token: 0x04001D63 RID: 7523
		private bool allowNtlm;

		// Token: 0x04001D64 RID: 7524
		private int MaxPromptAttempts;
	}
}
