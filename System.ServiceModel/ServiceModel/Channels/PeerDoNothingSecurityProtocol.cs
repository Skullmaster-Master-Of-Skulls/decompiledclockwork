using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A25 RID: 2597
	internal class PeerDoNothingSecurityProtocol : SecurityProtocol
	{
		// Token: 0x06006737 RID: 26423 RVA: 0x00181A21 File Offset: 0x0017FC21
		public PeerDoNothingSecurityProtocol(SecurityProtocolFactory factory) : base(factory, null, null)
		{
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x00181A2C File Offset: 0x0017FC2C
		public override void SecureOutgoingMessage(ref Message message, TimeSpan timeout)
		{
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x00181A30 File Offset: 0x0017FC30
		public override void VerifyIncomingMessage(ref Message request, TimeSpan timeout)
		{
			try
			{
				int num = request.Headers.FindHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
				if (num >= 0)
				{
					request.Headers.AddUnderstood(num);
				}
			}
			catch (MessageHeaderException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (XmlException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (SerializationException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
			}
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x00181AB0 File Offset: 0x0017FCB0
		public override void OnAbort()
		{
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x00181AB2 File Offset: 0x0017FCB2
		public override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x00181AB4 File Offset: 0x0017FCB4
		public override void OnOpen(TimeSpan timeout)
		{
		}
	}
}
