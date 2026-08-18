using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001CF RID: 463
	internal class NTAuthentication
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00061797 File Offset: 0x0005F997
		internal string UniqueUserId
		{
			get
			{
				return this.m_UniqueUserId;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0006179F File Offset: 0x0005F99F
		internal bool IsCompleted
		{
			get
			{
				return this.m_IsCompleted;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x000617A7 File Offset: 0x0005F9A7
		internal bool IsValidContext
		{
			get
			{
				return this.m_SecurityContext != null && !this.m_SecurityContext.IsInvalid;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x000617C4 File Offset: 0x0005F9C4
		internal string AssociatedName
		{
			get
			{
				if (!this.IsValidContext || !this.IsCompleted)
				{
					throw new Win32Exception(-2146893055);
				}
				return SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.Names) as string;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00061804 File Offset: 0x0005FA04
		internal bool IsConfidentialityFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.Confidentiality) > ContextFlags.Zero;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00061812 File Offset: 0x0005FA12
		internal bool IsIntegrityFlag
		{
			get
			{
				return (this.m_ContextFlags & (this.m_IsServer ? ContextFlags.AcceptIntegrity : ContextFlags.AcceptStream)) > ContextFlags.Zero;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00061832 File Offset: 0x0005FA32
		internal bool IsMutualAuthFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.MutualAuth) > ContextFlags.Zero;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0006183F File Offset: 0x0005FA3F
		internal bool IsDelegationFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.Delegate) > ContextFlags.Zero;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0006184C File Offset: 0x0005FA4C
		internal bool IsIdentifyFlag
		{
			get
			{
				return (this.m_ContextFlags & (this.m_IsServer ? ContextFlags.InitManualCredValidation : ContextFlags.AcceptIntegrity)) > ContextFlags.Zero;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0006186C File Offset: 0x0005FA6C
		internal string Spn
		{
			get
			{
				return this.m_Spn;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00061874 File Offset: 0x0005FA74
		internal string ClientSpecifiedSpn
		{
			get
			{
				if (this.m_ClientSpecifiedSpn == null)
				{
					this.m_ClientSpecifiedSpn = this.GetClientSpecifiedSpn();
				}
				return this.m_ClientSpecifiedSpn;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00061890 File Offset: 0x0005FA90
		internal bool OSSupportsExtendedProtection
		{
			get
			{
				int num;
				SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.ClientSpecifiedSpn, out num);
				return num != -2146893054;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000618BD File Offset: 0x0005FABD
		internal bool IsServer
		{
			get
			{
				return this.m_IsServer;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000618C5 File Offset: 0x0005FAC5
		internal bool IsKerberos
		{
			get
			{
				if (this.m_LastProtocolName == null)
				{
					this.m_LastProtocolName = this.ProtocolName;
				}
				return this.m_LastProtocolName == "Kerberos";
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000618E8 File Offset: 0x0005FAE8
		internal bool IsNTLM
		{
			get
			{
				if (this.m_LastProtocolName == null)
				{
					this.m_LastProtocolName = this.ProtocolName;
				}
				return this.m_LastProtocolName == "NTLM";
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0006190B File Offset: 0x0005FB0B
		internal string Package
		{
			get
			{
				return this.m_Package;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00061914 File Offset: 0x0005FB14
		internal string ProtocolName
		{
			get
			{
				if (this.m_ProtocolName != null)
				{
					return this.m_ProtocolName;
				}
				NegotiationInfoClass negotiationInfoClass = null;
				if (this.IsValidContext)
				{
					negotiationInfoClass = (SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.NegotiationInfo) as NegotiationInfoClass);
					if (this.IsCompleted && negotiationInfoClass != null)
					{
						this.m_ProtocolName = negotiationInfoClass.AuthenticationPackage;
					}
				}
				if (negotiationInfoClass != null)
				{
					return negotiationInfoClass.AuthenticationPackage;
				}
				return string.Empty;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00061978 File Offset: 0x0005FB78
		internal SecSizes Sizes
		{
			get
			{
				if (this.m_Sizes == null)
				{
					this.m_Sizes = (SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.Sizes) as SecSizes);
				}
				return this.m_Sizes;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000619A4 File Offset: 0x0005FBA4
		internal ChannelBinding ChannelBinding
		{
			get
			{
				return this.m_ChannelBinding;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000619AC File Offset: 0x0005FBAC
		internal NTAuthentication(string package, NetworkCredential networkCredential, SpnToken spnToken, WebRequest request, ChannelBinding channelBinding) : this(false, package, networkCredential, spnToken.Spn, NTAuthentication.GetHttpContextFlags(request, spnToken.IsTrusted), request.GetWritingContext(), channelBinding)
		{
			if (package == "NTLM" || package == "Negotiate")
			{
				this.m_UniqueUserId = Interlocked.Increment(ref NTAuthentication.s_UniqueGroupId).ToString(NumberFormatInfo.InvariantInfo) + this.m_UniqueUserId;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00061A20 File Offset: 0x0005FC20
		private static ContextFlags GetHttpContextFlags(WebRequest request, bool trustedSpn)
		{
			ContextFlags contextFlags = ContextFlags.Connection;
			if (request.ImpersonationLevel == TokenImpersonationLevel.Anonymous)
			{
				throw new NotSupportedException(SR.GetString("net_auth_no_anonymous_support"));
			}
			if (request.ImpersonationLevel == TokenImpersonationLevel.Identification)
			{
				contextFlags |= ContextFlags.AcceptIntegrity;
			}
			else if (request.ImpersonationLevel == TokenImpersonationLevel.Delegation)
			{
				contextFlags |= ContextFlags.Delegate;
			}
			if (request.AuthenticationLevel == AuthenticationLevel.MutualAuthRequested || request.AuthenticationLevel == AuthenticationLevel.MutualAuthRequired)
			{
				contextFlags |= ContextFlags.MutualAuth;
			}
			if (!trustedSpn && ComNetOS.IsWin7Sp1orLater)
			{
				contextFlags |= ContextFlags.UnverifiedTargetName;
			}
			return contextFlags;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00061A98 File Offset: 0x0005FC98
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ContextAwareResult context, ChannelBinding channelBinding)
		{
			if (credential is SystemNetworkCredential)
			{
				WindowsIdentity windowsIdentity = (context == null) ? null : context.Identity;
				try
				{
					IDisposable disposable = (windowsIdentity == null) ? null : windowsIdentity.Impersonate();
					if (disposable != null)
					{
						using (disposable)
						{
							this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
							goto IL_87;
						}
					}
					ExecutionContext executionContext = (context == null) ? null : context.ContextCopy;
					if (executionContext == null)
					{
						this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
					}
					else
					{
						ExecutionContext.Run(executionContext, NTAuthentication.s_InitializeCallback, new NTAuthentication.InitializeCallbackContext(this, isServer, package, credential, spn, requestedContextFlags, channelBinding));
					}
					IL_87:
					return;
				}
				catch
				{
					throw;
				}
			}
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00061B5C File Offset: 0x0005FD5C
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ChannelBinding channelBinding)
		{
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00061B74 File Offset: 0x0005FD74
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal NTAuthentication(bool isServer, string package, string spn, ContextFlags requestedContextFlags, ChannelBinding channelBinding)
		{
			try
			{
				using (WindowsIdentity.Impersonate(IntPtr.Zero))
				{
					this.Initialize(isServer, package, SystemNetworkCredential.defaultCredential, spn, requestedContextFlags, channelBinding);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00061BD4 File Offset: 0x0005FDD4
		private static void InitializeCallback(object state)
		{
			NTAuthentication.InitializeCallbackContext initializeCallbackContext = (NTAuthentication.InitializeCallbackContext)state;
			initializeCallbackContext.thisPtr.Initialize(initializeCallbackContext.isServer, initializeCallbackContext.package, initializeCallbackContext.credential, initializeCallbackContext.spn, initializeCallbackContext.requestedContextFlags, initializeCallbackContext.channelBinding);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00061C18 File Offset: 0x0005FE18
		private void Initialize(bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ChannelBinding channelBinding)
		{
			this.m_TokenSize = SSPIWrapper.GetVerifyPackageInfo(GlobalSSPI.SSPIAuth, package, true).MaxToken;
			this.m_IsServer = isServer;
			this.m_Spn = spn;
			this.m_SecurityContext = null;
			this.m_RequestedContextFlags = requestedContextFlags;
			this.m_Package = package;
			this.m_ChannelBinding = channelBinding;
			if (credential is SystemNetworkCredential)
			{
				this.m_CredentialsHandle = SSPIWrapper.AcquireDefaultCredential(GlobalSSPI.SSPIAuth, package, this.m_IsServer ? CredentialUse.Inbound : CredentialUse.Outbound);
				this.m_UniqueUserId = "/S";
				return;
			}
			if (ComNetOS.IsWin7orLater)
			{
				SafeSspiAuthDataHandle safeSspiAuthDataHandle = null;
				try
				{
					SecurityStatus securityStatus = UnsafeNclNativeMethods.SspiHelper.SspiEncodeStringsAsAuthIdentity(credential.InternalGetUserName(), credential.InternalGetDomain(), credential.InternalGetPassword(), out safeSspiAuthDataHandle);
					if (securityStatus != SecurityStatus.OK)
					{
						if (Logging.On)
						{
							Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
							{
								"SspiEncodeStringsAsAuthIdentity()",
								string.Format(CultureInfo.CurrentCulture, "0x{0:X}", new object[]
								{
									(int)securityStatus
								})
							}));
						}
						throw new Win32Exception((int)securityStatus);
					}
					this.m_CredentialsHandle = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPIAuth, package, this.m_IsServer ? CredentialUse.Inbound : CredentialUse.Outbound, ref safeSspiAuthDataHandle);
					return;
				}
				finally
				{
					if (safeSspiAuthDataHandle != null)
					{
						safeSspiAuthDataHandle.Close();
					}
				}
			}
			string text = credential.InternalGetUserName();
			string text2 = credential.InternalGetDomain();
			AuthIdentity authIdentity = new AuthIdentity(text, credential.InternalGetPassword(), (package == "WDigest" && (text2 == null || text2.Length == 0)) ? null : text2);
			this.m_UniqueUserId = text2 + "/" + text + "/U";
			this.m_CredentialsHandle = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPIAuth, package, this.m_IsServer ? CredentialUse.Inbound : CredentialUse.Outbound, ref authIdentity);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00061DB8 File Offset: 0x0005FFB8
		internal SafeCloseHandle GetContextToken(out SecurityStatus status)
		{
			if (!this.IsValidContext)
			{
				throw new Win32Exception(-2146893055);
			}
			SafeCloseHandle result = null;
			status = (SecurityStatus)SSPIWrapper.QuerySecurityContextToken(GlobalSSPI.SSPIAuth, this.m_SecurityContext, out result);
			return result;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00061DF0 File Offset: 0x0005FFF0
		internal SafeCloseHandle GetContextToken()
		{
			SecurityStatus securityStatus;
			SafeCloseHandle contextToken = this.GetContextToken(out securityStatus);
			if (securityStatus != SecurityStatus.OK)
			{
				throw new Win32Exception((int)securityStatus);
			}
			return contextToken;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00061E11 File Offset: 0x00060011
		internal void CloseContext()
		{
			if (this.m_SecurityContext != null && !this.m_SecurityContext.IsClosed)
			{
				this.m_SecurityContext.Close();
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00061E34 File Offset: 0x00060034
		internal string GetOutgoingBlob(string incomingBlob)
		{
			byte[] array = null;
			if (incomingBlob != null && incomingBlob.Length > 0)
			{
				array = Convert.FromBase64String(incomingBlob);
			}
			byte[] array2 = null;
			if ((this.IsValidContext || this.IsCompleted) && array == null)
			{
				this.m_IsCompleted = true;
			}
			else
			{
				SecurityStatus securityStatus;
				array2 = this.GetOutgoingBlob(array, true, out securityStatus);
			}
			string result = null;
			if (array2 != null && array2.Length != 0)
			{
				result = Convert.ToBase64String(array2);
			}
			if (this.IsCompleted)
			{
				string protocolName = this.ProtocolName;
				this.CloseContext();
			}
			return result;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00061EA8 File Offset: 0x000600A8
		internal byte[] GetOutgoingBlob(byte[] incomingBlob, bool throwOnError, out SecurityStatus statusCode)
		{
			List<SecurityBuffer> list = new List<SecurityBuffer>(2);
			if (incomingBlob != null)
			{
				list.Add(new SecurityBuffer(incomingBlob, BufferType.Token));
			}
			if (this.m_ChannelBinding != null)
			{
				list.Add(new SecurityBuffer(this.m_ChannelBinding));
			}
			SecurityBuffer[] inputBuffers = null;
			if (list.Count > 0)
			{
				inputBuffers = list.ToArray();
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(this.m_TokenSize, BufferType.Token);
			bool flag = this.m_SecurityContext == null;
			try
			{
				if (!this.m_IsServer)
				{
					statusCode = (SecurityStatus)SSPIWrapper.InitializeSecurityContext(GlobalSSPI.SSPIAuth, this.m_CredentialsHandle, ref this.m_SecurityContext, this.m_Spn, this.m_RequestedContextFlags, Endianness.Network, inputBuffers, securityBuffer, ref this.m_ContextFlags);
					if (statusCode == SecurityStatus.CompleteNeeded)
					{
						SecurityBuffer[] inputBuffers2 = new SecurityBuffer[]
						{
							securityBuffer
						};
						statusCode = (SecurityStatus)SSPIWrapper.CompleteAuthToken(GlobalSSPI.SSPIAuth, ref this.m_SecurityContext, inputBuffers2);
						securityBuffer.token = null;
					}
				}
				else
				{
					statusCode = (SecurityStatus)SSPIWrapper.AcceptSecurityContext(GlobalSSPI.SSPIAuth, this.m_CredentialsHandle, ref this.m_SecurityContext, this.m_RequestedContextFlags, Endianness.Network, inputBuffers, securityBuffer, ref this.m_ContextFlags);
				}
			}
			finally
			{
				if (flag && this.m_CredentialsHandle != null)
				{
					this.m_CredentialsHandle.Close();
				}
			}
			if ((statusCode & (SecurityStatus)(-2147483648)) == SecurityStatus.OK)
			{
				if (flag && this.m_CredentialsHandle != null)
				{
					SSPIHandleCache.CacheCredential(this.m_CredentialsHandle);
				}
				if (statusCode == SecurityStatus.OK)
				{
					this.m_IsCompleted = true;
				}
				return securityBuffer.token;
			}
			this.CloseContext();
			this.m_IsCompleted = true;
			if (throwOnError)
			{
				Win32Exception ex = new Win32Exception((int)statusCode);
				throw ex;
			}
			return null;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00062018 File Offset: 0x00060218
		internal string GetOutgoingDigestBlob(string incomingBlob, string requestMethod, string requestedUri, string realm, bool isClientPreAuth, bool throwOnError, out SecurityStatus statusCode)
		{
			SecurityBuffer[] array = null;
			SecurityBuffer securityBuffer = new SecurityBuffer(this.m_TokenSize, isClientPreAuth ? BufferType.Parameters : BufferType.Token);
			bool flag = this.m_SecurityContext == null;
			try
			{
				if (!this.m_IsServer)
				{
					if (!isClientPreAuth)
					{
						if (incomingBlob != null)
						{
							List<SecurityBuffer> list = new List<SecurityBuffer>(5);
							list.Add(new SecurityBuffer(WebHeaderCollection.HeaderEncoding.GetBytes(incomingBlob), BufferType.Token));
							list.Add(new SecurityBuffer(WebHeaderCollection.HeaderEncoding.GetBytes(requestMethod), BufferType.Parameters));
							list.Add(new SecurityBuffer(null, BufferType.Parameters));
							list.Add(new SecurityBuffer(Encoding.Unicode.GetBytes(this.m_Spn), BufferType.TargetHost));
							if (this.m_ChannelBinding != null)
							{
								list.Add(new SecurityBuffer(this.m_ChannelBinding));
							}
							array = list.ToArray();
						}
						statusCode = (SecurityStatus)SSPIWrapper.InitializeSecurityContext(GlobalSSPI.SSPIAuth, this.m_CredentialsHandle, ref this.m_SecurityContext, requestedUri, this.m_RequestedContextFlags, Endianness.Network, array, securityBuffer, ref this.m_ContextFlags);
					}
					else
					{
						statusCode = SecurityStatus.OK;
					}
				}
				else
				{
					List<SecurityBuffer> list2 = new List<SecurityBuffer>(6);
					list2.Add((incomingBlob == null) ? new SecurityBuffer(0, BufferType.Token) : new SecurityBuffer(WebHeaderCollection.HeaderEncoding.GetBytes(incomingBlob), BufferType.Token));
					list2.Add((requestMethod == null) ? new SecurityBuffer(0, BufferType.Parameters) : new SecurityBuffer(WebHeaderCollection.HeaderEncoding.GetBytes(requestMethod), BufferType.Parameters));
					list2.Add((requestedUri == null) ? new SecurityBuffer(0, BufferType.Parameters) : new SecurityBuffer(WebHeaderCollection.HeaderEncoding.GetBytes(requestedUri), BufferType.Parameters));
					list2.Add(new SecurityBuffer(0, BufferType.Parameters));
					list2.Add((realm == null) ? new SecurityBuffer(0, BufferType.Parameters) : new SecurityBuffer(Encoding.Unicode.GetBytes(realm), BufferType.Parameters));
					if (this.m_ChannelBinding != null)
					{
						list2.Add(new SecurityBuffer(this.m_ChannelBinding));
					}
					array = list2.ToArray();
					statusCode = (SecurityStatus)SSPIWrapper.AcceptSecurityContext(GlobalSSPI.SSPIAuth, this.m_CredentialsHandle, ref this.m_SecurityContext, this.m_RequestedContextFlags, Endianness.Network, array, securityBuffer, ref this.m_ContextFlags);
					if (statusCode == SecurityStatus.CompleteNeeded)
					{
						array[4] = securityBuffer;
						statusCode = (SecurityStatus)SSPIWrapper.CompleteAuthToken(GlobalSSPI.SSPIAuth, ref this.m_SecurityContext, array);
						securityBuffer.token = null;
					}
				}
			}
			finally
			{
				if (flag && this.m_CredentialsHandle != null)
				{
					this.m_CredentialsHandle.Close();
				}
			}
			if ((statusCode & (SecurityStatus)(-2147483648)) == SecurityStatus.OK)
			{
				if (flag && this.m_CredentialsHandle != null)
				{
					SSPIHandleCache.CacheCredential(this.m_CredentialsHandle);
				}
				if (statusCode == SecurityStatus.OK)
				{
					this.m_IsCompleted = true;
				}
				byte[] token = securityBuffer.token;
				string result = null;
				if (token != null && token.Length != 0)
				{
					result = WebHeaderCollection.HeaderEncoding.GetString(token, 0, securityBuffer.size);
				}
				return result;
			}
			this.CloseContext();
			if (throwOnError)
			{
				Win32Exception ex = new Win32Exception((int)statusCode);
				throw ex;
			}
			return null;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000622B8 File Offset: 0x000604B8
		internal int Encrypt(byte[] buffer, int offset, int count, ref byte[] output, uint sequenceNumber)
		{
			SecSizes sizes = this.Sizes;
			try
			{
				int num = checked(2147483643 - sizes.BlockSize - sizes.SecurityTrailer);
				if (count > num || count < 0)
				{
					throw new ArgumentOutOfRangeException("count", SR.GetString("net_io_out_range", new object[]
					{
						num
					}));
				}
			}
			catch (Exception exception)
			{
				NclUtilities.IsFatal(exception);
				throw;
			}
			int num2 = count + sizes.SecurityTrailer + sizes.BlockSize;
			if (output == null || output.Length < num2 + 4)
			{
				output = new byte[num2 + 4];
			}
			Buffer.BlockCopy(buffer, offset, output, 4 + sizes.SecurityTrailer, count);
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(output, 4, sizes.SecurityTrailer, BufferType.Token),
				new SecurityBuffer(output, 4 + sizes.SecurityTrailer, count, BufferType.Data),
				new SecurityBuffer(output, 4 + sizes.SecurityTrailer + count, sizes.BlockSize, BufferType.Padding)
			};
			int num3;
			if (this.IsConfidentialityFlag)
			{
				num3 = SSPIWrapper.EncryptMessage(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, sequenceNumber);
			}
			else
			{
				if (this.IsNTLM)
				{
					array[1].type |= BufferType.ReadOnlyFlag;
				}
				num3 = SSPIWrapper.MakeSignature(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, 0U);
			}
			if (num3 != 0)
			{
				throw new Win32Exception(num3);
			}
			num2 = array[0].size;
			bool flag = false;
			if (num2 != sizes.SecurityTrailer)
			{
				flag = true;
				Buffer.BlockCopy(output, array[1].offset, output, 4 + num2, array[1].size);
			}
			num2 += array[1].size;
			if (array[2].size != 0 && (flag || num2 != count + sizes.SecurityTrailer))
			{
				Buffer.BlockCopy(output, array[2].offset, output, 4 + num2, array[2].size);
			}
			num2 += array[2].size;
			output[0] = (byte)(num2 & 255);
			output[1] = (byte)(num2 >> 8 & 255);
			output[2] = (byte)(num2 >> 16 & 255);
			output[3] = (byte)(num2 >> 24 & 255);
			return num2 + 4;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000624D0 File Offset: 0x000606D0
		internal int Decrypt(byte[] payload, int offset, int count, out int newOffset, uint expectedSeqNumber)
		{
			if (offset < 0 || offset > ((payload == null) ? 0 : payload.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((payload == null) ? 0 : (payload.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.IsNTLM)
			{
				return this.DecryptNtlm(payload, offset, count, out newOffset, expectedSeqNumber);
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(payload, offset, count, BufferType.Stream),
				new SecurityBuffer(0, BufferType.Data)
			};
			int num;
			if (this.IsConfidentialityFlag)
			{
				num = SSPIWrapper.DecryptMessage(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, expectedSeqNumber);
			}
			else
			{
				num = SSPIWrapper.VerifySignature(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, expectedSeqNumber);
			}
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			if (array[1].type != BufferType.Data)
			{
				throw new InternalException();
			}
			newOffset = array[1].offset;
			return array[1].size;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000625AC File Offset: 0x000607AC
		private string GetClientSpecifiedSpn()
		{
			return SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.ClientSpecifiedSpn) as string;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000625D4 File Offset: 0x000607D4
		private int DecryptNtlm(byte[] payload, int offset, int count, out int newOffset, uint expectedSeqNumber)
		{
			if (count < 16)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(payload, offset, 16, BufferType.Token),
				new SecurityBuffer(payload, offset + 16, count - 16, BufferType.Data)
			};
			BufferType bufferType = BufferType.Data;
			int num;
			if (this.IsConfidentialityFlag)
			{
				num = SSPIWrapper.DecryptMessage(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, expectedSeqNumber);
			}
			else
			{
				bufferType |= BufferType.ReadOnlyFlag;
				array[1].type = bufferType;
				num = SSPIWrapper.VerifySignature(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, expectedSeqNumber);
			}
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			if (array[1].type != bufferType)
			{
				throw new InternalException();
			}
			newOffset = array[1].offset;
			return array[1].size;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006268C File Offset: 0x0006088C
		internal int VerifySignature(byte[] buffer, int offset, int count)
		{
			if (offset < 0 || offset > ((buffer == null) ? 0 : buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((buffer == null) ? 0 : (buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(buffer, offset, count, BufferType.Stream),
				new SecurityBuffer(0, BufferType.Data)
			};
			int num = SSPIWrapper.VerifySignature(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, 0U);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			if (array[1].type != BufferType.Data)
			{
				throw new InternalException();
			}
			return array[1].size;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00062728 File Offset: 0x00060928
		internal int MakeSignature(byte[] buffer, int offset, int count, ref byte[] output)
		{
			SecSizes sizes = this.Sizes;
			int num = count + sizes.MaxSignature;
			if (output == null || output.Length < num)
			{
				output = new byte[num];
			}
			Buffer.BlockCopy(buffer, offset, output, sizes.MaxSignature, count);
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(output, 0, sizes.MaxSignature, BufferType.Token),
				new SecurityBuffer(output, sizes.MaxSignature, count, BufferType.Data)
			};
			int num2 = SSPIWrapper.MakeSignature(GlobalSSPI.SSPIAuth, this.m_SecurityContext, array, 0U);
			if (num2 != 0)
			{
				throw new Win32Exception(num2);
			}
			return array[0].size + array[1].size;
		}

		// Token: 0x040014BE RID: 5310
		private static int s_UniqueGroupId = 1;

		// Token: 0x040014BF RID: 5311
		private static ContextCallback s_InitializeCallback = new ContextCallback(NTAuthentication.InitializeCallback);

		// Token: 0x040014C0 RID: 5312
		private bool m_IsServer;

		// Token: 0x040014C1 RID: 5313
		private SafeFreeCredentials m_CredentialsHandle;

		// Token: 0x040014C2 RID: 5314
		private SafeDeleteContext m_SecurityContext;

		// Token: 0x040014C3 RID: 5315
		private string m_Spn;

		// Token: 0x040014C4 RID: 5316
		private string m_ClientSpecifiedSpn;

		// Token: 0x040014C5 RID: 5317
		private int m_TokenSize;

		// Token: 0x040014C6 RID: 5318
		private ContextFlags m_RequestedContextFlags;

		// Token: 0x040014C7 RID: 5319
		private ContextFlags m_ContextFlags;

		// Token: 0x040014C8 RID: 5320
		private string m_UniqueUserId;

		// Token: 0x040014C9 RID: 5321
		private bool m_IsCompleted;

		// Token: 0x040014CA RID: 5322
		private string m_ProtocolName;

		// Token: 0x040014CB RID: 5323
		private SecSizes m_Sizes;

		// Token: 0x040014CC RID: 5324
		private string m_LastProtocolName;

		// Token: 0x040014CD RID: 5325
		private string m_Package;

		// Token: 0x040014CE RID: 5326
		private ChannelBinding m_ChannelBinding;

		// Token: 0x02000753 RID: 1875
		private class InitializeCallbackContext
		{
			// Token: 0x06004204 RID: 16900 RVA: 0x00112546 File Offset: 0x00110746
			internal InitializeCallbackContext(NTAuthentication thisPtr, bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ChannelBinding channelBinding)
			{
				this.thisPtr = thisPtr;
				this.isServer = isServer;
				this.package = package;
				this.credential = credential;
				this.spn = spn;
				this.requestedContextFlags = requestedContextFlags;
				this.channelBinding = channelBinding;
			}

			// Token: 0x0400320F RID: 12815
			internal readonly NTAuthentication thisPtr;

			// Token: 0x04003210 RID: 12816
			internal readonly bool isServer;

			// Token: 0x04003211 RID: 12817
			internal readonly string package;

			// Token: 0x04003212 RID: 12818
			internal readonly NetworkCredential credential;

			// Token: 0x04003213 RID: 12819
			internal readonly string spn;

			// Token: 0x04003214 RID: 12820
			internal readonly ContextFlags requestedContextFlags;

			// Token: 0x04003215 RID: 12821
			internal readonly ChannelBinding channelBinding;
		}
	}
}
