using System;
using System.IdentityModel.Selectors;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000877 RID: 2167
	internal class ClientWebSocketTransportDuplexSessionChannel : WebSocketTransportDuplexSessionChannel
	{
		// Token: 0x06005218 RID: 21016 RVA: 0x0012E26F File Offset: 0x0012C46F
		static ClientWebSocketTransportDuplexSessionChannel()
		{
			WebSocket.RegisterPrefixes();
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0012E276 File Offset: 0x0012C476
		public ClientWebSocketTransportDuplexSessionChannel(HttpChannelFactory<IDuplexSessionChannel> channelFactory, ClientWebSocketFactory connectionFactory, EndpointAddress remoteAddresss, Uri via, ConnectionBufferPool bufferPool) : base(channelFactory, remoteAddresss, via, bufferPool)
		{
			this.channelFactory = channelFactory;
			this.connectionFactory = connectionFactory;
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600521A RID: 21018 RVA: 0x0012E292 File Offset: 0x0012C492
		protected override bool IsStreamedOutput
		{
			get
			{
				return TransferModeHelper.IsRequestStreamed(base.TransferMode);
			}
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0012E2A0 File Offset: 0x0012C4A0
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			IAsyncResult result;
			try
			{
				if (TD.WebSocketConnectionRequestSendStartIsEnabled())
				{
					TD.WebSocketConnectionRequestSendStart(base.EventTraceActivity, (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
				}
				this.httpWebRequest = this.CreateHttpWebRequest(timeout);
				IAsyncResult asyncResult = this.httpWebRequest.BeginGetResponse(callback, state);
				flag = true;
				result = asyncResult;
			}
			catch (WebException ex)
			{
				if (TD.WebSocketConnectionFailedIsEnabled())
				{
					TD.WebSocketConnectionFailed(base.EventTraceActivity, ex.Message);
				}
				ClientWebSocketTransportDuplexSessionChannel.TryConvertAndThrow(ex);
				throw FxTrace.Exception.AsError(HttpChannelUtilities.CreateRequestWebException(ex, this.httpWebRequest, HttpAbortReason.None));
			}
			finally
			{
				if (!flag)
				{
					this.CleanupTokenProviders();
					this.CleanupOnError(this.httpWebRequest, null);
				}
			}
			return result;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0012E370 File Offset: 0x0012C570
		protected override void OnEndOpen(IAsyncResult result)
		{
			bool flag = false;
			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)this.httpWebRequest.EndGetResponse(result);
				this.HandleHttpWebResponse(this.httpWebRequest, response);
				this.RemoveIdentityMapping(false);
				flag = true;
				if (TD.WebSocketConnectionRequestSendStopIsEnabled())
				{
					TD.WebSocketConnectionRequestSendStop(base.EventTraceActivity, (base.WebSocket != null) ? base.WebSocket.GetHashCode() : -1);
				}
			}
			catch (WebException ex)
			{
				if (TD.WebSocketConnectionFailedIsEnabled())
				{
					TD.WebSocketConnectionFailed(base.EventTraceActivity, ex.Message);
				}
				ClientWebSocketTransportDuplexSessionChannel.TryConvertAndThrow(ex);
				throw FxTrace.Exception.AsError(HttpChannelUtilities.CreateRequestWebException(ex, this.httpWebRequest, HttpAbortReason.None));
			}
			finally
			{
				this.CleanupTokenProviders();
				if (!flag)
				{
					this.CleanupOnError(this.httpWebRequest, response);
				}
			}
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0012E440 File Offset: 0x0012C640
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse response = null;
			bool flag = false;
			try
			{
				if (TD.WebSocketConnectionRequestSendStartIsEnabled())
				{
					TD.WebSocketConnectionRequestSendStart(base.EventTraceActivity, (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
				}
				httpWebRequest = this.CreateHttpWebRequest(timeoutHelper.RemainingTime());
				response = (HttpWebResponse)httpWebRequest.GetResponse();
				this.HandleHttpWebResponse(httpWebRequest, response);
				this.RemoveIdentityMapping(false);
				flag = true;
				if (TD.WebSocketConnectionRequestSendStopIsEnabled())
				{
					TD.WebSocketConnectionRequestSendStop(base.EventTraceActivity, (base.WebSocket != null) ? base.WebSocket.GetHashCode() : -1);
				}
			}
			catch (WebException ex)
			{
				if (TD.WebSocketConnectionFailedIsEnabled())
				{
					TD.WebSocketConnectionFailed(base.EventTraceActivity, ex.Message);
				}
				ClientWebSocketTransportDuplexSessionChannel.TryConvertAndThrow(ex);
				throw FxTrace.Exception.AsError(HttpChannelUtilities.CreateRequestWebException(ex, httpWebRequest, HttpAbortReason.None));
			}
			finally
			{
				this.CleanupTokenProviders();
				if (!flag)
				{
					this.CleanupOnError(httpWebRequest, response);
				}
			}
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0012E548 File Offset: 0x0012C748
		protected override void OnCleanup()
		{
			this.cleanupStarted = true;
			base.OnCleanup();
			if (this.connection != null)
			{
				this.connection.Close();
			}
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0012E56C File Offset: 0x0012C76C
		private static void CheckResponseHeader(HttpWebResponse response, string headerKey, string expectedValue, bool ignoreCase)
		{
			string text = response.Headers[headerKey];
			if (text == null)
			{
				throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketTransportError"), new WebSocketException(SR.GetString("WebSocketUpgradeFailedHeaderMissingError", new object[]
				{
					headerKey
				}))));
			}
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (!text.Equals(expectedValue, comparisonType))
			{
				throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketTransportError"), new WebSocketException(SR.GetString("WebSocketUpgradeFailedWrongHeaderError", new object[]
				{
					headerKey,
					text,
					expectedValue
				}))));
			}
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0012E60C File Offset: 0x0012C80C
		private static void TryConvertAndThrow(WebException ex)
		{
			if (ex.Response != null)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				if (httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
				{
					string value = httpWebResponse.Headers["soap-content-type"];
					if (!string.IsNullOrWhiteSpace(value))
					{
						string value2 = httpWebResponse.Headers["microsoft-binary-transfer-mode"];
						if (!string.IsNullOrWhiteSpace(value2))
						{
							throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketContentTypeAndTransferModeMismatchFromServer"), ex));
						}
						throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketContentTypeMismatchFromServer"), ex));
					}
				}
				else if (httpWebResponse.StatusCode == HttpStatusCode.UpgradeRequired)
				{
					string text = httpWebResponse.Headers["Sec-WebSocket-Version"];
					if (!string.IsNullOrWhiteSpace(text))
					{
						throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketVersionMismatchFromServer", new object[]
						{
							text
						}), ex));
					}
					string text2 = httpWebResponse.Headers["Sec-WebSocket-Protocol"];
					if (!string.IsNullOrWhiteSpace(text2))
					{
						throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketSubProtocolMismatchFromServer", new object[]
						{
							text2
						}), ex));
					}
				}
			}
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0012E734 File Offset: 0x0012C934
		private void ConfigureHttpWebRequestHeader(HttpWebRequest request)
		{
			if (base.WebSocketSettings.SubProtocol != null)
			{
				request.Headers["Sec-WebSocket-Protocol"] = base.WebSocketSettings.SubProtocol;
			}
			if (this.channelFactory.MessageVersion != MessageVersion.None)
			{
				request.Headers["soap-content-type"] = this.channelFactory.WebSocketSoapContentType;
				if (this.channelFactory.MessageEncoderFactory is BinaryMessageEncoderFactory)
				{
					request.Headers["microsoft-binary-transfer-mode"] = this.channelFactory.TransferMode.ToString();
				}
			}
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0012E7D1 File Offset: 0x0012C9D1
		private void CleanupOnError(HttpWebRequest request, HttpWebResponse response)
		{
			if (response != null)
			{
				response.Close();
			}
			if (request != null)
			{
				request.Abort();
			}
			base.Cleanup();
			this.RemoveIdentityMapping(true);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0012E7F4 File Offset: 0x0012C9F4
		private void RemoveIdentityMapping(bool aborting)
		{
			if (this.cleanupIdentity)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.cleanupIdentity)
					{
						this.cleanupIdentity = false;
						HttpTransportSecurityHelpers.RemoveIdentityMapping(this.Via, this.RemoteAddress, !aborting);
					}
				}
			}
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0012E860 File Offset: 0x0012CA60
		private HttpWebRequest CreateHttpWebRequest(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			ChannelParameterCollection channelParameters = new ChannelParameterCollection();
			if (HttpChannelFactory<IDuplexSessionChannel>.MapIdentity(this.RemoteAddress, this.channelFactory.AuthenticationScheme))
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.cleanupIdentity = HttpTransportSecurityHelpers.AddIdentityMapping(this.Via, this.RemoteAddress);
				}
			}
			this.channelFactory.CreateAndOpenTokenProviders(this.RemoteAddress, this.Via, channelParameters, timeoutHelper.RemainingTime(), out this.webRequestTokenProvider, out this.webRequestProxyTokenProvider);
			SecurityTokenContainer clientCertificateToken = null;
			HttpsChannelFactory<IDuplexSessionChannel> httpsChannelFactory = this.channelFactory as HttpsChannelFactory<IDuplexSessionChannel>;
			if (httpsChannelFactory != null && httpsChannelFactory.RequireClientCertificate)
			{
				SecurityTokenProvider certificateProvider = httpsChannelFactory.CreateAndOpenCertificateTokenProvider(this.RemoteAddress, this.Via, channelParameters, timeoutHelper.RemainingTime());
				clientCertificateToken = httpsChannelFactory.GetCertificateSecurityToken(certificateProvider, this.RemoteAddress, this.Via, channelParameters, ref timeoutHelper);
			}
			HttpWebRequest webRequest = this.channelFactory.GetWebRequest(this.RemoteAddress, this.Via, this.webRequestTokenProvider, this.webRequestProxyTokenProvider, clientCertificateToken, timeoutHelper.RemainingTime(), true);
			if (this.connectionFactory != null)
			{
				this.UseWebSocketVersionFromFactory(webRequest);
			}
			this.webSocketKey = webRequest.Headers["Sec-WebSocket-Key"];
			this.ConfigureHttpWebRequestHeader(webRequest);
			webRequest.Timeout = (int)timeoutHelper.RemainingTime().TotalMilliseconds;
			return webRequest;
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0012E9CC File Offset: 0x0012CBCC
		private void CleanupTokenProviders()
		{
			if (this.webRequestTokenProvider != null)
			{
				this.webRequestTokenProvider.Abort();
				this.webRequestTokenProvider = null;
			}
			if (this.webRequestProxyTokenProvider != null)
			{
				this.webRequestProxyTokenProvider.Abort();
				this.webRequestProxyTokenProvider = null;
			}
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0012EA04 File Offset: 0x0012CC04
		private void HandleHttpWebResponse(HttpWebRequest request, HttpWebResponse response)
		{
			this.ValidateHttpWebResponse(response);
			this.connection = response.GetResponseStream();
			WebSocket webSocket = null;
			try
			{
				if (this.connectionFactory != null)
				{
					webSocket = (base.WebSocket = this.CreateWebSocketWithFactory());
				}
				else
				{
					byte[] array = base.TakeBuffer();
					try
					{
						webSocket = (base.WebSocket = WebSocket.CreateClientWebSocket(this.connection, base.WebSocketSettings.SubProtocol, WebSocketHelper.GetReceiveBufferSize(this.channelFactory.MaxReceivedMessageSize), 16384, base.WebSocketSettings.GetEffectiveKeepAliveInterval(), base.WebSocketSettings.DisablePayloadMasking, new ArraySegment<byte>(array)));
					}
					finally
					{
						base.InternalBuffer = array;
					}
				}
			}
			finally
			{
				if (webSocket != null && this.cleanupStarted)
				{
					webSocket.Abort();
					CommunicationObjectAbortedException ex = new CommunicationObjectAbortedException(new WebSocketException(WebSocketError.ConnectionClosedPrematurely).Message);
					FxTrace.Exception.AsWarning(ex);
					throw ex;
				}
			}
			bool useStreaming = TransferModeHelper.IsResponseStreamed(base.TransferMode);
			SecurityMessageProperty securityMessageProperty = this.channelFactory.CreateReplySecurityProperty(request, response);
			if (securityMessageProperty != null)
			{
				base.RemoteSecurity = securityMessageProperty;
			}
			base.SetMessageSource(new WebSocketTransportDuplexSessionChannel.WebSocketMessageSource(this, base.WebSocket, useStreaming, this));
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0012EB2C File Offset: 0x0012CD2C
		private void ValidateHttpWebResponse(HttpWebResponse response)
		{
			if (response.StatusCode != HttpStatusCode.SwitchingProtocols)
			{
				throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketTransportError"), new WebSocketException(SR.GetString("WebSocketUpgradeFailedError", new object[]
				{
					(int)response.StatusCode,
					response.StatusDescription,
					101,
					HttpStatusCode.SwitchingProtocols
				}))));
			}
			ClientWebSocketTransportDuplexSessionChannel.CheckResponseHeader(response, "Connection", "Upgrade", true);
			ClientWebSocketTransportDuplexSessionChannel.CheckResponseHeader(response, "Upgrade", "websocket", true);
			string expectedValue = WebSocketHelper.ComputeAcceptHeader(this.webSocketKey);
			ClientWebSocketTransportDuplexSessionChannel.CheckResponseHeader(response, "Sec-WebSocket-Accept", expectedValue, false);
			if (base.WebSocketSettings.SubProtocol != null)
			{
				ClientWebSocketTransportDuplexSessionChannel.CheckResponseHeader(response, "Sec-WebSocket-Protocol", base.WebSocketSettings.SubProtocol, true);
				return;
			}
			string text = response.Headers["Sec-WebSocket-Protocol"];
			if (!string.IsNullOrWhiteSpace(text))
			{
				throw FxTrace.Exception.AsError(new CommunicationException(SR.GetString("WebSocketTransportError"), new WebSocketException(SR.GetString("WebSocketUpgradeFailedInvalidProtocolError", new object[]
				{
					text
				}))));
			}
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0012EC4C File Offset: 0x0012CE4C
		private void UseWebSocketVersionFromFactory(HttpWebRequest request)
		{
			if (TD.WebSocketUseVersionFromClientWebSocketFactoryIsEnabled())
			{
				TD.WebSocketUseVersionFromClientWebSocketFactory(base.EventTraceActivity, this.connectionFactory.GetType().FullName);
			}
			string webSocketVersion;
			try
			{
				webSocketVersion = this.connectionFactory.WebSocketVersion;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_GetWebSocketVersionFailed", new object[]
				{
					this.connectionFactory.GetType().Name
				}), ex));
			}
			if (string.IsNullOrWhiteSpace(webSocketVersion))
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_InvalidWebSocketVersion", new object[]
				{
					this.connectionFactory.GetType().Name
				})));
			}
			try
			{
				request.Headers["Sec-WebSocket-Version"] = webSocketVersion;
			}
			catch (ArgumentException innerException)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_InvalidWebSocketVersion", new object[]
				{
					this.connectionFactory.GetType().Name
				}), innerException));
			}
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0012ED6C File Offset: 0x0012CF6C
		private WebSocket CreateWebSocketWithFactory()
		{
			if (TD.WebSocketCreateClientWebSocketWithFactoryIsEnabled())
			{
				TD.WebSocketCreateClientWebSocketWithFactory(base.EventTraceActivity, this.connectionFactory.GetType().FullName);
			}
			WebSocket webSocket;
			try
			{
				webSocket = this.connectionFactory.CreateWebSocket(this.connection, base.WebSocketSettings.Clone());
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_CreateWebSocketFailed", new object[]
				{
					this.connectionFactory.GetType().Name
				}), ex));
			}
			if (webSocket == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_InvalidWebSocket", new object[]
				{
					this.connectionFactory.GetType().Name
				})));
			}
			if (webSocket.State != WebSocketState.Open)
			{
				webSocket.Dispose();
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_InvalidWebSocket", new object[]
				{
					this.connectionFactory.GetType().Name
				})));
			}
			string subProtocol = base.WebSocketSettings.SubProtocol;
			string subProtocol2 = webSocket.SubProtocol;
			if (!((subProtocol == null) ? string.IsNullOrWhiteSpace(subProtocol2) : subProtocol.Equals(subProtocol2, StringComparison.OrdinalIgnoreCase)))
			{
				webSocket.Dispose();
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ClientWebSocketFactory_InvalidSubProtocol", new object[]
				{
					this.connectionFactory.GetType().Name,
					subProtocol2,
					subProtocol
				})));
			}
			return webSocket;
		}

		// Token: 0x0400323C RID: 12860
		private readonly ClientWebSocketFactory connectionFactory;

		// Token: 0x0400323D RID: 12861
		private HttpChannelFactory<IDuplexSessionChannel> channelFactory;

		// Token: 0x0400323E RID: 12862
		private Stream connection;

		// Token: 0x0400323F RID: 12863
		private SecurityTokenProviderContainer webRequestTokenProvider;

		// Token: 0x04003240 RID: 12864
		private SecurityTokenProviderContainer webRequestProxyTokenProvider;

		// Token: 0x04003241 RID: 12865
		private HttpWebRequest httpWebRequest;

		// Token: 0x04003242 RID: 12866
		private string webSocketKey;

		// Token: 0x04003243 RID: 12867
		private volatile bool cleanupStarted;

		// Token: 0x04003244 RID: 12868
		private volatile bool cleanupIdentity;
	}
}
