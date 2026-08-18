using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000878 RID: 2168
	internal class DefaultWebSocketConnectionHandler : WebSocketConnectionHandler
	{
		// Token: 0x0600522A RID: 21034 RVA: 0x0012EEEC File Offset: 0x0012D0EC
		public DefaultWebSocketConnectionHandler(string subProtocol, string currentVersion, MessageVersion messageVersion, MessageEncoderFactory encoderFactory, TransferMode transferMode)
		{
			this.subProtocol = subProtocol;
			this.currentVersion = currentVersion;
			this.checkVersionFunc = new Func<string, bool>(this.CheckVersion);
			if (messageVersion != MessageVersion.None)
			{
				this.needToCheckContentType = true;
				this.encoder = encoderFactory.CreateSessionEncoder();
				this.checkContentTypeFunc = new Func<string, bool>(this.CheckContentType);
				if (encoderFactory is BinaryMessageEncoderFactory)
				{
					this.needToCheckTransferMode = true;
					this.transferMode = transferMode.ToString();
					this.checkTransferModeFunc = new Func<string, bool>(this.CheckTransferMode);
				}
			}
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0012EF84 File Offset: 0x0012D184
		protected internal override HttpResponseMessage AcceptWebSocket(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (!DefaultWebSocketConnectionHandler.CheckHttpHeader(request, "Sec-WebSocket-Version", this.checkVersionFunc))
			{
				return DefaultWebSocketConnectionHandler.GetUpgradeRequiredResponseMessageWithVersion(request, this.currentVersion);
			}
			if (this.needToCheckContentType)
			{
				if (!DefaultWebSocketConnectionHandler.CheckHttpHeader(request, "soap-content-type", this.checkContentTypeFunc))
				{
					return this.GetBadRequestResponseMessageWithContentTypeAndTransfermode(request);
				}
				if (this.needToCheckTransferMode && !DefaultWebSocketConnectionHandler.CheckHttpHeader(request, "microsoft-binary-transfer-mode", this.checkTransferModeFunc))
				{
					return this.GetBadRequestResponseMessageWithContentTypeAndTransfermode(request);
				}
			}
			HttpResponseMessage webSocketAcceptedResponseMessage = WebSocketConnectionHandler.GetWebSocketAcceptedResponseMessage(request);
			DefaultWebSocketConnectionHandler.SubprotocolParseResult subprotocolParseResult = DefaultWebSocketConnectionHandler.ParseSubprotocolValues(request);
			if (subprotocolParseResult.HeaderFound)
			{
				if (!subprotocolParseResult.HeaderValid)
				{
					return WebSocketConnectionHandler.GetBadRequestResponseMessage(request);
				}
				string text = null;
				foreach (string text2 in subprotocolParseResult.ParsedSubprotocols)
				{
					if (string.Compare(text2, this.subProtocol, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = text2;
						break;
					}
				}
				if (text == null)
				{
					FxTrace.Exception.AsWarning(new WebException(SR.GetString("WebSocketInvalidProtocolNotInClientList", new object[]
					{
						this.subProtocol,
						string.Join(", ", subprotocolParseResult.ParsedSubprotocols)
					})));
					return DefaultWebSocketConnectionHandler.GetUpgradeRequiredResponseMessageWithSubProtocol(request, this.subProtocol);
				}
				webSocketAcceptedResponseMessage.Headers.Remove("Sec-WebSocket-Protocol");
				if (text != string.Empty)
				{
					webSocketAcceptedResponseMessage.Headers.Add("Sec-WebSocket-Protocol", text);
				}
			}
			else if (!string.IsNullOrEmpty(this.subProtocol))
			{
				FxTrace.Exception.AsWarning(new WebException(SR.GetString("WebSocketInvalidProtocolNoHeader", new object[]
				{
					this.subProtocol,
					"Sec-WebSocket-Protocol"
				})));
				return DefaultWebSocketConnectionHandler.GetUpgradeRequiredResponseMessageWithSubProtocol(request, this.subProtocol);
			}
			return webSocketAcceptedResponseMessage;
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0012F13C File Offset: 0x0012D33C
		private static DefaultWebSocketConnectionHandler.SubprotocolParseResult ParseSubprotocolValues(HttpRequestMessage request)
		{
			IEnumerable<string> enumerable = null;
			if (request.Headers.TryGetValues("Sec-WebSocket-Protocol", out enumerable))
			{
				List<string> list = new List<string>();
				foreach (string subProtocolValue in enumerable)
				{
					List<string> collection;
					if (!WebSocketHelper.TryParseSubProtocol(subProtocolValue, out collection))
					{
						return DefaultWebSocketConnectionHandler.SubprotocolParseResult.HeaderInvalid;
					}
					list.AddRange(collection);
				}
				if (list.Count == 0)
				{
					list.Add(string.Empty);
				}
				return new DefaultWebSocketConnectionHandler.SubprotocolParseResult(true, true, list);
			}
			return DefaultWebSocketConnectionHandler.SubprotocolParseResult.HeaderNotFound;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0012F1DC File Offset: 0x0012D3DC
		private static HttpResponseMessage GetUpgradeRequiredResponseMessageWithSubProtocol(HttpRequestMessage request, string subprotocol)
		{
			HttpResponseMessage upgradeRequiredResponseMessage = WebSocketConnectionHandler.GetUpgradeRequiredResponseMessage(request);
			if (!string.IsNullOrEmpty(subprotocol))
			{
				upgradeRequiredResponseMessage.Headers.Add("Sec-WebSocket-Protocol", subprotocol);
			}
			return upgradeRequiredResponseMessage;
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0012F20C File Offset: 0x0012D40C
		private static HttpResponseMessage GetUpgradeRequiredResponseMessageWithVersion(HttpRequestMessage request, string version)
		{
			HttpResponseMessage upgradeRequiredResponseMessage = WebSocketConnectionHandler.GetUpgradeRequiredResponseMessage(request);
			upgradeRequiredResponseMessage.Headers.Add("Sec-WebSocket-Version", version);
			return upgradeRequiredResponseMessage;
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0012F234 File Offset: 0x0012D434
		private static bool CheckHttpHeader(HttpRequestMessage request, string header, Func<string, bool> validator)
		{
			IEnumerable<string> enumerable;
			if (!request.Headers.TryGetValues(header, out enumerable))
			{
				return false;
			}
			foreach (string text in enumerable)
			{
				if (text != null && !validator(text.Trim()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0012F2A8 File Offset: 0x0012D4A8
		private bool CheckVersion(string headerValue)
		{
			return headerValue == this.currentVersion;
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0012F2B6 File Offset: 0x0012D4B6
		private bool CheckContentType(string headerValue)
		{
			return this.encoder.IsContentTypeSupported(headerValue);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0012F2C4 File Offset: 0x0012D4C4
		private bool CheckTransferMode(string headerValue)
		{
			return headerValue.Equals(this.transferMode, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0012F2D4 File Offset: 0x0012D4D4
		private HttpResponseMessage GetBadRequestResponseMessageWithContentTypeAndTransfermode(HttpRequestMessage request)
		{
			HttpResponseMessage badRequestResponseMessage = WebSocketConnectionHandler.GetBadRequestResponseMessage(request);
			badRequestResponseMessage.Headers.Add("soap-content-type", this.encoder.ContentType);
			if (this.needToCheckTransferMode)
			{
				badRequestResponseMessage.Headers.Add("microsoft-binary-transfer-mode", this.transferMode.ToString());
			}
			return badRequestResponseMessage;
		}

		// Token: 0x04003245 RID: 12869
		private string currentVersion;

		// Token: 0x04003246 RID: 12870
		private string subProtocol;

		// Token: 0x04003247 RID: 12871
		private MessageEncoder encoder;

		// Token: 0x04003248 RID: 12872
		private string transferMode;

		// Token: 0x04003249 RID: 12873
		private bool needToCheckContentType;

		// Token: 0x0400324A RID: 12874
		private bool needToCheckTransferMode;

		// Token: 0x0400324B RID: 12875
		private Func<string, bool> checkVersionFunc;

		// Token: 0x0400324C RID: 12876
		private Func<string, bool> checkContentTypeFunc;

		// Token: 0x0400324D RID: 12877
		private Func<string, bool> checkTransferModeFunc;

		// Token: 0x02000D5B RID: 3419
		private struct SubprotocolParseResult
		{
			// Token: 0x06007D58 RID: 32088 RVA: 0x001D4990 File Offset: 0x001D2B90
			public SubprotocolParseResult(bool headerFound, bool headerValid, IEnumerable<string> parsedSubprotocols)
			{
				this.headerFound = headerFound;
				this.headerValid = headerValid;
				this.parsedSubprotocols = parsedSubprotocols;
			}

			// Token: 0x17001C01 RID: 7169
			// (get) Token: 0x06007D59 RID: 32089 RVA: 0x001D49A7 File Offset: 0x001D2BA7
			public bool HeaderFound
			{
				get
				{
					return this.headerFound;
				}
			}

			// Token: 0x17001C02 RID: 7170
			// (get) Token: 0x06007D5A RID: 32090 RVA: 0x001D49AF File Offset: 0x001D2BAF
			public bool HeaderValid
			{
				get
				{
					return this.headerValid;
				}
			}

			// Token: 0x17001C03 RID: 7171
			// (get) Token: 0x06007D5B RID: 32091 RVA: 0x001D49B7 File Offset: 0x001D2BB7
			public IEnumerable<string> ParsedSubprotocols
			{
				get
				{
					return this.parsedSubprotocols;
				}
			}

			// Token: 0x040047F7 RID: 18423
			public static readonly DefaultWebSocketConnectionHandler.SubprotocolParseResult HeaderInvalid = new DefaultWebSocketConnectionHandler.SubprotocolParseResult(true, false, null);

			// Token: 0x040047F8 RID: 18424
			public static readonly DefaultWebSocketConnectionHandler.SubprotocolParseResult HeaderNotFound = new DefaultWebSocketConnectionHandler.SubprotocolParseResult(false, false, null);

			// Token: 0x040047F9 RID: 18425
			private bool headerFound;

			// Token: 0x040047FA RID: 18426
			private bool headerValid;

			// Token: 0x040047FB RID: 18427
			private IEnumerable<string> parsedSubprotocols;
		}
	}
}
