using System;
using System.Globalization;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Mail
{
	// Token: 0x020006C8 RID: 1736
	internal class SmtpConnection
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x000E40A1 File Offset: 0x000E30A1
		private static PooledStream CreateSmtpPooledStream(ConnectionPool pool)
		{
			return new SmtpPooledStream(pool, TimeSpan.MaxValue, false);
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000E40B0 File Offset: 0x000E30B0
		internal SmtpConnection(SmtpTransport parent, SmtpClient client, ICredentialsByHost credentials, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			this.credentials = credentials;
			this.authenticationModules = authenticationModules;
			this.parent = parent;
			this.onCloseHandler = new EventHandler(this.OnClose);
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000E4108 File Offset: 0x000E3108
		internal BufferBuilder BufferBuilder
		{
			get
			{
				return this.bufferBuilder;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000E4110 File Offset: 0x000E3110
		internal bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000E4118 File Offset: 0x000E3118
		internal bool IsStreamOpen
		{
			get
			{
				return this.isStreamOpen;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000E4120 File Offset: 0x000E3120
		internal bool DSNEnabled
		{
			get
			{
				return this.pooledStream != null && ((SmtpPooledStream)this.pooledStream).dsnEnabled;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000E413C File Offset: 0x000E313C
		internal SmtpReplyReaderFactory Reader
		{
			get
			{
				return this.responseReader;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000E4144 File Offset: 0x000E3144
		// (set) Token: 0x0600359A RID: 13722 RVA: 0x000E414C File Offset: 0x000E314C
		internal bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.enableSsl = value;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000E4155 File Offset: 0x000E3155
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000E415D File Offset: 0x000E315D
		internal int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000E4166 File Offset: 0x000E3166
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x000E416E File Offset: 0x000E316E
		internal X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
			set
			{
				this.clientCertificates = value;
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000E4178 File Offset: 0x000E3178
		internal IAsyncResult BeginGetConnection(string host, int port, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
			ServicePoint servicePoint = ServicePointManager.FindServicePoint(host, port);
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, servicePoint);
			}
			if (this.EnableSsl && this.ClientCertificates != null && this.ClientCertificates.Count > 0)
			{
				this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, this.ClientCertificates.GetHashCode().ToString(NumberFormatInfo.InvariantInfo), SmtpConnection.m_CreateConnectionCallback);
			}
			else
			{
				this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, "", SmtpConnection.m_CreateConnectionCallback);
			}
			SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = new SmtpConnection.ConnectAndHandshakeAsyncResult(this, host, port, outerResult, callback, state);
			connectAndHandshakeAsyncResult.GetConnection(false);
			return connectAndHandshakeAsyncResult;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000E4215 File Offset: 0x000E3215
		internal IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			return this.pooledStream.UnsafeBeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, callback, state);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000E423B File Offset: 0x000E323B
		internal void EndFlush(IAsyncResult result)
		{
			this.pooledStream.EndWrite(result);
			this.bufferBuilder.Reset();
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000E4254 File Offset: 0x000E3254
		internal void Flush()
		{
			this.pooledStream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
			this.bufferBuilder.Reset();
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000E4284 File Offset: 0x000E3284
		internal void ReleaseConnection()
		{
			if (!this.isClosed)
			{
				lock (this)
				{
					if (!this.isClosed && this.pooledStream != null)
					{
						if (this.channelBindingToken != null)
						{
							this.channelBindingToken.Close();
						}
						((SmtpPooledStream)this.pooledStream).previouslyUsed = true;
						this.connectionPool.PutConnection(this.pooledStream, this.pooledStream.Owner, this.Timeout);
					}
					this.isClosed = true;
				}
			}
			this.isConnected = false;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000E4320 File Offset: 0x000E3320
		internal void Abort()
		{
			if (!this.isClosed)
			{
				lock (this)
				{
					if (!this.isClosed && this.pooledStream != null)
					{
						if (this.channelBindingToken != null)
						{
							this.channelBindingToken.Close();
						}
						this.pooledStream.Close(0);
						this.connectionPool.PutConnection(this.pooledStream, this.pooledStream.Owner, this.Timeout);
					}
					this.isClosed = true;
				}
			}
			this.isConnected = false;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000E43B8 File Offset: 0x000E33B8
		internal void ParseExtensions(string[] extensions)
		{
			this.supportedAuth = SupportedAuth.None;
			foreach (string text in extensions)
			{
				if (string.Compare(text, 0, "auth ", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
				{
					string[] array = text.Split(new char[]
					{
						' '
					});
					foreach (string strA in array)
					{
						if (string.Compare(strA, "login", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.Login;
						}
						else if (string.Compare(strA, "ntlm", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.NTLM;
						}
						else if (string.Compare(strA, "gssapi", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.GGSAPI;
						}
						else if (string.Compare(strA, "wdigest", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.supportedAuth |= SupportedAuth.WDigest;
						}
					}
				}
				else if (string.Compare(text, 0, "dsn ", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
				{
					((SmtpPooledStream)this.pooledStream).dsnEnabled = true;
				}
				else if (string.Compare(text, 0, "STARTTLS", 0, 8, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.serverSupportsStartTls = true;
				}
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000E44EC File Offset: 0x000E34EC
		internal bool AuthSupported(ISmtpAuthenticationModule module)
		{
			if (module is SmtpLoginAuthenticationModule)
			{
				if ((this.supportedAuth & SupportedAuth.Login) > SupportedAuth.None)
				{
					return true;
				}
			}
			else if (module is SmtpNegotiateAuthenticationModule)
			{
				if ((this.supportedAuth & SupportedAuth.GGSAPI) > SupportedAuth.None)
				{
					this.sawNegotiate = true;
					return true;
				}
			}
			else if (module is SmtpNtlmAuthenticationModule)
			{
				if (!this.sawNegotiate && (this.supportedAuth & SupportedAuth.NTLM) > SupportedAuth.None)
				{
					return true;
				}
			}
			else if (module is SmtpDigestAuthenticationModule && (this.supportedAuth & SupportedAuth.WDigest) > SupportedAuth.None)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000E4560 File Offset: 0x000E3560
		internal void GetConnection(string host, int port)
		{
			if (this.isConnected)
			{
				throw new InvalidOperationException(SR.GetString("SmtpAlreadyConnected"));
			}
			ServicePoint servicePoint = ServicePointManager.FindServicePoint(host, port);
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, servicePoint);
			}
			this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, "", SmtpConnection.m_CreateConnectionCallback);
			PooledStream connection = this.connectionPool.GetConnection(this, null, this.Timeout);
			while (((SmtpPooledStream)connection).creds != null && ((SmtpPooledStream)connection).creds != this.credentials)
			{
				connection.Close();
				this.connectionPool.PutConnection(connection, connection.Owner, this.Timeout);
				connection = this.connectionPool.GetConnection(this, null, this.Timeout);
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, connection);
			}
			lock (this)
			{
				this.pooledStream = connection;
			}
			((SmtpPooledStream)connection).creds = this.credentials;
			this.responseReader = new SmtpReplyReaderFactory(connection.NetworkStream);
			connection.UpdateLifetime();
			if (((SmtpPooledStream)connection).previouslyUsed)
			{
				this.isConnected = true;
				return;
			}
			LineInfo lineInfo = this.responseReader.GetNextReplyReader().ReadLine();
			SmtpStatusCode statusCode = lineInfo.StatusCode;
			if (statusCode != SmtpStatusCode.ServiceReady)
			{
				throw new SmtpException(lineInfo.StatusCode, lineInfo.Line, true);
			}
			try
			{
				this.extensions = EHelloCommand.Send(this, this.client.clientDomain);
				this.ParseExtensions(this.extensions);
			}
			catch (SmtpException ex)
			{
				if (ex.StatusCode != SmtpStatusCode.CommandUnrecognized && ex.StatusCode != SmtpStatusCode.CommandNotImplemented)
				{
					throw ex;
				}
				HelloCommand.Send(this, this.client.clientDomain);
				this.supportedAuth = SupportedAuth.Login;
			}
			if (this.enableSsl)
			{
				if (!this.serverSupportsStartTls && !(connection.NetworkStream is TlsStream))
				{
					throw new SmtpException(SR.GetString("MailServerDoesNotSupportStartTls"));
				}
				StartTlsCommand.Send(this);
				TlsStream tlsStream = new TlsStream(servicePoint.Host, connection.NetworkStream, this.clientCertificates, servicePoint, this.client, null);
				connection.NetworkStream = tlsStream;
				this.channelBindingToken = tlsStream.GetChannelBinding(ChannelBindingKind.Unique);
				this.responseReader = new SmtpReplyReaderFactory(connection.NetworkStream);
				this.extensions = EHelloCommand.Send(this, this.client.clientDomain);
				this.ParseExtensions(this.extensions);
			}
			if (this.credentials != null)
			{
				for (int i = 0; i < this.authenticationModules.Length; i++)
				{
					if (this.AuthSupported(this.authenticationModules[i]))
					{
						NetworkCredential credential = this.credentials.GetCredential(host, port, this.authenticationModules[i].AuthenticationType);
						if (credential != null)
						{
							Authorization authorization = this.SetContextAndTryAuthenticate(this.authenticationModules[i], credential, null);
							if (authorization != null && authorization.Message != null)
							{
								lineInfo = AuthCommand.Send(this, this.authenticationModules[i].AuthenticationType, authorization.Message);
								if (lineInfo.StatusCode != SmtpStatusCode.CommandParameterNotImplemented)
								{
									while (lineInfo.StatusCode == (SmtpStatusCode)334)
									{
										authorization = this.authenticationModules[i].Authenticate(lineInfo.Line, null, this, this.client.TargetName, this.channelBindingToken);
										if (authorization == null)
										{
											throw new SmtpException(SR.GetString("SmtpAuthenticationFailed"));
										}
										lineInfo = AuthCommand.Send(this, authorization.Message);
										if (lineInfo.StatusCode == (SmtpStatusCode)235)
										{
											this.authenticationModules[i].CloseContext(this);
											this.isConnected = true;
											return;
										}
									}
								}
							}
						}
					}
				}
			}
			this.isConnected = true;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000E4914 File Offset: 0x000E3914
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private Authorization SetContextAndTryAuthenticate(ISmtpAuthenticationModule module, NetworkCredential credential, ContextAwareResult context)
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
							return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
						}
					}
					ExecutionContext executionContext = (context == null) ? null : context.ContextCopy;
					if (executionContext == null)
					{
						return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
					}
					ExecutionContext.Run(executionContext, SmtpConnection.s_AuthenticateCallback, new SmtpConnection.AuthenticateCallbackContext(this, module, credential, this.client.TargetName, this.channelBindingToken));
				}
				catch
				{
					throw;
				}
			}
			return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000E4A10 File Offset: 0x000E3A10
		private static void AuthenticateCallback(object state)
		{
			SmtpConnection.AuthenticateCallbackContext authenticateCallbackContext = (SmtpConnection.AuthenticateCallbackContext)state;
			authenticateCallbackContext.module.Authenticate(null, authenticateCallbackContext.credential, authenticateCallbackContext.thisPtr, authenticateCallbackContext.spn, authenticateCallbackContext.token);
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000E4A49 File Offset: 0x000E3A49
		internal void EndGetConnection(IAsyncResult result)
		{
			SmtpConnection.ConnectAndHandshakeAsyncResult.End(result);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000E4A54 File Offset: 0x000E3A54
		internal Stream GetClosableStream()
		{
			ClosableStream result = new ClosableStream(this.pooledStream.NetworkStream, this.onCloseHandler);
			this.isStreamOpen = true;
			return result;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000E4A80 File Offset: 0x000E3A80
		private void OnClose(object sender, EventArgs args)
		{
			this.isStreamOpen = false;
			DataStopCommand.Send(this);
		}

		// Token: 0x040030F0 RID: 12528
		private static readonly CreateConnectionDelegate m_CreateConnectionCallback = new CreateConnectionDelegate(SmtpConnection.CreateSmtpPooledStream);

		// Token: 0x040030F1 RID: 12529
		private static readonly ContextCallback s_AuthenticateCallback = new ContextCallback(SmtpConnection.AuthenticateCallback);

		// Token: 0x040030F2 RID: 12530
		private BufferBuilder bufferBuilder = new BufferBuilder();

		// Token: 0x040030F3 RID: 12531
		private bool isConnected;

		// Token: 0x040030F4 RID: 12532
		private bool isClosed;

		// Token: 0x040030F5 RID: 12533
		private bool isStreamOpen;

		// Token: 0x040030F6 RID: 12534
		private bool sawNegotiate;

		// Token: 0x040030F7 RID: 12535
		private EventHandler onCloseHandler;

		// Token: 0x040030F8 RID: 12536
		internal SmtpTransport parent;

		// Token: 0x040030F9 RID: 12537
		internal SmtpClient client;

		// Token: 0x040030FA RID: 12538
		private SmtpReplyReaderFactory responseReader;

		// Token: 0x040030FB RID: 12539
		private PooledStream pooledStream;

		// Token: 0x040030FC RID: 12540
		private ConnectionPool connectionPool;

		// Token: 0x040030FD RID: 12541
		private SupportedAuth supportedAuth;

		// Token: 0x040030FE RID: 12542
		private bool serverSupportsStartTls;

		// Token: 0x040030FF RID: 12543
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x04003100 RID: 12544
		private ICredentialsByHost credentials;

		// Token: 0x04003101 RID: 12545
		private int timeout = 100000;

		// Token: 0x04003102 RID: 12546
		private string[] extensions;

		// Token: 0x04003103 RID: 12547
		private ChannelBinding channelBindingToken;

		// Token: 0x04003104 RID: 12548
		private bool enableSsl;

		// Token: 0x04003105 RID: 12549
		private X509CertificateCollection clientCertificates;

		// Token: 0x020006C9 RID: 1737
		private class AuthenticateCallbackContext
		{
			// Token: 0x060035AE RID: 13742 RVA: 0x000E4AB3 File Offset: 0x000E3AB3
			internal AuthenticateCallbackContext(SmtpConnection thisPtr, ISmtpAuthenticationModule module, NetworkCredential credential, string spn, ChannelBinding Token)
			{
				this.thisPtr = thisPtr;
				this.module = module;
				this.credential = credential;
				this.spn = spn;
				this.token = Token;
			}

			// Token: 0x04003106 RID: 12550
			internal readonly SmtpConnection thisPtr;

			// Token: 0x04003107 RID: 12551
			internal readonly ISmtpAuthenticationModule module;

			// Token: 0x04003108 RID: 12552
			internal readonly NetworkCredential credential;

			// Token: 0x04003109 RID: 12553
			internal readonly string spn;

			// Token: 0x0400310A RID: 12554
			internal readonly ChannelBinding token;
		}

		// Token: 0x020006CA RID: 1738
		private class ConnectAndHandshakeAsyncResult : LazyAsyncResult
		{
			// Token: 0x060035AF RID: 13743 RVA: 0x000E4AE0 File Offset: 0x000E3AE0
			internal ConnectAndHandshakeAsyncResult(SmtpConnection connection, string host, int port, ContextAwareResult outerResult, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.connection = connection;
				this.host = host;
				this.port = port;
				this.m_OuterResult = outerResult;
			}

			// Token: 0x060035B0 RID: 13744 RVA: 0x000E4B14 File Offset: 0x000E3B14
			private static void ConnectionCreatedCallback(object request, object state)
			{
				SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)request;
				if (state is Exception)
				{
					connectAndHandshakeAsyncResult.InvokeCallback((Exception)state);
					return;
				}
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)((PooledStream)state);
				try
				{
					while (smtpPooledStream.creds != null && smtpPooledStream.creds != connectAndHandshakeAsyncResult.connection.credentials)
					{
						smtpPooledStream.Close();
						connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout);
						smtpPooledStream = (SmtpPooledStream)connectAndHandshakeAsyncResult.connection.connectionPool.GetConnection(connectAndHandshakeAsyncResult, SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, connectAndHandshakeAsyncResult.connection.Timeout);
						if (smtpPooledStream == null)
						{
							return;
						}
					}
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, connectAndHandshakeAsyncResult.connection, smtpPooledStream);
					}
					smtpPooledStream.Owner = connectAndHandshakeAsyncResult.connection;
					smtpPooledStream.creds = connectAndHandshakeAsyncResult.connection.credentials;
					lock (connectAndHandshakeAsyncResult.connection)
					{
						if (connectAndHandshakeAsyncResult.connection.isClosed)
						{
							smtpPooledStream.Close();
							connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout);
							connectAndHandshakeAsyncResult.InvokeCallback(null);
							return;
						}
						connectAndHandshakeAsyncResult.connection.pooledStream = smtpPooledStream;
					}
					connectAndHandshakeAsyncResult.Handshake();
				}
				catch (Exception result)
				{
					connectAndHandshakeAsyncResult.InvokeCallback(result);
				}
			}

			// Token: 0x060035B1 RID: 13745 RVA: 0x000E4C98 File Offset: 0x000E3C98
			internal static void End(IAsyncResult result)
			{
				SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result;
				object obj = connectAndHandshakeAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
			}

			// Token: 0x060035B2 RID: 13746 RVA: 0x000E4CC4 File Offset: 0x000E3CC4
			internal void GetConnection(bool synchronous)
			{
				if (this.connection.isConnected)
				{
					throw new InvalidOperationException(SR.GetString("SmtpAlreadyConnected"));
				}
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)this.connection.connectionPool.GetConnection(this, synchronous ? null : SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, this.connection.Timeout);
				if (smtpPooledStream != null)
				{
					try
					{
						while (smtpPooledStream.creds != null && smtpPooledStream.creds != this.connection.credentials)
						{
							smtpPooledStream.Close();
							this.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, this.connection.Timeout);
							smtpPooledStream = (SmtpPooledStream)this.connection.connectionPool.GetConnection(this, synchronous ? null : SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, this.connection.Timeout);
							if (smtpPooledStream == null)
							{
								return;
							}
						}
						smtpPooledStream.creds = this.connection.credentials;
						smtpPooledStream.Owner = this.connection;
						lock (this.connection)
						{
							this.connection.pooledStream = smtpPooledStream;
						}
						this.Handshake();
					}
					catch (Exception result)
					{
						base.InvokeCallback(result);
					}
				}
			}

			// Token: 0x060035B3 RID: 13747 RVA: 0x000E4E08 File Offset: 0x000E3E08
			private void Handshake()
			{
				this.connection.responseReader = new SmtpReplyReaderFactory(this.connection.pooledStream.NetworkStream);
				this.connection.pooledStream.UpdateLifetime();
				if (((SmtpPooledStream)this.connection.pooledStream).previouslyUsed)
				{
					this.connection.isConnected = true;
					base.InvokeCallback();
					return;
				}
				SmtpReplyReader nextReplyReader = this.connection.Reader.GetNextReplyReader();
				IAsyncResult asyncResult = nextReplyReader.BeginReadLine(SmtpConnection.ConnectAndHandshakeAsyncResult.handshakeCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				LineInfo lineInfo = nextReplyReader.EndReadLine(asyncResult);
				if (lineInfo.StatusCode != SmtpStatusCode.ServiceReady)
				{
					throw new SmtpException(lineInfo.StatusCode, lineInfo.Line, true);
				}
				try
				{
					if (!this.SendEHello())
					{
					}
				}
				catch
				{
					if (!this.SendHello())
					{
					}
				}
			}

			// Token: 0x060035B4 RID: 13748 RVA: 0x000E4EEC File Offset: 0x000E3EEC
			private static void HandshakeCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						try
						{
							LineInfo lineInfo = connectAndHandshakeAsyncResult.connection.Reader.CurrentReader.EndReadLine(result);
							if (lineInfo.StatusCode != SmtpStatusCode.ServiceReady)
							{
								connectAndHandshakeAsyncResult.InvokeCallback(new SmtpException(lineInfo.StatusCode, lineInfo.Line, true));
							}
							else if (!connectAndHandshakeAsyncResult.SendEHello())
							{
							}
						}
						catch (SmtpException)
						{
							if (!connectAndHandshakeAsyncResult.SendHello())
							{
							}
						}
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060035B5 RID: 13749 RVA: 0x000E4F90 File Offset: 0x000E3F90
			private bool SendEHello()
			{
				IAsyncResult asyncResult = EHelloCommand.BeginSend(this.connection, this.connection.client.clientDomain, SmtpConnection.ConnectAndHandshakeAsyncResult.sendEHelloCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.connection.extensions = EHelloCommand.EndSend(asyncResult);
				this.connection.ParseExtensions(this.connection.extensions);
				if (this.connection.pooledStream.NetworkStream is TlsStream)
				{
					this.Authenticate();
					return true;
				}
				if (this.connection.EnableSsl)
				{
					if (!this.connection.serverSupportsStartTls && !(this.connection.pooledStream.NetworkStream is TlsStream))
					{
						throw new SmtpException(SR.GetString("MailServerDoesNotSupportStartTls"));
					}
					this.SendStartTls();
				}
				else
				{
					this.Authenticate();
				}
				return true;
			}

			// Token: 0x060035B6 RID: 13750 RVA: 0x000E5064 File Offset: 0x000E4064
			private static void SendEHelloCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						try
						{
							connectAndHandshakeAsyncResult.connection.extensions = EHelloCommand.EndSend(result);
							connectAndHandshakeAsyncResult.connection.ParseExtensions(connectAndHandshakeAsyncResult.connection.extensions);
							if (connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream is TlsStream)
							{
								connectAndHandshakeAsyncResult.Authenticate();
								return;
							}
						}
						catch (SmtpException ex)
						{
							if (ex.StatusCode != SmtpStatusCode.CommandUnrecognized && ex.StatusCode != SmtpStatusCode.CommandNotImplemented)
							{
								throw ex;
							}
							if (!connectAndHandshakeAsyncResult.SendHello())
							{
								return;
							}
						}
						if (connectAndHandshakeAsyncResult.connection.EnableSsl)
						{
							if (!connectAndHandshakeAsyncResult.connection.serverSupportsStartTls && !(connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream is TlsStream))
							{
								throw new SmtpException(SR.GetString("MailServerDoesNotSupportStartTls"));
							}
							connectAndHandshakeAsyncResult.SendStartTls();
						}
						else
						{
							connectAndHandshakeAsyncResult.Authenticate();
						}
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060035B7 RID: 13751 RVA: 0x000E5174 File Offset: 0x000E4174
			private bool SendHello()
			{
				if (!ServicePointManager.AllowSmtpFallbackToPlainText && this.connection.enableSsl)
				{
					throw new SmtpException("MailServerDoesNotSupportStartTls");
				}
				IAsyncResult asyncResult = HelloCommand.BeginSend(this.connection, this.connection.client.clientDomain, SmtpConnection.ConnectAndHandshakeAsyncResult.sendHelloCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.connection.supportedAuth = SupportedAuth.Login;
					HelloCommand.EndSend(asyncResult);
					this.Authenticate();
					return true;
				}
				return false;
			}

			// Token: 0x060035B8 RID: 13752 RVA: 0x000E51E8 File Offset: 0x000E41E8
			private static void SendHelloCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						HelloCommand.EndSend(result);
						connectAndHandshakeAsyncResult.Authenticate();
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060035B9 RID: 13753 RVA: 0x000E5234 File Offset: 0x000E4234
			private bool SendStartTls()
			{
				IAsyncResult asyncResult = StartTlsCommand.BeginSend(this.connection, new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendStartTlsCallback), this);
				if (asyncResult.CompletedSynchronously)
				{
					StartTlsCommand.EndSend(asyncResult);
					TlsStream networkStream = new TlsStream(this.connection.pooledStream.ServicePoint.Host, this.connection.pooledStream.NetworkStream, this.connection.ClientCertificates, this.connection.pooledStream.ServicePoint, this.connection.client, this.m_OuterResult.ContextCopy);
					this.connection.pooledStream.NetworkStream = networkStream;
					this.connection.responseReader = new SmtpReplyReaderFactory(this.connection.pooledStream.NetworkStream);
					this.SendEHello();
					return true;
				}
				return false;
			}

			// Token: 0x060035BA RID: 13754 RVA: 0x000E5304 File Offset: 0x000E4304
			private static void SendStartTlsCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						StartTlsCommand.EndSend(result);
						TlsStream networkStream = new TlsStream(connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint.Host, connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream, connectAndHandshakeAsyncResult.connection.ClientCertificates, connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint, connectAndHandshakeAsyncResult.connection.client, connectAndHandshakeAsyncResult.m_OuterResult.ContextCopy);
						connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream = networkStream;
						connectAndHandshakeAsyncResult.connection.responseReader = new SmtpReplyReaderFactory(connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream);
						connectAndHandshakeAsyncResult.SendEHello();
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060035BB RID: 13755 RVA: 0x000E53E0 File Offset: 0x000E43E0
			private void Authenticate()
			{
				if (this.connection.credentials != null)
				{
					while (++this.currentModule < this.connection.authenticationModules.Length)
					{
						ISmtpAuthenticationModule smtpAuthenticationModule = this.connection.authenticationModules[this.currentModule];
						if (this.connection.AuthSupported(smtpAuthenticationModule))
						{
							NetworkCredential credential = this.connection.credentials.GetCredential(this.host, this.port, smtpAuthenticationModule.AuthenticationType);
							if (credential != null)
							{
								Authorization authorization = this.connection.SetContextAndTryAuthenticate(smtpAuthenticationModule, credential, this.m_OuterResult);
								if (authorization != null && authorization.Message != null)
								{
									IAsyncResult asyncResult = AuthCommand.BeginSend(this.connection, this.connection.authenticationModules[this.currentModule].AuthenticationType, authorization.Message, SmtpConnection.ConnectAndHandshakeAsyncResult.authenticateCallback, this);
									if (!asyncResult.CompletedSynchronously)
									{
										return;
									}
									LineInfo lineInfo = AuthCommand.EndSend(asyncResult);
									if (lineInfo.StatusCode == (SmtpStatusCode)334)
									{
										this.authResponse = lineInfo.Line;
										if (!this.AuthenticateContinue())
										{
											return;
										}
									}
									else if (lineInfo.StatusCode == (SmtpStatusCode)235)
									{
										smtpAuthenticationModule.CloseContext(this.connection);
										this.connection.isConnected = true;
										break;
									}
								}
							}
						}
					}
				}
				this.connection.isConnected = true;
				base.InvokeCallback();
			}

			// Token: 0x060035BC RID: 13756 RVA: 0x000E5538 File Offset: 0x000E4538
			private static void AuthenticateCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						LineInfo lineInfo = AuthCommand.EndSend(result);
						if (lineInfo.StatusCode == (SmtpStatusCode)334)
						{
							connectAndHandshakeAsyncResult.authResponse = lineInfo.Line;
							if (!connectAndHandshakeAsyncResult.AuthenticateContinue())
							{
								return;
							}
						}
						else if (lineInfo.StatusCode == (SmtpStatusCode)235)
						{
							connectAndHandshakeAsyncResult.connection.authenticationModules[connectAndHandshakeAsyncResult.currentModule].CloseContext(connectAndHandshakeAsyncResult.connection);
							connectAndHandshakeAsyncResult.connection.isConnected = true;
							connectAndHandshakeAsyncResult.InvokeCallback();
							return;
						}
						connectAndHandshakeAsyncResult.Authenticate();
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060035BD RID: 13757 RVA: 0x000E55EC File Offset: 0x000E45EC
			private bool AuthenticateContinue()
			{
				for (;;)
				{
					Authorization authorization = this.connection.authenticationModules[this.currentModule].Authenticate(this.authResponse, null, this.connection, this.connection.client.TargetName, this.connection.channelBindingToken);
					if (authorization == null)
					{
						break;
					}
					IAsyncResult asyncResult = AuthCommand.BeginSend(this.connection, authorization.Message, SmtpConnection.ConnectAndHandshakeAsyncResult.authenticateContinueCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					LineInfo lineInfo = AuthCommand.EndSend(asyncResult);
					if (lineInfo.StatusCode == (SmtpStatusCode)235)
					{
						goto Block_2;
					}
					if (lineInfo.StatusCode != (SmtpStatusCode)334)
					{
						return true;
					}
					this.authResponse = lineInfo.Line;
				}
				throw new SmtpException(SR.GetString("SmtpAuthenticationFailed"));
				Block_2:
				this.connection.authenticationModules[this.currentModule].CloseContext(this.connection);
				this.connection.isConnected = true;
				base.InvokeCallback();
				return false;
			}

			// Token: 0x060035BE RID: 13758 RVA: 0x000E56D8 File Offset: 0x000E46D8
			private static void AuthenticateContinueCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						LineInfo lineInfo = AuthCommand.EndSend(result);
						if (lineInfo.StatusCode == (SmtpStatusCode)235)
						{
							connectAndHandshakeAsyncResult.connection.authenticationModules[connectAndHandshakeAsyncResult.currentModule].CloseContext(connectAndHandshakeAsyncResult.connection);
							connectAndHandshakeAsyncResult.connection.isConnected = true;
							connectAndHandshakeAsyncResult.InvokeCallback();
						}
						else
						{
							if (lineInfo.StatusCode == (SmtpStatusCode)334)
							{
								connectAndHandshakeAsyncResult.authResponse = lineInfo.Line;
								if (!connectAndHandshakeAsyncResult.AuthenticateContinue())
								{
									return;
								}
							}
							connectAndHandshakeAsyncResult.Authenticate();
						}
					}
					catch (Exception result2)
					{
						connectAndHandshakeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x0400310B RID: 12555
			private static readonly GeneralAsyncDelegate m_ConnectionCreatedCallback = new GeneralAsyncDelegate(SmtpConnection.ConnectAndHandshakeAsyncResult.ConnectionCreatedCallback);

			// Token: 0x0400310C RID: 12556
			private string authResponse;

			// Token: 0x0400310D RID: 12557
			private SmtpConnection connection;

			// Token: 0x0400310E RID: 12558
			private int currentModule = -1;

			// Token: 0x0400310F RID: 12559
			private int port;

			// Token: 0x04003110 RID: 12560
			private static AsyncCallback handshakeCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.HandshakeCallback);

			// Token: 0x04003111 RID: 12561
			private static AsyncCallback sendEHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendEHelloCallback);

			// Token: 0x04003112 RID: 12562
			private static AsyncCallback sendHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendHelloCallback);

			// Token: 0x04003113 RID: 12563
			private static AsyncCallback authenticateCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateCallback);

			// Token: 0x04003114 RID: 12564
			private static AsyncCallback authenticateContinueCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateContinueCallback);

			// Token: 0x04003115 RID: 12565
			private string host;

			// Token: 0x04003116 RID: 12566
			private readonly ContextAwareResult m_OuterResult;
		}
	}
}
