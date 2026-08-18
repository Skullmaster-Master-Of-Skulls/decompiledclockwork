using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A5 RID: 1957
	internal class ClientContextProtocol : ContextProtocol, IContextManager
	{
		// Token: 0x06004A05 RID: 18949 RVA: 0x0010FBD4 File Offset: 0x0010DDD4
		public ClientContextProtocol(ContextExchangeMechanism contextExchangeMechanism, Uri uri, IChannel owner, Uri callbackAddress, bool contextManagementEnabled) : base(contextExchangeMechanism)
		{
			if (contextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				this.cookieContainer = new CookieContainer();
			}
			this.context = ContextMessageProperty.Empty;
			this.contextManagementEnabled = contextManagementEnabled;
			this.owner = owner;
			this.thisLock = new object();
			this.uri = uri;
			this.callbackAddress = callbackAddress;
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004A06 RID: 18950 RVA: 0x0010FC2B File Offset: 0x0010DE2B
		protected Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x0010FC33 File Offset: 0x0010DE33
		// (set) Token: 0x06004A08 RID: 18952 RVA: 0x0010FC3B File Offset: 0x0010DE3B
		bool IContextManager.Enabled
		{
			get
			{
				return this.contextManagementEnabled;
			}
			set
			{
				if (this.owner.State != CommunicationState.Created)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ChannelIsOpen")));
				}
				this.contextManagementEnabled = value;
			}
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x0010FC6B File Offset: 0x0010DE6B
		public IDictionary<string, string> GetContext()
		{
			if (!this.contextManagementEnabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextManagementNotEnabled")));
			}
			return new Dictionary<string, string>(this.GetCurrentContext().Context);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0010FCA0 File Offset: 0x0010DEA0
		public override void OnIncomingMessage(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			ContextMessageProperty contextMessageProperty;
			if (base.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				contextMessageProperty = this.OnReceiveHttpCookies(message);
			}
			else
			{
				contextMessageProperty = this.OnReceiveSoapContextHeader(message);
			}
			if (contextMessageProperty != null)
			{
				if (this.contextManagementEnabled)
				{
					this.EnsureInvariants(true, contextMessageProperty);
				}
				else
				{
					contextMessageProperty.AddOrReplaceInMessage(message);
				}
			}
			if (message.Headers.FindHeader("CallbackContext", "http://schemas.microsoft.com/ws/2008/02/context") != -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ProtocolException(SR.GetString("CallbackContextNotExpectedOnIncomingMessageAtClient", new object[]
				{
					message.Headers.Action,
					"CallbackContext",
					"http://schemas.microsoft.com/ws/2008/02/context"
				})));
			}
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0010FD50 File Offset: 0x0010DF50
		public override void OnOutgoingMessage(Message message, RequestContext requestContext)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			ContextMessageProperty contextMessageProperty = null;
			if (ContextMessageProperty.TryGet(message, out contextMessageProperty) && this.contextManagementEnabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidMessageContext")));
			}
			if (base.ContextExchangeMechanism == ContextExchangeMechanism.ContextSoapHeader)
			{
				if (this.contextManagementEnabled)
				{
					contextMessageProperty = this.GetCurrentContext();
				}
				if (contextMessageProperty != null)
				{
					base.OnSendSoapContextHeader(message, contextMessageProperty);
				}
			}
			else if (base.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				if (this.contextManagementEnabled)
				{
					this.OnSendHttpCookies(message, null);
				}
				else
				{
					this.OnSendHttpCookies(message, contextMessageProperty);
				}
			}
			CallbackContextMessageProperty callbackContextMessageProperty;
			if (CallbackContextMessageProperty.TryGet(message, out callbackContextMessageProperty))
			{
				EndpointAddress address = callbackContextMessageProperty.CallbackAddress;
				if (address == null && this.callbackAddress != null)
				{
					address = callbackContextMessageProperty.CreateCallbackAddress(this.callbackAddress);
				}
				if (address != null)
				{
					if (base.ContextExchangeMechanism != ContextExchangeMechanism.ContextSoapHeader)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CallbackContextOnlySupportedInSoap")));
					}
					message.Headers.Add(new CallbackContextMessageHeader(address, message.Version.Addressing));
				}
			}
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0010FE64 File Offset: 0x0010E064
		public void SetContext(IDictionary<string, string> context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			ContextMessageProperty contextMessageProperty = new ContextMessageProperty(context);
			this.EnsureInvariants(false, contextMessageProperty);
			if (base.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				CookieContainer obj = this.cookieContainer;
				lock (obj)
				{
					this.cookieContainer.SetCookies(this.Uri, this.GetCookieHeaderFromContext(contextMessageProperty));
				}
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0010FEE4 File Offset: 0x0010E0E4
		private void EnsureInvariants(bool isServerIssued, ContextMessageProperty newContext)
		{
			if (!this.contextManagementEnabled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextManagementNotEnabled")));
			}
			if ((isServerIssued && !this.contextInitialized) || this.owner.State == CommunicationState.Created)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if ((isServerIssued && !this.contextInitialized) || this.owner.State == CommunicationState.Created)
					{
						this.context = newContext;
						this.contextInitialized = true;
						return;
					}
				}
			}
			if (isServerIssued)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("InvalidContextReceived")));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CachedContextIsImmutable")));
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0010FFB8 File Offset: 0x0010E1B8
		private string GetCookieHeaderFromContext(ContextMessageProperty contextMessageProperty)
		{
			if (contextMessageProperty.Context.Count == 0)
			{
				return "WscContext;Max-Age=0";
			}
			return ContextProtocol.HttpCookieToolbox.EncodeContextAsHttpSetCookieHeader(contextMessageProperty, this.Uri);
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0010FFDC File Offset: 0x0010E1DC
		private ContextMessageProperty GetCurrentContext()
		{
			if (this.cookieContainer != null)
			{
				CookieContainer obj = this.cookieContainer;
				lock (obj)
				{
					if (this.cookieContainer.GetCookies(this.Uri)["WscContext"] == null)
					{
						return ContextMessageProperty.Empty;
					}
					return this.context;
				}
			}
			return this.context;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00110054 File Offset: 0x0010E254
		private ContextMessageProperty OnReceiveHttpCookies(Message message)
		{
			ContextMessageProperty result = null;
			object obj;
			if (message.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
			{
				HttpResponseMessageProperty httpResponseMessageProperty = obj as HttpResponseMessageProperty;
				if (httpResponseMessageProperty != null)
				{
					string text = httpResponseMessageProperty.Headers[HttpResponseHeader.SetCookie];
					if (!string.IsNullOrEmpty(text))
					{
						CookieContainer obj2 = this.cookieContainer;
						lock (obj2)
						{
							if (!string.IsNullOrEmpty(text))
							{
								this.cookieContainer.SetCookies(this.Uri, text);
								ContextProtocol.HttpCookieToolbox.TryCreateFromHttpCookieHeader(text, out result);
							}
							if (!this.contextManagementEnabled)
							{
								this.cookieContainer.SetCookies(this.Uri, "WscContext;Max-Age=0");
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x00110110 File Offset: 0x0010E310
		private ContextMessageProperty OnReceiveSoapContextHeader(Message message)
		{
			ContextMessageProperty contextFromHeaderIfExists = ContextMessageHeader.GetContextFromHeaderIfExists(message);
			if (contextFromHeaderIfExists != null && DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 983046, SR.GetString("TraceCodeContextProtocolContextRetrievedFromMessage"), this);
			}
			return contextFromHeaderIfExists;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x00110148 File Offset: 0x0010E348
		private void OnSendHttpCookies(Message message, ContextMessageProperty context)
		{
			string value = null;
			if (this.contextManagementEnabled || context == null)
			{
				CookieContainer obj = this.cookieContainer;
				lock (obj)
				{
					value = this.cookieContainer.GetCookieHeader(this.Uri);
					goto IL_A3;
				}
			}
			if (context != null)
			{
				string cookieHeaderFromContext = this.GetCookieHeaderFromContext(context);
				CookieContainer obj2 = this.cookieContainer;
				lock (obj2)
				{
					this.cookieContainer.SetCookies(this.Uri, cookieHeaderFromContext);
					value = this.cookieContainer.GetCookieHeader(this.Uri);
					this.cookieContainer.SetCookies(this.Uri, "WscContext;Max-Age=0");
				}
			}
			IL_A3:
			if (!string.IsNullOrEmpty(value))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = null;
				object obj3;
				if (message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj3))
				{
					httpRequestMessageProperty = (obj3 as HttpRequestMessageProperty);
				}
				if (httpRequestMessageProperty == null)
				{
					httpRequestMessageProperty = new HttpRequestMessageProperty();
					message.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
				}
				httpRequestMessageProperty.Headers.Add(HttpRequestHeader.Cookie, value);
			}
		}

		// Token: 0x04002EEC RID: 12012
		private ContextMessageProperty context;

		// Token: 0x04002EED RID: 12013
		private bool contextInitialized;

		// Token: 0x04002EEE RID: 12014
		private bool contextManagementEnabled;

		// Token: 0x04002EEF RID: 12015
		private CookieContainer cookieContainer;

		// Token: 0x04002EF0 RID: 12016
		private IChannel owner;

		// Token: 0x04002EF1 RID: 12017
		private object thisLock;

		// Token: 0x04002EF2 RID: 12018
		private Uri uri;

		// Token: 0x04002EF3 RID: 12019
		private Uri callbackAddress;
	}
}
