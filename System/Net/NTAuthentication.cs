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
	// Token: 0x020004F7 RID: 1271
	internal class NTAuthentication
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000A39C9 File Offset: 0x000A29C9
		internal string UniqueUserId
		{
			get
			{
				return this.m_UniqueUserId;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000A39D1 File Offset: 0x000A29D1
		internal bool IsCompleted
		{
			get
			{
				return this.m_IsCompleted;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000A39D9 File Offset: 0x000A29D9
		internal bool IsValidContext
		{
			get
			{
				return this.m_SecurityContext != null && !this.m_SecurityContext.IsInvalid;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x000A39F4 File Offset: 0x000A29F4
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

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x000A3A34 File Offset: 0x000A2A34
		internal bool IsConfidentialityFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.Confidentiality) != ContextFlags.Zero;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000A3A45 File Offset: 0x000A2A45
		internal bool IsIntegrityFlag
		{
			get
			{
				return (this.m_ContextFlags & (this.m_IsServer ? ContextFlags.AcceptIntegrity : ContextFlags.AcceptStream)) != ContextFlags.Zero;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000A3A68 File Offset: 0x000A2A68
		internal bool IsMutualAuthFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.MutualAuth) != ContextFlags.Zero;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000A3A78 File Offset: 0x000A2A78
		internal bool IsDelegationFlag
		{
			get
			{
				return (this.m_ContextFlags & ContextFlags.Delegate) != ContextFlags.Zero;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000A3A88 File Offset: 0x000A2A88
		internal bool IsIdentifyFlag
		{
			get
			{
				return (this.m_ContextFlags & (this.m_IsServer ? ContextFlags.InitManualCredValidation : ContextFlags.AcceptIntegrity)) != ContextFlags.Zero;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000A3AAB File Offset: 0x000A2AAB
		internal string Spn
		{
			get
			{
				return this.m_Spn;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000A3AB3 File Offset: 0x000A2AB3
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

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000A3AD0 File Offset: 0x000A2AD0
		internal bool OSSupportsExtendedProtection
		{
			get
			{
				int num;
				SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.ClientSpecifiedSpn, out num);
				return num != -2146893054;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000A3AFD File Offset: 0x000A2AFD
		internal bool IsServer
		{
			get
			{
				return this.m_IsServer;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000A3B05 File Offset: 0x000A2B05
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000A3B28 File Offset: 0x000A2B28
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

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000A3B4B File Offset: 0x000A2B4B
		internal string Package
		{
			get
			{
				return this.m_Package;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000A3B54 File Offset: 0x000A2B54
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
					if (this.IsCompleted)
					{
						if (negotiationInfoClass == null)
						{
							if (ComNetOS.IsWin9x)
							{
								this.m_ProtocolName = "NTLM";
								return this.m_ProtocolName;
							}
						}
						else
						{
							this.m_ProtocolName = negotiationInfoClass.AuthenticationPackage;
						}
					}
				}
				if (negotiationInfoClass != null)
				{
					return negotiationInfoClass.AuthenticationPackage;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000A3BD1 File Offset: 0x000A2BD1
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000A3BFD File Offset: 0x000A2BFD
		internal ChannelBinding ChannelBinding
		{
			get
			{
				return this.m_ChannelBinding;
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000A3C08 File Offset: 0x000A2C08
		internal NTAuthentication(string package, NetworkCredential networkCredential, string spn, WebRequest request, ChannelBinding channelBinding) : this(false, package, networkCredential, spn, NTAuthentication.GetHttpContextFlags(request), request.GetWritingContext(), channelBinding)
		{
			if (package == "NTLM" || package == "Negotiate")
			{
				this.m_UniqueUserId = Interlocked.Increment(ref NTAuthentication.s_UniqueGroupId).ToString(NumberFormatInfo.InvariantInfo) + this.m_UniqueUserId;
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000A3C74 File Offset: 0x000A2C74
		private static ContextFlags GetHttpContextFlags(WebRequest request)
		{
			ContextFlags contextFlags = ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.Connection;
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
			return contextFlags;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000A3CD8 File Offset: 0x000A2CD8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ContextAwareResult context, ChannelBinding channelBinding)
		{
			if (credential is SystemNetworkCredential && ComNetOS.IsWinNt)
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
							goto IL_91;
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
					IL_91:
					return;
				}
				catch
				{
					throw;
				}
			}
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000A3DA8 File Offset: 0x000A2DA8
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlags requestedContextFlags, ChannelBinding channelBinding)
		{
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000A3DC0 File Offset: 0x000A2DC0
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

		// Token: 0x060027D3 RID: 10195 RVA: 0x000A3E20 File Offset: 0x000A2E20
		private static void InitializeCallback(object state)
		{
			NTAuthentication.InitializeCallbackContext initializeCallbackContext = (NTAuthentication.InitializeCallbackContext)state;
			initializeCallbackContext.thisPtr.Initialize(initializeCallbackContext.isServer, initializeCallbackContext.package, initializeCallbackContext.credential, initializeCallbackContext.spn, initializeCallbackContext.requestedContextFlags, initializeCallbackContext.channelBinding);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000A3E64 File Offset: 0x000A2E64
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
			string text = credential.InternalGetUserName();
			string text2 = credential.InternalGetDomain();
			AuthIdentity authIdentity = new AuthIdentity(text, credential.InternalGetPassword(), (package == "WDigest" && (text2 == null || text2.Length == 0)) ? null : text2);
			this.m_UniqueUserId = text2 + "/" + text + "/U";
			this.m_CredentialsHandle = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPIAuth, package, this.m_IsServer ? CredentialUse.Inbound : CredentialUse.Outbound, ref authIdentity);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000A3F54 File Offset: 0x000A2F54
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

		// Token: 0x060027D6 RID: 10198 RVA: 0x000A3F8C File Offset: 0x000A2F8C
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

		// Token: 0x060027D7 RID: 10199 RVA: 0x000A3FAD File Offset: 0x000A2FAD
		internal void CloseContext()
		{
			if (this.m_SecurityContext != null && !this.m_SecurityContext.IsClosed)
			{
				this.m_SecurityContext.Close();
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000A3FD0 File Offset: 0x000A2FD0
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
			if (array2 != null && array2.Length > 0)
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

		// Token: 0x060027D9 RID: 10201 RVA: 0x000A4044 File Offset: 0x000A3044
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

		// Token: 0x060027DA RID: 10202 RVA: 0x000A41B4 File Offset: 0x000A31B4
		internal string GetOutgoingDigestBlob(string incomingBlob, string requestMethod, string requestedUri, string realm, bool isClientPreAuth, bool throwOnError, out SecurityStatus statusCode)
		{
			this.m_RequestedContextFlags |= (ContextFlags.ReplayDetect | ContextFlags.SequenceDetect);
			SecurityBuffer[] array = null;
			SecurityBuffer securityBuffer = new SecurityBuffer(this.m_TokenSize, isClientPreAuth ? BufferType.Parameters : BufferType.Token);
			bool flag = this.m_SecurityContext == null;
			try
			{
				if (!this.m_IsServer)
				{
					if (!isClientPreAuth)
					{
						this.m_RequestedContextFlags &= ~ContextFlags.Confidentiality;
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
				if (token != null && token.Length > 0)
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

		// Token: 0x060027DB RID: 10203 RVA: 0x000A4470 File Offset: 0x000A3470
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

		// Token: 0x060027DC RID: 10204 RVA: 0x000A469C File Offset: 0x000A369C
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

		// Token: 0x060027DD RID: 10205 RVA: 0x000A4778 File Offset: 0x000A3778
		private string GetClientSpecifiedSpn()
		{
			return SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPIAuth, this.m_SecurityContext, ContextAttribute.ClientSpecifiedSpn) as string;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000A47A0 File Offset: 0x000A37A0
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

		// Token: 0x060027DF RID: 10207 RVA: 0x000A4858 File Offset: 0x000A3858
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

		// Token: 0x060027E0 RID: 10208 RVA: 0x000A48F4 File Offset: 0x000A38F4
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

		// Token: 0x040026F8 RID: 9976
		private static int s_UniqueGroupId = 1;

		// Token: 0x040026F9 RID: 9977
		private static ContextCallback s_InitializeCallback = new ContextCallback(NTAuthentication.InitializeCallback);

		// Token: 0x040026FA RID: 9978
		private bool m_IsServer;

		// Token: 0x040026FB RID: 9979
		private SafeFreeCredentials m_CredentialsHandle;

		// Token: 0x040026FC RID: 9980
		private SafeDeleteContext m_SecurityContext;

		// Token: 0x040026FD RID: 9981
		private string m_Spn;

		// Token: 0x040026FE RID: 9982
		private string m_ClientSpecifiedSpn;

		// Token: 0x040026FF RID: 9983
		private int m_TokenSize;

		// Token: 0x04002700 RID: 9984
		private ContextFlags m_RequestedContextFlags;

		// Token: 0x04002701 RID: 9985
		private ContextFlags m_ContextFlags;

		// Token: 0x04002702 RID: 9986
		private string m_UniqueUserId;

		// Token: 0x04002703 RID: 9987
		private bool m_IsCompleted;

		// Token: 0x04002704 RID: 9988
		private string m_ProtocolName;

		// Token: 0x04002705 RID: 9989
		private SecSizes m_Sizes;

		// Token: 0x04002706 RID: 9990
		private string m_LastProtocolName;

		// Token: 0x04002707 RID: 9991
		private string m_Package;

		// Token: 0x04002708 RID: 9992
		private ChannelBinding m_ChannelBinding;

		// Token: 0x020004F8 RID: 1272
		private class InitializeCallbackContext
		{
			// Token: 0x060027E2 RID: 10210 RVA: 0x000A49AC File Offset: 0x000A39AC
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

			// Token: 0x04002709 RID: 9993
			internal readonly NTAuthentication thisPtr;

			// Token: 0x0400270A RID: 9994
			internal readonly bool isServer;

			// Token: 0x0400270B RID: 9995
			internal readonly string package;

			// Token: 0x0400270C RID: 9996
			internal readonly NetworkCredential credential;

			// Token: 0x0400270D RID: 9997
			internal readonly string spn;

			// Token: 0x0400270E RID: 9998
			internal readonly ContextFlags requestedContextFlags;

			// Token: 0x0400270F RID: 9999
			internal readonly ChannelBinding channelBinding;
		}
	}
}
