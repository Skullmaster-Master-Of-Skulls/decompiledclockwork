using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x0200035F RID: 863
	internal class NegoState
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x00091603 File Offset: 0x0008F803
		internal NegoState(Stream innerStream, bool leaveStreamOpen)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._InnerStream = innerStream;
			this._LeaveStreamOpen = leaveStreamOpen;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00091627 File Offset: 0x0008F827
		internal static string DefaultPackage
		{
			get
			{
				return "Negotiate";
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00091630 File Offset: 0x0008F830
		internal void ValidateCreateContext(string package, NetworkCredential credential, string servicePrincipalName, ExtendedProtectionPolicy policy, ProtectionLevel protectionLevel, TokenImpersonationLevel impersonationLevel)
		{
			if (policy != null)
			{
				if (!AuthenticationManager.OSSupportsExtendedProtection)
				{
					if (policy.PolicyEnforcement == PolicyEnforcement.Always)
					{
						throw new PlatformNotSupportedException(SR.GetString("security_ExtendedProtection_NoOSSupport"));
					}
				}
				else if (policy.CustomChannelBinding == null && policy.CustomServiceNames == null)
				{
					throw new ArgumentException(SR.GetString("net_auth_must_specify_extended_protection_scheme"), "policy");
				}
				this._ExtendedProtectionPolicy = policy;
			}
			else
			{
				this._ExtendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
			}
			this.ValidateCreateContext(package, true, credential, servicePrincipalName, this._ExtendedProtectionPolicy.CustomChannelBinding, protectionLevel, impersonationLevel);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000916B8 File Offset: 0x0008F8B8
		internal void ValidateCreateContext(string package, bool isServer, NetworkCredential credential, string servicePrincipalName, ChannelBinding channelBinding, ProtectionLevel protectionLevel, TokenImpersonationLevel impersonationLevel)
		{
			if (this._Exception != null && !this._CanRetryAuthentication)
			{
				throw this._Exception;
			}
			if (this._Context != null && this._Context.IsValidContext)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_reauth"));
			}
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			if (servicePrincipalName == null)
			{
				throw new ArgumentNullException("servicePrincipalName");
			}
			if (impersonationLevel != TokenImpersonationLevel.Identification && impersonationLevel != TokenImpersonationLevel.Impersonation && impersonationLevel != TokenImpersonationLevel.Delegation)
			{
				throw new ArgumentOutOfRangeException("impersonationLevel", impersonationLevel.ToString(), SR.GetString("net_auth_supported_impl_levels"));
			}
			if (this._Context != null && this.IsServer != isServer)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_client_server"));
			}
			this._Exception = null;
			this._RemoteOk = false;
			this._Framer = new StreamFramer(this._InnerStream);
			this._Framer.WriteHeader.MessageId = 22;
			this._ExpectedProtectionLevel = protectionLevel;
			this._ExpectedImpersonationLevel = (isServer ? impersonationLevel : TokenImpersonationLevel.None);
			this._WriteSequenceNumber = 0U;
			this._ReadSequenceNumber = 0U;
			ContextFlags contextFlags = ContextFlags.Connection;
			if (protectionLevel == ProtectionLevel.None && !isServer)
			{
				package = "NTLM";
			}
			else if (protectionLevel == ProtectionLevel.EncryptAndSign)
			{
				contextFlags |= ContextFlags.Confidentiality;
			}
			else if (protectionLevel == ProtectionLevel.Sign)
			{
				contextFlags |= (ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.AcceptStream);
			}
			if (isServer)
			{
				if (this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.WhenSupported)
				{
					contextFlags |= ContextFlags.AllowMissingBindings;
				}
				if (this._ExtendedProtectionPolicy.PolicyEnforcement != PolicyEnforcement.Never && this._ExtendedProtectionPolicy.ProtectionScenario == ProtectionScenario.TrustedProxy)
				{
					contextFlags |= ContextFlags.ProxyBindings;
				}
			}
			else
			{
				if (protectionLevel != ProtectionLevel.None)
				{
					contextFlags |= ContextFlags.MutualAuth;
				}
				if (impersonationLevel == TokenImpersonationLevel.Identification)
				{
					contextFlags |= ContextFlags.AcceptIntegrity;
				}
				if (impersonationLevel == TokenImpersonationLevel.Delegation)
				{
					contextFlags |= ContextFlags.Delegate;
				}
			}
			this._CanRetryAuthentication = false;
			if (!(credential is SystemNetworkCredential))
			{
				ExceptionHelper.ControlPrincipalPermission.Demand();
			}
			try
			{
				this._Context = new NTAuthentication(isServer, package, credential, servicePrincipalName, contextFlags, channelBinding);
			}
			catch (Win32Exception innerException)
			{
				throw new AuthenticationException(SR.GetString("net_auth_SSPI"), innerException);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000918A4 File Offset: 0x0008FAA4
		private Exception SetException(Exception e)
		{
			if (this._Exception == null || !(this._Exception is ObjectDisposedException))
			{
				this._Exception = e;
			}
			if (this._Exception != null && this._Context != null)
			{
				this._Context.CloseContext();
			}
			return this._Exception;
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000918E3 File Offset: 0x0008FAE3
		internal bool IsAuthenticated
		{
			get
			{
				return this._Context != null && this.HandshakeComplete && this._Exception == null && this._RemoteOk;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00091905 File Offset: 0x0008FB05
		internal bool IsMutuallyAuthenticated
		{
			get
			{
				return this.IsAuthenticated && !this._Context.IsNTLM && this._Context.IsMutualAuthFlag;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0009192B File Offset: 0x0008FB2B
		internal bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated && this._Context.IsConfidentialityFlag;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00091942 File Offset: 0x0008FB42
		internal bool IsSigned
		{
			get
			{
				return this.IsAuthenticated && (this._Context.IsIntegrityFlag || this._Context.IsConfidentialityFlag);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00091968 File Offset: 0x0008FB68
		internal bool IsServer
		{
			get
			{
				return this._Context != null && this._Context.IsServer;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0009197F File Offset: 0x0008FB7F
		internal bool CanGetSecureStream
		{
			get
			{
				return this._Context.IsConfidentialityFlag || this._Context.IsIntegrityFlag;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0009199B File Offset: 0x0008FB9B
		internal TokenImpersonationLevel AllowedImpersonation
		{
			get
			{
				this.CheckThrow(true);
				return this.PrivateImpersonationLevel;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000919AA File Offset: 0x0008FBAA
		private TokenImpersonationLevel PrivateImpersonationLevel
		{
			get
			{
				if (this._Context.IsDelegationFlag && this._Context.ProtocolName != "NTLM")
				{
					return TokenImpersonationLevel.Delegation;
				}
				if (!this._Context.IsIdentifyFlag)
				{
					return TokenImpersonationLevel.Impersonation;
				}
				return TokenImpersonationLevel.Identification;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000919E2 File Offset: 0x0008FBE2
		private bool HandshakeComplete
		{
			get
			{
				return this._Context.IsCompleted && this._Context.IsValidContext;
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00091A00 File Offset: 0x0008FC00
		internal IIdentity GetIdentity()
		{
			this.CheckThrow(true);
			string name = this._Context.IsServer ? this._Context.AssociatedName : this._Context.Spn;
			string type = "NTLM";
			type = this._Context.ProtocolName;
			if (this._Context.IsServer)
			{
				SafeCloseHandle safeCloseHandle = null;
				try
				{
					safeCloseHandle = this._Context.GetContextToken();
					string protocolName = this._Context.ProtocolName;
					return new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), protocolName, WindowsAccountType.Normal, true);
				}
				catch (SecurityException)
				{
				}
				finally
				{
					if (safeCloseHandle != null)
					{
						safeCloseHandle.Close();
					}
				}
			}
			return new GenericIdentity(name, type);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00091AC0 File Offset: 0x0008FCC0
		internal void CheckThrow(bool authSucessCheck)
		{
			if (this._Exception != null)
			{
				throw this._Exception;
			}
			if (authSucessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_noauth"));
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00091AEC File Offset: 0x0008FCEC
		internal void Close()
		{
			this._Exception = new ObjectDisposedException("NegotiateStream");
			if (this._Context != null)
			{
				this._Context.CloseContext();
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00091B14 File Offset: 0x0008FD14
		internal void ProcessAuthentication(LazyAsyncResult lazyResult)
		{
			this.CheckThrow(false);
			if (Interlocked.Exchange(ref this._NestedAuth, 1) == 1)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(lazyResult == null) ? "BeginAuthenticate" : "Authenticate",
					"authenticate"
				}));
			}
			try
			{
				if (this._Context.IsServer)
				{
					this.StartReceiveBlob(lazyResult);
				}
				else
				{
					this.StartSendBlob(null, lazyResult);
				}
			}
			catch (Exception exception)
			{
				exception = this.SetException(exception);
				throw;
			}
			finally
			{
				if (lazyResult == null || this._Exception != null)
				{
					this._NestedAuth = 0;
				}
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00091BC4 File Offset: 0x0008FDC4
		internal void EndProcessAuthentication(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					result.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedAuth, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndAuthenticate"
				}));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			Exception ex = lazyAsyncResult.Result as Exception;
			if (ex != null)
			{
				ex = this.SetException(ex);
				throw ex;
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00091C60 File Offset: 0x0008FE60
		private bool CheckSpn()
		{
			if (this._Context.IsKerberos)
			{
				return true;
			}
			if (this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Never || this._ExtendedProtectionPolicy.CustomServiceNames == null)
			{
				return true;
			}
			if (!AuthenticationManager.OSSupportsExtendedProtection)
			{
				return true;
			}
			string clientSpecifiedSpn = this._Context.ClientSpecifiedSpn;
			if (string.IsNullOrEmpty(clientSpecifiedSpn))
			{
				return this._ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.WhenSupported;
			}
			return this._ExtendedProtectionPolicy.CustomServiceNames.Contains(clientSpecifiedSpn);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00091CD8 File Offset: 0x0008FED8
		private void StartSendBlob(byte[] message, LazyAsyncResult lazyResult)
		{
			Win32Exception ex = null;
			if (message != NegoState._EmptyMessage)
			{
				message = this.GetOutgoingBlob(message, ref ex);
			}
			if (ex != null)
			{
				this.StartSendAuthResetSignal(lazyResult, message, ex);
				return;
			}
			if (this.HandshakeComplete)
			{
				if (this._Context.IsServer && !this.CheckSpn())
				{
					Exception exception = new AuthenticationException(SR.GetString("net_auth_bad_client_creds_or_target_mismatch"));
					int num = 1790;
					message = new byte[8];
					for (int i = message.Length - 1; i >= 0; i--)
					{
						message[i] = (byte)(num & 255);
						num = (int)((uint)num >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, exception);
					return;
				}
				if (this.PrivateImpersonationLevel < this._ExpectedImpersonationLevel)
				{
					Exception exception2 = new AuthenticationException(SR.GetString("net_auth_context_expectation", new object[]
					{
						this._ExpectedImpersonationLevel.ToString(),
						this.PrivateImpersonationLevel.ToString()
					}));
					int num2 = 1790;
					message = new byte[8];
					for (int j = message.Length - 1; j >= 0; j--)
					{
						message[j] = (byte)(num2 & 255);
						num2 = (int)((uint)num2 >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, exception2);
					return;
				}
				ProtectionLevel protectionLevel = this._Context.IsConfidentialityFlag ? ProtectionLevel.EncryptAndSign : (this._Context.IsIntegrityFlag ? ProtectionLevel.Sign : ProtectionLevel.None);
				if (protectionLevel < this._ExpectedProtectionLevel)
				{
					Exception exception3 = new AuthenticationException(SR.GetString("net_auth_context_expectation", new object[]
					{
						protectionLevel.ToString(),
						this._ExpectedProtectionLevel.ToString()
					}));
					int num3 = 1790;
					message = new byte[8];
					for (int k = message.Length - 1; k >= 0; k--)
					{
						message[k] = (byte)(num3 & 255);
						num3 = (int)((uint)num3 >> 8);
					}
					this.StartSendAuthResetSignal(lazyResult, message, exception3);
					return;
				}
				this._Framer.WriteHeader.MessageId = 20;
				if (this._Context.IsServer)
				{
					this._RemoteOk = true;
					if (message == null)
					{
						message = NegoState._EmptyMessage;
					}
				}
			}
			else if (message == null || message == NegoState._EmptyMessage)
			{
				throw new InternalException();
			}
			if (message != null)
			{
				if (lazyResult == null)
				{
					this._Framer.WriteMessage(message);
				}
				else
				{
					IAsyncResult asyncResult = this._Framer.BeginWriteMessage(message, NegoState._WriteCallback, lazyResult);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this._Framer.EndWriteMessage(asyncResult);
				}
			}
			this.CheckCompletionBeforeNextReceive(lazyResult);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00091F38 File Offset: 0x00090138
		private void CheckCompletionBeforeNextReceive(LazyAsyncResult lazyResult)
		{
			if (this.HandshakeComplete && this._RemoteOk)
			{
				if (lazyResult != null)
				{
					lazyResult.InvokeCallback();
				}
				return;
			}
			this.StartReceiveBlob(lazyResult);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00091F5C File Offset: 0x0009015C
		private void StartReceiveBlob(LazyAsyncResult lazyResult)
		{
			byte[] message;
			if (lazyResult == null)
			{
				message = this._Framer.ReadMessage();
			}
			else
			{
				IAsyncResult asyncResult = this._Framer.BeginReadMessage(NegoState._ReadCallback, lazyResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				message = this._Framer.EndReadMessage(asyncResult);
			}
			this.ProcessReceivedBlob(message, lazyResult);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00091FAC File Offset: 0x000901AC
		private void ProcessReceivedBlob(byte[] message, LazyAsyncResult lazyResult)
		{
			if (message == null)
			{
				throw new AuthenticationException(SR.GetString("net_auth_eof"), null);
			}
			if (this._Framer.ReadHeader.MessageId == 21)
			{
				Win32Exception ex = null;
				if (message.Length >= 8)
				{
					long num = 0L;
					for (int i = 0; i < 8; i++)
					{
						num = (num << 8) + (long)((ulong)message[i]);
					}
					ex = new Win32Exception((int)num);
				}
				if (ex != null)
				{
					if (ex.NativeErrorCode == -2146893044)
					{
						throw new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds"), ex);
					}
					if (ex.NativeErrorCode == 1790)
					{
						throw new AuthenticationException(SR.GetString("net_auth_context_expectation_remote"), ex);
					}
				}
				throw new AuthenticationException(SR.GetString("net_auth_alert"), ex);
			}
			if (this._Framer.ReadHeader.MessageId == 20)
			{
				this._RemoteOk = true;
			}
			else if (this._Framer.ReadHeader.MessageId != 22)
			{
				throw new AuthenticationException(SR.GetString("net_io_header_id", new object[]
				{
					"MessageId",
					this._Framer.ReadHeader.MessageId,
					22
				}), null);
			}
			this.CheckCompletionBeforeNextSend(message, lazyResult);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000920D4 File Offset: 0x000902D4
		private void CheckCompletionBeforeNextSend(byte[] message, LazyAsyncResult lazyResult)
		{
			if (!this.HandshakeComplete)
			{
				this.StartSendBlob(message, lazyResult);
				return;
			}
			if (!this._RemoteOk)
			{
				throw new AuthenticationException(SR.GetString("net_io_header_id", new object[]
				{
					"MessageId",
					this._Framer.ReadHeader.MessageId,
					20
				}), null);
			}
			if (lazyResult != null)
			{
				lazyResult.InvokeCallback();
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00092144 File Offset: 0x00090344
		private void StartSendAuthResetSignal(LazyAsyncResult lazyResult, byte[] message, Exception exception)
		{
			this._Framer.WriteHeader.MessageId = 21;
			Win32Exception ex = exception as Win32Exception;
			if (ex != null && ex.NativeErrorCode == -2146893044)
			{
				if (this.IsServer)
				{
					exception = new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds"), exception);
				}
				else
				{
					exception = new InvalidCredentialException(SR.GetString("net_auth_bad_client_creds_or_target_mismatch"), exception);
				}
			}
			if (!(exception is AuthenticationException))
			{
				exception = new AuthenticationException(SR.GetString("net_auth_SSPI"), exception);
			}
			if (lazyResult == null)
			{
				this._Framer.WriteMessage(message);
			}
			else
			{
				lazyResult.Result = exception;
				IAsyncResult asyncResult = this._Framer.BeginWriteMessage(message, NegoState._WriteCallback, lazyResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this._Framer.EndWriteMessage(asyncResult);
			}
			this._CanRetryAuthentication = true;
			throw exception;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0009220C File Offset: 0x0009040C
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)transportResult.AsyncState;
			try
			{
				NegoState negoState = (NegoState)lazyAsyncResult.AsyncObject;
				negoState._Framer.EndWriteMessage(transportResult);
				if (lazyAsyncResult.Result is Exception)
				{
					negoState._CanRetryAuthentication = true;
					throw (Exception)lazyAsyncResult.Result;
				}
				negoState.CheckCompletionBeforeNextReceive(lazyAsyncResult);
			}
			catch (Exception result)
			{
				if (lazyAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				lazyAsyncResult.InvokeCallback(result);
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00092294 File Offset: 0x00090494
		private static void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)transportResult.AsyncState;
			try
			{
				NegoState negoState = (NegoState)lazyAsyncResult.AsyncObject;
				byte[] message = negoState._Framer.EndReadMessage(transportResult);
				negoState.ProcessReceivedBlob(message, lazyAsyncResult);
			}
			catch (Exception result)
			{
				if (lazyAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				lazyAsyncResult.InvokeCallback(result);
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00092300 File Offset: 0x00090500
		private byte[] GetOutgoingBlob(byte[] incomingBlob, ref Win32Exception e)
		{
			SecurityStatus securityStatus;
			byte[] array = this._Context.GetOutgoingBlob(incomingBlob, false, out securityStatus);
			if ((securityStatus & (SecurityStatus)(-2147483648)) != SecurityStatus.OK)
			{
				e = new Win32Exception((int)securityStatus);
				array = new byte[8];
				for (int i = array.Length - 1; i >= 0; i--)
				{
					array[i] = (byte)(securityStatus & (SecurityStatus)255);
					securityStatus >>= 8;
				}
			}
			if (array != null && array.Length == 0)
			{
				array = NegoState._EmptyMessage;
			}
			return array;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00092362 File Offset: 0x00090562
		internal int EncryptData(byte[] buffer, int offset, int count, ref byte[] outBuffer)
		{
			this.CheckThrow(true);
			this._WriteSequenceNumber += 1U;
			return this._Context.Encrypt(buffer, offset, count, ref outBuffer, this._WriteSequenceNumber);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0009238F File Offset: 0x0009058F
		internal int DecryptData(byte[] buffer, int offset, int count, out int newOffset)
		{
			this.CheckThrow(true);
			this._ReadSequenceNumber += 1U;
			return this._Context.Decrypt(buffer, offset, count, out newOffset, this._ReadSequenceNumber);
		}

		// Token: 0x04001D14 RID: 7444
		private const int ERROR_TRUST_FAILURE = 1790;

		// Token: 0x04001D15 RID: 7445
		private static readonly byte[] _EmptyMessage = new byte[0];

		// Token: 0x04001D16 RID: 7446
		private static readonly AsyncCallback _ReadCallback = new AsyncCallback(NegoState.ReadCallback);

		// Token: 0x04001D17 RID: 7447
		private static readonly AsyncCallback _WriteCallback = new AsyncCallback(NegoState.WriteCallback);

		// Token: 0x04001D18 RID: 7448
		private Stream _InnerStream;

		// Token: 0x04001D19 RID: 7449
		private bool _LeaveStreamOpen;

		// Token: 0x04001D1A RID: 7450
		private Exception _Exception;

		// Token: 0x04001D1B RID: 7451
		private StreamFramer _Framer;

		// Token: 0x04001D1C RID: 7452
		private NTAuthentication _Context;

		// Token: 0x04001D1D RID: 7453
		private int _NestedAuth;

		// Token: 0x04001D1E RID: 7454
		internal const int c_MaxReadFrameSize = 65536;

		// Token: 0x04001D1F RID: 7455
		internal const int c_MaxWriteDataSize = 64512;

		// Token: 0x04001D20 RID: 7456
		private bool _CanRetryAuthentication;

		// Token: 0x04001D21 RID: 7457
		private ProtectionLevel _ExpectedProtectionLevel;

		// Token: 0x04001D22 RID: 7458
		private TokenImpersonationLevel _ExpectedImpersonationLevel;

		// Token: 0x04001D23 RID: 7459
		private uint _WriteSequenceNumber;

		// Token: 0x04001D24 RID: 7460
		private uint _ReadSequenceNumber;

		// Token: 0x04001D25 RID: 7461
		private ExtendedProtectionPolicy _ExtendedProtectionPolicy;

		// Token: 0x04001D26 RID: 7462
		private bool _RemoteOk;
	}
}
