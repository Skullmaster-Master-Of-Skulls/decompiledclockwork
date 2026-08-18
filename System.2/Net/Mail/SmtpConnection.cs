using System;
using System.Globalization;
using System.IO;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Mail
{
	// Token: 0x02000288 RID: 648
	internal class SmtpConnection
	{
		// Token: 0x06001832 RID: 6194 RVA: 0x0007B39D File Offset: 0x0007959D
		private static PooledStream CreateSmtpPooledStream(ConnectionPool pool)
		{
			return new SmtpPooledStream(pool, TimeSpan.MaxValue, false);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0007B3AC File Offset: 0x000795AC
		internal SmtpConnection(SmtpTransport parent, SmtpClient client, ICredentialsByHost credentials, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			this.credentials = credentials;
			this.authenticationModules = authenticationModules;
			this.parent = parent;
			this.onCloseHandler = new EventHandler(this.OnClose);
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0007B404 File Offset: 0x00079604
		internal BufferBuilder BufferBuilder
		{
			get
			{
				return this.bufferBuilder;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0007B40C File Offset: 0x0007960C
		internal bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0007B414 File Offset: 0x00079614
		internal bool IsStreamOpen
		{
			get
			{
				return this.isStreamOpen;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0007B41C File Offset: 0x0007961C
		internal bool DSNEnabled
		{
			get
			{
				return this.pooledStream != null && ((SmtpPooledStream)this.pooledStream).dsnEnabled;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0007B438 File Offset: 0x00079638
		internal SmtpReplyReaderFactory Reader
		{
			get
			{
				return this.responseReader;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0007B440 File Offset: 0x00079640
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0007B448 File Offset: 0x00079648
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

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0007B451 File Offset: 0x00079651
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x0007B459 File Offset: 0x00079659
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

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0007B462 File Offset: 0x00079662
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x0007B46A File Offset: 0x0007966A
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

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0007B474 File Offset: 0x00079674
		internal bool ServerSupportsEai
		{
			get
			{
				SmtpPooledStream smtpPooledStream = (SmtpPooledStream)this.pooledStream;
				return smtpPooledStream.serverSupportsEai;
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0007B494 File Offset: 0x00079694
		internal IAsyncResult BeginGetConnection(ServicePoint servicePoint, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
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
			SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = new SmtpConnection.ConnectAndHandshakeAsyncResult(this, servicePoint.Host, servicePoint.Port, outerResult, callback, state);
			connectAndHandshakeAsyncResult.GetConnection(false);
			return connectAndHandshakeAsyncResult;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0007B532 File Offset: 0x00079732
		internal IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			return this.pooledStream.UnsafeBeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, callback, state);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0007B558 File Offset: 0x00079758
		internal void EndFlush(IAsyncResult result)
		{
			this.pooledStream.EndWrite(result);
			this.bufferBuilder.Reset();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0007B571 File Offset: 0x00079771
		internal void Flush()
		{
			this.pooledStream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
			this.bufferBuilder.Reset();
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0007B5A0 File Offset: 0x000797A0
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

		// Token: 0x06001845 RID: 6213 RVA: 0x0007B644 File Offset: 0x00079844
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
						this.connectionPool.PutConnection(this.pooledStream, this.pooledStream.Owner, this.Timeout, false);
					}
					this.isClosed = true;
				}
			}
			this.isConnected = false;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0007B6E4 File Offset: 0x000798E4
		internal void ParseExtensions(string[] extensions)
		{
			this.supportedAuth = SupportedAuth.None;
			foreach (string text in extensions)
			{
				if (string.Compare(text, 0, "auth", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
				{
					string[] array = text.Remove(0, 4).Split(new char[]
					{
						' ',
						'='
					}, StringSplitOptions.RemoveEmptyEntries);
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
							this.supportedAuth |= SupportedAuth.GSSAPI;
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
				else if (string.Compare(text, 0, "SMTPUTF8", 0, 8, StringComparison.OrdinalIgnoreCase) == 0)
				{
					((SmtpPooledStream)this.pooledStream).serverSupportsEai = true;
				}
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0007B844 File Offset: 0x00079A44
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
				if ((this.supportedAuth & SupportedAuth.GSSAPI) > SupportedAuth.None)
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

		// Token: 0x06001848 RID: 6216 RVA: 0x0007B8B8 File Offset: 0x00079AB8
		internal void GetConnection(ServicePoint servicePoint)
		{
			if (this.isConnected)
			{
				throw new InvalidOperationException(SR.GetString("SmtpAlreadyConnected"));
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, servicePoint);
			}
			this.connectionPool = ConnectionPoolManager.GetConnectionPool(servicePoint, "", SmtpConnection.m_CreateConnectionCallback);
			PooledStream connection = this.connectionPool.GetConnection(this, null, this.Timeout);
			while (((SmtpPooledStream)connection).creds != null && ((SmtpPooledStream)connection).creds != this.credentials)
			{
				this.connectionPool.PutConnection(connection, connection.Owner, this.Timeout, false);
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
				TlsStream tlsStream = new TlsStream(servicePoint.Host, connection.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, this.clientCertificates, servicePoint, this.client, null);
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
						NetworkCredential credential = this.credentials.GetCredential(servicePoint.Host, servicePoint.Port, this.authenticationModules[i].AuthenticationType);
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

		// Token: 0x06001849 RID: 6217 RVA: 0x0007BC7C File Offset: 0x00079E7C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private Authorization SetContextAndTryAuthenticate(ISmtpAuthenticationModule module, NetworkCredential credential, ContextAwareResult context)
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
							return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
						}
					}
					ExecutionContext executionContext = (context == null) ? null : context.ContextCopy;
					if (executionContext != null)
					{
						SmtpConnection.AuthenticateCallbackContext authenticateCallbackContext = new SmtpConnection.AuthenticateCallbackContext(this, module, credential, this.client.TargetName, this.channelBindingToken);
						ExecutionContext.Run(executionContext, SmtpConnection.s_AuthenticateCallback, authenticateCallbackContext);
						return authenticateCallbackContext.result;
					}
					return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
				}
				catch
				{
					throw;
				}
			}
			return module.Authenticate(null, credential, this, this.client.TargetName, this.channelBindingToken);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0007BD7C File Offset: 0x00079F7C
		private static void AuthenticateCallback(object state)
		{
			SmtpConnection.AuthenticateCallbackContext authenticateCallbackContext = (SmtpConnection.AuthenticateCallbackContext)state;
			authenticateCallbackContext.result = authenticateCallbackContext.module.Authenticate(null, authenticateCallbackContext.credential, authenticateCallbackContext.thisPtr, authenticateCallbackContext.spn, authenticateCallbackContext.token);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0007BDBA File Offset: 0x00079FBA
		internal void EndGetConnection(IAsyncResult result)
		{
			SmtpConnection.ConnectAndHandshakeAsyncResult.End(result);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0007BDC4 File Offset: 0x00079FC4
		internal Stream GetClosableStream()
		{
			ClosableStream result = new ClosableStream(this.pooledStream.NetworkStream, this.onCloseHandler);
			this.isStreamOpen = true;
			return result;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0007BDF0 File Offset: 0x00079FF0
		private void OnClose(object sender, EventArgs args)
		{
			this.isStreamOpen = false;
			DataStopCommand.Send(this);
		}

		// Token: 0x0400183E RID: 6206
		private static readonly CreateConnectionDelegate m_CreateConnectionCallback = new CreateConnectionDelegate(SmtpConnection.CreateSmtpPooledStream);

		// Token: 0x0400183F RID: 6207
		private static readonly ContextCallback s_AuthenticateCallback = new ContextCallback(SmtpConnection.AuthenticateCallback);

		// Token: 0x04001840 RID: 6208
		private BufferBuilder bufferBuilder = new BufferBuilder();

		// Token: 0x04001841 RID: 6209
		private bool isConnected;

		// Token: 0x04001842 RID: 6210
		private bool isClosed;

		// Token: 0x04001843 RID: 6211
		private bool isStreamOpen;

		// Token: 0x04001844 RID: 6212
		private bool sawNegotiate;

		// Token: 0x04001845 RID: 6213
		private EventHandler onCloseHandler;

		// Token: 0x04001846 RID: 6214
		internal SmtpTransport parent;

		// Token: 0x04001847 RID: 6215
		internal SmtpClient client;

		// Token: 0x04001848 RID: 6216
		private SmtpReplyReaderFactory responseReader;

		// Token: 0x04001849 RID: 6217
		private const int sizeOfAuthString = 5;

		// Token: 0x0400184A RID: 6218
		private const int sizeOfAuthExtension = 4;

		// Token: 0x0400184B RID: 6219
		private const string authExtension = "auth";

		// Token: 0x0400184C RID: 6220
		private const string authLogin = "login";

		// Token: 0x0400184D RID: 6221
		private const string authNtlm = "ntlm";

		// Token: 0x0400184E RID: 6222
		private const string authGssapi = "gssapi";

		// Token: 0x0400184F RID: 6223
		private const string authWDigest = "wdigest";

		// Token: 0x04001850 RID: 6224
		private PooledStream pooledStream;

		// Token: 0x04001851 RID: 6225
		private ConnectionPool connectionPool;

		// Token: 0x04001852 RID: 6226
		private SupportedAuth supportedAuth;

		// Token: 0x04001853 RID: 6227
		private bool serverSupportsStartTls;

		// Token: 0x04001854 RID: 6228
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x04001855 RID: 6229
		private ICredentialsByHost credentials;

		// Token: 0x04001856 RID: 6230
		private int timeout = 100000;

		// Token: 0x04001857 RID: 6231
		private string[] extensions;

		// Token: 0x04001858 RID: 6232
		private ChannelBinding channelBindingToken;

		// Token: 0x04001859 RID: 6233
		private bool enableSsl;

		// Token: 0x0400185A RID: 6234
		private X509CertificateCollection clientCertificates;

		// Token: 0x020007A0 RID: 1952
		private class AuthenticateCallbackContext
		{
			// Token: 0x060042FC RID: 17148 RVA: 0x001185F7 File Offset: 0x001167F7
			internal AuthenticateCallbackContext(SmtpConnection thisPtr, ISmtpAuthenticationModule module, NetworkCredential credential, string spn, ChannelBinding Token)
			{
				this.thisPtr = thisPtr;
				this.module = module;
				this.credential = credential;
				this.spn = spn;
				this.token = Token;
				this.result = null;
			}

			// Token: 0x040033B7 RID: 13239
			internal readonly SmtpConnection thisPtr;

			// Token: 0x040033B8 RID: 13240
			internal readonly ISmtpAuthenticationModule module;

			// Token: 0x040033B9 RID: 13241
			internal readonly NetworkCredential credential;

			// Token: 0x040033BA RID: 13242
			internal readonly string spn;

			// Token: 0x040033BB RID: 13243
			internal readonly ChannelBinding token;

			// Token: 0x040033BC RID: 13244
			internal Authorization result;
		}

		// Token: 0x020007A1 RID: 1953
		private class ConnectAndHandshakeAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042FD RID: 17149 RVA: 0x0011862B File Offset: 0x0011682B
			internal ConnectAndHandshakeAsyncResult(SmtpConnection connection, string host, int port, ContextAwareResult outerResult, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.connection = connection;
				this.host = host;
				this.port = port;
				this.m_OuterResult = outerResult;
			}

			// Token: 0x060042FE RID: 17150 RVA: 0x0011865C File Offset: 0x0011685C
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
						connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout, false);
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
					SmtpConnection obj = connectAndHandshakeAsyncResult.connection;
					lock (obj)
					{
						if (connectAndHandshakeAsyncResult.connection.isClosed)
						{
							connectAndHandshakeAsyncResult.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, connectAndHandshakeAsyncResult.connection.Timeout, false);
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

			// Token: 0x060042FF RID: 17151 RVA: 0x001187E0 File Offset: 0x001169E0
			internal static void End(IAsyncResult result)
			{
				SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result;
				object obj = connectAndHandshakeAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
			}

			// Token: 0x06004300 RID: 17152 RVA: 0x0011880C File Offset: 0x00116A0C
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
							this.connection.connectionPool.PutConnection(smtpPooledStream, smtpPooledStream.Owner, this.connection.Timeout, false);
							smtpPooledStream = (SmtpPooledStream)this.connection.connectionPool.GetConnection(this, synchronous ? null : SmtpConnection.ConnectAndHandshakeAsyncResult.m_ConnectionCreatedCallback, this.connection.Timeout);
							if (smtpPooledStream == null)
							{
								return;
							}
						}
						smtpPooledStream.creds = this.connection.credentials;
						smtpPooledStream.Owner = this.connection;
						SmtpConnection obj = this.connection;
						lock (obj)
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

			// Token: 0x06004301 RID: 17153 RVA: 0x00118950 File Offset: 0x00116B50
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
					this.SendEHello();
				}
				catch
				{
					this.SendHello();
				}
			}

			// Token: 0x06004302 RID: 17154 RVA: 0x00118A30 File Offset: 0x00116C30
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

			// Token: 0x06004303 RID: 17155 RVA: 0x00118AD4 File Offset: 0x00116CD4
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

			// Token: 0x06004304 RID: 17156 RVA: 0x00118BA8 File Offset: 0x00116DA8
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

			// Token: 0x06004305 RID: 17157 RVA: 0x00118CB8 File Offset: 0x00116EB8
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

			// Token: 0x06004306 RID: 17158 RVA: 0x00118D2C File Offset: 0x00116F2C
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

			// Token: 0x06004307 RID: 17159 RVA: 0x00118D78 File Offset: 0x00116F78
			private bool SendStartTls()
			{
				IAsyncResult asyncResult = StartTlsCommand.BeginSend(this.connection, new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendStartTlsCallback), this);
				if (asyncResult.CompletedSynchronously)
				{
					StartTlsCommand.EndSend(asyncResult);
					TlsStream networkStream = new TlsStream(this.connection.pooledStream.ServicePoint.Host, this.connection.pooledStream.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, this.connection.ClientCertificates, this.connection.pooledStream.ServicePoint, this.connection.client, this.m_OuterResult.ContextCopy);
					this.connection.pooledStream.NetworkStream = networkStream;
					this.connection.responseReader = new SmtpReplyReaderFactory(this.connection.pooledStream.NetworkStream);
					this.SendEHello();
					return true;
				}
				return false;
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x00118E50 File Offset: 0x00117050
			private static void SendStartTlsCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SmtpConnection.ConnectAndHandshakeAsyncResult connectAndHandshakeAsyncResult = (SmtpConnection.ConnectAndHandshakeAsyncResult)result.AsyncState;
					try
					{
						StartTlsCommand.EndSend(result);
						TlsStream networkStream = new TlsStream(connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint.Host, connectAndHandshakeAsyncResult.connection.pooledStream.NetworkStream, ServicePointManager.CheckCertificateRevocationList, (SslProtocols)ServicePointManager.SecurityProtocol, connectAndHandshakeAsyncResult.connection.ClientCertificates, connectAndHandshakeAsyncResult.connection.pooledStream.ServicePoint, connectAndHandshakeAsyncResult.connection.client, connectAndHandshakeAsyncResult.m_OuterResult.ContextCopy);
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

			// Token: 0x06004309 RID: 17161 RVA: 0x00118F34 File Offset: 0x00117134
			private void Authenticate()
			{
				if (this.connection.credentials != null)
				{
					ISmtpAuthenticationModule smtpAuthenticationModule;
					for (;;)
					{
						int num = this.currentModule + 1;
						this.currentModule = num;
						if (num >= this.connection.authenticationModules.Length)
						{
							goto IL_139;
						}
						smtpAuthenticationModule = this.connection.authenticationModules[this.currentModule];
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
										break;
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
										goto Block_9;
									}
								}
							}
						}
					}
					return;
					Block_9:
					smtpAuthenticationModule.CloseContext(this.connection);
					this.connection.isConnected = true;
				}
				IL_139:
				this.connection.isConnected = true;
				base.InvokeCallback();
			}

			// Token: 0x0600430A RID: 17162 RVA: 0x0011908C File Offset: 0x0011728C
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

			// Token: 0x0600430B RID: 17163 RVA: 0x00119140 File Offset: 0x00117340
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

			// Token: 0x0600430C RID: 17164 RVA: 0x0011922C File Offset: 0x0011742C
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

			// Token: 0x040033BD RID: 13245
			private static readonly GeneralAsyncDelegate m_ConnectionCreatedCallback = new GeneralAsyncDelegate(SmtpConnection.ConnectAndHandshakeAsyncResult.ConnectionCreatedCallback);

			// Token: 0x040033BE RID: 13246
			private string authResponse;

			// Token: 0x040033BF RID: 13247
			private SmtpConnection connection;

			// Token: 0x040033C0 RID: 13248
			private int currentModule = -1;

			// Token: 0x040033C1 RID: 13249
			private int port;

			// Token: 0x040033C2 RID: 13250
			private static AsyncCallback handshakeCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.HandshakeCallback);

			// Token: 0x040033C3 RID: 13251
			private static AsyncCallback sendEHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendEHelloCallback);

			// Token: 0x040033C4 RID: 13252
			private static AsyncCallback sendHelloCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.SendHelloCallback);

			// Token: 0x040033C5 RID: 13253
			private static AsyncCallback authenticateCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateCallback);

			// Token: 0x040033C6 RID: 13254
			private static AsyncCallback authenticateContinueCallback = new AsyncCallback(SmtpConnection.ConnectAndHandshakeAsyncResult.AuthenticateContinueCallback);

			// Token: 0x040033C7 RID: 13255
			private string host;

			// Token: 0x040033C8 RID: 13256
			private readonly ContextAwareResult m_OuterResult;
		}
	}
}
