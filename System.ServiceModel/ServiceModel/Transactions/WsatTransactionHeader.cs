using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Transactions;
using System.Xml;
using Microsoft.Transactions.Wsat.Messaging;
using Microsoft.Transactions.Wsat.Protocol;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BF RID: 447
	internal class WsatTransactionHeader : MessageHeader
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x00034B64 File Offset: 0x00032D64
		public WsatTransactionHeader(CoordinationContext context, ProtocolVersion protocolVersion)
		{
			this.context = context;
			CoordinationStrings coordinationStrings = CoordinationStrings.Version(protocolVersion);
			this.wsatHeaderElement = coordinationStrings.CoordinationContext;
			this.wsatNamespace = coordinationStrings.Namespace;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00034B9D File Offset: 0x00032D9D
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00034BA0 File Offset: 0x00032DA0
		public override string Name
		{
			get
			{
				return this.wsatHeaderElement;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public override string Namespace
		{
			get
			{
				return this.wsatNamespace;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public static CoordinationContext GetCoordinationContext(Message message, ProtocolVersion protocolVersion)
		{
			CoordinationStrings coordinationStrings = CoordinationStrings.Version(protocolVersion);
			string coordinationContext = coordinationStrings.CoordinationContext;
			string @namespace = coordinationStrings.Namespace;
			int num;
			try
			{
				num = message.Headers.FindHeader(coordinationContext, @namespace);
			}
			catch (MessageHeaderException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				return null;
			}
			if (num < 0)
			{
				return null;
			}
			XmlDictionaryReader readerAtHeader = message.Headers.GetReaderAtHeader(num);
			CoordinationContext coordinationContext2;
			using (readerAtHeader)
			{
				coordinationContext2 = WsatTransactionHeader.GetCoordinationContext(readerAtHeader, protocolVersion);
			}
			MessageHeaderInfo headerInfo = message.Headers[num];
			if (!message.Headers.UnderstoodHeaders.Contains(headerInfo))
			{
				message.Headers.UnderstoodHeaders.Add(headerInfo);
			}
			return coordinationContext2;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00034C78 File Offset: 0x00032E78
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.context.WriteContent(writer);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00034C88 File Offset: 0x00032E88
		public static CoordinationContext GetCoordinationContext(XmlDictionaryReader reader, ProtocolVersion protocolVersion)
		{
			CoordinationXmlDictionaryStrings coordinationXmlDictionaryStrings = CoordinationXmlDictionaryStrings.Version(protocolVersion);
			CoordinationContext result;
			try
			{
				result = CoordinationContext.ReadFrom(reader, coordinationXmlDictionaryStrings.CoordinationContext, coordinationXmlDictionaryStrings.Namespace, protocolVersion);
			}
			catch (InvalidCoordinationContextException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("WsatHeaderCorrupt"), ex));
			}
			return result;
		}

		// Token: 0x04001770 RID: 6000
		private string wsatHeaderElement;

		// Token: 0x04001771 RID: 6001
		private string wsatNamespace;

		// Token: 0x04001772 RID: 6002
		private CoordinationContext context;
	}
}
