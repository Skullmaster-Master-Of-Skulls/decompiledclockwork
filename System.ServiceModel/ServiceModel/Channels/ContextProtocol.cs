using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B4 RID: 1972
	internal abstract class ContextProtocol
	{
		// Token: 0x06004A92 RID: 19090 RVA: 0x00112052 File Offset: 0x00110252
		protected ContextProtocol(ContextExchangeMechanism contextExchangeMechanism)
		{
			if (!ContextExchangeMechanismHelper.IsDefined(contextExchangeMechanism))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("contextExchangeMechanism"));
			}
			this.contextExchangeMechanism = contextExchangeMechanism;
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0011207E File Offset: 0x0011027E
		protected ContextExchangeMechanism ContextExchangeMechanism
		{
			get
			{
				return this.contextExchangeMechanism;
			}
		}

		// Token: 0x06004A94 RID: 19092
		public abstract void OnIncomingMessage(Message message);

		// Token: 0x06004A95 RID: 19093
		public abstract void OnOutgoingMessage(Message message, RequestContext requestContext);

		// Token: 0x06004A96 RID: 19094 RVA: 0x00112088 File Offset: 0x00110288
		protected void OnSendSoapContextHeader(Message message, ContextMessageProperty context)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.Context.Count > 0)
			{
				message.Headers.Add(new ContextMessageHeader(context.Context));
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 983045, SR.GetString("TraceCodeContextProtocolContextAddedToMessage"), this);
			}
		}

		// Token: 0x04002F20 RID: 12064
		private ContextExchangeMechanism contextExchangeMechanism;

		// Token: 0x02000CF8 RID: 3320
		internal static class HttpCookieToolbox
		{
			// Token: 0x06007A99 RID: 31385 RVA: 0x001C87B0 File Offset: 0x001C69B0
			public static string EncodeContextAsHttpSetCookieHeader(ContextMessageProperty context, Uri uri)
			{
				if (uri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
				}
				if (context == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
				}
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					OmitXmlDeclaration = true
				});
				ContextMessageHeader contextMessageHeader = new ContextMessageHeader(context.Context);
				contextMessageHeader.WriteHeader(xmlWriter, MessageVersion.Default);
				xmlWriter.Flush();
				return string.Format(CultureInfo.InvariantCulture, "{0}=\"{1}\";Path={2}", new object[]
				{
					"WscContext",
					Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length),
					uri.AbsolutePath
				});
			}

			// Token: 0x06007A9A RID: 31386 RVA: 0x001C8864 File Offset: 0x001C6A64
			public static bool TryCreateFromHttpCookieHeader(string httpCookieHeader, out ContextMessageProperty context)
			{
				if (httpCookieHeader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("httpCookieHeader");
				}
				context = null;
				foreach (string text in httpCookieHeader.Split(new char[]
				{
					';'
				}))
				{
					string text2 = text.Trim();
					if (text2.StartsWith("WscContext", StringComparison.Ordinal))
					{
						int num = text2.IndexOf('=');
						if (num < 0)
						{
							context = new ContextMessageProperty();
							break;
						}
						if (num < text2.Length - 1)
						{
							string text3 = text2.Substring(num + 1).Trim();
							if (text3.Length > 1 && text3[0] == '"' && text3[text3.Length - 1] == '"')
							{
								text3 = text3.Substring(1, text3.Length - 2);
							}
							try
							{
								context = ContextMessageHeader.ParseContextHeader(XmlReader.Create(new MemoryStream(Convert.FromBase64String(text3))));
								break;
							}
							catch (SerializationException exception)
							{
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
							}
							catch (ProtocolException exception2)
							{
								DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
							}
						}
					}
				}
				return context != null;
			}

			// Token: 0x04004615 RID: 17941
			public const string ContextHttpCookieName = "WscContext";

			// Token: 0x04004616 RID: 17942
			public const string RemoveContextHttpCookieHeader = "WscContext;Max-Age=0";
		}
	}
}
