using System;
using System.Diagnostics;
using System.Net;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C3 RID: 1987
	internal class ServiceContextProtocol : ContextProtocol
	{
		// Token: 0x06004AEC RID: 19180 RVA: 0x001129B3 File Offset: 0x00110BB3
		public ServiceContextProtocol(ContextExchangeMechanism contextExchangeMechanism) : base(contextExchangeMechanism)
		{
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001129BC File Offset: 0x00110BBC
		public override void OnIncomingMessage(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (base.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				this.OnReceiveHttpCookies(message);
			}
			else
			{
				this.OnReceiveSoapContextHeader(message);
			}
			int num = message.Headers.FindHeader("CallbackContext", "http://schemas.microsoft.com/ws/2008/02/context");
			if (num > 0)
			{
				CallbackContextMessageProperty property = CallbackContextMessageHeader.ParseCallbackContextHeader(message.Headers.GetReaderAtHeader(num), message.Version.Addressing);
				message.Properties.Add(CallbackContextMessageProperty.Name, property);
			}
			ContextExchangeCorrelationHelper.AddIncomingContextCorrelationData(message);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00112A44 File Offset: 0x00110C44
		public override void OnOutgoingMessage(Message message, RequestContext requestContext)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			ContextMessageProperty context;
			if (ContextMessageProperty.TryGet(message, out context))
			{
				if (base.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
				{
					Uri uri = null;
					if (requestContext.RequestMessage.Properties != null)
					{
						uri = requestContext.RequestMessage.Properties.Via;
					}
					if (uri == null)
					{
						uri = requestContext.RequestMessage.Headers.To;
					}
					this.OnSendHttpCookies(message, context, uri);
				}
				else
				{
					base.OnSendSoapContextHeader(message, context);
				}
			}
			CallbackContextMessageProperty callbackContextMessageProperty;
			if (CallbackContextMessageProperty.TryGet(message, out callbackContextMessageProperty))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CallbackContextNotExpectedOnOutgoingMessageAtServer", new object[]
				{
					message.Headers.Action
				})));
			}
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00112AFC File Offset: 0x00110CFC
		private void OnReceiveHttpCookies(Message message)
		{
			object obj;
			if (message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
				if (httpRequestMessageProperty != null)
				{
					string text = httpRequestMessageProperty.Headers[HttpRequestHeader.Cookie];
					ContextMessageProperty contextMessageProperty;
					if (!string.IsNullOrEmpty(text) && ContextProtocol.HttpCookieToolbox.TryCreateFromHttpCookieHeader(text, out contextMessageProperty))
					{
						contextMessageProperty.AddOrReplaceInMessage(message);
					}
				}
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00112B50 File Offset: 0x00110D50
		private void OnReceiveSoapContextHeader(Message message)
		{
			ContextMessageProperty contextFromHeaderIfExists = ContextMessageHeader.GetContextFromHeaderIfExists(message);
			if (contextFromHeaderIfExists != null)
			{
				contextFromHeaderIfExists.AddOrReplaceInMessage(message);
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 983046, SR.GetString("TraceCodeContextProtocolContextRetrievedFromMessage"), this);
				}
			}
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00112B8C File Offset: 0x00110D8C
		private void OnSendHttpCookies(Message message, ContextMessageProperty context, Uri requestUri)
		{
			HttpResponseMessageProperty httpResponseMessageProperty = null;
			object obj;
			if (message.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
			{
				httpResponseMessageProperty = (obj as HttpResponseMessageProperty);
			}
			if (httpResponseMessageProperty == null)
			{
				httpResponseMessageProperty = new HttpResponseMessageProperty();
				message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
			}
			string value = ContextProtocol.HttpCookieToolbox.EncodeContextAsHttpSetCookieHeader(context, requestUri);
			httpResponseMessageProperty.Headers.Add(HttpResponseHeader.SetCookie, value);
		}
	}
}
