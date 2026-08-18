using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B1 RID: 1969
	internal class ContextMessageHeader : MessageHeader
	{
		// Token: 0x06004A7B RID: 19067 RVA: 0x00111B92 File Offset: 0x0010FD92
		public ContextMessageHeader(IDictionary<string, string> context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.context = context;
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x00111BB4 File Offset: 0x0010FDB4
		public override string Name
		{
			get
			{
				return "Context";
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x00111BBB File Offset: 0x0010FDBB
		public override string Namespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/context";
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00111BC4 File Offset: 0x0010FDC4
		public static ContextMessageProperty GetContextFromHeaderIfExists(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			int num = message.Headers.FindHeader("Context", "http://schemas.microsoft.com/ws/2006/05/context");
			if (num >= 0)
			{
				MessageHeaders headers = message.Headers;
				ContextMessageProperty result = ContextMessageHeader.ParseContextHeader(headers.GetReaderAtHeader(num));
				headers.AddUnderstood(num);
				return result;
			}
			return null;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00111C1C File Offset: 0x0010FE1C
		internal static ChannelProtectionRequirements GetChannelProtectionRequirements(ProtectionLevel protectionLevel)
		{
			ChannelProtectionRequirements result;
			if (protectionLevel == ProtectionLevel.EncryptAndSign)
			{
				if (ContextMessageHeader.encryptAndSignChannelProtectionRequirements == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.HeaderTypes.Add(new XmlQualifiedName("Context", "http://schemas.microsoft.com/ws/2006/05/context"));
					ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
					channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.MakeReadOnly();
					ContextMessageHeader.encryptAndSignChannelProtectionRequirements = channelProtectionRequirements;
				}
				result = ContextMessageHeader.encryptAndSignChannelProtectionRequirements;
			}
			else
			{
				if (protectionLevel != ProtectionLevel.Sign)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("protectionLevel"));
				}
				if (ContextMessageHeader.signChannelProtectionRequirements == null)
				{
					MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
					messagePartSpecification2.HeaderTypes.Add(new XmlQualifiedName("Context", "http://schemas.microsoft.com/ws/2006/05/context"));
					ChannelProtectionRequirements channelProtectionRequirements2 = new ChannelProtectionRequirements();
					channelProtectionRequirements2.IncomingSignatureParts.AddParts(messagePartSpecification2);
					channelProtectionRequirements2.OutgoingSignatureParts.AddParts(messagePartSpecification2);
					channelProtectionRequirements2.MakeReadOnly();
					ContextMessageHeader.signChannelProtectionRequirements = channelProtectionRequirements2;
				}
				result = ContextMessageHeader.signChannelProtectionRequirements;
			}
			return result;
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00111D18 File Offset: 0x0010FF18
		internal static ContextMessageProperty ParseContextHeader(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			ContextMessageProperty contextMessageProperty = new ContextMessageProperty();
			try
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement("Context", "http://schemas.microsoft.com/ws/2006/05/context");
					while (reader.MoveToContent() == XmlNodeType.Element)
					{
						if (reader.LocalName != "Property" || reader.NamespaceURI != "http://schemas.microsoft.com/ws/2006/05/context")
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("SchemaViolationInsideContextHeader")));
						}
						string attribute = reader.GetAttribute("name");
						if (string.IsNullOrEmpty(attribute) || !ContextDictionary.TryValidateKeyValueSpace(attribute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("InvalidCookieContent", new object[]
							{
								attribute
							})));
						}
						contextMessageProperty.Context[attribute] = reader.ReadElementString();
					}
					if (reader.NodeType != XmlNodeType.EndElement)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("SchemaViolationInsideContextHeader")));
					}
				}
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("XmlFormatViolationInContextHeader"), innerException));
			}
			return contextMessageProperty;
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00111E50 File Offset: 0x00110050
		internal static void WriteHeaderContents(XmlDictionaryWriter writer, IDictionary<string, string> context)
		{
			foreach (KeyValuePair<string, string> keyValuePair in context)
			{
				writer.WriteStartElement("Property", "http://schemas.microsoft.com/ws/2006/05/context");
				writer.WriteAttributeString("name", null, keyValuePair.Key);
				writer.WriteValue(keyValuePair.Value);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00111EC8 File Offset: 0x001100C8
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			ContextMessageHeader.WriteHeaderContents(writer, this.context);
		}

		// Token: 0x04002F14 RID: 12052
		public const string ContextHeaderName = "Context";

		// Token: 0x04002F15 RID: 12053
		public const string ContextHeaderNamespace = "http://schemas.microsoft.com/ws/2006/05/context";

		// Token: 0x04002F16 RID: 12054
		public const string ContextPropertyElement = "Property";

		// Token: 0x04002F17 RID: 12055
		public const string ContextPropertyNameAttribute = "name";

		// Token: 0x04002F18 RID: 12056
		private static ChannelProtectionRequirements encryptAndSignChannelProtectionRequirements;

		// Token: 0x04002F19 RID: 12057
		private static ChannelProtectionRequirements signChannelProtectionRequirements;

		// Token: 0x04002F1A RID: 12058
		private IDictionary<string, string> context;
	}
}
