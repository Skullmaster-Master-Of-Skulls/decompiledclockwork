using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Transactions;
using System.Xml;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001AF RID: 431
	internal class OleTxTransactionHeader : MessageHeader
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00032DB9 File Offset: 0x00030FB9
		public OleTxTransactionHeader(byte[] propagationToken, WsatExtendedInformation wsatInfo)
		{
			this.propagationToken = propagationToken;
			this.wsatInfo = wsatInfo;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00032DCF File Offset: 0x00030FCF
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00032DD2 File Offset: 0x00030FD2
		public override string Name
		{
			get
			{
				return "OleTxTransaction";
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00032DD9 File Offset: 0x00030FD9
		public override string Namespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/02/tx/oletx";
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00032DE0 File Offset: 0x00030FE0
		public byte[] PropagationToken
		{
			get
			{
				return this.propagationToken;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00032DE8 File Offset: 0x00030FE8
		public WsatExtendedInformation WsatExtendedInformation
		{
			get
			{
				return this.wsatInfo;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00032DF0 File Offset: 0x00030FF0
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (this.wsatInfo != null)
			{
				if (this.wsatInfo.Timeout != 0U)
				{
					writer.WriteAttributeString(XD.CoordinationExternalDictionary.Expires, OleTxTransactionHeader.CoordinationNamespace, XmlConvert.ToString(this.wsatInfo.Timeout));
				}
				if (!string.IsNullOrEmpty(this.wsatInfo.Identifier))
				{
					writer.WriteAttributeString(XD.CoordinationExternalDictionary.Identifier, OleTxTransactionHeader.CoordinationNamespace, this.wsatInfo.Identifier);
				}
			}
			OleTxTransactionHeader.WritePropagationTokenElement(writer, this.propagationToken);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032E78 File Offset: 0x00031078
		public static OleTxTransactionHeader ReadFrom(Message message)
		{
			int num;
			try
			{
				num = message.Headers.FindHeader("OleTxTransaction", "http://schemas.microsoft.com/ws/2006/02/tx/oletx");
			}
			catch (MessageHeaderException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("OleTxHeaderCorrupt"), ex));
			}
			if (num < 0)
			{
				return null;
			}
			XmlDictionaryReader readerAtHeader = message.Headers.GetReaderAtHeader(num);
			OleTxTransactionHeader result;
			using (readerAtHeader)
			{
				try
				{
					result = OleTxTransactionHeader.ReadFrom(readerAtHeader);
				}
				catch (XmlException ex2)
				{
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("OleTxHeaderCorrupt"), ex2));
				}
			}
			MessageHeaderInfo headerInfo = message.Headers[num];
			if (!message.Headers.UnderstoodHeaders.Contains(headerInfo))
			{
				message.Headers.UnderstoodHeaders.Add(headerInfo);
			}
			return result;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00032F70 File Offset: 0x00031170
		private static OleTxTransactionHeader ReadFrom(XmlDictionaryReader reader)
		{
			WsatExtendedInformation wsatExtendedInformation = null;
			if (reader.IsStartElement(XD.OleTxTransactionExternalDictionary.OleTxTransaction, XD.OleTxTransactionExternalDictionary.Namespace))
			{
				string attribute = reader.GetAttribute(XD.CoordinationExternalDictionary.Identifier, OleTxTransactionHeader.CoordinationNamespace);
				Uri uri;
				if (!string.IsNullOrEmpty(attribute) && !Uri.TryCreate(attribute, UriKind.Absolute, out uri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidWsatExtendedInfo")));
				}
				string attribute2 = reader.GetAttribute(XD.CoordinationExternalDictionary.Expires, OleTxTransactionHeader.CoordinationNamespace);
				uint num = 0U;
				if (!string.IsNullOrEmpty(attribute2))
				{
					try
					{
						num = XmlConvert.ToUInt32(attribute2);
					}
					catch (FormatException ex)
					{
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidWsatExtendedInfo"), ex));
					}
					catch (OverflowException ex2)
					{
						DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Error);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidWsatExtendedInfo"), ex2));
					}
				}
				if (!string.IsNullOrEmpty(attribute) || num != 0U)
				{
					wsatExtendedInformation = new WsatExtendedInformation(attribute, num);
				}
			}
			reader.ReadFullStartElement(XD.OleTxTransactionExternalDictionary.OleTxTransaction, XD.OleTxTransactionExternalDictionary.Namespace);
			byte[] array = OleTxTransactionHeader.ReadPropagationTokenElement(reader);
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return new OleTxTransactionHeader(array, wsatExtendedInformation);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000330C8 File Offset: 0x000312C8
		public static void WritePropagationTokenElement(XmlDictionaryWriter writer, byte[] propagationToken)
		{
			writer.WriteStartElement(XD.OleTxTransactionExternalDictionary.PropagationToken, XD.OleTxTransactionExternalDictionary.Namespace);
			writer.WriteBase64(propagationToken, 0, propagationToken.Length);
			writer.WriteEndElement();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000330F5 File Offset: 0x000312F5
		public static bool IsStartPropagationTokenElement(XmlDictionaryReader reader)
		{
			return reader.IsStartElement(XD.OleTxTransactionExternalDictionary.PropagationToken, XD.OleTxTransactionExternalDictionary.Namespace);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00033114 File Offset: 0x00031314
		public static byte[] ReadPropagationTokenElement(XmlDictionaryReader reader)
		{
			reader.ReadFullStartElement(XD.OleTxTransactionExternalDictionary.PropagationToken, XD.OleTxTransactionExternalDictionary.Namespace);
			byte[] array = reader.ReadContentAsBase64();
			if (array.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidPropagationToken")));
			}
			reader.ReadEndElement();
			return array;
		}

		// Token: 0x0400173B RID: 5947
		private const string OleTxHeaderElement = "OleTxTransaction";

		// Token: 0x0400173C RID: 5948
		private const string OleTxNamespace = "http://schemas.microsoft.com/ws/2006/02/tx/oletx";

		// Token: 0x0400173D RID: 5949
		private static readonly XmlDictionaryString CoordinationNamespace = XD.CoordinationExternal10Dictionary.Namespace;

		// Token: 0x0400173E RID: 5950
		private byte[] propagationToken;

		// Token: 0x0400173F RID: 5951
		private WsatExtendedInformation wsatInfo;
	}
}
