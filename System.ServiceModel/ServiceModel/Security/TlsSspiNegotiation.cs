using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030E RID: 782
	internal sealed class TlsSspiNegotiation : ISspiNegotiation, IDisposable
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x00064AA0 File Offset: 0x00062CA0
		public TlsSspiNegotiation(string destination, SchProtocols protocolFlags, X509Certificate2 clientCertificate) : this(destination, false, protocolFlags, null, clientCertificate, false)
		{
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00064AAE File Offset: 0x00062CAE
		public TlsSspiNegotiation(SchProtocols protocolFlags, X509Certificate2 serverCertificate, bool clientCertRequired) : this(null, true, protocolFlags, serverCertificate, null, clientCertRequired)
		{
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00064ABC File Offset: 0x00062CBC
		static TlsSspiNegotiation()
		{
			TlsSspiNegotiation.ServerStandardFlags = (TlsSspiNegotiation.StandardFlags | SspiContextFlags.InitStream | SspiContextFlags.AcceptStream);
			TlsSspiNegotiation.ClientStandardFlags = (TlsSspiNegotiation.StandardFlags | SspiContextFlags.AcceptIdentify | SspiContextFlags.InitStream);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00064AF4 File Offset: 0x00062CF4
		private TlsSspiNegotiation(string destination, bool isServer, SchProtocols protocolFlags, X509Certificate2 serverCertificate, X509Certificate2 clientCertificate, bool clientCertRequired)
		{
			SspiWrapper.GetVerifyPackageInfo("Microsoft Unified Security Protocol Provider");
			this.destination = destination;
			this.isServer = isServer;
			this.protocolFlags = protocolFlags;
			this.serverCertificate = serverCertificate;
			this.clientCertificate = clientCertificate;
			this.clientCertRequired = clientCertRequired;
			this.securityContext = null;
			if (isServer)
			{
				this.ValidateServerCertificate();
			}
			else
			{
				this.ValidateClientCertificate();
			}
			if (this.isServer)
			{
				try
				{
					this.AcquireServerCredentials();
					return;
				}
				catch (Win32Exception ex)
				{
					if (ex.NativeErrorCode != -2146893043)
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					Thread.Sleep(0);
					this.AcquireServerCredentials();
					return;
				}
			}
			this.AcquireDummyCredentials();
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00064BB0 File Offset: 0x00062DB0
		public X509Certificate2 ClientCertificate
		{
			get
			{
				this.ThrowIfDisposed();
				return this.clientCertificate;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00064BBE File Offset: 0x00062DBE
		public bool ClientCertRequired
		{
			get
			{
				this.ThrowIfDisposed();
				return this.clientCertRequired;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00064BCC File Offset: 0x00062DCC
		public string Destination
		{
			get
			{
				this.ThrowIfDisposed();
				return this.destination;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00064BDA File Offset: 0x00062DDA
		public DateTime ExpirationTimeUtc
		{
			get
			{
				this.ThrowIfDisposed();
				return SecurityUtils.MaxUtcDateTime;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00064BE7 File Offset: 0x00062DE7
		public bool IsCompleted
		{
			get
			{
				this.ThrowIfDisposed();
				return this.isCompleted;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00064BF5 File Offset: 0x00062DF5
		public bool IsMutualAuthFlag
		{
			get
			{
				this.ThrowIfDisposed();
				return (this.attributes & SspiContextFlags.MutualAuth) > SspiContextFlags.Zero;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00064C08 File Offset: 0x00062E08
		public bool IsValidContext
		{
			get
			{
				return this.securityContext != null && !this.securityContext.IsInvalid;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00064C22 File Offset: 0x00062E22
		public string KeyEncryptionAlgorithm
		{
			get
			{
				return "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00064C29 File Offset: 0x00062E29
		public X509Certificate2 RemoteCertificate
		{
			get
			{
				this.ThrowIfDisposed();
				if (!this.IsValidContext)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
				}
				if (this.remoteCertificate == null)
				{
					this.ExtractRemoteCertificate();
				}
				return this.remoteCertificate;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00064C62 File Offset: 0x00062E62
		public X509Certificate2Collection RemoteCertificateChain
		{
			get
			{
				this.ThrowIfDisposed();
				if (!this.IsValidContext)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
				}
				if (this.remoteCertificateChain == null)
				{
					this.ExtractRemoteCertificate();
				}
				return this.remoteCertificateChain;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00064C9B File Offset: 0x00062E9B
		public X509Certificate2 ServerCertificate
		{
			get
			{
				this.ThrowIfDisposed();
				return this.serverCertificate;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00064CA9 File Offset: 0x00062EA9
		public bool WasClientCertificateSent
		{
			get
			{
				this.ThrowIfDisposed();
				return this.wasClientCertificateSent;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00064CB8 File Offset: 0x00062EB8
		internal SslConnectionInfo ConnectionInfo
		{
			get
			{
				this.ThrowIfDisposed();
				if (!this.IsValidContext)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
				}
				if (this.connectionInfo == null)
				{
					SslConnectionInfo result = SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.ConnectionInfo) as SslConnectionInfo;
					if (this.IsCompleted)
					{
						this.connectionInfo = result;
					}
					return result;
				}
				return this.connectionInfo;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00064D1C File Offset: 0x00062F1C
		internal StreamSizes StreamSizes
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.streamSizes == null)
				{
					StreamSizes result = (StreamSizes)SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.StreamSizes);
					if (this.IsCompleted)
					{
						this.streamSizes = result;
					}
					return result;
				}
				return this.streamSizes;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00064D60 File Offset: 0x00062F60
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00064D68 File Offset: 0x00062F68
		internal string IncomingValueTypeUri
		{
			get
			{
				return this.incomingValueTypeUri;
			}
			set
			{
				this.incomingValueTypeUri = value;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00064D74 File Offset: 0x00062F74
		public string GetRemoteIdentityName()
		{
			if (!this.IsValidContext)
			{
				return string.Empty;
			}
			X509Certificate2 x509Certificate = this.RemoteCertificate;
			if (x509Certificate == null)
			{
				return string.Empty;
			}
			return SecurityUtils.GetCertificateId(x509Certificate);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00064DA8 File Offset: 0x00062FA8
		public byte[] Decrypt(byte[] encryptedContent)
		{
			this.ThrowIfDisposed();
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(encryptedContent.Length);
			Buffer.BlockCopy(encryptedContent, 0, array, 0, encryptedContent.Length);
			int num = 0;
			int srcOffset;
			this.DecryptInPlace(array, out srcOffset, out num);
			byte[] array2 = DiagnosticUtility.Utility.AllocateByteArray(num);
			Buffer.BlockCopy(array, srcOffset, array2, 0, num);
			return array2;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00064DF9 File Offset: 0x00062FF9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00064E08 File Offset: 0x00063008
		public byte[] Encrypt(byte[] input)
		{
			this.ThrowIfDisposed();
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(checked(input.Length + this.StreamSizes.header + this.StreamSizes.trailer));
			Buffer.BlockCopy(input, 0, array, this.StreamSizes.header, input.Length);
			int num = 0;
			this.EncryptInPlace(array, 0, input.Length, out num);
			if (num == array.Length)
			{
				return array;
			}
			byte[] array2 = DiagnosticUtility.Utility.AllocateByteArray(num);
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00064E88 File Offset: 0x00063088
		public byte[] GetOutgoingBlob(byte[] incomingBlob, ChannelBinding channelbinding, ExtendedProtectionPolicy protectionPolicy)
		{
			this.ThrowIfDisposed();
			SecurityBuffer inputBuffer = null;
			if (incomingBlob != null)
			{
				inputBuffer = new SecurityBuffer(incomingBlob, BufferType.Token);
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(null, BufferType.Token);
			this.remoteCertificate = null;
			int num;
			if (this.isServer)
			{
				num = SspiWrapper.AcceptSecurityContext(this.credentialsHandle, ref this.securityContext, TlsSspiNegotiation.ServerStandardFlags | (this.clientCertRequired ? SspiContextFlags.MutualAuth : SspiContextFlags.Zero), Endianness.Native, inputBuffer, securityBuffer, ref this.attributes);
			}
			else
			{
				num = SspiWrapper.InitializeSecurityContext(this.credentialsHandle, ref this.securityContext, this.destination, TlsSspiNegotiation.ClientStandardFlags, Endianness.Native, inputBuffer, securityBuffer, ref this.attributes);
			}
			if ((num & -2147483648) != 0)
			{
				this.Dispose();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (num == 0)
			{
				if (SecurityUtils.ShouldValidateSslCipherStrength())
				{
					SslConnectionInfo sslConnectionInfo = (SslConnectionInfo)SspiWrapper.QueryContextAttributes(this.securityContext, ContextAttribute.ConnectionInfo);
					if (sslConnectionInfo == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("CannotObtainSslConnectionInfo")));
					}
					SecurityUtils.ValidateSslCipherStrength(sslConnectionInfo.DataKeySize);
				}
				this.isCompleted = true;
			}
			else
			{
				if (num == 590624)
				{
					this.AcquireClientCredentials();
					if (this.ClientCertificate != null)
					{
						this.wasClientCertificateSent = true;
					}
					return this.GetOutgoingBlob(incomingBlob, channelbinding, protectionPolicy);
				}
				if (num != 590610)
				{
					this.Dispose();
					if (num == -2146893052)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num, SR.GetString("LsaAuthorityNotContacted")));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
				}
			}
			return securityBuffer.token;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00064FF8 File Offset: 0x000631F8
		internal void DecryptInPlace(byte[] encryptedContent, out int dataStartOffset, out int dataLen)
		{
			this.ThrowIfDisposed();
			dataStartOffset = this.StreamSizes.header;
			dataLen = 0;
			byte[] data = new byte[0];
			byte[] data2 = new byte[0];
			byte[] data3 = new byte[0];
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(encryptedContent, 0, encryptedContent.Length, BufferType.Data),
				new SecurityBuffer(data, BufferType.Empty),
				new SecurityBuffer(data2, BufferType.Empty),
				new SecurityBuffer(data3, BufferType.Empty)
			};
			int num = SspiWrapper.DecryptMessage(this.securityContext, array, 0U, false);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == BufferType.Data)
				{
					dataLen = array[i].size;
					return;
				}
			}
			this.OnBadData();
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000650BC File Offset: 0x000632BC
		internal void EncryptInPlace(byte[] buffer, int bufferStartOffset, int dataLen, out int encryptedDataLen)
		{
			this.ThrowIfDisposed();
			encryptedDataLen = 0;
			if (bufferStartOffset + dataLen + this.StreamSizes.header + this.StreamSizes.trailer > buffer.Length)
			{
				this.OnBadData();
			}
			byte[] data = new byte[0];
			int offset = bufferStartOffset + this.StreamSizes.header + dataLen;
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(buffer, bufferStartOffset, this.StreamSizes.header, BufferType.Header),
				new SecurityBuffer(buffer, bufferStartOffset + this.StreamSizes.header, dataLen, BufferType.Data),
				new SecurityBuffer(buffer, offset, this.StreamSizes.trailer, BufferType.Trailer),
				new SecurityBuffer(data, BufferType.Empty)
			};
			int num = SspiWrapper.EncryptMessage(this.securityContext, array, 0U);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == BufferType.Trailer)
				{
					int size = array[i].size;
					encryptedDataLen = this.StreamSizes.header + dataLen + size;
					return;
				}
			}
			this.OnBadData();
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000651D0 File Offset: 0x000633D0
		private static void ValidatePrivateKey(X509Certificate2 certificate)
		{
			bool flag = false;
			try
			{
				if (LocalAppContextSwitches.DisableCngCertificates)
				{
					flag = (certificate != null && certificate.PrivateKey != null);
				}
				else
				{
					flag = (certificate.HasPrivateKey && SecurityUtils.CanReadPrivateKey(certificate));
				}
			}
			catch (SecurityException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SslCertMayNotDoKeyExchange", new object[]
				{
					certificate.SubjectName.Name
				}), innerException));
			}
			catch (CryptographicException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SslCertMayNotDoKeyExchange", new object[]
				{
					certificate.SubjectName.Name
				}), innerException2));
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SslCertMustHavePrivateKey", new object[]
				{
					certificate.SubjectName.Name
				})));
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000652BC File Offset: 0x000634BC
		private void ValidateServerCertificate()
		{
			if (this.serverCertificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serverCertificate");
			}
			TlsSspiNegotiation.ValidatePrivateKey(this.serverCertificate);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000652E1 File Offset: 0x000634E1
		private void ValidateClientCertificate()
		{
			if (this.clientCertificate != null)
			{
				TlsSspiNegotiation.ValidatePrivateKey(this.clientCertificate);
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000652F8 File Offset: 0x000634F8
		private void AcquireClientCredentials()
		{
			SecureCredential scc = new SecureCredential(4, this.ClientCertificate, SecureCredential.Flags.ValidateManual | SecureCredential.Flags.NoDefaultCred, this.protocolFlags);
			this.credentialsHandle = SspiWrapper.AcquireCredentialsHandle("Microsoft Unified Security Protocol Provider", CredentialUse.Outbound, scc);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00065330 File Offset: 0x00063530
		private void AcquireDummyCredentials()
		{
			SecureCredential scc = new SecureCredential(4, null, SecureCredential.Flags.ValidateManual | SecureCredential.Flags.NoDefaultCred, this.protocolFlags);
			this.credentialsHandle = SspiWrapper.AcquireCredentialsHandle("Microsoft Unified Security Protocol Provider", CredentialUse.Outbound, scc);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00065360 File Offset: 0x00063560
		private void AcquireServerCredentials()
		{
			SecureCredential scc = new SecureCredential(4, this.serverCertificate, SecureCredential.Flags.Zero, this.protocolFlags);
			this.credentialsHandle = SspiWrapper.AcquireCredentialsHandle("Microsoft Unified Security Protocol Provider", CredentialUse.Inbound, scc);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00065394 File Offset: 0x00063594
		private void Dispose(bool disposing)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					if (disposing)
					{
						if (this.securityContext != null)
						{
							this.securityContext.Close();
							this.securityContext = null;
						}
						if (this.credentialsHandle != null)
						{
							this.credentialsHandle.Close();
							this.credentialsHandle = null;
						}
					}
					this.connectionInfo = null;
					this.destination = null;
					this.streamSizes = null;
				}
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006542C File Offset: 0x0006362C
		private SafeFreeCertContext ExtractCertificateHandle(ContextAttribute contextAttribute)
		{
			return SspiWrapper.QueryContextAttributes(this.securityContext, contextAttribute) as SafeFreeCertContext;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006544C File Offset: 0x0006364C
		private void ExtractRemoteCertificate()
		{
			SafeFreeCertContext safeFreeCertContext = null;
			this.remoteCertificate = null;
			this.remoteCertificateChain = null;
			try
			{
				safeFreeCertContext = this.ExtractCertificateHandle(ContextAttribute.RemoteCertificate);
				if (safeFreeCertContext != null && !safeFreeCertContext.IsInvalid)
				{
					this.remoteCertificateChain = TlsSspiNegotiation.UnmanagedCertificateContext.GetStore(safeFreeCertContext);
					this.remoteCertificate = new X509Certificate2(safeFreeCertContext.DangerousGetHandle());
				}
			}
			finally
			{
				if (safeFreeCertContext != null)
				{
					safeFreeCertContext.Close();
				}
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000654B8 File Offset: 0x000636B8
		internal bool TryGetContextIdentity(out WindowsIdentity mappedIdentity)
		{
			if (!this.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(-2146893055));
			}
			SafeCloseHandle safeCloseHandle = null;
			bool result;
			try
			{
				SecurityStatus securityStatus = (SecurityStatus)SspiWrapper.QuerySecurityContextToken(this.securityContext, out safeCloseHandle);
				if (securityStatus != SecurityStatus.OK)
				{
					mappedIdentity = null;
					result = false;
				}
				else
				{
					mappedIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), "SSL/PCT");
					result = true;
				}
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00065530 File Offset: 0x00063730
		private void OnBadData()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadData")));
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006554C File Offset: 0x0006374C
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

		// Token: 0x04001D3B RID: 7483
		private static SspiContextFlags ClientStandardFlags;

		// Token: 0x04001D3C RID: 7484
		private static SspiContextFlags ServerStandardFlags;

		// Token: 0x04001D3D RID: 7485
		private static SspiContextFlags StandardFlags = SspiContextFlags.ReplayDetect | SspiContextFlags.Confidentiality | SspiContextFlags.AllocateMemory;

		// Token: 0x04001D3E RID: 7486
		private SspiContextFlags attributes;

		// Token: 0x04001D3F RID: 7487
		private X509Certificate2 clientCertificate;

		// Token: 0x04001D40 RID: 7488
		private bool clientCertRequired;

		// Token: 0x04001D41 RID: 7489
		private SslConnectionInfo connectionInfo;

		// Token: 0x04001D42 RID: 7490
		private SafeFreeCredentials credentialsHandle;

		// Token: 0x04001D43 RID: 7491
		private string destination;

		// Token: 0x04001D44 RID: 7492
		private bool disposed;

		// Token: 0x04001D45 RID: 7493
		private bool isCompleted;

		// Token: 0x04001D46 RID: 7494
		private bool isServer;

		// Token: 0x04001D47 RID: 7495
		private SchProtocols protocolFlags;

		// Token: 0x04001D48 RID: 7496
		private X509Certificate2 remoteCertificate;

		// Token: 0x04001D49 RID: 7497
		private SafeDeleteContext securityContext;

		// Token: 0x04001D4A RID: 7498
		private const string SecurityPackage = "Microsoft Unified Security Protocol Provider";

		// Token: 0x04001D4B RID: 7499
		private X509Certificate2 serverCertificate;

		// Token: 0x04001D4C RID: 7500
		private StreamSizes streamSizes;

		// Token: 0x04001D4D RID: 7501
		private object syncObject = new object();

		// Token: 0x04001D4E RID: 7502
		private bool wasClientCertificateSent;

		// Token: 0x04001D4F RID: 7503
		private X509Certificate2Collection remoteCertificateChain;

		// Token: 0x04001D50 RID: 7504
		private string incomingValueTypeUri;

		// Token: 0x02000B6B RID: 2923
		private static class UnmanagedCertificateContext
		{
			// Token: 0x0600725B RID: 29275 RVA: 0x001AB000 File Offset: 0x001A9200
			internal static X509Certificate2Collection GetStore(SafeFreeCertContext certContext)
			{
				X509Certificate2Collection result = new X509Certificate2Collection();
				if (certContext.IsInvalid)
				{
					return result;
				}
				TlsSspiNegotiation.UnmanagedCertificateContext._CERT_CONTEXT cert_CONTEXT = (TlsSspiNegotiation.UnmanagedCertificateContext._CERT_CONTEXT)Marshal.PtrToStructure(certContext.DangerousGetHandle(), typeof(TlsSspiNegotiation.UnmanagedCertificateContext._CERT_CONTEXT));
				if (cert_CONTEXT.hCertStore != IntPtr.Zero)
				{
					X509Store x509Store = null;
					try
					{
						x509Store = new X509Store(cert_CONTEXT.hCertStore);
						result = x509Store.Certificates;
					}
					finally
					{
						if (x509Store != null)
						{
							x509Store.Close();
						}
					}
				}
				return result;
			}

			// Token: 0x02000EF6 RID: 3830
			private struct _CERT_CONTEXT
			{
				// Token: 0x04004D37 RID: 19767
				internal int dwCertEncodingType;

				// Token: 0x04004D38 RID: 19768
				internal IntPtr pbCertEncoded;

				// Token: 0x04004D39 RID: 19769
				internal int cbCertEncoded;

				// Token: 0x04004D3A RID: 19770
				internal IntPtr pCertInfo;

				// Token: 0x04004D3B RID: 19771
				internal IntPtr hCertStore;
			}
		}
	}
}
