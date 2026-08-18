using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Transactions.Wsat.Messaging;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BB RID: 443
	internal class WsatRegistrationHeader : AddressHeader
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x00034568 File Offset: 0x00032768
		public WsatRegistrationHeader(Guid transactionId, string contextId, string tokenId)
		{
			this.transactionId = transactionId;
			this.contextId = contextId;
			this.tokenId = tokenId;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00034585 File Offset: 0x00032785
		public override string Name
		{
			get
			{
				return "RegisterInfo";
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0003458C File Offset: 0x0003278C
		public override string Namespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/02/transactions";
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00034593 File Offset: 0x00032793
		public Guid TransactionId
		{
			get
			{
				return this.transactionId;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003459B File Offset: 0x0003279B
		public string ContextId
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x000345A3 File Offset: 0x000327A3
		public string TokenId
		{
			get
			{
				return this.tokenId;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000345AB File Offset: 0x000327AB
		protected override void OnWriteStartAddressHeader(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement("mstx", XD.DotNetAtomicTransactionExternalDictionary.RegisterInfo, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000345CC File Offset: 0x000327CC
		protected override void OnWriteAddressHeaderContents(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(XD.DotNetAtomicTransactionExternalDictionary.LocalTransactionId, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
			writer.WriteValue(this.transactionId);
			writer.WriteEndElement();
			if (this.contextId != null)
			{
				writer.WriteStartElement(XD.DotNetAtomicTransactionExternalDictionary.ContextId, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
				writer.WriteValue(this.contextId);
				writer.WriteEndElement();
			}
			if (this.tokenId != null)
			{
				writer.WriteStartElement(XD.DotNetAtomicTransactionExternalDictionary.TokenId, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
				writer.WriteValue(this.tokenId);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00034670 File Offset: 0x00032870
		public static WsatRegistrationHeader ReadFrom(Message message)
		{
			int num;
			try
			{
				num = message.Headers.FindHeader("RegisterInfo", "http://schemas.microsoft.com/ws/2006/02/transactions");
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
			WsatRegistrationHeader result;
			using (readerAtHeader)
			{
				try
				{
					result = WsatRegistrationHeader.ReadFrom(readerAtHeader);
				}
				catch (XmlException ex)
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnlistmentHeaderException(ex.Message, ex));
				}
			}
			MessageHeaderInfo headerInfo = message.Headers[num];
			if (!message.Headers.UnderstoodHeaders.Contains(headerInfo))
			{
				message.Headers.UnderstoodHeaders.Add(headerInfo);
			}
			return result;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00034754 File Offset: 0x00032954
		private static WsatRegistrationHeader ReadFrom(XmlDictionaryReader reader)
		{
			reader.ReadFullStartElement(XD.DotNetAtomicTransactionExternalDictionary.RegisterInfo, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
			reader.MoveToStartElement(XD.DotNetAtomicTransactionExternalDictionary.LocalTransactionId, XD.DotNetAtomicTransactionExternalDictionary.Namespace);
			Guid a = reader.ReadElementContentAsGuid();
			if (a == Guid.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidRegistrationHeaderTransactionId")));
			}
			string text;
			if (reader.IsStartElement(XD.DotNetAtomicTransactionExternalDictionary.ContextId, XD.DotNetAtomicTransactionExternalDictionary.Namespace))
			{
				text = reader.ReadElementContentAsString().Trim();
				Uri uri;
				if (text.Length == 0 || text.Length > 256 || !Uri.TryCreate(text, UriKind.Absolute, out uri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidRegistrationHeaderIdentifier")));
				}
			}
			else
			{
				text = null;
			}
			string text2;
			if (reader.IsStartElement(XD.DotNetAtomicTransactionExternalDictionary.TokenId, XD.DotNetAtomicTransactionExternalDictionary.Namespace))
			{
				text2 = reader.ReadElementContentAsString().Trim();
				if (text2.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidRegistrationHeaderTokenId")));
				}
			}
			else
			{
				text2 = null;
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return new WsatRegistrationHeader(a, text, text2);
		}

		// Token: 0x04001765 RID: 5989
		private const string HeaderName = "RegisterInfo";

		// Token: 0x04001766 RID: 5990
		private const string HeaderNamespace = "http://schemas.microsoft.com/ws/2006/02/transactions";

		// Token: 0x04001767 RID: 5991
		private Guid transactionId;

		// Token: 0x04001768 RID: 5992
		private string contextId;

		// Token: 0x04001769 RID: 5993
		private string tokenId;
	}
}
